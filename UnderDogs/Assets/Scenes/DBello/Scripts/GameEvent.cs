using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    HashSet<GameEventListener> _Listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach (var globalEventListener in _Listeners)
            globalEventListener.RaiseEvent();
    }

    public void Register(GameEventListener gameEventListener) => _Listeners.Add(gameEventListener);

    public void Deregister(GameEventListener gameEventListener) => _Listeners.Remove(gameEventListener);
}
