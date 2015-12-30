using UnityEngine;
using System.Collections;

public class PathSpawner : MonoBehaviour {
    public WaypointsController waypointControl;

    // should we start spawning based on distance from the camera? If distanceBased is false, we will need to call this class from elsewhere, to spawn
    public bool distanceBasedSpawnStart;

    // If we're using distance based spawning, at what distance should we start?

}
