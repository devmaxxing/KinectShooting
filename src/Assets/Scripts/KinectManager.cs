using UnityEngine;
using System.Collections.Generic;
using Windows.Kinect;

public class KinectManager: MonoBehaviour {

    public GameController controller;
    public static bool kinectEnabled = false;
	private KinectSensor _Sensor;
	private BodyFrameReader _Reader;
	private Body[] _Data = null;

    private PlayerScript playerScript;

    private List<GestureDetector> gestureDetectorList;

	// Use this for initialization
	void Start () {
		_Sensor = KinectSensor.GetDefault();

		if (_Sensor != null)
		{
			_Reader = _Sensor.BodyFrameSource.OpenReader();

			if (!_Sensor.IsOpen)
			{
				_Sensor.Open();
			}
            if(_Sensor.IsAvailable)
                kinectEnabled = true;
            gestureDetectorList = new List<GestureDetector>();
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (_Reader != null)
		{
            if (_Sensor.IsAvailable)
                kinectEnabled = true;
            var frame = _Reader.AcquireLatestFrame();

			if (frame != null)
			{
				if (_Data == null)
				{
					_Data = new Body[_Sensor.BodyFrameSource.BodyCount];
				}

				frame.GetAndRefreshBodyData(_Data);
                frame.Dispose();
                frame = null;

                // we may have lost/acquired bodies, so update the corresponding gesture detectors
                // loop through all bodies to see if any of the gesture detectors need to be updated
                for (int i = 0; i < _Data.Length; ++i)
                {
                    if(gestureDetectorList.Count <= i)
                    {
                        //Debug.Log(i);
                        GestureDetector detector = new GestureDetector(_Sensor, this);
                        this.gestureDetectorList.Add(detector);
                    }
                    Body body = _Data[i];
                    ulong trackingId = body.TrackingId;

                    GestureDetector currentDetector = gestureDetectorList[i];

                    if(currentDetector.shotFired)
                    {
                        shotFired(currentDetector.TrackingId);
                    }

                    // if the current body TrackingId changed, update the corresponding gesture detector with the new value
                    if (trackingId != this.gestureDetectorList[i].TrackingId)
                    {
                        this.gestureDetectorList[i].TrackingId = trackingId;

                        // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                        // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                        this.gestureDetectorList[i].IsPaused = trackingId == 0;
                    }
                }   
			}
		}
	}

	void OnApplicationQuit()
	{
		if (_Reader != null)
		{
			_Reader.Dispose();
			_Reader = null;
		}

		if (_Sensor != null)
		{
			if (_Sensor.IsOpen)
			{
				_Sensor.Close();
			}
			_Sensor = null;
		}
	}

    public Body[] getData()
    {
        return _Data;
    }

    public int getNumBodies()
    {
        return _Sensor.BodyFrameSource.BodyCount;
    }

    //Callback from GestureDetector with its corresponding tracking ID
    public void shotFired(ulong trackingID)
    {
        Debug.Log("bang! " + trackingID);
        /*
        for (int i=0; i < _Data.Length; i++)
        {
            Debug.Log(_Data[i].TrackingId);
            if(_Data[i].TrackingId == trackingID)
            {
                int playerIndex = i+1;
                Debug.Log("Player " + playerIndex + " shot his gun.");
                return;
            }
        }
        */
        controller.fireShot();
    }
}
