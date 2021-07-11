using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    GameObject Player;
    GameControl gameControl;
    NavMeshAgent navMesh;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        gameControl = Camera.main.GetComponent<GameControl>();
        navMesh = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        navMesh.destination = Player.transform.position;
    }

   

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(collider.gameObject);
            gameControl.EnemyDead(gameObject);
           
        }

    }
}
