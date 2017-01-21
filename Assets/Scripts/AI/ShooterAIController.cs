using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAIController : MonoBehaviour {

    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public float shootingSpeed = 10.0f; 
    public float shootingStartTime = 0.0f;
    public float shootingInterval = 5.0f; 
    public bool shooting = false;

    public GameObject targetObject; 
    public GameObject projectile; 

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        if (angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed;
        }

        InvokeRepeating("LaunchProjectile", shootingInterval, shootingInterval); 
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current Transform: " + transform.position + " Target Transform" + target.position); 
        if (targetObject != null)
        { 
            if (!shooting)
            {
                agent.SetDestination(targetObject.transform.position);
            }
        }
        
    }

    void LaunchProjectile()
    {
        shooting = true;
        agent.velocity = Vector3.zero; 
        // TO-DO play anim
        //Debug.Log("Shooting!");
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z); 
        GameObject bullet = Instantiate(projectile);
        Rigidbody bulletPhysics = bullet.GetComponent<Rigidbody>();

        Vector3 targetVelocity = (targetObject.transform.position - transform.position).normalized * shootingSpeed;
        targetVelocity.y = 0;
        bulletPhysics.velocity = targetVelocity; 
        shooting = false; 
    }

    public void SetNewTarget(GameObject newTarget)
    {
        targetObject = newTarget;
    }
}
