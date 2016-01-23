using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class KinectManager: MonoBehaviour {

    public static bool kinectEnabled = false;
	private KinectSensor _Sensor;
	private BodyFrameReader _Reader;
	private Body[] _Data = null;

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
            kinectEnabled = true;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		if (_Reader != null)
		{
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
}
