using UnityEngine;
using UnityEngine.UI;				// Para trabajar con texto hay que usar esta clase.
using UnityEngine.SceneManagement;  // Para reinicializar las escena hay que usar esta clase.
using System.Collections;

[System.Serializable]
public class Limite2 {
	public float xMin, xMax, zMin, zMax;
}

public class controllerGENERAL : MonoBehaviour {

	public float speed;
	public GameObject enemi; 
	public Limite2 limite2;
	public int startWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public float spawnWait;
	public Vector3 movement;

	public GameObject asteroides;
	
	/* ESTA ES LA VARIABLE QUE VOY A MODIFICAR PARA CAMBIAR 
	*  LA CANTIDAD DE METEORITOS QUE CAEN
	*/
	public int nombreAsteroids; //
	public float dropRate;
	public Transform shotSpawn;


	// TEXTOS !!!!
    public Text pointsText;
	public Text pressP;
	public Text pressPcons;
	private bool gameOver;

	// PARA CONTROLAR INICIO y RESTART
	private bool restart;
	public bool sePAUSO = false;
	public bool seCOMENZO = false;

	private int counter;
	private int countBatiments;

	// AUDIOS !!!!
	public AudioSource audio; //----- private
	private AudioSource audioSol;
	private AudioSource audioClique;
	private AudioSource audioCiudad;

	public AudioSource mainAUDIO;
	public AudioSource pauseAUDIO;

	//private GameObject asteroides;

	// BOTONES PAUSE 
	public Button boton1;
	public Button boton2;
	public Button boton3;
	public Button boton4;

	// OTROS BOTONES
	public Button score;
	public Button newGame;
	public Button settings;

	// CREO INSTANCIA DE LA CLASE QUE MUEVE LA NAVE
	public moveNavire mover; 

	void Start() {

		asteroides = gameObject.GetComponent<GameObject> ();
		boton1 = boton1.GetComponent<Button> ();
		boton2 = boton2.GetComponent<Button> ();
		boton3 = boton3.GetComponent<Button> ();
		boton4 = boton4.GetComponent<Button> ();
		newGame = newGame.GetComponent<Button>();
		settings = settings.GetComponent<Button> ();

		mainAUDIO = Camera.main.transform.Find ("mainAUDIO").GetComponent<AudioSource>();
		pauseAUDIO = Camera.main.transform.Find ("pauseAUDIO").GetComponent<AudioSource> ();

		GameObject controladorMover = GameObject.FindWithTag ("enemi1");
		if (controladorMover != null)
		{
			mover = controladorMover.GetComponent<moveNavire> ();
		}

		// INICIALIZADORES
		this.inicializarBOTONESPAUSE(); 
		this.inicializarAUDIO();
		this.inicializarVARIABLES();
		this.inicializarBOTONESLATERALES();

		// ESTA ES LA FUNCION QUE COMIENZA A MOVER NAVE Y ASTEROIDES.	
		this.comenzarJUEGO();

		//EL JUEGO COMIENZA EN PAUSA
		Time.timeScale = 0;  
	}

	void Update () 
	{
		spawnWait = Random.Range (spawnLeastWait,spawnMostWait); 

		// LA LLAMO TODO EL TIEMPO PARA VERIFICAR SI EL USUARIO QUIERE HACER PAUSA O NO.
		startPAUSE ("rutina"); 
	}
	public void startPAUSE(string ordena)
	{	
		// VERIFICO SI EL USUARIO QUIERE REINICIAR EL JUEGO DESPUES DE HABER PRESIONADO 'R'
		
		if (restart || ordena == "restart") 
		{
			if (Input.GetKeyDown (KeyCode.R) || ordena == "restart") 
			{
				if (this.tag == "MainCamera") 
				{			
					SceneManager.LoadScene("monsanto 1", LoadSceneMode.Single); 	
				} else if (this.tag == "MainCamera2") 
				{
					SceneManager.LoadScene("monsantoSegunda", LoadSceneMode.Single); 
				} else if (this.tag == "MainCamera3") 
				{
					SceneManager.LoadScene("monsantoTercera", LoadSceneMode.Single);
				}
			} else if (Input.GetKeyDown (KeyCode.P)) 
			{
				return;
			}
			
			// IL FAUT APPUYER P POUR COMMENCER LE JEU ET POUR LE PAUSER.		

		} else 	if (Input.GetKeyDown (KeyCode.P) || ordena == "continuer") 
		{  
					if (Time.timeScale == 1) 
					{
						 Time.timeScale = 0;
						 this.hacerBotonesAccesibles(); // HAGO ACCESIBLE LOS BOTONES
						 this.accionPAUSAR();
					} else if (sePAUSO == false) 
					{
						 Time.timeScale = 1;
						 this.comenzarJUEGO();
						 this.accionREINICIAR();
					} else 
					{
					     Time.timeScale = 1;
						 this.accionREINICIAR();
						 this.inicializarBOTONESPAUSE();
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
	
		if (counter != 0) 
		{
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
		//	gameOverText.text = "Game Over !!!";
		gameOver = true;
	}

	// FUNCION QUE TOCA LOS DIFERENTES SONIDOS 
	public void playSound(string sonido) 
	{
		
		if (sonido == "sol") {
			audioSol.Play ();
		} else if (sonido == "clique") 
		{
			audioClique.Play ();
		} else if (sonido == "ciudad") 
		{
			audioCiudad.Play ();
		}
	}

	private void inicializarBOTONESPAUSE()
	{
		// DESACTIVO LOS BOTONES PAUSA.
		boton1.gameObject.SetActive (false);
		boton2.gameObject.SetActive (false);
		boton3.gameObject.SetActive (false);
		boton4.gameObject.SetActive (false);
		return;
	}
	private void inicializarBOTONESLATERALES()
	{
		// BOTONES LATERALES DERECHO
		newGame.gameObject.SetActive (false);
		settings.gameObject.SetActive (false);
		return;
	}
	private void inicializarAUDIO()
	{
		// INICIALIZO VARIABLES DE AUDIO
		AudioSource[] audiosExplosion = GetComponents<AudioSource> ();
		audio = audiosExplosion [0];
		audioClique = audiosExplosion [1];
		audioSol = audiosExplosion [2];
		audioCiudad = audiosExplosion [3];

		pauseAUDIO.Stop();  // EVIDENTEMENTE EL AUDIO COMIENZA PARADO.

		return;
	}
	private void inicializarVARIABLES()
	{
		// INICIALIZO VARIABLES RELACIONADAS A LOS PUNTOS Y LA 'VIDA' DEL USUARIO.
		counter = 0;
		countBatiments = 0;
		updateScore ();
					
		// INICIALIZO VARIABLES CONTROL FIN DE JUEGO Y VACIO LOS VALORES DE MIS TEXTOS.
		gameOver = false;
		restart = false;

		pointsText.enabled = false;
		score.gameObject.SetActive (false);
		pressPcons.enabled = false;
		pressP.enabled = true;
		return;
	}
	private void hacerBotonesAccesibles()
	{
		 boton1.gameObject.SetActive (true);
		 boton2.gameObject.SetActive (true);
		 boton3.gameObject.SetActive (true);
		 boton4.gameObject.SetActive (true);
		 boton1.interactable = true;
		 boton2.interactable = true;
		 boton3.interactable = true;
		 boton4.interactable = true;
		 return;
	}

	private void accionPAUSAR()
	{
		mainAUDIO.mute = true;  // APAGO EL AUDIO PRINCIPAL.
		pauseAUDIO.Play(); // ENCIENDO EL AUDIO DE PAUSA.
		score.gameObject.SetActive (false);
		pressPcons.enabled = false;
		return;
	}
	private void accionREINICIAR()
	{
		//textoInicio.enabled = false;
		pointsText.enabled = true;		 				
		pressP.enabled = false; 
		pressPcons.enabled = true;
		score.gameObject.SetActive (true);
		mainAUDIO.mute = false;  // ENCIENDO EL AUDIO PRINCIPAL
		pauseAUDIO.Stop(); // PARO EL AUDIO DE PAUSA.
		//pressP.enabled = true;
		sePAUSO = true;
		seCOMENZO = true;
		return;
	}
	private void comenzarJUEGO()
	{
		// COMIENZA EL JUEGO				 
		mover.StartCoroutine (mover.waitSpawner ());
		return;
	}
}