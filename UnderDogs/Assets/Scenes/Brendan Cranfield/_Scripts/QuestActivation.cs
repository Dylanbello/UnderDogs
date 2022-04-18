using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class QuestActivation : MonoBehaviour
{
    public UnityEvent OnActivation;
    public List<GameObject> listToDestroy = new List<GameObject>();

    private void Update() 
    {
        for (int i = 0; i < listToDestroy.Count; i++)
        {
            if(listToDestroy[i] == null) { listToDestroy.RemoveAt(i); }
        }

        if(listToDestroy.Count == 0) { OnActivation.Invoke(); }
    }
}
