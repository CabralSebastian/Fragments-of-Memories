using UnityEngine;

public class Passiflora : Interactable
{
  [SerializeField] private GameObject _vines;
  [SerializeField] private SoundPlayer _sound;
  public override string Action => "\"F\" Para Tocar";

  public override void Interact()
  {
    bool areVinesActive = _vines.activeSelf;

    _sound.PlaySound();

    _vines.SetActive(!areVinesActive);
  }
}
