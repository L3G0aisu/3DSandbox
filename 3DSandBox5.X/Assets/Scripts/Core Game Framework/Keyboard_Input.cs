using UnityEngine;
using System.Collections;

public class Keyboard_Input : BaseInputController {

	public override void CheckInput () {
		// get input data from vertical and horizontal axes and store them internally in vert and horz so we don't have to access them every time we need to relay input data out
		vertical = Input.GetAxis( "Vertical" );
		horizontal = Input.GetAxis( "Horizontal" );

		// set up some Boolean values for up, down, left, and right
		up = ( vertical > 0 );
		down = ( vertical < 0 );
		left = ( horizontal > 0 );
		right = ( horizontal < 0 );

		// get fire/action buttons
		fire1 = Input.GetButton( "Fire1" );
		shouldRespawn = Input.GetButton( "Fire3" );
	}

	public void LateUpdate () {
		// check inputs each LateUpdate() ready for the next tick
		CheckInput();
	}
}
