public class AstralizeMemory : Memory
{
  public override void OnInteract()
  {
    GameManager.Instance.RememberAstralize = true;
    GameManager.Instance.Player.AstralSkills.RememberAstralize();

    GameManager.Instance.RememberIvyLabyrinth = true;
  }
}
