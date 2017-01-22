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
    public GameObject waveProjectilePrefab;
    bool isHit;

    Animator animator;

    private PlayerScript m_playerScript; 
    // Use this for initialization
    void Start () {
        isAttacking = false;
        isHit = false;
        comboEnd = false;
        specialActive = false; 

        //Debug.Log(swingButton);
        animator = GetComponent<Animator>();

        m_playerScript = GetComponent<PlayerScript>(); 
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Special()
    {
        //Debug.Log(playerNumber + " Special Logic!");
        GameManager.gmInstance.SetWavePlayer(m_playerScript);
        PlayerScript stickPlayer = GameManager.gmInstance.GetStickPlayer();

        if (stickPlayer != null)
        {
            // Create wave and launch forward 
            waveProjectilePrefab.transform.position = new Vector3(stickPlayer.transform.position.x, stickPlayer.transform.position.y + 2.0f, stickPlayer.transform.position.z);
            waveProjectilePrefab.transform.forward = transform.forward; 
            GameObject wave = Instantiate(waveProjectilePrefab);
        }
    }

}
