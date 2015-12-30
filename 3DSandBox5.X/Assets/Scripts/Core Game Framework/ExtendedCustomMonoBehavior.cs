using UnityEngine;
using System.Collections;

public class ExtendedCustomMonoBehavior : MonoBehaviour {
	// This class is used to add some common veriables to MonoBehavior, rather than constantly repeating the same declarations in every class

	public Transform myTransform;
	public GameObject myGameObject;
	public Rigidbody myRigidBody;

	public bool didInit;
	public bool canControl;

	public int id;

	[System.NonSerialized]
	public Vector3 tempVector3;

	[System.NonSerialized]
	public Transform tempTransform;

	public virtual void SetID ( int anID ) {
		id = anID;
	}
}
