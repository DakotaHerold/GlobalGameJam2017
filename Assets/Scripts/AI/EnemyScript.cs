using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public float health;
    public float damage;
    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public float stoppingDistance = 0.0f; 
    public int tears;
    public bool isDead;
    public Transform target;

    protected Animator anim;

    [HideInInspector]
    public int contact = 0;


    private Color currentColor;
    private bool routineActive = false;
    public NavMeshAgent agent;

    public virtual void Start()
    {
        //tears = 0;
        currentColor = GetComponent<Renderer>().material.color;
        isDead = false;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        //agent.stoppingDistance = 1.0f;  


        agent.speed = speed;
        if (angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isDead)
        {
            if (anim != null)
            {
                anim.SetBool("IsDead", true);
            }
            Death();
        }
        else
        {

            if (target == null)
            {
                SetClosestPlayerToTarget();
                return;
            }

            float currentDist = Vector3.Distance(target.transform.position, transform.position);
            //Debug.Log(currentDist);
            if (currentDist < stoppingDistance)
            {
                if (anim != null)
                { anim.SetBool("IsMoving", false); }
                agent.Stop();
            }
            else
            {
                if (anim != null)
                {
                    anim.SetBool("IsMoving", true);
                }
                agent.Resume();
            }

            // Rotate towards target
            Vector3 targetDir = target.position - transform.position;
            float step = angularSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            //Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);

            //Death(); 
        }
        
    }
    public virtual void TakeDamage(float damg)
    {
        //Debug.Log("Health before " + health);
        if (anim != null)
        {
            anim.SetBool("GotHit", true);
        }
        health -= damg;
        if(health <= 0)
        {
            isDead = true; 
        }
        //Debug.Log("After: " + health);

        //gameObject.GetComponent<Animator>().Play("Hit");
        if(anim != null)
        {
            anim.Play("Hit");
        }
    }
    public virtual void Death()
    {
        // TO DO play death anim 
        if (anim != null)
        { anim.Play("Death"); }
        agent = null; 
        if(GameManager.gmInstance.enemies.Contains(this.gameObject))
        {
            GameManager.gmInstance.enemies.Remove(this.gameObject); 
        }
        
        DestroyObject(gameObject); 
    }
    
    // Getters 
    public Color GetCurrentColor()
    {
        return currentColor; 
    }

    public void StartFlasher()
    {
        if (!isDead && this != null)
        {
            StartCoroutine(Flash());
        }
    }

    public void SetClosestPlayerToTarget()
    {
        GameObject[] playersObjects = GameObject.FindGameObjectsWithTag("player") as GameObject[];

        GameObject closestPlayer = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject player in playersObjects)
        {
            float currentDist = Vector3.Distance(player.transform.position, transform.position);
            if (currentDist < minDist)
            {
                minDist = currentDist;
                closestPlayer = player;
            }
        }

        if(closestPlayer != null)
        {
            target = closestPlayer.transform; 
        }
        else
        {
            Debug.Log(gameObject.name + " couldn't find a closest player in scene!"); 
        }
        //Debug.Log("Closest player set for " + gameObject.name);
    }

    IEnumerator Flash()
    {
        routineActive = true;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        GetComponent<Renderer>().material.color = GetCurrentColor();
        routineActive = false;
    }
}
