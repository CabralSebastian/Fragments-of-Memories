using UnityEngine;

public class AstralSkills : MonoBehaviour
{
  [Header("Peep")]
  [SerializeField] private KeyCode _peepKey = KeyCode.Q;
  [SerializeField] private PeepSkill _peepSkill;
  public PeepSkill Peep => _peepSkill;
  private bool _isPeepActive = false;

  [Header("Grab")]
  [SerializeField] private KeyCode _grabKey = KeyCode.P;
  [SerializeField] private GrabSkill _grabSkill;
  public bool IsGrabActive => _grabSkill.IsGrabbing;

  [Header("Astralize")]
  [SerializeField] private KeyCode _astralizeKey = KeyCode.R;
  [SerializeField] private AstralizeSkill _astralizeSkill;
  public bool Astralizing = false;

  /* Peep */
  public bool PeepKeyPressed => Input.GetKeyDown(_peepKey);
  public void ActivatePeep() => _peepSkill.Activate();
  public void DeActivatePeep() => _peepSkill.DeActivate();

  /* Grab */
  public bool GrabKeyPressed => Input.GetKeyDown(_grabKey);
  public void Grab() => _grabSkill.Grab();
  public void Release() => _grabSkill.Release();

  /* Astralize */
  public void SetAstralizeEnabled(bool enabled) => _astralizeSkill.gameObject.SetActive(enabled);
  public bool AstralizeKeyPressed => Input.GetKeyDown(_astralizeKey);
  public bool IsAstralized => _astralizeSkill.IsAstralized;
  public void Astralize() => _astralizeSkill.Astralize();
  public void UnAstralize() => _astralizeSkill.UnAstralize();

  private void Update()
  {
    HandlePeepInput();
    HandleGrabInput();
  }

  private void HandlePeepInput()
  {
    if (!PeepKeyPressed)
      return;

    if (_isPeepActive)
    {
      DeActivatePeep();
      _isPeepActive = false;
    }
    else
    {
      ActivatePeep();
      _isPeepActive = true;
    }
  }
  
  private void HandleGrabInput()
  {
    if (!GrabKeyPressed)
      return;

    if (IsGrabActive)
      Release();
    else
      Grab();
  }
}
