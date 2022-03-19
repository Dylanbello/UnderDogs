using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAtCheckpoint : MonoBehaviour
{
    public Vector3 spawnPoint = new Vector3(-15,0,8);//These values are the starting spawnpoint
    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController CC = player.GetComponent<CharacterController>();
        if (other.gameObject.tag == "checkpoint")
        {
            spawnPoint = other.transform.position;
        }

        if (other.gameObject.tag == "respawn")
        {
            CC.enabled = false;
            player.transform.position = spawnPoint;
            CC.enabled = true;
            
        }
    }
}
