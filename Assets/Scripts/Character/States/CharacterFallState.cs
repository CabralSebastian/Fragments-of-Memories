public class CharacterFallState : BaseState
{
  private readonly CharacterController _controller;

	public CharacterFallState(CharacterController controller)
	{
		_controller = controller;
	}

	public override void OnEnter()
	{
		_controller.PlayAnimation("Jump_Idle");
		_controller.SetGravityMultiplier(2f);
	}

	public override void OnExit()
	{
		_controller.SetGravityMultiplier(1f);
	}

	public override void Update(float deltaTime)
	{
		HandleTransitions();
	}

	private void HandleTransitions()
	{
		if (_controller.IsGrounded)
			Transition<CharacterIdleState>();

		else if (_controller.IsRising)
			Transition<CharacterJumpState>();

		else if (_controller.AstralSkills.AstralizeKeyPressed && _controller.AstralSkills.IsAstralized)
			Transition<CharacterWakeUpState>();
	}
}
