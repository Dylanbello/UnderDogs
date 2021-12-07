using System;
using UnityEngine;

//Active - In Use
public interface IInput
{
    Action<Vector3> MovementDirectionInput { get; set; }
    Action<Vector2> MovementInput { get; set; }
}