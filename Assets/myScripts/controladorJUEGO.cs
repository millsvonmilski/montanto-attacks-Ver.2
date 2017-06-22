/*
using UnityEngine;
using UnityEngine.UI;				// Para trabajar con texto hay que usar esta clase.
using UnityEngine.SceneManagement;  // Para reinicializar las escena hay que usar esta clase.
using System.Collections;

[System.Serializable]
public class Limite {
	public float xMin, xMax, zMin, zMax;
}

public class controladorJUEGO : MonoBehaviour {

	public float speed;
	public GameObject enemi; 
	public Limite limite;
	public int startWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public float spawnWait;
	Vector3 movement;

	public GameObject asteroids;
	public int nombreAsteroids;
	public Transform shotSpawn;

	public float dropRate;

	private bool gameOver;
	private bool restart;

	private int counter;
	private int countBatiments;

	// AUDIOS !!!!
	private AudioSource audio;
	private AudioSource audioSol;
	private AudioSource audioClique;
	private AudioSource audioCiudad;

	private GameObject asteroides;

	private bool sePAUSO = false;

	//	public GameObject niveau;


	void Start() {



		// INICIALIZO VARIABLES RELACIONADAS A LOS PUNTOS Y LA 'VIDA' DEL USUARIO.
		counter = 0;
		countBatiments = 0;
		//updateScore ();
		////////////////////

		//StartCoroutine (waitSpawner ());


		Time.timeScale = 0;  // J'arrete le temps.  Le jeu commence en pause.


		// INICIALIZO VARIABLES DE AUDIO

		AudioSource[] audiosExplosion = GetComponents<AudioSource> ();
		audio = audiosExplosion [0];
		audioClique = audiosExplosion [1];
		audioSol = audiosExplosion [2];
		audioCiudad = audiosExplosion [3];

		// ASIGNO EL SCROLL A LA VARIABLE niveau

		//niveau = GetComponent<Scrollbar> ().gameObject;

	}



	void Update () {

		
		spawnWait = Random.Range (spawnLeastWait,spawnMostWait); // VER 
	


		// VERIFICO SI EL USUARIO QUIERE REINICIAR EL JUEGO DESPUES DE HABER PRESIONADO 'R'

		if (restart) {

			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene("interiorVille", LoadSceneMode.Single); // NUEVO COMANDO PARA REINICIALIZAR LA ESCENA
				//	Application.LoadLevel (Application.LoadLevel);

			} else if (Input.GetKeyDown (KeyCode.P)) {

				return;
			}

		} else 	if (Input.GetKeyDown (KeyCode.P)) {  // IL FAUT APPUYER P POUR COMMENCER LE JEU ET POUR LE PAUSER.		

			if (Time.timeScale == 1) {

				Time.timeScale = 0;

			} else if (sePAUSO == false) {

				Time.timeScale = 1;

				// COMIENZA EL JUEGO	

				StartCoroutine (waitSpawner ()); // 
			
				sePAUSO = true;

			} else {

				Time.timeScale = 1;
				//textoInicio.enabled = false;
				sePAUSO = true;

			}
		}

	}




	// FUNCION QUE GENERA LOS METEORITOS Y MUEVE LA NAVE.

	IEnumerator waitSpawner() {

		yield return new WaitForSeconds(startWait);


		while (true) {
			/*
			for (int i = 0; i < nombreAsteroids; i++) {

				Instantiate (asteroids, shotSpawn.transform.position, shotSpawn.transform.rotation);// as GameObject;
				audio.Play ();
				yield return new WaitForSeconds(1);  // Cuanto tiempo espero para tirar las bolas antes de mover la nave.
			}
			*/

/*
			movement = GetComponent <Rigidbody>().position = new Vector3 
				(
					Random.Range (limite.xMin, limite.xMax),
					62.7f,
					Random.Range(limite.zMin, limite.zMax));

			movement = movement.normalized * speed * Time.deltaTime * 1;



			GetComponent <Rigidbody>().AddForce(movement * speed * Time.deltaTime);



			GetComponent <Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp (GetComponent<Rigidbody>().position.x, limite.xMin, limite.xMax), 
					62.7f, 
					Mathf.Clamp (GetComponent<Rigidbody>().position.z, limite.zMin, limite.zMax)
				);

			yield return new WaitForSeconds(spawnWait);


		}

	}




	// FUNCIONES DEDICADAS A CALCULAR CUANTOS PUNTOS LLEVA EL JUGADOR Y A SUMAR CADA VEZ QUE HAGA UNO MAS.

	public void AddScore (int newScoreValue) {

		counter += newScoreValue;
	//	updateScore ();

	}



	public void substractScore(int newScoreValue,int batiments) {

		if (counter != 0) {

			counter -= newScoreValue;
			//updateScore ();
		}

		if (countBatiments < 9) {
			countBatiments += batiments;
			Debug.Log (countBatiments);

		} else {

			GameOver ();	
		}
	}




	public void GameOver() {

		//gameOverText.text = "Game Over !!!";
		gameOver = true;
	}



	// FUNCION QUE TOCA LOS DIFERENTES SONIDOS 

	public void playSound(string sonido) {

		if (sonido == "sol") {

			audioSol.Play ();

		} else if (sonido == "clique") {

			audioClique.Play ();

		} else if (sonido == "ciudad") {

			audioCiudad.Play ();

		}

	}
}
*/