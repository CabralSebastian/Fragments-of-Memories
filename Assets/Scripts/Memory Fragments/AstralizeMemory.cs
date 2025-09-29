public class AstralizeMemory : Interactable
{
  public override void Interact()
  {
    GameManager.Instance.RememberAstralize = true;
    GameManager.Instance.Player.AstralSkills.RememberAstralize();

    GameManager.Instance.RememberIvyLabyrinth = true;

    gameObject.SetActive(false);
  }
}
