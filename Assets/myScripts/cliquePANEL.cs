using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class cliquePANEL : MonoBehaviour {

	public controllerGENERAL controlador;

	void Start () {


		GameObject controladorObject1 = GameObject.FindWithTag ("MainCamera");  // Creo instancia del enemiController.
		/*GameObject controladorObject2 = GameObject.FindWithTag ("MainCamera2");
		GameObject controladorObject3 = GameObject.FindWithTag ("MainCamera3");*/

		if (controladorObject1 != null) {
			controlador = controladorObject1.GetComponent<controllerGENERAL> ();

		} /*else if (controladorObject2 != null) {
			
			controlador = controladorObject2.GetComponent<controllerGENERAL> ();

		} else if (controladorObject3 != null){

			controlador = controladorObject3.GetComponent<controllerGENERAL> ();
		}

		if (controlador == null) {
			Debug.Log ("NO PUEDO ENCONTRAR EL 'controllerGENERAL' script");
		} 
		*/
	}

	public void OnMouseDown(){


		if (this.tag == "panel1") {
		

			SceneManager.LoadScene ("monsanto 1", LoadSceneMode.Single);	

		} else if (this.tag == "panel2") {
		
			SceneManager.LoadScene ("monsantoSegunda", LoadSceneMode.Single);	
		
		} else if (this.tag == "panel3") {
		
			SceneManager.LoadScene ("monsantoTercera", LoadSceneMode.Single);
		
		} else if (this.tag == "boton1") {

			controlador.startPAUSE ("continuer");

		} else if (this.tag == "boton2") {

			controlador.startPAUSE ("restart");

		} else if (this.tag == "menu") {
		
			SceneManager.LoadScene ("menuPrinVille", LoadSceneMode.Single);

		} else if (this.tag == "menuAnte"){
		
			SceneManager.LoadScene ("menuPrincipal", LoadSceneMode.Single);

		} else if (this.tag == "boton3") {

			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif

		}
	
	}



	public void cliqueBoton() {
	/*
		if (this.tag == "boton0") {
		
			Debug.Log ("Clicaste el boton recomenzar");
		
		} else if (this.tag == "boton1") {
		
			Debug.Log ("Clicaste el boton Reiniciar");
		
		} else if (this.tag == "boton2") {
		
			Debug.Log ("Clicaste Salir");
		
		}
	*/
	}

}
