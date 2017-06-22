using UnityEngine;
using System.Collections;

public class destruirCLICK : MonoBehaviour {

	public GameObject asteroides;
	public GameObject explosionChute;
	private enemiController controladorGeneral;
	public GameObject explosionClick;
	private GameObject clone;
	public int scoreValue;
	void Start()
	{
		asteroides = GameObject.FindGameObjectWithTag("asteroide");
		GameObject controladorObjeto = GameObject.FindWithTag("MainCamera");

		if (controladorObjeto != null)
		{
			controladorGeneral = controladorObjeto.GetComponent<enemiController>();
		}
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray,out hit))
			{
				if (hit.transform.tag == "asteroide")
				{
					Instantiate(explosionClick, hit.transform.position, hit.transform.rotation);
					controladorGeneral.playSound("clique");
					controladorGeneral.AddScore(scoreValue);	
					Destroy(hit.transform.gameObject);

				} else 
				{
					return;
				}
			}
		}
	}

}
