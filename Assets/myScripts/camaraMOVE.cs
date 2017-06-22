using UnityEngine;
using System.Collections;

public class camaraMOVE : MonoBehaviour {


	public float speed = 2.0f;
	public float zoomSpeed = 2.0f;


	// VARIABLES PARA EL ROTATE

	public float minX = -360.0f;
	public float maxX = 360.0f;

	public float minY = -45.0f;
	public float maxY = 45.0f;

	public float sensX = 100.0f;
	public float sensY = 100.0f;

	//float rotationY = 0.0f;
	//float rotationX = 0.0f;

	private Camera camara1;


	void Start () {


	}

	void Update () {

		/*
		 ESTE CODIGO ME SIRVE PARA MOVER LA CAMARA A PARTIR E LAS TECLAS.
		*/

		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

	
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// Quizas puedo encontrar una manera de utilizar esta especie 
		// de movimiento zoom in / zoom out.

		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.back * speed * Time.deltaTime;
		}


	}
}

/*  PARA MOVER LA CAMARA CON LAS TECLAS
	if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.back * speed * Time.deltaTime;
		}

*/


/*
ESTE CODIGO ME SIRVE PARA HACER ZOOM 

float scroll = Input.GetAxis("Mouse ScrollWheel");
transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);

*/

/*
ESTE CODIGO ME SIRVE PARA HACER ROTATE 


if (Input.GetMouseButton (0)) {
	rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
	rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
	rotationY = Mathf.Clamp (rotationY, minY, maxY);
	transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
}
*/