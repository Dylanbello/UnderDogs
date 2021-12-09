using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
    }

    private void RotateCharacter()
    {
        var rotationY = (transform.rotation.y);
        player.rotation = new Quaternion(player.rotation.x, player.rotation.y, rotationY, player.rotation.z);
    }
}
