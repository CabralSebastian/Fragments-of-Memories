public class AstralizeMemory : Interactable
{
  // "🅕 Para Tocar"
  public override string Action => "\"F\" Para Tocar";

  public override void Interact()
  {
    GameManager.Instance.RememberAstralize = true;
    GameManager.Instance.Player.AstralSkills.RememberAstralize();

    GameManager.Instance.RememberIvyLabyrinth = true;

    gameObject.SetActive(false);
  }
}
