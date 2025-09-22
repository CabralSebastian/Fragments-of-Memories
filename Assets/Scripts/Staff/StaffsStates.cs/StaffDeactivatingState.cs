public class StaffDeactivatingState : BaseState
{
  private readonly StaffController _staff;

  public StaffDeactivatingState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter()
  {
    _staff.Synthesizer.EndSynthesis();
  }

  public override void OnExit() { }

  public override void Update(float deltaTime)
  {
    if (_staff.Synthesizer.State == SynthesizerState.OFF)
      Transition<StaffStuckState>();
  }
}
