using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float health;
    public float damage;
    public int tears;
    public bool isDead;

    public virtual void Start()
    {
        //tears = 0;
        isDead = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Death(); 
        if(isDead)
        {
            Death(); 
        }
    }
    public virtual void TakeDamage(float damg)
    {
        health -= damg;
        if(health <= 0)
        {
            isDead = true; 
        }
    }
    public virtual void Death()
    {
        // To do play death anim 
        Debug.Log(gameObject.name + " died");
        
    }
    
}
