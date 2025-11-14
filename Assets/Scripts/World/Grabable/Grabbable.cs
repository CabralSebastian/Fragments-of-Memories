using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, IGrabbable
{
  private Rigidbody _rigidbody;
  protected bool _isGrabbed = false;

  public bool IsGrabbed => _isGrabbed;

  [SerializeField] private float _dragSpeed = 10f;
  [SerializeField] private float _holdDistance = 10f;

  protected virtual void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  protected virtual void Update()
  {
    if (!_isGrabbed)
      return;
    Camera mainCamera = GameManager.Instance.MainCamera;
    Vector3 targetPos = mainCamera.transform.position + mainCamera.transform.forward * _holdDistance;
    Vector3 direction = targetPos - transform.position;

    _rigidbody.linearVelocity = direction * _dragSpeed;
  }

  public virtual void Grab()
  {
    _isGrabbed = true;
    _rigidbody.useGravity = false;
    _rigidbody.linearVelocity = Vector3.zero;
  }

  public virtual void Release()
  {
    _isGrabbed = false;
    _rigidbody.linearVelocity = Vector3.zero;
    _rigidbody.useGravity = true;
  }
}
