using UnityEngine;
using System.Collections;

public class TriggerSpawner : MonoBehaviour {

	public GameObject ObjectToSpawnOnTrigger;
	public Vector3 offsetPosition;
	public bool onlySpawnOnce;
	public int layerToCauseTriggerHit = 13; // this could be set to the number of the camera layer

	private Transform myTransform;

	void Start () {
		Vector3 tempPosition = transform.position;
		tempPosition.y = Camera.main.transform.position.y;
		transform.position = tempPosition;

		// cache transform
		myTransform = transform;
	}

	void OnTriggerEnter ( Collider other ) {
		// make sure that the layer of the object entering our trigger is the one to cause the boss to spawn
		if ( other.gameObject.layer != layerToCauseTriggerHit )
			return;

		// instantiate the object to spawn on trigger enter
		Instantiate( ObjectToSpawnOnTrigger, myTransform.position + offsetPosition, Quaternion.identity );

		// if we are to only spawn once, destroy this gameobject after spawn occurs
		if ( onlySpawnOnce )
			Destroy( gameObject );
	}
}
