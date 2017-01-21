using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public Transform stickSpawn;
    public GameObject StickPrefab;

    public float health;
    public float healthGen;
    public float regenTimer;
    public float speed;
    public float throwingSpeed;
    public int tears;
    public bool regainHealth;
    public bool isDead;
    public bool hasWeapon;
    public bool isPinging;
	void Start ()
    {
        stickSpawn = transform.GetChild(0);

        health = 100;
        healthGen = 5;
        regenTimer = 1.0f;
        throwingSpeed = 3;
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
        HealthReGen();
        CheckIfPing();
        ThrowStick();
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
        if(hasWeapon == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                GameObject stickClone = Instantiate(StickPrefab, stickSpawn.transform.position, stickSpawn.transform.rotation) as GameObject;
                Rigidbody stickPhysics = stickClone.GetComponent<Rigidbody>(); // You should be able to hold to throw?

                Vector3 targetVelocity = (transform.forward + transform.up).normalized * throwingSpeed;
                stickPhysics.velocity = targetVelocity;
                hasWeapon = false;
            }
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            TakeDamage(other.GetComponent<EnemyScript>().attDamage);
        }
        if (other.gameObject.tag == "tear")
        {
            tears += 1;
        }
        if(hasWeapon == false)
        {
            if (other.gameObject.tag == "stick")
            {
                hasWeapon = true;
                
                transform.GetChild(1).gameObject.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
}
