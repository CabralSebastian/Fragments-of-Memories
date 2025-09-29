//using System.Numerics;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;

    [SerializeField] private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private Rigidbody _player;

    private Vector3 _difference;

    //private Rigidbody _playerRb;

    void Start()
    {
        TargetNextWaypoint();
    }

    

    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        //_playerRb.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        if (_player != null)
        {
            Vector3 previousPlayerPosition = new Vector3(_previousWaypoint.position.x, _player.position.y, _previousWaypoint.position.z);
            Vector3 TargetPlayerPosition = new Vector3(_targetWaypoint.position.x, _player.position.y, _targetWaypoint.position.z);

            _player.position = Vector3.Lerp(previousPlayerPosition, TargetPlayerPosition, elapsedPercentage);
        }

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }
}
