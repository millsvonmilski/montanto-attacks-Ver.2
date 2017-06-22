using UnityEngine;
using System.Collections;

public class detruireByClick : MonoBehaviour {
	private GameObject asteroides;
	public GameObject explosionChute;
	private controllerGENERAL controladorGeneral;
	public GameObject explosionClick;


	void Start () 
	{
		asteroides = GameObject.FindGameObjectWithTag ("asteroid");

		GameObject controladorObject1 = GameObject.FindWithTag ("MainCamera");  
		
		if (controladorObject1 != null) 
		{
			controladorGeneral = controladorObject1.GetComponent<controllerGENERAL> ();
		} 
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.gameObject.tag == "sol"
			|| collision.gameObject.tag == "sol2"
			|| collision.gameObject.tag == "sol3") 
			{

			Instantiate (explosionChute, transform.position, transform.rotation);

			controladorGeneral.playSound("sol");

			Destroy (asteroides.gameObject);
			Destroy (gameObject);
		} 

		else if (collision.gameObject.tag == "MainCamera" 
				|| collision.gameObject.tag == "fond"
			    || collision.gameObject.tag == "fond2"
				|| collision.gameObject.tag == "fond3"
				|| collision.gameObject.tag == "enemi1"
				|| collision.gameObject.tag == "enemi2"
				|| collision.gameObject.tag == "enemi3") 
				{

			return;

		} 
		else if (collision.gameObject.tag != "asteroid")
		{

			// CODIGO EN EL QUE DESTRUYO LOS ELEMENTOS DE LA CIUDAD, CONTROLARE LAS VIDAS 
			// DEL USUARIO Y LLAMARE LA FUNCION GAMEOVER CUANDO HAGA FALTA
			
			Instantiate (explosionChute, transform.position, transform.rotation);
			controladorGeneral.playSound("ciudad");


			Destroy (collision.gameObject);
			Destroy (gameObject);
			
			// SIEMPRE RESTO 10 CUANDO SE DESTRUYE UN ELEMENTO
			controladorGeneral.substractScore(10,1); 
		}
	}
}
