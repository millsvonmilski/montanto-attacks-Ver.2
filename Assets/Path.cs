using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	public bool bDebug = true;
	public float Radius = 2.0f;
	public Vector3[] pointA;



	// Esta propiedad devuelve el tamanio y el size of the waypoint if requested.
	public float Length
	{
		get {
			return pointA.Length;
		}
	}


	// This method return the Vector3 position of a particular waypoint at a specified index in the array.
	public Vector3 GetPoint(int index)
	{
		return pointA[index];
	}



	void OnDrawGizmos()
	{
		if (!bDebug) return;

		for (int i = 0; i < pointA.Length; i++)
		{
			if (i + 1 < pointA.Length)
			{
				Debug.DrawLine(pointA[i], pointA[i + i],
				Color.red);				
			}
		}		
	}
}
