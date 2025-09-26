using UnityEngine;

public class Hitbox : MonoBehaviour
{
  [SerializeField] private int _damage;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.TryGetComponent(out Health health))
      health.TakeDamage(_damage);
  }
}
