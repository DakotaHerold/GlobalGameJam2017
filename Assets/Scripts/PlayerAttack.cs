using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public int combo;
    public float specialCD;
    public float attackTimer;
    public float attackDelay;
    public float attDamage;
    public bool isAttacking;
    public bool comboEnd;
    public bool specialActive;
    bool isHit;
    public int playerNumber;

    private string swingButton;
    private string specialButton;
    // Use this for initialization
    void Start () {
        /*
        combo = 0;
        specialCD = 0.0f;
        attackTimer = 0.0f;
        attackDelay = 0.5f;
        attDamage = 3;*/
        isAttacking = false;
        isHit = false;
        comboEnd = false;
        specialActive = false;
        playerNumber = 0;

        swingButton = "Swing" + playerNumber;
        specialButton = "Special" + playerNumber;

    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<PlayerScript>().hasWeapon)
        {
            Attack();
        }

        Special();
		
	}
    IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(1.0f);
        combo = 0;
        comboEnd = false;
    }
    void Attack()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown(swingButton)) && comboEnd == false)
        {
            //Debug.Log("attack!");
            combo += 1;
            attackTimer = attackDelay;
            isAttacking = true;

            if (combo >= 3)
            {
                comboEnd = true;
                StartCoroutine(ResetCombo());
            }
        }

        if(attackTimer <= 0.0f)
        {
            attackTimer = 0.0f;
            combo = 0;
            isAttacking = false;
        }
        else
        {
            attackTimer -= 0.01f;
        }
    }
    void Special()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown(specialButton)) && specialCD <= 0.0f)
        {
            specialActive = true;
            specialCD = 3.0f;
        }

        if (specialCD <= 0.0f)
        {
            specialCD = 0.0f;
            specialActive = false;
        }
        else
        {
            specialCD -= 0.01f;
        }

    }

}
