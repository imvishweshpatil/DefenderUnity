using UnityEngine;


public class CameraPositionManager : MonoBehaviour
{
    [SerializeField] private Vector3 _leftPosition, _rightPosition;
    [SerializeField] private float _lerpSpeed = 10f;
    private Transform _transform;
    private GameManager _gameManager;
    private PlayerShip PlayerShip => _gameManager.PlayerShip;
    
    private void Awake()
    {
        _transform = transform;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void LateUpdate()
    {
        if (PlayerShip == null) return;
        var desiredPosition = PlayerShip.Direction > 0 ? _leftPosition : _rightPosition;
        if (Vector3.Distance(desiredPosition, _transform.position) > float.Epsilon)
        {
            _transform.position = Vector3.Lerp(_transform.position, desiredPosition, _lerpSpeed * Time.deltaTime);
        }
    }
}

