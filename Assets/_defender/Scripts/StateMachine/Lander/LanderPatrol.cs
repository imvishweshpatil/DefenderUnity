using UnityEngine;

public class LanderPatrol : IState
{
    private readonly Transform _transform;
    private readonly Vector3 _patrolMin;
    private readonly Vector3 _patrolMax;
    private readonly float _patrolDistance;
    private readonly float _patrolSpeed;
    private readonly float _fireDelayMin;
    private readonly float _fireDelayMax;
    private readonly GameObject _projectilePrefab;
    private GameObject _patrolDestination;

    public LanderPatrol(
        Transform transform, 
        Vector3 patrolMin, 
        Vector3 patrolMax, 
        float patrolDistance,
        float patrolSpeed,
        float fireDelayMin,
        float fireDelayMax,
        GameObject projectilePrefab)
    {
        _transform = transform;
        _patrolMin = patrolMin;
        _patrolMax = patrolMax;
        _patrolDistance = patrolDistance;
        _patrolSpeed = patrolSpeed;
        _fireDelayMin = fireDelayMin;
        _fireDelayMax = fireDelayMax;
        _projectilePrefab = projectilePrefab;
    }
    public void Tick()
    {
        var position = _transform.position;
        if (position.FlatDistance(_patrolDestination.transform.position) <= 0.2f)
        {
            Debug.Log($"{_transform.name} reached destination. Getting a new one");
            GetNewPatrolDestination();
            return;
        }
        _transform.MoveTowardsTarget(_patrolDestination.transform, _patrolSpeed);
    }

    public void OnEntry()
    {
        GetNewPatrolDestination();
    }

    public void OnExist()
    {
        if (_patrolDestination)
        {
            Object.Destroy(_patrolDestination); 
        }
    }

    private void GetNewPatrolDestination()
    {
        if (!_patrolDestination)
        {
            _patrolDestination = new GameObject();
            _patrolDestination.AddComponent<MobScroller>();
        }
        Vector3 position = _transform.position;
        position.x = Mathf.Clamp(position.x + Random.Range(-_patrolDistance, _patrolDistance), _patrolMin.x, _patrolMax.x);
        position.y = Mathf.Clamp(position.y + Random.Range(-_patrolDistance, _patrolDistance), _patrolMin.y, _patrolMax.y);
        _patrolDestination.transform.position = position;
        Debug.Log($"Patrol Destination now {_patrolDestination.transform.position}");
    }
}