using UnityEngine;
using System.Collections;

public class WaypointsController : MonoBehaviour {

	[ExecuteInEditMode]

	// this script simply gives us a visual path to make it easier to edit our waypoints
	private ArrayList transforms; // arraylist for easy access to transforms

	private Vector3 firstPoint; // store our first waypoint so we can loop the path

	private float distance; // used to calculate distance between points

	private Transform tempTransform; // a temporary holder for a transform
	private int tempIndex; // a temporary holder for an index number
	private int totalTransforms;

	private Vector3 difference;
	private float currentDistance;
	private Transform closest;

	private Vector3 currentPosition;
	private Vector3 lastPosition;
	private Transform pointT;

	public bool closed = true;
	public bool shouldReverse;

	void Start () {
		// make sure that when this script starts that we have grabbed the transforms for each waypoint
//		GetTransforms();
	}

	void OnDrawGizmos () {
		// we only want to draw the waypoints when we're editing, not when we are playing the game
		if ( Application.isPlaying )
			return;

//		GetTransforms();
		// make sure that we have more than one transform in the list, otherwise we can't draw lines between them
		if ( totalTransforms < 2 )
			return;

		// draw our path first, we grab the position of the very first waypoint so that our line has a start point
		tempTransform = (Transform)transforms[0];
		lastPosition = tempTransform.position;

		// we point each waypoint at the next, so that we can use this rotation data to find out when the player is going
		// the wrong way or to position the player after a reset facing the correct direction.
		// so first we need to hold a reference to the transform we are going to point
		pointT = (Transform)transforms[0];

		// also, as this is the first point we store it to use for closing the path later
		firstPoint = lastPosition;

		// now we loop through all of the waypoints drawing lines between them
		for ( int i = 1; i < transforms.Count; i++ ) {
			tempTransform = (Transform)transforms[i];
			if (tempTransform == null) {
//				GetTransforms();
				return;
			}

			// grab the current waypoint position
			currentPosition = tempTransform.position;

			Gizmos.color = Color.green;
			Gizmos.DrawSphere( currentPosition, 2 );

			// draw the line between the last waypoint and this one
			Gizmos.color = Color.red;
			Gizmos.DrawLine( lastPosition, currentPosition );

			// point our last transform at the latest position
			pointT.LookAt( currentPosition );

			// update our 'last' waypoint to become this one as we move on to find the next...
			lastPosition = currentPosition;

			// update the pointing transform
//			pointT = (transform)transforms[i];
		}


	}
}
