using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrapBehaviourScript : MonoBehaviour {

    void  Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gameObj = other.gameObject;

        PlayerScript player = gameObj.GetComponent<PlayerScript>();
        if (player != null)
        {
            player.isDead = true;
        }

       //Destroy(gameObj);
    }

}
