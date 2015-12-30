using UnityEngine;
using System.Collections;

public class CrossProduct : MonoBehaviour {

	public Transform midpoint, vectorA, vectorB, vectorC, vectorD;

	[ExecuteInEditMode]
	void Update () {
		Vector3 pointA = midpoint.TransformPoint(vectorA.position).normalized;
		Vector3 pointB = midpoint.TransformPoint(vectorB.position).normalized;
		Vector3 pointC = GetCrossProduct(pointA, pointB, midpoint.position).normalized;
		vectorC.transform.position = pointC * 5f;
		vectorD.transform.position = GetCrossProduct(pointB, pointC, midpoint.position) * 5f;
	}

	private Vector3 GetCrossProduct (Vector3 a, Vector3 b, Vector3 c) {
		return Vector3.Cross((b - a), (c - a)).normalized;
	}
}
