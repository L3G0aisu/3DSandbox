using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Console : MonoBehaviour {
	
	private static Console m_Instance = null;
	public static Console Instance { get { return m_Instance; } }

	private Text console;
	
	void Awake() {
		m_Instance = this;
		try {
			GameObject consoleObj = GameObject.Find("Console");
			try {
				console = consoleObj.GetComponent<Text>();
			} catch {
				Debug.LogError("No Text Component attached to \"Console\"");
			}
		} catch {
			Debug.LogError("Could not find object with name \"Console\"");
		}
	}

	public void Log (string s) {
		if (console.text != "")
			console.text += "\n";

		console.text += s;
	}

	public void ClearLog () {
		console.text = "";
	}
}
