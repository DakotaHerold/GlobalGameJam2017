using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    GameObject stick;

    public float health;
    public float healthGen;
    public float regenTimer;
    public float speed;
    public float attDamage;
    public int tears;
    public bool regainHealth;
    public bool isDead;
    public bool hasWeapon;
    public bool isPinging;
	void Start () {
        stick = null;

        health = 100;
        healthGen = 5;
        regenTimer = 1.0f;
        attDamage = 3;
        tears = 0;
        regainHealth = false;
        isDead = false;
        hasWeapon = false;
        isPinging = false;


    }
	
	// Update is called once per frame
	void Update ()
    {
        Death();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(50);
        }
        //HealthReGen();
        CheckIfPing();	
	}
    public void TakeDamage(float damg)
    {
        health -= damg;
    }
    IEnumerator healthRegen()
    {
        yield return new WaitForSeconds(regenTimer);
        regainHealth = false;
        if (hasWeapon == false && health < 100)
        {
            health += healthGen;
        }
    }
    void HealthReGen()
    {
        if(regainHealth == false)
        {
            regainHealth = true;
            StartCoroutine(healthRegen());
        }
    }
    void CheckIfPing()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Is pinging");
        }
    }
    void Death()
    {
        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Is Deads");
        }
        if(isDead == true)
        {
            healthGen = 0;
        }
    }
    void ThrowStick()
    {
        if(stick != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {

                stick = null;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {

        }
        if (other.gameObject.tag == "tear")
        {

        }
    }
}
