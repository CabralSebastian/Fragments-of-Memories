using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour
{
  private BoxCollider _collider;

  private void Awake()
  {
    _collider = GetComponent<BoxCollider>();
    _collider.isTrigger = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
      GameManager.Instance.Reset();
  }
} 