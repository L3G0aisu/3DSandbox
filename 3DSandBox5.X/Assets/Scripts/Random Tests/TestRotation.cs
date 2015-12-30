using UnityEngine;
using System.Collections;

public class TestRotation : MonoBehaviour {

	public GameObject cameras;
	public GameObject orientationObjs;

	private Quaternion offsetRotation;
	private bool offset = false;

	void Update () {
		Console.Instance.ClearLog();
		GetOffset();

		Quaternion temp = DeviceRotation.Get();
		temp = offsetRotation * temp;
		SetRotation(temp);
		Console.Instance.Log("Device Rotation: " + DeviceRotation.Get().ToString());
		Console.Instance.Log("Offset Rotation: " + offsetRotation.ToString());
		Console.Instance.Log("InGame Rotation: " + temp.ToString());
	}

	void GetOffset () {
		if (DeviceRotation.Get().ToString() != "(0.0, 0.0, 0.0, 0.0)" && !offset) {

			offsetRotation = Quaternion.Inverse(DeviceRotation.Get());
			offset = true;
		}
	}

	void NewOffset () {
		offset = false;
	}

	void SetRotation (Quaternion q) {
		cameras.transform.rotation = q;
		orientationObjs.transform.rotation = q;
	}
}
