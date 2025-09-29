using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinTrigger : MonoBehaviour
{
  private void Start()
  {
    BoxCollider collider = GetComponent<BoxCollider>();
    collider.isTrigger = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
      GameManager.Instance.Win();
  }
}
