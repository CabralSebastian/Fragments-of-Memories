using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
  [Header("Gravity Settings")]
  public float GravityMultiplier = 1f;

  [Header("Movement Settings")]
  [SerializeField] private float _speed = 5f;
  [SerializeField] private KeyCode _forwardKey = KeyCode.W;
  [SerializeField] private KeyCode _backwardKey = KeyCode.S;
  [SerializeField] private KeyCode _leftKey = KeyCode.A;
  [SerializeField] private KeyCode _rightKey = KeyCode.D;

  [Header("Jump Settings")]
  [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
  [SerializeField] private float _jumpHeight = 2f;

  [Header("Ground Settings")]
  [SerializeField] private Transform _feetTransform;
  [SerializeField] private LayerMask _groundLayerMask;
  [SerializeField] private float _groundDistanceCheck = 0.01f;

  private Rigidbody _rigidbody;
  private Vector3 _direction;

  public bool IsGrounded => Physics.CheckSphere(_feetTransform.position, _groundDistanceCheck, _groundLayerMask);
  public bool IsMoving => _direction.sqrMagnitude > 0.01f;
  public bool Jumped => Input.GetKeyDown(_jumpKey) && IsGrounded;
  private float HorizontalAxis => (Input.GetKey(_leftKey) ? -1f : 0f) + (Input.GetKey(_rightKey) ? 1f : 0f);
  private float VerticalAxis => (Input.GetKey(_forwardKey) ? 1f : 0f) + (Input.GetKey(_backwardKey) ? -1f : 0f);

  public bool IsRising => _rigidbody.linearVelocity.y > 0f;
  public bool IsFalling => _rigidbody.linearVelocity.y < 0f;

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    if (!DialogueManager.Instance.inputEnabled)
    {
        return;
    }
        _direction = transform.forward * VerticalAxis + transform.right * HorizontalAxis;
    _direction.Normalize();

    if (Jumped)
      Jump();
  }

  private void FixedUpdate()
  {
    if (IsMoving)
      MovePositon();

    Vector3 gravity = Physics.gravity * GravityMultiplier;
    _rigidbody.AddForce(gravity, ForceMode.Acceleration);
  }

  private void MovePositon()
  {
    Vector3 move = _speed * Time.fixedDeltaTime * _direction;
    Vector3 targetPosition = _rigidbody.position + new Vector3(move.x, 0f, move.z);

    _rigidbody.MovePosition(targetPosition);
  }

  private void Jump()
  {
    float gravity = Physics.gravity.y * GravityMultiplier;
    float jumpVelocity = Mathf.Sqrt(-2f * gravity * _jumpHeight);

    _rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
  }

  public void Stop()
  {
    // _speed = 0f;
  }

  public void JumpToHeight(float targetHeight)
  {
    float gravity = Physics.gravity.y; // * GravityMultiplier;
      Vector3 velocity = _rigidbody.linearVelocity;
      _rigidbody.linearVelocity = new Vector3(velocity.x, 0f, velocity.z);

      float requiredJumpVelocity = Mathf.Sqrt(-2f * gravity * targetHeight);
      _rigidbody.AddForce(Vector3.up * requiredJumpVelocity, ForceMode.VelocityChange);
  }

}
