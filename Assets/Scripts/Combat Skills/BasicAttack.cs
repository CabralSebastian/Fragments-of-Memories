using UnityEngine;

public class BasicAttack : MonoBehaviour
{
  [SerializeField] private SkillData _skillData;
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

    if (Input.GetMouseButtonDown(0))
    {
      CastSkill();
      _cooldownTimer = 0;
    }
  }

  private void CastSkill()
  {
    GameObject projectileGo = Instantiate(_skillData.ProjectilePrefab, _spawnPoint.position, Quaternion.LookRotation(_aimTarget.forward));
    Projectile projectile = projectileGo.GetComponent<Projectile>();
    projectile.SetTarget(_aimTarget.position);
  }
}
