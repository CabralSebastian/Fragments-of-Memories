using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ExplosiveFruit : Grabbable
{
  [SerializeField] private Texture _inestableTexture;
  [SerializeField] private Explosion _explosionEffect;
  private readonly WaitForSeconds _waitForExplosion = new(3f);
  private ExplosiveFlower _flower;
  public bool IsSynthesized { get; set; } = false;
  public bool IsAstralWorld => GameManager.Instance.IsAstralWorld == 1f;

  private MeshRenderer _renderer;
  private bool _isStable = true;

  protected override void Start()
  {
    base.Start();
    _renderer = GetComponent<MeshRenderer>();
  }

  protected override void Update()
  {
    base.Update();

    if (_isGrabbed && (IsSynthesized ^ IsAstralWorld) && _isStable)
      StartCoroutine(StartExplosion());
  }

  public void SetExplosiveFlower(ExplosiveFlower flower)
  {
    _flower = flower;
  }

  private IEnumerator StartExplosion()
  {
    _isStable = false;
    _renderer.material.SetTexture("_PhysicTexture", _inestableTexture);
    _renderer.material.SetTexture("_AstralTexture", _inestableTexture);

    yield return _waitForExplosion;

    if (_isGrabbed)
      GameManager.Instance.Player.AstralSkills.Release();

    _explosionEffect.StartExplotion();
    _flower.SpawnExplosiveFruit();
    Destroy(gameObject);
  }
} 