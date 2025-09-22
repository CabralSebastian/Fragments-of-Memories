using UnityEngine;

public class AstralizeSkill : MonoBehaviour
{
	[SerializeField] private GameObject _meditatingWitch;
	[SerializeField] private CharacterController _character;
	private bool _isAstralized = false;
	public bool IsAstralized => _isAstralized;
	
	private void Start()
	{
		_meditatingWitch.SetActive(false);
	}

	public void Astralize()
	{
		if (_isAstralized)
			return;

		_meditatingWitch.SetActive(true);
		_meditatingWitch.transform.SetParent(null);

		// Set Astral Shader
		GameManager.Instance.Astralize();
		_isAstralized = true;
	}

	public void UnAstralize()
	{
		if (!_isAstralized)
			return;

		_character.TeleportTo(_meditatingWitch.transform.position);
		_meditatingWitch.SetActive(false);
		_meditatingWitch.transform.SetParent(transform);
		_meditatingWitch.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

		// Set Default Shader
		GameManager.Instance.UnAstralize();
		_isAstralized = false;
	}
}
