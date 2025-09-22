using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
  [SerializeField] private float _speed = 20f;
  private Vector3 _targetPosition;
  private const float _arrivalThreshold = 0.1f;

  private void Update()
  {
    if (Vector3.Distance(transform.position, _targetPosition) < _arrivalThreshold)
      Destroy(gameObject);
  }

  void FixedUpdate()
  {
    transform.position = Vector3.MoveTowards(
      transform.position,
      _targetPosition,
      _speed * Time.fixedDeltaTime
    );
  }

  public void SetTarget(Vector3 targetPosition)
  {
    _targetPosition = targetPosition;
  }
}