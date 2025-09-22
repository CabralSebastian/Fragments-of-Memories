using UnityEngine;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(Rigidbody))]
public class TreeLog : MonoBehaviour
{
  private Grabbable _grabable;
  private Rigidbody _rigidbody;

  private void Awake()
  {
    _grabable = GetComponent<Grabbable>();
    _grabable.enabled = false;

    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.isKinematic = true;
  }

  public void Activate()
  {
    _grabable.enabled = true;
    _rigidbody.isKinematic = false;
  }
  
  public void Push(Vector3 direction)
  {
    Vector3 offsetPosition = transform.position + Vector3.up;
    _rigidbody.AddForceAtPosition(direction * 30, offsetPosition, ForceMode.Impulse);
  }
}