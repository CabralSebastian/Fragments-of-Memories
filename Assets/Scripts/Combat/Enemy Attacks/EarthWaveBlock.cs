using System.Collections;
using UnityEngine;

public class EarthWaveBlock : MonoBehaviour
{
	[SerializeField] private float _raiseAmount = 2.5f;
	[SerializeField] private float _timeToPeakSeconds = 1f;
	[SerializeField] private float _pauseAtPeakDuration = 0.1f;
	[SerializeField] private float _timeToDescentSeconds = 1f;

	public bool IsRunning { get; private set; } = false;
	private float _initialHeight;

	private float HeightToReach => _initialHeight + _raiseAmount;

	private void Awake()
	{
		gameObject.SetActive(false);
	}

	public void Raise()
	{
		if (IsRunning)
				return;

		IsRunning = true;
		_initialHeight = transform.position.y;
		StartCoroutine(Raising());
	}

	private IEnumerator Raising()
	{
		float elapsedTime = 0f;

		while (elapsedTime < _timeToPeakSeconds)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / _timeToPeakSeconds);
			float newY = Mathf.Lerp(_initialHeight, HeightToReach, t);
			SetHeight(newY);
			yield return null;
		}

		SetHeight(HeightToReach);
		yield return new WaitForSeconds(_pauseAtPeakDuration);
		yield return Descending();
	}

	private IEnumerator Descending()
	{
			float elapsedTime = 0f;

		while (elapsedTime < _timeToDescentSeconds)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / _timeToDescentSeconds);
			float newY = Mathf.Lerp(HeightToReach, _initialHeight, t);
			SetHeight(newY);
			yield return null;
		}

		SetHeight(_initialHeight);
		IsRunning = false;
		gameObject.SetActive(false);
	}

	private void SetHeight(float height)
	{
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
	}
}
