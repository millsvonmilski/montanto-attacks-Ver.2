using UnityEngine;
using System.Collections;

public class VehicleAvoidance : MonoBehaviour
{
    public float speed = 20.0f;
    public float mass = 5.0f;
    public float force = 50.0f;
    public float minimumDistToAvoid = 20.0f;

    //Actual speed of the vehicle
    private float curSpeed;
    private Vector3 targetPoint;

    void Start()
    {
        mass = 5.0f;
        targetPoint = Vector3.zero;        
    }

    void OnGUI()
    {
        GUILayout.Label("Click anywhere to move the vehicle.");
    }

    void Update()
    {
        // Vehicle move by mouse click 
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay // I shoot a ray from the camera in the direction its looking.
			(Input.mousePosition);

		if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray,out hit, 100.0f))
		{
			targetPoint = hit.point;  // Once we get the target position vector from the target position
									  // 	
		}

		// Directional vector to the target position
		Vector3 dir = (targetPoint - transform.position);  // Now we can calculate the direction vector by 
		dir.Normalize(); 								   // substracting the current position vector from the 
														   // target position vector

		// Apply obstacle avoidance
		AvoidObstacles(ref dir);				        


        //Dont move the vehicle whe the target point 
        // is reached
        if (Vector3.Distance(targetPoint,transform.position) < 3.0f) return;
        
        curSpeed = speed * Time.deltaTime;

        //Rotate the vehicle to its target
        //directional vector
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5.0f * Time.deltaTime);

        //Move the vehicle towards 
        transform.position += transform.forward * curSpeed;

    }


    // Calculate the new directinal vector to avoid
	// the obstacle
	public void AvoidObstacles(ref Vector3 dir)
	{
		RaycastHit hit;

		// Only detect layer 8 (Obstacles)
		int layerMask = 1<<8;

		//Check that the vehicle hit with the obstacles within
		// its minimum distance to avoid
		if (Physics.Raycast(transform.position,transform.forward, out hit,minimumDistToAvoid, layerMask))
		{ 
			// Get the normal of the hit point to calculate the
			// new direction
			Vector3 hitNormal = hit.normal;
			hitNormal.y = 0.0f; // Dont want to mo in Y-Space	

			//Get the new directional vector by adding force to
			//vehicles current forward vector
			dir = transform.forward + hitNormal * force;
		}
	}	
}