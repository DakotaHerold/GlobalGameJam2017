using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAIController : MonoBehaviour {

    public float speed = 1.0f;
    public float angularSpeed = 0.0f; 
    public Transform target;
    

    private NavMeshAgent agent;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed; 
        if(angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
