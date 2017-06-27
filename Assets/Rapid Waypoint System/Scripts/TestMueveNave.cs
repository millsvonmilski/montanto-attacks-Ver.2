using UnityEngine;
using System.Collections;

public class TestMueveNave : MonoBehaviour {

    public GameObject m_agentPrefab;
    public GameObject asteroide;
    public Transform shotSpaw;
    protected WaypointManager m_waypointManager;
    public enemiController controladorNave;
    float spawnWaitLocal;
    //public int m_amountToSpawn;
    //public float spawnInterval = 1.25f;
    //protected int m_spawned = 0;
    //public Transform m_spawnPoint;
    //public bool m_infinite = false;


	void Start () {
        m_waypointManager = GetComponent<WaypointManager>();
       
        GameObject m_agentPrefab = GameObject.FindWithTag("enemi");
        GameObject controladorObjetoNave = GameObject.FindWithTag("MainCamera");

		if (controladorObjetoNave != null)
		{
			controladorNave = controladorObjetoNave.GetComponent<enemiController>();
		}
            StartCoroutine(Spawn());           
	}

    void Update()
    {
        spawnWaitLocal = controladorNave.spawnWait;       
    }

    IEnumerator Spawn()
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
                // m_waypointManager.AddEntity((GameObject)
                // Instantiate(m_agentPrefab, m_spawnPoint.position, m_spawnPoint.rotation));
                Debug.Log("ANTES DE ASIGNAR ENTIDAD");
                //m_waypointManager.AddEntity((GameObject) (m_agentPrefab));
                m_waypointManager.AddEntity(m_agentPrefab.gameObject);
                //controladorNave.movement = controladorNave.movement.normalized * controladorNave.speed * Time.deltaTime * 1;
                /*    
				controladorNave.movement = GetComponent<Rigidbody>().position = new Vector3 (
							Random.Range(controladorNave.boundary.xMin,controladorNave.boundary.xMax),
							62.7f,
							Random.Range(controladorNave.boundary.zMin,controladorNave.boundary.zMax));
				
				controladorNave.movement = controladorNave.movement.normalized * controladorNave.speed * Time.deltaTime * 1;

				//GetComponent<Rigidbody>().AddForce(controladorNave.movement * controladorNave.speed * Time.deltaTime);
                /* 
				GetComponent<Rigidbody>().position = new Vector3 (
					Mathf.Clamp(GetComponent<Rigidbody>().position.x, controladorNave.boundary.xMin, controladorNave.boundary.xMax),
					62.7f,
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, controladorNave.boundary.zMin, controladorNave.boundary.zMax)	
				);
                */   
				yield return new WaitForSeconds(controladorNave.spawnWait);
			}
       
       
       
       
        //yield return new WaitForSeconds(spawnInterval);

        //m_waypointManager.AddEntity((GameObject)Instantiate(m_agentPrefab, m_spawnPoint.position, m_spawnPoint.rotation));
        /* 
        if (m_spawned++ < m_amountToSpawn - 1 | m_infinite)
            StartCoroutine(Spawn());
        */
    }
}
