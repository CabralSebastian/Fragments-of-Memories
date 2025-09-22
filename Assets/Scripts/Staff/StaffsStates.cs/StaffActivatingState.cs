public class StaffActivatingState : BaseState
{
  private readonly StaffController _staff;

  public StaffActivatingState(StaffController staff)
  {
    _staff = staff;
  }

  public override void OnEnter()
  {
    _staff.Synthesizer.StartSynthesis();
  }

  public override void OnExit() { }

  public override void Update(float deltaTime)
  {
    if (_staff.Synthesizer.State == SynthesizerState.ACTIVE)
      Transition<StaffActiveState>();
  }
}
