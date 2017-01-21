using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Attributes
    public GameObject spawnContainer;
    public GameObject[] enemies; 
    public bool shouldSpawn = true;
    public float spawnInterval;
    public int numEnemies;

    private List<Transform> enemySpawnPoints = new List<Transform>();
    private List<Transform> spawnedEnemies = new List<Transform>(); 
    private GameObject[] players;
    private PlayerScript stickPlayer; 
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

        if(spawnTimer > spawnInterval && shouldSpawn)
        {
            // instantiate enemies here 
            SpawnEnemies();
            Debug.Log("Spawning");
            spawnTimer = 0.0f; 
        }
	}



    public void SpawnEnemies()
    {
        int spawnPointIndex = Random.Range(0, enemySpawnPoints.Count); 

        for(int i = 0; i < numEnemies; i++)
        {
            int enemyTypeIndex = Random.Range(0, enemies.Length);
            enemies[enemyTypeIndex].transform.position = enemySpawnPoints[spawnPointIndex].transform.position;


            if(enemies[enemyTypeIndex].GetComponent<MeleeAIController>() != null)
            {
                MeleeAIController meleeEnemy = enemies[enemyTypeIndex].GetComponent<MeleeAIController>();
                meleeEnemy.target = players[0].transform; 
            } else if (enemies[enemyTypeIndex].GetComponent<ShooterAIController>() != null)
            {
                ShooterAIController shooterEnemey = enemies[enemyTypeIndex].GetComponent<ShooterAIController>();
                shooterEnemey.targetObject = players[0];
            }

            Instantiate(enemies[enemyTypeIndex]); 
        }
    }


   

}
