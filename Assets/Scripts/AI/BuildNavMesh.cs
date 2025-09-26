using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

[RequireComponent(typeof(NavMeshSurface))]
public class BuildNavMesh : MonoBehaviour
{
	private NavMeshSurface _navMeshSurface;
	private readonly WaitForEndOfFrame _waitForEndOfFrame = new();
	[SerializeField] private GameObject[] _enemies;
	[SerializeField] private Transform[] _spawnPoints;
	
	private void Awake()
	{
		_navMeshSurface = GetComponent<NavMeshSurface>();
	}

	private void Start()
	{
		_navMeshSurface.BuildNavMesh();
		// StartCoroutine(SpawnEnemies());
	}

	private IEnumerator SpawnEnemies()
	{
		yield return _waitForEndOfFrame;

		foreach (Transform spawnPoint in _spawnPoints)
		{
			Instantiate(_enemies[Random.Range(0, _enemies.Length)], spawnPoint.position, spawnPoint.rotation);
			// enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
		}
		
	}
}
