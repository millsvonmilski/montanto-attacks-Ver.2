using UnityEngine;
using System.Collections;

public class detruireByClick1 : MonoBehaviour {
	public GameObject asteroides;
	public GameObject explosionChute;
	private controllerGENERAL controladorGeneral;
	public GameObject explosionClick;
	private GameObject clone;
	public int scoreValue;

	void Start () 
	{
		asteroides = GameObject.FindGameObjectWithTag ("asteroid");

		GameObject controladorObject1 = GameObject.FindWithTag ("MainCamera");  
	
		if (controladorObject1 != null) 
		{
			controladorGeneral = controladorObject1.GetComponent<controllerGENERAL> ();
		} 
	}
		

	void FixedUpdate () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.transform.tag == "asteroid") 
				{	
					Instantiate (explosionClick, hit.transform.position, hit.transform.rotation);

					controladorGeneral.playSound("clique");
					
					// Asigno un nuevo valor de score.
					controladorGeneral.AddScore (scoreValue); 
					Destroy (hit.transform.gameObject);

				} 
				else 
				{	
					return;
				}
			}
		}
}
}
		