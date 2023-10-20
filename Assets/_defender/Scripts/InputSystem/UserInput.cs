using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour, IUserInput
{
    public static IUserInput Instance;
    
    public event Action<InputValue> OnMoveReceived;
    public event Action<InputValue> OnThrustReceived;
    public event Action OnFlipPressed;
    public event Action OnFirePressed;
    public event Action OnStartPressed;
    public event Action OnSubmitPressed;
    
    public bool IsThrusting => _thrustInput.y > 0;
    public Vector2 MoveInput => _moveInput;
    public Vector2 ThrustInput => _thrustInput;
    public bool UpPressed => _moveInput.y > 0;
    public bool DownPressed => _moveInput.y < 0;
    

    private Vector2 _thrustInput;
    private Vector2 _moveInput;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    void OnMove(InputValue value)
    {
        Debug.Log($"OnMove({value})");
        _moveInput = value.Get<Vector2>();
        OnMoveReceived?.Invoke(value);
    }

    void OnThrust(InputValue value)
    {
        Debug.Log($"OnThrust{value}");
        _thrustInput = value.Get<Vector2>();
        OnThrustReceived?.Invoke(value);
    }

    void OnFlip() => OnFlipPressed?.Invoke();

    void OnFire() => OnFirePressed?.Invoke();

    void OnStart() => OnStartPressed?.Invoke();

    void OnSubmit() => OnSubmitPressed?.Invoke();
}