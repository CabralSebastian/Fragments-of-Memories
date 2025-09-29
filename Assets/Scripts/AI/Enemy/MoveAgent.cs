using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
	private NavMeshAgent _agent;
	private float _speed;

	public bool ReachedTarget => _agent.remainingDistance < 0.1f && !_agent.pathPending;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_speed = _agent.speed;
	}
	public void MoveTo(Vector3 target)
	{
		_agent.SetDestination(target);
	}

	public void MoveToRandomPoint(float wanderRadius)
	{
		if (GetRandomPointOnNavMesh(transform.position, wanderRadius, out Vector3 randomPoint))
			_agent.SetDestination(randomPoint);
	}

	public void MultiplySpeed(float multiplier)
	{
		_agent.speed = _speed * multiplier;
	}

	public void IsStopped(bool stop)
	{
		_agent.isStopped = stop;
	}

	private bool GetRandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPos = center + Random.insideUnitSphere * radius;
			randomPos.y = center.y;

			if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}

		result = Vector3.zero;
		return false;
	}
}
