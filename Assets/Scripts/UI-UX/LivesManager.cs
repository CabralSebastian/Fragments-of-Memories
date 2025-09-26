using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
	[SerializeField] private Image[] _lives;

	public void Update()
	{
		for (int i = 0; i < _lives.Length; i++)
			_lives[i].enabled = i < GameManager.Instance.Player.Health.Value;
	}
}
