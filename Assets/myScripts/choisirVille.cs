using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class choisirVille : MonoBehaviour {
	
	void Start()
	{
		/* Tuve que utilizar el siguiente comando
		*  para hacer que el menu aparezca siempre.
		*/

		Time.timeScale = 1; 
	}
	public void Enclick ()
	{

		SceneManager.LoadScene("menuPrinVille",LoadSceneMode.Single);
	
	}


}
