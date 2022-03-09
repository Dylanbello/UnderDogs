using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HE_GameManage : MonoBehaviour
{
    public static HE_GameManage instance;

    public List<PlayerInput> Players;

    private void Awake(){
        if(instance == null){
        instance = this;
        }
        else if(instance != this){
        Destroy(this);
        }
    }


}
