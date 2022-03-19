using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private Transform player;
    private int checkpointCount = 1;

    #if UNITY_EDITOR
    public GameObject Waypoints;

    private void OnValidate(){
        respawnPoints = Waypoints.GetComponentsInChildren<Transform>();
    }
    #endif

    private void OnTriggerEnter(Collider other)
    {
        CharacterController CC = player.GetComponent<CharacterController>();
        if (other.gameObject.tag == "checkpoint")
        {
            BoxCollider BC = other.gameObject.GetComponent<BoxCollider>();
            BC.enabled = false;
            checkpointCount++;
        }
        if (other.gameObject.tag == "respawn")
        {
            CC.enabled = false;
            player.transform.position = respawnPoints[checkpointCount].transform.position;
            CC.enabled = true;
            
        }
    }
}
