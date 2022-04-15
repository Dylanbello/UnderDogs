using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerObject : MonoBehaviour
{

    public Vector3 Player1Pos = new Vector3(0, 0, 0);
    public Vector3 Player2Pos = new Vector3(0, 0, 0);
    public Vector3 center = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player1Pos = GameObject.FindGameObjectWithTag("character1").transform.position;
        Player2Pos = GameObject.FindGameObjectWithTag("character2").transform.position;

        Vector3 center = ((Player1Pos + Player2Pos) * 0.5f);
        transform.position = center;
    }
}
