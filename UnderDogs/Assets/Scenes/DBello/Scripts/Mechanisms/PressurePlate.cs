using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameEvent _onActivate;
    [SerializeField] GameEvent _onDeactivate;
    //public GameObject mechanism;

    public bool _activated;

    private void OnTriggerEnter(Collider other)
    {
        if (_activated == false)
            Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_activated == true)
            Deactivate();
    }


    void Activate()
    {
        _onActivate?.Invoke();
        Debug.Log("Activated");
        _activated = true;
    }

    void Deactivate()
    {
        _onDeactivate?.Invoke();
        Debug.Log("Deactivated");
        _activated = false;
    }
}
