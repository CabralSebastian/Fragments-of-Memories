using UnityEngine;

public class Skill : MonoBehaviour
{
  [SerializeField] private SkillData _skillData;
  [SerializeField] private KeyCode _castKey;
  [SerializeField] private Transform _aimTarget;
  [SerializeField] private Transform _spawnPoint;
  private float _cooldownTimer = 0;

  private void Start()
  {
    _cooldownTimer = _skillData.Cooldown;
  } 

  private void Update()
  {
    _cooldownTimer += Time.deltaTime;
    if (_cooldownTimer < _skillData.Cooldown)
      return;

    if (Input.GetKeyDown(_castKey))
    {
      CastSkill();
      _cooldownTimer = 0;
    }
  }

	private void CastSkill()
	{
		Vector3 direction = (_aimTarget.position - _spawnPoint.transform.position).normalized;
		GameObject projectile = Instantiate(_skillData.ProjectilePrefab, _spawnPoint.position, Quaternion.LookRotation(direction));
  }
}