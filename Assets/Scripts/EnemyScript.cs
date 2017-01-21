using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float health;
    public float attDamage;
    public int tears;
    public bool isDead;

    void Start()
    {
        health = 50;
        attDamage = 3;
        tears = 0;
        isDead = false;


    }

    // Update is called once per frame
    void Update()
    {
        Death();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(50);
        }
    }
    public void TakeDamage(float damg)
    {
        health -= damg;
    }
    void Death()
    {
        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Is Deads");
        }
        if (isDead == true)
        {
            
        }
    }
}
