using System.Collections;
using UnityEngine;

public class ExplosiveFruit : Grabbable
{
  [SerializeField] private GameObject _explosionEffect;
  private readonly WaitForSeconds _waitForExplosion = new(0.5f);
  public bool IsSynthesized { get; set; } = false;
  public bool IsAstralWorld => GameManager.Instance.IsAstralWorld == 1f;

  private bool _isStable = true;

  protected override void Update()
  {
    base.Update();

    if (_isGrabbed && (IsSynthesized ^ IsAstralWorld) && _isStable)
      StartCoroutine(StartExplosion());
  }

  private IEnumerator StartExplosion()
  {
    _isStable = false;
    yield return _waitForExplosion;

    if (_isGrabbed)
      GameManager.Instance.Player.AstralSkills.Release();
    
    gameObject.SetActive(false);
    _explosionEffect.SetActive(true);
  }
} 