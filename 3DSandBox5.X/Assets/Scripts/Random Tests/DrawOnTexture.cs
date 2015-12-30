using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer), typeof(MeshCollider), typeof(Material))]
public class DrawOnTexture : MonoBehaviour {

	public Texture2D texture;
	public Camera cam;

	void Awake () {
//		cam = GetComponent<Camera>();
	}

	void Start () {
//		Texture2D texture = new Texture2D(128, 128);
		GetComponent<Renderer>().material.mainTexture = texture;
//
//		for (int y = 0; y < texture.height; y++) {
//			for (int x = 0; x < texture.width; x++) {
//				Color color = ((x & y) != 0 ? Color.blue : Color.white);
//				texture.SetPixel(x, y, color);
//			}
//		}
//		texture.Apply();
	}

	void Update () {
		if (!Input.GetMouseButton(0))
			return;
		
		RaycastHit hit;
		if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) {
			Debug.Log("no raycast hit");
			return;
		}
		
		Renderer rend = hit.transform.GetComponent<Renderer>();
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (rend == null) {
			Debug.Log("rend is null");
			return;
		}
		if (rend.sharedMaterial == null) {
			Debug.Log("rend.sharedMaterial is null");
			return;
		}
		if (rend.sharedMaterial.mainTexture == null) {
			Debug.Log("rend.sharedMaterial.mainTexture is null");
			return;
		}
		if (meshCollider == null) {
			Debug.Log("mesh collider is null");
			return;
		}

		if (hit.textureCoord != null) {
			Texture2D tex = rend.material.mainTexture as Texture2D;
			Vector2 pixelUV = hit.textureCoord;
			pixelUV.x *= tex.width;
			pixelUV.y *= tex.height;
			tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
			tex.Apply();
		}
	}
}
