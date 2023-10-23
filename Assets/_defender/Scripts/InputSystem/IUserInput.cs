using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IUserInput
{
    event Action<InputValue> OnMoveReceived;
    event Action<InputValue> OnThrustReceived;
    event Action OnFlipPressed;
    event Action OnFirePressed;
    event Action OnStartPressed;
    event Action OnSubmitPressed;
    event Action OnSmartBombPressed;
    event Action onHyperspacePressed;
    Vector2 MoveInput { get; }
    bool UpPressed { get; }
    bool DownPressed { get; }
    bool IsThrusting { get; }
    
}