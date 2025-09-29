public class CharacterJumpState : BaseState
{
	private readonly CharacterController _controller;
	
	public CharacterJumpState(CharacterController controller)
	{
		_controller = controller;
	}

	public override void OnEnter()
	{
    _controller.PlayAnimation("Jump_Start");
    _controller.SetAnimationSpeed(3f);
	}

	public override void OnExit()
	{ 
		_controller.SetAnimationSpeed(1f);
	}

	public override void Update(float deltaTime)
	{
		HandleTransitions();
		if (_controller.IsAnimationFinished("Jump_Start"))
		{
			_controller.PlayAnimation("Jump_Idle");
			_controller.SetAnimationSpeed(1f);
		}
	}

	private void HandleTransitions()
	{
		if (_controller.IsFalling)
			Transition<CharacterFallState>();

		else if (_controller.AstralSkills.AstralizeKeyPressed && _controller.AstralSkills.IsAstralized)
			Transition<CharacterWakeUpState>();
	}
}
