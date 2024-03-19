using System;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypointIndex = 0;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        if (_waypoints == null)
        {
            enabled = false;
            throw new ArgumentNullException(nameof(_waypoints));
        }
    }

    private void Update()
    {
        Vector3 direction = GetDirection();

        transform.position = Vector3.MoveTowards(transform.position, direction, _speed * Time.deltaTime);
        transform.LookAt(direction);
    }

    private Vector3 GetDirection()
    {
        int step = 1;

        if (transform.position == _waypoints[_currentWaypointIndex].position)
            _currentWaypointIndex = (_currentWaypointIndex + step) % _waypoints.Length;

        return _waypoints[_currentWaypointIndex].position;
    }
}
