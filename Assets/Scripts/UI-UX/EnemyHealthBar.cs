using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private Health _health;
	[SerializeField] private Slider _healthSlider;

	public void Update()
	{
		_healthSlider.value = _health.Value / _health.MaxValue;
		_healthSlider.transform.LookAt(GameManager.Instance.MainCamera.transform);
	}
}
