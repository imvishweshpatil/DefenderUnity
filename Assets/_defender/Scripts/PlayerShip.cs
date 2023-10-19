using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShip : MonoBehaviour
{
    public float Speed => _speed;
    public float Direction => _direction;
    
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private GameObject _engineThrust;
    [SerializeField] private AudioClip _fireSound, _explosionSound;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _speed = 5f, _verticalSpeed = 500f;
    [SerializeField] private Vector2 _legalAltitude = new Vector2(-4.65f, 3f);
    [SerializeField] private Gun _gun;
    
    private IUserInput _userInput;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private bool _isThrusting;
    private int _direction;
    private Vector2 _moveInput, _thrustInput;
    private int _sprite;
    

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _userInput = UserInput.Instance;
    }

    private void OnEnable()
    {
        _userInput.OnMoveReceived += HandleOnMove;
        _userInput.OnThrustReceived += HandleOnThrust;
        _userInput.OnFlipPressed += HandleOnFlipDirection;
        _userInput.OnFirePressed += HandleOnFire;
        _direction = 1;
        UpdateEngineThruster();
    }

    private void OnDisable()
    {
        _userInput.OnMoveReceived -= HandleOnMove;
        _userInput.OnThrustReceived -= HandleOnThrust;
        _userInput.OnFlipPressed -= HandleOnFlipDirection;
        _userInput.OnFirePressed -= HandleOnFire;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = Vector2.zero;
        if (_userInput.UpPressed && _transform.position.y < _legalAltitude.y)
        {
            velocity.y = _verticalSpeed * Time.fixedDeltaTime;
        }
        else if (_userInput.DownPressed && _transform.position.y > _legalAltitude.x)
        {
            velocity.y = _verticalSpeed * Time.fixedDeltaTime * -1f;
        }

        _rigidbody.velocity = velocity;
    }

    private void HandleOnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void HandleOnThrust(InputValue value)
    {
        _thrustInput = value.Get<Vector2>();
        UpdateEngineThruster();
    }

    private void HandleOnFlipDirection()
    {
        var rotation = _transform.localRotation;
        rotation.y = rotation.y == 0 ? 180 : 0;
        _transform.localRotation = rotation;
        _direction *= -1;
    }

    private void HandleOnFire()
    {
        _gun.FireGun();
    }
    
    private void UpdateEngineThruster()
    {
        _engineThrust.SetActive(_userInput.IsThrusting);
        _sprite = _userInput.IsThrusting ? 1 : 0;
        _renderer.sprite = _sprites[_sprite];
    }
}

