using UnityEngine;
using System.Collections;

public class BaseGameController : MonoBehaviour {

	bool paused;
	public GameObject explosionPrefab;

	public virtual void PlayerLostLife () {
		// life damage functions
	}

	public virtual void SpawnPlayer () {
		// player spawn functions
	}

	public virtual void Respawn () {
		// player is respawning
	}

	public virtual void StartGame () {
		// start game functions
	}

	public void Explode ( Vector3 aPosition ) {
		// instantiate an explosion at the position passed into this function
		Instantiate ( explosionPrefab, aPosition, Quaternion.identity );
	}

	public virtual void EnemyDestroyed ( Vector3 aPosition, int pointsValue, int hitByID ) {
		// deal with enemy destroyed
	}

	public virtual void BossDestroyed () {
		// deal with end of boss battle
	}

	public virtual void RestartGameButtonPressed () {
		// deal with restart button (default behaviour re-loads the currently loaded scene)
		Application.LoadLevel ( Application.loadedLevelName );
	}

	public bool Paused {
		get {
			// get paused
			return paused;
		}
		set {
			// set paused
			paused = value;

			if ( paused ) {
				// pause time
				Time.timeScale = 0f;
				Time.fixedDeltaTime = 0f;
			} else {
				// unpause Unity
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f;
			}
		}
	}
}
