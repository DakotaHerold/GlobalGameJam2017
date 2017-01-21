using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Attributes
    public GameObject cameraObject; 
    public GameObject spawnContainer;
    public GameObject[] enemies; 
    public bool shouldSpawn = true;
    public float spawnInterval;
    public int numEnemies;

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
        

        //foreach()
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
        int spawnPointIndex = Random.Range(0, enemySpawnPoints.Count);


        int enemyTypeIndex = Random.Range(0, enemies.Length);
        enemies[enemyTypeIndex].transform.position = enemySpawnPoints[spawnPointIndex].transform.position;


        if (enemies[enemyTypeIndex].GetComponent<MeleeAIController>() != null)
        {
            MeleeAIController meleeEnemy = enemies[enemyTypeIndex].GetComponent<MeleeAIController>();
            meleeEnemy.target = playersObjects[0].transform;
        }
        else if (enemies[enemyTypeIndex].GetComponent<ShooterAIController>() != null)
        {
            ShooterAIController shooterEnemey = enemies[enemyTypeIndex].GetComponent<ShooterAIController>();
            shooterEnemey.targetObject = playersObjects[0];
        }

        Instantiate(enemies[enemyTypeIndex]);
        
    }


   

}
