using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Attributes
    public GameObject spawnContainer;
    public bool spawnEnemies = true;
    public float spawnInterval = 3.0f; 

    private List<Transform> enemySpawnPoints = new List<Transform>(); 
    private GameObject[] players;
    private float spawnTimer = 0.0f; 
	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("player") as GameObject[];

        for(int i = 0; i < spawnContainer.transform.childCount; i++)
        {
            enemySpawnPoints.Add(spawnContainer.transform.GetChild(i).GetComponent<Transform>()); 
        }

        //foreach(Transform t in enemySpawnPoints)
        //{
        //    Debug.Log(t.position); 
        //}
        
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime; 

        if(spawnTimer > spawnInterval && spawnEnemies)
        {
            spawnTimer = 0.0f; 
        }
	}

    public void SpawnEnemy()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (spawnEnemies)
        {
            SpawnEnemy(); 
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
