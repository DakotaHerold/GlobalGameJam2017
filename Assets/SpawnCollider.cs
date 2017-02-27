using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        GameManager.gmInstance.StartSpawning(); 
    }
}
