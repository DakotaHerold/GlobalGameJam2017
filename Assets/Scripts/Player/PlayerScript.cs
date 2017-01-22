using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public Transform stickSpawn;
    public GameObject StickPrefab;
    public GameObject stickHold;

    public float health;
    public float healthGen;
    public float regenTimer;
    public float speed;
    public float throwingSpeed;
    public float immunity;
    public float blinkTimer;
    public int blink;
    public int tears;
    public bool regainHealth;
    public bool isDead;
    public bool hasWeapon;
    public bool isPinging;
    public bool isHit;

    [HideInInspector]
    public CharacterController3D controller; 

    Animator anim;
    
    void Start ()
    {
        stickSpawn = transform.GetChild(0);

        regainHealth = false;
        isDead = false;
        hasWeapon = false;
        isPinging = false;
        isHit = false;
        controller = GetComponent<CharacterController3D>(); 

        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Death();
        HealthReGen();
        /*
        if (isHit)
        {
            Blink();
            if(blink == 3)
            {
                isHit = false;
                blink = 0;
                blinkTimer = 1.0f;
            }

        }*/
        if(immunity > 0.0f)
        {
            immunity -= 0.01f;
        }

	}
    public void TakeDamage(float damg)
    {
        if (immunity <= 0.0f)
        {
            health -= damg;
            immunity = 0.5f;
        }
        
    }
    IEnumerator healthRegen()
    {
        yield return new WaitForSeconds(regenTimer);
        regainHealth = false;
        if (hasWeapon == false && health < 6)
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

    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "enemy")
        {
            TakeDamage(other.GetComponent<EnemyScript>().attDamage);
        }*/
       
        if (other.gameObject.tag == "tear")
        {
            tears += 1;
        }
        if(hasWeapon == false)
        {
            if (other.gameObject.tag == "stick")
            {
                //Debug.Log("test");
                hasWeapon = true;
                anim.SetBool("HasWeapon", true);
                stickHold.transform.GetChild(0).GetComponent<WeaponScript>().player = this.gameObject; 

                stickHold.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
    void Blink()
    {
        if (blinkTimer <= 0.0f)
        {
            if (GetComponent<Renderer>().enabled == true)
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                blink += 1;
            }
            blinkTimer = 1.0f;
        }
        blinkTimer -= 0.1f;
    }
}
