using UnityEngine;
using System.Collections;

public class mueveNAVE : MonoBehaviour {
	public enemiController controladorNave;
	public GameObject asteroide;
	public Transform shotSpaw;
	float spawnWaitLocal;

	// PARA MOVER LA CAMARA //
	private Vector3 fuera;

	void Start()
	{
		GameObject controladorObjetoNave = GameObject.FindWithTag("MainCamera");

		if (controladorObjetoNave != null)
		{
			controladorNave = controladorObjetoNave.GetComponent<enemiController>();
		}
	}


	void Update()
	{
		spawnWaitLocal = controladorNave.spawnWait;
	}

	// FUNCION QUE GENERA LOS METEORITOS Y MUEVE LA NAVE.
	public IEnumerator waitSpawner()
	{
		yield return new WaitForSeconds(spawnWaitLocal);

			while (true)
			{
				for (int i = 0; i < controladorNave.nombreAsteroids; i++)
				{
					if (controladorNave.seCOMENZO)
					{
						Instantiate(asteroide,shotSpaw.transform.position,
								shotSpaw.transform.rotation);
								controladorNave.audio.Play();
					}
					// Cuanto tiempo espero para tirar las bolas antes de mover la nave.	
					yield return new WaitForSeconds(1);
				}

				controladorNave.movement = GetComponent<Rigidbody>().position = new Vector3 (
							Random.Range(controladorNave.boundary.xMin,controladorNave.boundary.xMax),
							62.7f,
							Random.Range(controladorNave.boundary.zMin,controladorNave.boundary.zMax));
				
				controladorNave.movement = controladorNave.movement.normalized * controladorNave.speed * Time.deltaTime * 1;

				GetComponent<Rigidbody>().AddForce(controladorNave.movement * controladorNave.speed * Time.deltaTime);

				GetComponent<Rigidbody>().position = new Vector3 (
					Mathf.Clamp(GetComponent<Rigidbody>().position.x, controladorNave.boundary.xMin, controladorNave.boundary.xMax),
					62.7f,
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, controladorNave.boundary.zMin, controladorNave.boundary.zMax)	
				);

				yield return new WaitForSeconds(controladorNave.spawnWait);
			}
	}	
}
