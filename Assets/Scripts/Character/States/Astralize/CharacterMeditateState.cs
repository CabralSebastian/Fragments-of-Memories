
using UnityEngine;

public class CharacterMeditateState : BaseState
{
  private readonly CharacterController _controller;

  public CharacterMeditateState(CharacterController controller)
  {
    _controller = controller;
  }

  public override void OnEnter()
  {
    if (_controller.Staff && _controller.Staff.IsOnHand)
    {
      _controller.AstralSkills.Astralizing = true;
      Transition<CharacterStickStaffState>();
      return;
    }
    _controller.DisableMovement();

    _controller.PlayAnimation("Sit_Down");
  }

  public override void OnExit()
  {
    // Change Shader to Astral Shader
    _controller.EnableMovement();
  }
  
  public override void Update(float deltaTime)
  {
    HandleTransitions();
    float astralization = _controller.GetAnimationNormalizedTime();
    GameManager.Instance.IsAstralWorld = astralization;
    Material skyMat = RenderSettings.skybox;
    skyMat.SetFloat("_Astralization", astralization);
  }

  private void HandleTransitions()
  {
    if (_controller.IsAnimationFinished("Sit_Down"))
    {
      _controller.AstralSkills.Astralize();
      Transition<CharacterIdleState>();
    }
  }
}