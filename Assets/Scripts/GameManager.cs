﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Attributes
    public GameObject cameraObject; 
    public GameObject spawnContainer;
    public GameObject[] enemyObjectPrefabs; 
    public bool shouldSpawn = true;
    public float spawnInterval;
    public int numEnemies;

    private List<EnemyScript> enemies = new List<EnemyScript>();
    private List<Transform> enemySpawnPoints = new List<Transform>();
    private List<Transform> spawnedEnemies = new List<Transform>(); 
    private GameObject[] playersObjects;
    private List<PlayerScript> players = new List<PlayerScript>(); 
    private PlayerScript stickPlayer; 
    private float spawnTimer = 0.0f;
    private CameraControl cameraController; 

	// Use this for initialization
	void Start () {

        cameraController = cameraObject.GetComponent<CameraControl>(); 

        playersObjects = GameObject.FindGameObjectsWithTag("player") as GameObject[];

        foreach(GameObject obj in playersObjects)
        {
            PlayerScript player = obj.GetComponent<PlayerScript>(); 
            if(player != null)
            {
                players.Add(player); 
            }
        }
        //Debug.Log("Player count : " + players.Count);


        for (int i = 0; i < spawnContainer.transform.childCount; i++)
        {
            enemySpawnPoints.Add(spawnContainer.transform.GetChild(i).GetComponent<Transform>());
        }
        

        
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;

        // Are any players left
        if(players.Count < 1)
        {
            // TO-DO Game over logic here 
            return; 
        }
        
        // Iterate over players 
        for(int i = players.Count - 1; i >= 0; i--)
        {
            if(players[i].hasWeapon)
            {
                stickPlayer = players[i];
            }

            // Check if players died, if so remove them from the camera manager and destroy them 
            if (players[i].isDead)
            {
                cameraController.cameraTargets.Remove(players[i].gameObject.transform);
                Debug.Log("Cam count: " + cameraController.cameraTargets.Count);
                Destroy(players[i].gameObject); 
                players.RemoveAt(i);
            }
        }
        

        //Debug.Log("Camera targets: " + cameraController.cameraTargets.Count);

        // Enemies 
        if (spawnTimer > spawnInterval && shouldSpawn)
        {
            // instantiate enemies here 
            for(int i = 0; i < numEnemies; i++)
            {
                SpawnRandomEnemy(); 
            }
            Debug.Log("Spawning");
            spawnTimer = 0.0f; 
        }
	}



    public void SpawnRandomEnemy()
    {
        // Get random point out of spawn points 
        int spawnPointIndex = Random.Range(0, enemySpawnPoints.Count);

        // Get random enemy from enemy types 
        int enemyTypeIndex = Random.Range(0, enemyObjectPrefabs.Length);
        // set enemy type position to spawn point 
        enemyObjectPrefabs[enemyTypeIndex].transform.position = enemySpawnPoints[spawnPointIndex].transform.position;

        EnemyScript enemyAI = enemyObjectPrefabs[enemyTypeIndex].GetComponent<EnemyScript>(); 
        if(enemyAI == null)
        {
            Debug.Log("enemy prefab doesn't contain ai. cannot spawn");
            return; 
        }
        else if (enemyAI is MeleeAIController)
        {
            // Melee targets always go to stick player else closest player 
            if(stickPlayer != null)
            {
                enemyAI.target = stickPlayer.transform; 
            }
            else
            {
                enemyAI.SetClosestPlayerToTarget(); 
            }
        }
        else if (enemyAI is ShooterAIController)
        {
            enemyAI.SetClosestPlayerToTarget(); 
        }

        
        Instantiate(enemyObjectPrefabs[enemyTypeIndex]);
        
    }


   

}
