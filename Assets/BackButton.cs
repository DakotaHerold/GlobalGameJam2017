using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BackButton : MonoBehaviour {

    Button button;
    public GameObject mainMenu;
    public GameObject creditsMenu; 
	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { Toggle(); });	
	}

    void Toggle()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
	
	
}
