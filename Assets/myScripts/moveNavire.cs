using UnityEngine;
using System.Collections;

public class moveNavire : MonoBehaviour 
{
	public controllerGENERAL controladorNave;
	public GameObject asteroide;
	public Transform shotSpaw;
	float spawnWaitLocal;
	// POUR BOUGER LA CAMERA //
	private Vector3 fuera;


	void Start()
	{
			
		GameObject controladorObjectNave = GameObject.FindWithTag ("MainCamera"); 
		
		if (controladorObjectNave != null) {
			controladorNave = controladorObjectNave.GetComponent<controllerGENERAL> ();
		} 
	}

	void Update(){
		//controladorNave.spawnWait = Random.Range (controladorNave.spawnLeastWait,controladorNave.spawnMostWait);
		spawnWaitLocal = controladorNave.spawnWait;
	}


	// FUNCION QUE GENERA LOS METEORITOS Y MUEVE LA NAVE.
	public IEnumerator waitSpawner() 
	{
		yield return new WaitForSeconds (spawnWaitLocal);

			while (true) 
			{
				for (int i = 0; i < controladorNave.nombreAsteroids; i++) 
				{					
					if (controladorNave.seCOMENZO) 
					{
						Instantiate (asteroide, shotSpaw.transform.position, shotSpaw.transform.rotation);
									controladorNave.audio.Play ();
					}
					// Cuanto tiempo espero para tirar las bolas antes de mover la nave.	
					yield return new WaitForSeconds (1);  
				}

		controladorNave.movement = GetComponent <Rigidbody> ().position = new Vector3 (
				Random.Range (controladorNave.limite2.xMin, controladorNave.limite2.xMax),
				62.7f,
				Random.Range (controladorNave.limite2.zMin, controladorNave.limite2.zMax));

							
		controladorNave.movement = controladorNave.movement.normalized * controladorNave.speed * Time.deltaTime * 1;

							
			GetComponent <Rigidbody> ().AddForce (controladorNave.movement * controladorNave.speed * Time.deltaTime);
			
			GetComponent <Rigidbody> ().position = new Vector3 (
				Mathf.Clamp (GetComponent<Rigidbody> ().position.x, controladorNave.limite2.xMin, 
							controladorNave.limite2.xMax), 
				62.7f, 
				Mathf.Clamp (GetComponent<Rigidbody> ().position.z, controladorNave.limite2.zMin, 
							controladorNave.limite2.zMax));

			yield return new WaitForSeconds (controladorNave.spawnWait);

						// COMPRUEBO SI GAMEOVER ES VERDADERO
						/*
					if (gameOver) {

						//restartText.text = "Appuyez 'R' pour Redémarrer";
						restart = true;  // BOOLEAN QUE DEFINI ARRIBA
						pressP.enabled = false;	
						break;
					}
					*/
			}
		}
	}