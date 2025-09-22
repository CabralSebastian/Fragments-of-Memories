using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInteraction))]
[RequireComponent(typeof(CharacterLook))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
{
	private FSM _fms;
	private CharacterMovement _movement;
	private CharacterInteraction _interaction;
	private Rigidbody _rigidbody;
	private Animator _animator;

	public StaffController Staff;
	public AstralSkills AstralSkills;

	private bool _isPause = false;

	/* Awake & Update */
	public void Awake()
	{
		_movement = GetComponent<CharacterMovement>();
		_interaction = GetComponent<CharacterInteraction>();
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();

		CharacterStateFactory stateFactory = new(this);
		IState initialState = stateFactory.Create<CharacterIdleState>();

		_fms = new FSM(stateFactory, initialState);

		GameManager.Instance.Player = this;
	}

	public void Update()
	{
		if (_isPause)
			return;

		_fms.Update(Time.deltaTime);
	}

	/* Pause */
	public void SetPause(bool pause)
	{
		_isPause = pause;
		bool enabled = !_isPause;

		_movement.enabled = enabled;
		_interaction.enabled = enabled;
		// _animator.enabled = enabled;
		Staff.enabled = enabled;
		AstralSkills.enabled = enabled;
	}

	/* Movement */
	public bool IsMoving => _movement.IsMoving;
	public bool Jumped => _movement.Jumped;
	public bool IsFalling => _rigidbody.linearVelocity.y < -0.1f;
	public bool IsRising => _rigidbody.linearVelocity.y > 0.1f;
	public bool IsGrounded => _movement.IsGrounded;

	public void SetGravityMultiplier(float multiplier) => _movement.GravityMultiplier = multiplier;

	public void EnableMovement() => _movement.enabled = true;
	public void DisableMovement()
	{
		_movement.enabled = false;
		_movement.Stop();
	}

	public void TeleportTo(Vector3 position)
	{
		_rigidbody.isKinematic = true;
		_rigidbody.position = position;
		_rigidbody.isKinematic = false;
	}

	public void GeyserEruption()
	{
		float desiredWorldHeight = 7.2f;
		float currentHeight = transform.position.y;
		float heightDifference = desiredWorldHeight - currentHeight;

		_movement.JumpToHeight(heightDifference);
	}
	
	/* Interaction */
	public bool Interacted => _interaction.Interacted && _interaction.ThereIsAnInteractable();
	public void Interact() => _interaction.Interact();

	/* Animation */
	private AnimatorStateInfo AnimationStateInfo => _animator.GetCurrentAnimatorStateInfo(0);
	public void PlayAnimation(string animationName) => _animator.Play(animationName, 0, 0f);
	public void SetAnimationSpeed(float speed) => _animator.speed = speed;
	public bool IsAnimationFinished(string animationName) => !(AnimationStateInfo.normalizedTime < 1f || !AnimationStateInfo.IsName(animationName));
}
