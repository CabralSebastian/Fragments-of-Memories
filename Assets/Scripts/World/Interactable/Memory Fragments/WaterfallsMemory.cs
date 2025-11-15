public class WaterfallsMemory : Memory
{
  public override void OnInteract()
  {
    GameManager.Instance.RememberWaterfalls = true;
  }
}
