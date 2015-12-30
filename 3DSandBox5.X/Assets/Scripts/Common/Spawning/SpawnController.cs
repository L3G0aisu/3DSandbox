using UnityEngine;
using System.Collections;

public class SpawnController : ScriptableObject {

	private ArrayList playerTransforms;
	private ArrayList playerGameObjects;

	private Transform tempTransform;
	private GameObject tempGameObject;

	private GameObject[] playerPrefabList;
	private Vector3[] startPositions;
	private Quaternion[] startRotations;

	private static SpawnController instance;

	public SpawnController () {
		// this function will be called whenever an instance of the SpawnController class is made
		// first, we check that an instance does not already exist
		if (instance != null) {
			Debug.LogWarning ("An instance of SpawnController already exists.");
			return;
		}

		instance = this;
	}

	public static SpawnController Instance {
		// to every other script, this getter setter is the way they get access to the singleton instance of this script.
		get {
			// the other script is trying to access an instance of this script, so we need to see if an instance already exists
			if (instance == null) {
				// no instance exists yet, so we go ahead and create one
				ScriptableObject.CreateInstance<SpawnController>();
			}

			// now we pass the reference to this instance back to the other script so it can communicate with it.
			return instance;
		}
	}

	public void Restart () {
		playerTransforms = new ArrayList();
		playerGameObjects = new ArrayList();
	}

	public void SetUpPlayers ( GameObject[] playerPrefabs, Vector3[] playerStartPositions, Quaternion[] playerStartRotations, Transform parent, int totalPlayers ) {
		// we pass in everything needed to spawn players and take care of spawning players in this class so that we don't have to replicate this code in every game controller
		playerPrefabList = playerPrefabs;
		startPositions = playerStartPositions;
		startRotations = playerStartRotations;

		// call the function to take care of spawning all the players and putting them in the right places
		CreatePlayers( parent, totalPlayers );
	}

	public void CreatePlayers ( Transform parent, int totalPlayers ) {
		playerTransforms = new ArrayList();
		playerGameObjects = new ArrayList();

		for (int i = 0; i < totalPlayers; i++) {
			// spawn a player
			tempTransform = Spawn( playerPrefabList[i], startPositions[i], startRotations[i] );

			// if we have passed in an object to parent the players to, set the parent
			if ( parent != null ) {
				tempTransform.parent = parent;
				tempTransform.localPosition = startPositions[i];
			}

			// add this transform into our list of player transforms
			playerTransforms.Add( tempTransform );

			// add its gameObject into our list of player gameObjects (these are cached seperately)
			playerGameObjects.Add( tempTransform.gameObject );
		}
	}

	public GameObject GetPlayerGameObject (int index) {
		return (GameObject)playerGameObjects[index];
	}

	public Transform GetPlayerTransform (int index) {
		return (Transform)playerTransforms[index];
	}

	public Transform Spawn ( GameObject anObject, Vector3 aPosition, Quaternion aRotation ) {
		// instantiate the object
		tempGameObject = (GameObject)Instantiate(anObject, aPosition, aRotation);
		tempTransform = tempGameObject.transform;

		// return the object to whatever was calling
		return tempTransform;
	}

	// here we just provide a convenient function to return the spawned object's gameObject rather than its transform
	public GameObject SpawnGameObject ( GameObject anObject, Vector3 aPosition, Quaternion aRotation ) {
		//instantiate the object
		tempGameObject = (GameObject)Instantiate( anObject, aPosition, aRotation );
		tempTransform = tempGameObject.transform;

		// return the object to whatever was calling
		return tempGameObject;
	}

	public ArrayList GetAllSpawnedPlayers () {
		return playerTransforms;
	}
}
