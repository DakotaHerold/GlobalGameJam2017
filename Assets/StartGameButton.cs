using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { StartGame();  }); 
	}
	
	void StartGame()
    {
        SceneManager.LoadScene("MovementTestScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(0);
    }
}
