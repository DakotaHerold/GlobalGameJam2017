using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Attributes

    public static GameManager gmInstance = null;

    public GameObject cameraObject; 
    public GameObject spawnContainer;
    public GameObject[] enemyObjectPrefabs; 
    public bool shouldSpawn = true;
    public float spawnInterval;
    public int numEnemies;

    public List<GameObject> enemies = new List<GameObject>(); 
    private List<Transform> enemySpawnPoints = new List<Transform>();
    private List<Transform> usedSpawnPoints = new List<Transform>(); 
    private List<Transform> spawnedEnemies = new List<Transform>(); 
    private GameObject[] playersObjects;
    private List<PlayerScript> players = new List<PlayerScript>(); 
    private PlayerScript stickPlayer;
    private PlayerScript wavePlayer; 
    private float spawnTimer = 0.0f;
    [HideInInspector]
    public CameraControl cameraController;
    //[HideInInspector]
    public CameraRumble cameraShaker; 

	// Use this for initialization
	void Start () {
        gmInstance = this; 
        cameraController = cameraObject.GetComponent<CameraControl>();
        cameraShaker = cameraObject.transform.GetChild(0).GetComponent<CameraRumble>(); 
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
            else
            {
                wavePlayer = players[i]; 
            }

            // Check if players died, if so remove them from the camera manager and destroy them 
            if (players[i].isDead)
            {
                cameraController.cameraTargets.Remove(players[i].gameObject.transform);
                Debug.Log("Cam count: " + cameraController.cameraTargets.Count);
                GameObject objToDestroy = players[i].gameObject;
                players.RemoveAt(i);
            }
        }
        

        //Debug.Log("Camera targets: " + cameraController.cameraTargets.Count);
        
        // Check if all enemies have been killed 
        //for (int i = enemies.Count - 1; i >= 0; i--)
        //{
        //    if(enemies[i].isDead)
        //    {
        //        GameObject objToDestroy = enemies[i].gameObject;
        //        //enemies.RemoveAt(i); 
        //    }
        //}

        if(enemies.Count > 0)
        {
            shouldSpawn = false; 
        }
        else
        {
            shouldSpawn = true; 
        }

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
        // Reset spawn points if all have been used 
        if(enemySpawnPoints.Count < 1)
        {
            foreach(Transform point in usedSpawnPoints)
            {
                enemySpawnPoints.Add(point); 
            }
            usedSpawnPoints.Clear(); 
        }


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

        // Spawn 
        enemies.Add(Instantiate(enemyObjectPrefabs[enemyTypeIndex]));

        // Clean up 
        usedSpawnPoints.Add(enemySpawnPoints[spawnPointIndex]);
        enemySpawnPoints.RemoveAt(spawnPointIndex); 
        
    }

    public void SetStickPlayer(PlayerScript player)
    {
        stickPlayer = player; 
    }

    public PlayerScript GetStickPlayer()
    {
        return stickPlayer;
    }

    public void SetWavePlayer(PlayerScript player)
    {
        wavePlayer = player;
    }

    public PlayerScript GetWavePlayer()
    {
        return wavePlayer;
    }

    //public void ChangeSpawnPoints()
    //{
    //    enemySpawnPoints.Clear();
    //    usedSpawnPoints.Clear(); 
    //}



}
