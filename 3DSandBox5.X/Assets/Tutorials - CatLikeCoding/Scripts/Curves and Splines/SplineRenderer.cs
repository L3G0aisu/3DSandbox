using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SplineRenderer : MonoBehaviour {

	public BezierSpline spline;

	public int radius, segmentCount;
	public int stepsPerCurve = 10;
	private Mesh mesh;
	private Vector3[] vertices, points;
	private Vector2[] uv;
	private int[] triangles;
	
	public void Update () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Pipe";
		SetVertices();
		SetUV();
		SetTriangles();
		mesh.RecalculateNormals();
	}

	private void SetPoints () {
		int steps = stepsPerCurve * spline.CurveCount;
		points = new Vector3[(segmentCount * (steps + 1))];
		for (int i = 1; i <= steps; i++) {
			float jStep = (2f * Mathf.PI) / segmentCount;
			for (int j = 0; j < segmentCount; j ++) {
				points[((i * segmentCount) + j)] = (GetPointOnTorus((i / (float)steps), (j * jStep)));
			}
		}
		//mesh.vertices = vertices;
	}

	/* Torus Segmentation Math:
	 * 			x = (R + r cos v) cos u
	 * 			y = (R + r cos v) sin u
	 * 			z = r cos v
	 * 
	 * Where:	r is the radius of the pipe
	 * and		R is the radius of the curve
	 * and		u is the angle along the curve, in radians
	 * and		v is the angle along the pipe
	 */

	private void SetVertices () {
		vertices = new Vector3[segmentCount * stepsPerCurve * spline.CurveCount * 4];
		float uStep = 1f / (stepsPerCurve * spline.CurveCount);

		CreateFirstQuadRing(uStep);
		int iDelta = segmentCount * 4;
		for (int u = 2, i = iDelta; u <= stepsPerCurve * spline.CurveCount; u++, i += iDelta) {
			CreateQuadRing(u * uStep, i);
		}
		mesh.vertices = vertices;
	}

	private void CreateFirstQuadRing (float u) {
		float vStep = (2f * Mathf.PI) / segmentCount;
		
		Vector3 vertexA = GetPointOnTorus(0f, 0f) - spline.transform.position;
		Vector3 vertexB = GetPointOnTorus(u, 0f) - spline.transform.position;
		for (int v = 1, i = 0; v <= segmentCount; v++, i += 4) {
			vertices[i] = vertexA;
			vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep) - spline.transform.position;
			vertices[i + 2] = vertexB;
			vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep) - spline.transform.position;
		}
	}
	
	private void CreateQuadRing (float u, int i) {
		float vStep = (2f * Mathf.PI) / segmentCount;
		int ringOffset = segmentCount * 4;
		
		Vector3 vertex = GetPointOnTorus(u, 0f) - spline.transform.position;
		for (int v = 1; v <= segmentCount; v++, i += 4) {
			vertices[i] = vertices[i - ringOffset + 2];
			vertices[i + 1] = vertices[i - ringOffset + 3];
			vertices[i + 2] = vertex;
			vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep) - spline.transform.position;
		}
	}

	private void SetUV () {
		uv = new Vector2[vertices.Length];
		for (int i = 0; i < vertices.Length; i+= 4) {
			uv[i] = Vector2.zero;
			uv[i + 1] = Vector2.right;
			uv[i + 2] = Vector2.up;
			uv[i + 3] = Vector2.one;
		}
		mesh.uv = uv;
	}
	
	/*	Torus Segmentation Math:
	 *	x(θ) = r*cos(θ)*a1 + r*sin(θ)*b1
	 *	y(θ) = r*cos(θ)*a2 + r*sin(θ)*b2
	 *	z(θ) = r*cos(θ)*a3 + r*sin(θ)*b3
	 *
	 * Where:	r is the radius of the pipe
	 * and		a1, a2, a3 are the vector properties of the "up" vector
	 * and		b1, b2, b3 are the vector properties of the "right" vector
	 * and		θ is the angle around the pipe
	 */
	
	private Vector3 GetPointOnTorus (float pStep, float theta) {
		//float uStep = 1f / (stepsPerCurve * spline.CurveCount);
		Vector3 p = spline.GetPoint(pStep);

		Vector3 acc = (Vector3.zero - p).normalized; //((spline.GetChangeInDirection(pStep - 2f * uStep) + spline.GetChangeInDirection(pStep - uStep) + spline.GetChangeInDirection(pStep) + spline.GetChangeInDirection(pStep + uStep) + spline.GetChangeInDirection(pStep + 2f * uStep)) / 5f);
		Vector3 dir = spline.GetDirection(pStep);
		Vector3 a = GetCrossProduct(acc, dir, Vector3.zero);
		Vector3 b = GetCrossProduct(dir, a, Vector3.zero);

		Vector3 point = new Vector3((radius * Mathf.Cos(theta) * a.x) + (radius * Mathf.Sin(theta) * b.x),
		                            (radius * Mathf.Cos(theta) * a.y) + (radius * Mathf.Sin(theta) * b.y),
		                            (radius * Mathf.Cos(theta) * a.z) + (radius * Mathf.Sin(theta) * b.z));
		return (point + p);
	}
	
	private void SetTriangles () {
		triangles = new int[segmentCount * stepsPerCurve * spline.CurveCount * 6];
		for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4) {
			triangles[t] = i;
			triangles[t + 1] = triangles[t + 4] = i + 2;
			triangles[t + 2] = triangles[t + 3] = i + 1;
			triangles[t + 5] = i + 3;
		}
		mesh.triangles = triangles;
	}

	private Vector3 GetCrossProduct (Vector3 a, Vector3 b, Vector3 c) {
		return Vector3.Cross((b - a), (c - a)).normalized;
	}

	private void OnDrawGizmos () {
		SetPoints();
		for (int u = 0; u < points.Length; u++) {
			Color c = new Color(1f, 1f, 1f);;
			if (u % segmentCount == 0) {
				c = new Color(0f, 0f, 0f);
			}
			Gizmos.color = c;
			Gizmos.DrawSphere(points[u], 0.1f);
		}
	}
}
