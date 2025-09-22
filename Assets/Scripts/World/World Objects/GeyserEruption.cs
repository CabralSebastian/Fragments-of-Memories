
using UnityEngine;

public class GeyserEruption : WorldObject
{
  private bool _playerIsInside = false;

  public override void Synthetize(Synthesizer synthesizer)
  {
    base.Synthetize(synthesizer);
    _collider.convex = true;
    _collider.isTrigger = true;

    if (_playerIsInside && IsAstralized)
      GameManager.Instance.Player.GeyserEruption();
  }

  public override void ResetMeshCollider()
  {
    base.ResetMeshCollider();
    _collider.convex = true;
    _collider.isTrigger = true;

    if (_playerIsInside && IsAstralized)
      GameManager.Instance.Player.GeyserEruption();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag("Player"))
      return;

    if (IsAstralized)
      GameManager.Instance.Player.GeyserEruption();
    else
      _playerIsInside = true;
  }
  
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
      _playerIsInside = false;
  }
}
