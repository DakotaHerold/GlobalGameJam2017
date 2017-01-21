using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAIController : EnemyScript {

    public float shootingSpeed = 10.0f; 
    public float shootingStartTime = 0.0f;
    public float shootingInterval = 5.0f; 
    public bool shooting = false;

    public GameObject projectile; 

    private NavMeshAgent agent;

    // Use this for initialization
    new void Start()
    {
        base.Start(); 
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        if (angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed;
        }

        InvokeRepeating("LaunchProjectile", shootingInterval, shootingInterval); 
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //Debug.Log("Current Transform: " + transform.position + " Target Transform" + target.position); 
        if (!shooting)
        {
            agent.SetDestination(target.position);
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

        Vector3 targetVelocity = (target.position - transform.position).normalized * shootingSpeed;
        targetVelocity.y = 0;
        bulletPhysics.velocity = targetVelocity; 
        shooting = false; 
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }
}
