using UnityEngine;
using System.Collections;

public class BaseInputController : MonoBehaviour {

	// directional buttons
	public bool up;
	public bool down;
	public bool left;
	public bool right;

	// fire / action buttons
	public bool fire1;
	
	// weapon slots
	public bool slot1;
	public bool slot2;
	public bool slot3;
	public bool slot4;
	public bool slot5;
	public bool slot6;
	public bool slot7;
	public bool slot8;
	public bool slot9;

	public float vertical;
	public float horizontal;
	public bool shouldRespawn;

	public Vector3 tempVector3;
	private Vector3 zeroVector = Vector3.zero;

	public virtual void CheckInput () {
		// override with your own code to deal with input
		horizontal = Input.GetAxis( "Horizontal" );
		vertical = Input.GetAxis( "Vertical" );
	}

	public virtual float GetHorizontal () {
		// returns our cached horizontal input axis value
		return horizontal;
	}
	
	public virtual float GetVertical () {
		// returns our cached vertical input axis value
		return vertical;
	}
	
	public virtual bool GetFire () {
		return fire1;
	}
	
	public virtual bool GetRespawn () {
		return shouldRespawn;
	}

	public virtual Vector3 GetMovementDirectionVector () {
		// temp vector for movement dir gets set to the value of an otherwise unused vector that always has the value of 0, 0, 0
		tempVector3 = zeroVector;
		
		//if we're going left or right, set the velocity vectior's X to our horizontal input value
		if ( left || right ) {
			tempVector3.x = horizontal;
		}
		
		//if we're going up or down, set the velocity vectior's Y to our vertical input value
		if ( up || down ) {
			tempVector3.x = vertical;
		}

		// return the movement vector
		return tempVector3;
	}
}
