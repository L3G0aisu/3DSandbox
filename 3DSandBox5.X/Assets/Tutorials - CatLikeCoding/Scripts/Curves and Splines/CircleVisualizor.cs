using UnityEngine;
using System.Collections;

public class CircleVisualizor : MonoBehaviour {

	public int radius, segmentCount;
	private Vector3 posIHat = new Vector3(0, 1, 0);
	private Vector3 posJHat = new Vector3(1, 0, 0);
	private Vector3 posKHat = new Vector3(0, 0, 1);
	private Vector3 midPoint = new Vector3(0, 0, 0);

	public void Update () {
		posIHat = transform.TransformDirection(Vector3.up);
		posKHat = transform.TransformDirection(Vector3.forward);
		midPoint = transform.position;
		posJHat = GetJHat(posIHat, posKHat, midPoint);
	}

	private Vector3 GetJHat (Vector3 a, Vector3 b, Vector3 c) {
		return Vector3.Cross((b - a), (c - a)).normalized;
	}

	/*
	 *	x(θ) = c1 + r*cos(θ)*a1 + r*sin(θ)*b1
	 *	y(θ) = c2 + r*cos(θ)*a2 + r*sin(θ)*b2
	 *	z(θ) = c3 + r*cos(θ)*a3 + r*sin(θ)*b3
	 */

	private Vector3 GetPoint (Vector3 a, Vector3 b, Vector3 c, float theta) {
		Vector3 point = new Vector3(c.x + (radius * Mathf.Cos(theta) * a.x) + (radius * Mathf.Sin(theta) * b.x),
		                            c.y + (radius * Mathf.Cos(theta) * a.y) + (radius * Mathf.Sin(theta) * b.y),
		                            c.z + (radius * Mathf.Cos(theta) * a.z) + (radius * Mathf.Sin(theta) * b.z));
		return point;
	}

	/*private void OnDrawGizmos () {
		float uStep = (2f * Mathf.PI) / curveSegmentCount;
		float vStep = (2f * Mathf.PI) / pipeSegmentCount;
		
		for (int u = 0; u < curveSegmentCount; u++) {
			for (int v = 0; v < pipeSegmentCount; v++) {
				Vector3 point = GetPointOnTorus(u * uStep, v * vStep);
				Gizmos.color = new Color(
					1f,
					(float)v / pipeSegmentCount,
					(float)u / curveSegmentCount);
				Gizmos.DrawSphere(point, 0.1f);
			}
		}
	}*/

	private void OnDrawGizmos () {
		float uStep = (2f * Mathf.PI) / segmentCount;

		for (int u = 0; u < segmentCount; u++) {
			Vector3 point = GetPoint(posIHat, posJHat, midPoint, (u * uStep));
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(point, 0.1f);
		}
	}
}
