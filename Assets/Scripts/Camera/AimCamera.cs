using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
  private static readonly WaitForSeconds _waitForSeconds0_25 = new(0.25f);
  [SerializeField] private CinemachineCamera _moveCamera;
	[SerializeField] private CinemachineCamera _aimCamera;
	[SerializeField] private GameObject _crosshair;
	private bool _isAiming = false;

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			ToggleAim();
	}

	private void ToggleAim()
	{
		_isAiming = !_isAiming;

		_moveCamera.Priority = _isAiming ? 0 : 10;
		_aimCamera.Priority = _isAiming ? 10 : 0;

		// UpdateCrosshair();
	}

	private void UpdateCrosshair()
	{
		if (_isAiming)
			StartCoroutine(WaitTransitionAndActiveCrosshair());
		else
			_crosshair.SetActive(false);
	}

	private IEnumerator WaitTransitionAndActiveCrosshair()
	{
		yield return _waitForSeconds0_25;
		
		_crosshair.SetActive(true);
	}
}
