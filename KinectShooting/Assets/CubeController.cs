using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class CubeController : MonoBehaviour {

	private KinectSensor _Sensor;
	private BodyFrameReader _Reader;
	private Body[] _Data = null;
	public GameObject p1, p2;

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

				int player1 = -1;
				int player2 = -1;
				for (int i = 0; i < _Sensor.BodyFrameSource.BodyCount; i++)
				{
					if (_Data[i].IsTracked)
					{
						if (player1 == -1)
							player1 = i;
						else
							player2 = i;
					}
				}
				moveGameObject (p1, player1);
				moveGameObject (p2, player2);
			}
		}
	}

	void moveGameObject(GameObject g, int playerIndex) {
		if (playerIndex>-1)
		{
			if (_Data[playerIndex].HandRightState != HandState.Closed)
			{
				float horizontal = 
					(float)(_Data[playerIndex].Joints[JointType.HandRight].Position.X 
						* 50-_Data[playerIndex].Joints[JointType.ShoulderRight].Position.X*60);
				float vertical = 
					(float)(_Data[playerIndex].Joints[JointType.HandRight].Position.Y 
						*30);
				Debug.Log (horizontal + ", " + vertical);

				g.transform.position = new Vector2 (horizontal, vertical);
			}
			/*
					if (_Data[idx].HandLeftState != HandState.Closed)
					{
						float angley = 
							(float)(_Data[idx].Joints[JointType.HandLeft].Position.X );
						float anglex = 
							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Y);
						float anglez = 
							(float)(_Data[idx].Joints[JointType.HandLeft].Position.Z);

						this.gameObject.transform.rotation =
							Quaternion.Euler(
								this.gameObject.transform.rotation.x+anglex * 100,
								this.gameObject.transform.rotation.y+angley * 100,
								this.gameObject.transform.rotation.z+anglez * 100);
					}
					*/
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
}
