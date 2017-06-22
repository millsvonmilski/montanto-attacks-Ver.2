using UnityEngine;
using System.Collections;

public class controladorSUELO : MonoBehaviour {

	//public GameObject arbusto1;
	//public GameObject arbusto2;
	public GameObject esfera;

	void Start () {
	
		//esfera = esfera.GetComponent<GameObject> ();

	}

	void Update () {
		if (this.tag == "esfera3") {

			this.transform.Rotate(10.0f*Time.deltaTime,0,10.0f*Time.deltaTime);
		
		}

		this.transform.Rotate(0, 10.0f*Time.deltaTime, 0);
	}
}
