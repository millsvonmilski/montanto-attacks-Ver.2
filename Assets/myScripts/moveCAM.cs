using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveCAM : MonoBehaviour {

	private moveINTERIORVILLE interior;


	void Start () {

		GameObject InteriorObject = GameObject.FindWithTag ("fond"); 

		if (InteriorObject != null) {
			interior = InteriorObject.GetComponent<moveINTERIORVILLE> ();
		} 

		if (interior == null) {
			Debug.Log ("NO PUEDO ENCONTRAR EL 'moveINTERIORVILLE' script");
		}

	}
	

	void Update () {
		

						if (interior.posicion == "tercera" || interior.camara1.transform.position.x == -38.02f
						    || interior.camara1.transform.position.y == 20.42f) { 

										if (interior.camara1.transform.position.z > 4.73f) {
											
											interior.posicion = "cuarta";
											interior.paraATRAS = true;
											this.transform.position = new Vector3 (interior.posCAMARA3.pos3X, interior.posCAMARA3.pos3Y, interior.posCAMARA3.pos3Z);
										}
						}



						if (interior.posicion == "cuarta" || interior.camara1.transform.position.x == 1.1f
						    || interior.camara1.transform.position.y == 34f) {

										if (interior.camara1.transform.position.z < 1) { 
											this.transform.position += Vector3.back * interior.speedCAM * Time.deltaTime;

											if(interior.camara1.transform.position.x == -38.88f 
												&& interior.camara1.transform.position.y == 3.7f 
												&& interior.camara1.transform.position.z < -16.165) {

												interior.posicion = "quinta";
												this.transform.position = new Vector3 (interior.posCAMARA4.pos4X, interior.posCAMARA4.pos4Y, interior.posCAMARA4.pos4Z);
											}
										}
						}



						if (interior.posicion == "quinta" || interior.camara1.transform.position.x == -27.16f
							|| interior.camara1.transform.position.y == 2.56f) {

									if (interior.camara1.transform.position.z < 1) {
											this.transform.position += Vector3.back * interior.speedCAM * Time.deltaTime;
									}

									if (interior.camara1.transform.position.z > -16f) {

										interior.posicion = "sexta";
										Debug.Log (this.transform.position);
							
									} else {
							
											SceneManager.LoadScene ("menuPrinVille", LoadSceneMode.Single);

									} 
											
						}

	}


	void OnCollisionEnter(Collision collision) {

							if (collision.gameObject.tag == "edificio2" 
								&& interior.posicion != "quinta" 
								&& interior.posicion != "sexta" ) {

												this.transform.position = new Vector3 (interior.posCAMARA2.pos2X, interior.posCAMARA2.pos2Y, interior.posCAMARA2.pos2Z);
												interior.posicion = "tercera";

							} 

						
	}
}

		
/* ESTO ME GENERA UN MOVIMIENTO DE PARA ADELANTE Y HACIA A TRAS EN BLUCE, INTERESANTE
		if (interior.camara1.transform.position.x == -38.02f 
			|| interior.camara1.transform.position.y == 20.42f 
			|| interior.camara1.transform.position.z > 6){

			this.transform.position = new Vector3 (interior.posCAMARA3.pos3X, interior.posCAMARA3.pos3Y, interior.posCAMARA3.pos3Z);
			interior.camara1.transform.position += Vector3.back * interior.speedCAM * Time.deltaTime;
		}
		*/


/*
if (interior.posicion == "quinta" || interior.camara1.transform.position.x == -38.88f
	|| interior.camara1.transform.position.y == 3.7f) {

	if (interior.camara1.transform.position.z < -16.165f) {

		this.transform.position = new Vector3 (interior.posCAMARA4.pos4X, interior.posCAMARA4.pos4Y, interior.posCAMARA4.pos4Z);

	}

}*/
