using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	private static SoundManager m_Instance = null;
	public static SoundManager Instance { get { return m_Instance; } }
	
	public AudioClip[] GameSounds;
	
	private int totalSounds;
	private ArrayList soundObjectList;
	private SoundObject tempSoundObj;
	
	public float volume = 1;
	public string gamePrefsName = "DefaultGame";
	
	void Awake() {
		m_Instance = this;
	}
	
	void Start () {
		AudioListener.volume = 1f;
		PlayerPrefs.SetFloat(gamePrefsName + "_SFXVol", 1f);
		volume = PlayerPrefs.GetFloat(gamePrefsName + "_SFXVol");
		Debug.Log ("SoundManager gets volume from prefs " + gamePrefsName + "_SFXVol at " + volume);
		soundObjectList = new ArrayList();
		
		foreach (AudioClip theSound in GameSounds) {
			tempSoundObj = new SoundObject (theSound, theSound.name, volume);
			soundObjectList.Add(tempSoundObj);
			totalSounds++;
		}
	}
	
	public void PlaySoundByIndex ( int anIndexNumber, Vector3 aPosition ) {
		if (anIndexNumber >= soundObjectList.Count || anIndexNumber < 0) {
			Debug.LogError("anIndexNumber out of bounds of soundObjectList on SoundManager");
			return;
		}
		
		tempSoundObj = (SoundObject) soundObjectList[anIndexNumber];
		tempSoundObj.PlaySound(aPosition);
	}
	
	public void OnDestroy () {
		m_Instance = null;
	}
}

public class SoundObject {
	public AudioSource source;
	public GameObject sourceGO;
	public Transform sourceTR;
	
	public AudioClip clip;
	public string name;
	
	public SoundObject ( AudioClip aClip, string aName, float aVolume ) {
		sourceGO = new GameObject("AudioSource_" + aName);
		sourceTR = sourceGO.transform;
		source = sourceGO.AddComponent<AudioSource>();
		source.name = "AudioSource_" + aName;
		source.playOnAwake = false;
		source.clip = aClip;
		source.volume = aVolume;
		clip = aClip;
		name = aName;
	}
	
	public void PlaySound ( Vector3 atPosition ) {
		sourceTR.position = atPosition;
		source.PlayOneShot(clip);
	}
}
