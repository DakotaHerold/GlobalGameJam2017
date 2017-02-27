using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField]
    GameObject Main;
    [SerializeField]
    GameObject Credit;
    [SerializeField]
    GameObject GameOver;
    bool mainbool = false;
    bool creditbool = false;
    bool overbool = false;
    float prev = 0;
    //int curr = 0;

	// Use this for initialization
	void Start () {

        if (mainbool)
        {
            Main.SetActive(true);
        }
        if (creditbool)
        {
            Credit.SetActive(true);
        }
        if (overbool)
        {
            GameOver.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        mainControl();
	}

    public void LoadMainMenu()
    {
        mainbool = true;
        creditbool = false;
        overbool = false;
        Application.LoadLevel(0);
    } 
    public void LoadGameOver()
    {
        mainbool = false;
        creditbool = false;
        overbool = true;
        Application.LoadLevel(0);
    }
    public void LoadCredits()
    {
        mainbool = false;
        creditbool = true;
        overbool = false;
        Application.LoadLevel(0);
    }

    private void mainControl()
    {
        if (Input.GetButton("Swing0"))
        {
            Application.LoadLevel(1);
        }
        if (Input.GetButton("Throw0"))
        {
            Debug.Log("b");
            //Application.LoadLevel(0);
            //Credit.SetActive(true);
            // Main.SetActive(false);
            LoadCredits();
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }
    private void creditControl()
    {
        if (Input.GetButton("Throw0"))
        {
            Main.SetActive(true);
            Credit.SetActive(false);
        }
    }

    private void gameOverControl()
    {
        if (Input.GetButton("Swing0"))
        {
            Main.SetActive(true);
        }
    }
}
