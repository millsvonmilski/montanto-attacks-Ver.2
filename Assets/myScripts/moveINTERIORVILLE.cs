using UnityEngine;
using System.Collections;

[System.Serializable]
public class POSCAMARA1 {
	public float pos1X,pos1Y,pos1Z;
}

[System.Serializable]
public class POSCAMARA2 {
	public float pos2X,pos2Y,pos2Z;
}

[System.Serializable]
public class POSCAMARA3 {
	public float pos3X,pos3Y,pos3Z;
}

[System.Serializable]
public class POSCAMARA4 {
	public float pos4X,pos4Y,pos4Z;
}


public class moveINTERIORVILLE : MonoBehaviour {

	/*
	public Vector3 positionCam1;
	public Vector3 positionCam2;
	public Vector3 positionCam3;
	*/

	// VARIABLES PARA ALOJAR A VARIAS CAMARAS

	//public Camera[] listaCamara;
	private int camaraACTUAL;

	public float speedCAM = 2.0f;
	//public float zoomSpeed = 2.0f;

	public POSCAMARA1 posCAMARA1;
	public POSCAMARA2 posCAMARA2;
	public POSCAMARA3 posCAMARA3;
	public POSCAMARA4 posCAMARA4;

	public Camera camara1;
	public string posicion;
	public bool paraATRAS;


	void Start () {
		


		posicion = "primera";
		paraATRAS = false;
		camara1 = camara1.gameObject.GetComponent<Camera> ();

		// Primera posicion de camara1.
		camara1.transform.position = new Vector3 (posCAMARA1.pos1X, posCAMARA1.pos1Y, posCAMARA1.pos1Z);

		/*
			for (int i = 0; i < listaCamara.Length; i++){
				listaCamara[i].gameObject.SetActive(false);

			/*
			*   SETEO LA POSICION INICIAL DE TODAS LAS CAMARAS.

	
			if (listaCamara [i].tag == "camara1") {
				
				listaCamara [i].transform.position = new Vector3 (posCAMARA1.pos1X, posCAMARA1.pos1Y, posCAMARA1.pos1Z);
				camara1 = listaCamara [i].gameObject.GetComponent<Camera> ();

			} else if (listaCamara [i].tag == "camara2") {

				listaCamara [i].transform.position = new Vector3 (posCAMARA2.pos2X, posCAMARA2.pos2Y, posCAMARA2.pos2Z);
				camara2 = listaCamara [i].gameObject.GetComponent<Camera> ();

			} else if (listaCamara [i].tag == "camara3") {

				listaCamara[i].transform.position = new Vector3 (posCAMARA3.pos3X, posCAMARA3.pos3Y, posCAMARA3.pos3Z);
				camara3 = listaCamara [i].gameObject.GetComponent<Camera> ();

			}
		}

		if (listaCamara.Length > 0){
			listaCamara[0].gameObject.SetActive (true);
		}
	*/
	}

	void Update () {


		/* PARA CAMBIAR DE ESCENA */

		/*if(Input.GetKeyDown (KeyCode.A)){
			Application.LoadLevel("monsanto");
		}*/

		// BUCLE PARA MOVER LA CAMARA SOLA. 

			//if (camara1.isActiveAndEnabled) {
			
		if (posicion == "primera" && camara1.transform.position.x > -25.84f) { 

			camara1.transform.position += Vector3.left * speedCAM * Time.deltaTime;

		
		} 

		if (camara1.transform.position.x < -25.84f && paraATRAS == false) { // Me gusta este efecto
			posicion = "segunda";
				camara1.transform.position += Vector3.forward * speedCAM * Time.deltaTime;
			}

		/*if(paraATRAS == true)  {
				camara1.transform.position += Vector3.back * speedCAM * Time.deltaTime;
			}*/
		}
			//if(posicion != "cuarta"){ 
				//camara1.transform.position += Vector3.forward * speedCAM * Time.deltaTime;
				//Debug.Log(camara1.transform.position.z);
				//Debug.Log("X" + camara1.transform.position.x);
				//Debug.Log("Y" + camara1.transform.position.y);
				//camara1.transform.position.y == 3.52f || 
		//	}
		}


		// CODIGO PARA EL CAMBIO DE CAMARAS
/*	
		if (Input.GetKeyDown("space")){
			camaraACTUAL++;
			if (camaraACTUAL < listaCamara.Length) {
				listaCamara [camaraACTUAL - 1].gameObject.SetActive (false);
				listaCamara [camaraACTUAL].gameObject.SetActive (true);
			} else {
				listaCamara [camaraACTUAL - 1].gameObject.SetActive (false);
				camaraACTUAL = 0;
				listaCamara [camaraACTUAL].gameObject.SetActive (true);
			}
		}


			
		}
		} 

		if (camara2.isActiveAndEnabled) {

			transform.position += Vector3.down * speed * Time.deltaTime;
			
		}
*/
			
//	}	