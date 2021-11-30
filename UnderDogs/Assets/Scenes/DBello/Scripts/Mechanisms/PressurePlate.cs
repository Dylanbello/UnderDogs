using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] UnityEvent plateActivation;
    [SerializeField] UnityEvent plateDeactivation;
    //[SerializeField] GameEvent _onActivate;
    //[SerializeField] GameEvent _onDeactivate;
    //public GameObject mechanism;

    //public bool _activated;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (_activated == false)
    //        Activate();
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (_activated == true)
    //        Deactivate();
    //}


    //void Activate()
    //{
    //    _onActivate?.Invoke();
    //    Debug.Log("Activated");
    //    _activated = true;
    //}

    //void Deactivate()
    //{
    //    _onDeactivate?.Invoke();
    //    Debug.Log("Deactivated");
    //    _activated = false;
    //}

    private void OnTriggerEnter(Collider other)
    {
        //Check which character is on the pressure plate.
        //or check objects weight, etc.
        

            if (other.CompareTag("character1")) { plateActivation.Invoke(); }
        else return;
    }

    private void OnTriggerExit(Collider other)
    {
        //Check which character is on the pressure plate.
        //or check objects weight, etc.
       

            if (other.CompareTag("character1")) { plateDeactivation.Invoke(); }
        else return;
    }
}
