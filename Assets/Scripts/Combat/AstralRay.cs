using UnityEngine;

public class AstralRay : MonoBehaviour
{
  [SerializeField] private LineRenderer _lineRenderer;
  [SerializeField] private Transform _aimTarget;
  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private float _damagePerSecond = 1f;

  private bool _isFiring = false;

  private void Start()
  {
    _lineRenderer.enabled = false;
  }

  private void Update()
  {
    DetectFiring();
    HandleFiring();
  }

  private void DetectFiring()
  {
    // if (!GameManager.Instance.MainCamera.TryGetComponent(out AimCamera aimCamera))
    //  return;

    if (Input.GetMouseButtonDown(0)) // || Input.GetMouseButton(0)) && aimCamera.IsAiming)
    {
      _isFiring = true;
      _lineRenderer.enabled = true;
    }

    else if (Input.GetMouseButtonUp(0)) // || !aimCamera.IsAiming)
    {
      _isFiring = false;
      _lineRenderer.enabled = false;
    }
  }

  private void HandleFiring()
  {
    if (!_isFiring)
      return;

    _lineRenderer.SetPosition(0, _spawnPoint.position);
    _lineRenderer.SetPosition(1, _aimTarget.position);

    Vector3 direction = (_aimTarget.position - _spawnPoint.position).normalized;
    float distance = Vector3.Distance(_spawnPoint.position, _aimTarget.position);

    Ray ray = new(_spawnPoint.position, direction);
    if (!Physics.Raycast(ray, out RaycastHit hit, distance))
      return;

    // _lineRenderer.SetPosition(1, hit.point);

    if (!hit.collider.TryGetComponent<Health>(out var health))
      return;

    health.TakeDamage(_damagePerSecond * Time.deltaTime);
  }
}
