#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleVisualizor))]
public class CircleVisInspector : Editor {

	private CircleVisualizor circle;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
	}

	private void OnSceneGUI () {
		circle = target as CircleVisualizor;
		handleTransform = circle.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
	}
}
#endif