using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
	[SerializeField] private Camera _brainCamera;
	[SerializeField] private CinemachineCamera _moveCamera;
	[SerializeField] private CinemachineCamera _aimCamera;
	[SerializeField] private GameObject _crosshair;
	[SerializeField] private LayerMask _seeThroughLayerMask;
	private bool _isAiming = false;
	public bool IsAiming => _isAiming;

	private readonly RaycastHit[] _hits = new RaycastHit[10];
	private readonly Stack<MeshRenderer> _meshRenderers = new();


	private void Start()
	{
		if (GameManager.Instance.MainCamera != null)
		{
			Destroy(gameObject);
			return;
		}

		GameManager.Instance.MainCamera = _brainCamera;
		transform.SetParent(GameManager.Instance.transform);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			ToggleAim();

		ResetSeeThrough();
		SeeThrough();
	}

	private void ToggleAim()
	{
		_isAiming = !_isAiming;

		_moveCamera.Priority = _isAiming ? 0 : 10;
		_aimCamera.Priority = _isAiming ? 10 : 0;
	}

	private void SeeThrough()
	{
		Vector3 origin = _brainCamera.transform.position;
		Vector3 target = GameManager.Instance.Player.transform.position + Vector3.up;
		Vector3 diference = target - origin;
		Vector3 direction = diference.normalized;
		float distance = diference.magnitude * 0.9f;

		Ray ray = new(origin, direction);

		int hitCount = Physics.RaycastNonAlloc(ray, _hits, distance, _seeThroughLayerMask);
		Debug.DrawRay(origin, direction * distance, Color.red);


		for (int i = 0; i < hitCount; i++)
			if (_hits[i].transform.gameObject.TryGetComponent(out MeshRenderer meshRenderer) && meshRenderer.enabled)
			{
				_meshRenderers.Push(meshRenderer);
				meshRenderer.enabled = false;
			}
	}

	public void ResetSeeThrough()
	{
		while (_meshRenderers.Count > 0)
			_meshRenderers.Pop().enabled = true;
	}
}
