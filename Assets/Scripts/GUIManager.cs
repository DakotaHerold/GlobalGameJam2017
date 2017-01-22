using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GameObject[] totalPlayers;
    public Image[] brainsP1;
    public Image[] brainsP2;
    public Image[] brainsP3;
    public Image[] brainsP4;
    public Sprite[] brainParts;
    int startHealth;
    int brainSwitch;

	// Use this for initialization
	void Start () {
        totalPlayers = GameObject.FindGameObjectsWithTag("player") as GameObject[];
        startHealth = 3;
        brainSwitch = 2;
        for (int i = 0; i < startHealth; i++)
        {
            brainsP1[i].enabled = true;
            brainsP1[i].sprite = brainParts[brainSwitch];
        }
        for (int i = 0; i < startHealth; i++)
        {
            brainsP2[i].enabled = true;
            brainsP2[i].sprite = brainParts[brainSwitch];
        }
    }
	
	// Update is called once per frame
	void Update () {
        HealthManager();
	}
    void HealthManager()
    {
        int strafe = 0;
        brainSwitch = 0;
        for (int i = 0; i < brainsP1.Length; i++)
        {
            brainsP1[i].enabled = false;
        }
        for (int i = 0; i < brainsP2.Length; i++)
        {
            brainsP2[i].enabled = false;
        }
        for (int i = 0; i <= totalPlayers[0].GetComponent<PlayerScript>().health + 2; i++)
            {
                if (i == 6)
                {
                    strafe += 1;
                    brainSwitch = 0;
                }
                else if (i == 3)
                {
                    strafe += 1;
                    brainSwitch = 0;
                }
                brainsP1[strafe].enabled = true;
                brainsP1[strafe].sprite = brainParts[brainSwitch];
                brainSwitch += 1;
            }
            strafe = 0;
            brainSwitch = 0;
        for (int i = 0; i <= totalPlayers[1].GetComponent<PlayerScript>().health + 2; i++)
        {
            if (i == 6)
            {
                strafe += 1;
                brainSwitch = 0;
            }
            else if (i == 3)
            {
                strafe += 1;
                brainSwitch = 0;
            }
            brainsP2[strafe].enabled = true;
            brainsP2[strafe].sprite = brainParts[brainSwitch];
            brainSwitch += 1;
        }

    }
}
