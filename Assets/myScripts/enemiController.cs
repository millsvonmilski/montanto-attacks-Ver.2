using UnityEngine;
using UnityEngine.UI;				// Para trabajar con texto hay que usar esta clase.
using System.Collections;
using UnityEngine.SceneManagement;  // Para reinicializar las escena hay que usar esta clase.

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}
public class enemiController : MonoBehaviour {

		public float speed;
		public GameObject enemi; 
		public Boundary boundary;
		public int startWait;
		public float spawnMostWait;
		public float spawnLeastWait;
		public float spawnWait;
		public Vector3 movement;

		public GameObject asteroids;
		public int nombreAsteroids;
		public Transform shotSpawn;
		public float dropRate;

		// TEXTOS !!!!
		public Text pointsText;
		public Text restartText;
		public Text gameOverText;
		public Text pressP;
		public Text textoInicio;

		private bool gameOver;
		private bool restart;
	 	
		private int counter;
		private int countBatiments;

		// AUDIOS !!!!
		public AudioSource audio;
		private AudioSource audioSol;
		private AudioSource audioClique;
		private AudioSource audioCiudad;
		public AudioSource mainAUDIO;
		public AudioSource pauseAUDIO;
		

		public bool sePAUSO = false;
		public bool seCOMENZO = false;

		public Canvas mainMenu;

		public mueveNAVE mueveNAVE;

		void Start() 
		{

			asteroids = gameObject.GetComponent<GameObject>();
			GameObject controladorMover = GameObject.FindWithTag("enemi");

			if (controladorMover != null)
			{
				mueveNAVE = controladorMover.GetComponent<mueveNAVE>();
			}

			// INICIALIZO VARIABLES RELACIONADAS A LOS PUNTOS Y LA 'VIDA' DEL USUARIO.
			counter = 0;
			countBatiments = 0;
			updateScore ();
		
			
			textoInicio.enabled = true;
			pointsText.enabled = false;
			pressP.enabled = false;
				

			Time.timeScale = 0;  // J'arrete le temps.  Le jeu commence en pause.

			// INICIALIZO VARIABLES CONTROL FIN DE JUEGO Y VACIO LOS VALORES DE MIS TEXTOS.

			gameOver = false;
			restart = false;
			gameOverText.text = "";
			restartText.text = "";

			// INICIALIZO VARIABLES DE AUDIO 
			AudioSource[] audiosExplosion = GetComponents<AudioSource> ();
			audio = audiosExplosion [0];
			audioClique = audiosExplosion [1];
			audioSol = audiosExplosion [2];
			audioCiudad = audiosExplosion [3];

			mainAUDIO = Camera.main.transform.Find("mainAUDIO").GetComponent<AudioSource>();
			pauseAUDIO = Camera.main.transform.Find("pauseAUDIO").GetComponent<AudioSource>();

			pauseAUDIO.Stop();
		}		

		void Update () 
		{
			spawnWait = Random.Range (spawnLeastWait,spawnMostWait);  

			/* PARA CAMBIAR DE ESCENA */

			if(Input.GetKeyDown (KeyCode.A))
			{
				SceneManager.LoadScene("interiorCIUDAD",LoadSceneMode.Single);
			}

		// VERIFICO SI EL USUARIO QUIERE REINICIAR EL JUEGO DESPUES DE HABER PRESIONADO 'R'
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				SceneManager.LoadScene("monsanto", LoadSceneMode.Single);
			} 
			else if (Input.GetKeyDown (KeyCode.P)) 
			{
				return;
			}

		} else 	if (Input.GetKeyDown (KeyCode.P)) // IL FAUT APPUYER P POUR COMMENCER LE JEU ET POUR LE PAUSER.
		{  		
			if (Time.timeScale == 1) 
			{
				
				Time.timeScale = 0;
				mainAUDIO.mute = true;
				pauseAUDIO.Play();

			} else if (sePAUSO == false) 
			{

				Time.timeScale = 1;
							
				// COMIENZA EL JUEGO	

				mueveNAVE.StartCoroutine (mueveNAVE.waitSpawner ());  // COMIENZO A MOVER LA NAVE E INSTANCIO LOS ASTEROIDES
						
				textoInicio.enabled = false;
				pointsText.enabled = true;
				pressP.enabled = true;
				sePAUSO = true;
				seCOMENZO = true;

				mainAUDIO.mute = false;
				pauseAUDIO.Stop();

			} else 
			{

				Time.timeScale = 1;
				textoInicio.enabled = false;
				pointsText.enabled = true;
				pressP.enabled = true;
				sePAUSO = true;
				mainAUDIO.mute = false;
				pauseAUDIO.Stop();
				//mainMenu.enabled = false;
			}
			}

		}
		
	// FUNCIONES DEDICADAS A CALCULAR CUANTOS PUNTOS LLEVA EL JUGADOR Y A SUMAR CADA VEZ QUE HAGA UNO MAS.
	public void AddScore (int newScoreValue) 
	{
		counter += newScoreValue;
		updateScore ();
	}

	void updateScore () 
	{
		pointsText.text = "Score " + counter;
	}
	public void substractScore(int newScoreValue,int batiments) 
	{
		if (counter != 0) {
			counter -= newScoreValue;
			updateScore ();
		}
		if (countBatiments < 9) 
		{
			countBatiments += batiments;
			Debug.Log (countBatiments);

		} else 
		{	
			GameOver ();	
		}
	}
	public void GameOver() 
	{
		gameOverText.text = "Game Over !!!";
		gameOver = true;
	}

	// FUNCION QUE TOCA LOS DIFERENTES SONIDOS 
	public void playSound(string sonido) 
	{
		if (sonido == "sol") 
		{
			audioSol.Play ();
		} else if (sonido == "clique") 
		{
			audioClique.Play ();
		} else if (sonido == "ciudad") 
		{
			audioCiudad.Play ();	
		}
	}
}