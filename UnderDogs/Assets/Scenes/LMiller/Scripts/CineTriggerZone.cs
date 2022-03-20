using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineTriggerZone : MonoBehaviour
{
    public GameObject cinematic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>())
        {
            cinematic.SetActive(true);
        }
    }
}
