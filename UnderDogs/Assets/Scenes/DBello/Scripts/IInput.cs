using System;
using UnityEngine;

public interface IInput
{
    Action<Vector3> MovementDirectionInput { get; set; }
    Action<Vector2> MovementInput { get; set; }
}