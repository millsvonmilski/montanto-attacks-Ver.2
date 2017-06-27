using UnityEngine;
using System.Collections;

public class destruirConCLICK : MonoBehaviour {

	private GameObject asteroides;
	public GameObject explosionChute;
	private enemiController controladorGeneral;
	public GameObject explosionClick;
	
	void Start()
	{
		asteroides = GameObject.FindGameObjectWithTag("asteroide");
		GameObject controladorObjeto = GameObject.FindGameObjectWithTag("MainCamera");

		if (controladorObjeto != null)
		{
			controladorGeneral = controladorObjeto.GetComponent<enemiController>();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "sol")
		{
			Instantiate(explosionChute, transform.position, transform.rotation);

			controladorGeneral.playSound("sol");

			Destroy(asteroides.gameObject);
			Destroy(gameObject);

		} else if (collision.gameObject.tag == "MainCamera"
				   || collision.gameObject.tag == "fond"
				   || collision.gameObject.tag == "enemi")
		{
			return;
		} else if (collision.gameObject.tag != "asteroide")
		{
			Instantiate(explosionChute, transform.position, transform.rotation);
			controladorGeneral.playSound("ciudad");

			Destroy (collision.gameObject);
			Destroy(gameObject);
			controladorGeneral.substractScore(10,1);
		}
	}
}
