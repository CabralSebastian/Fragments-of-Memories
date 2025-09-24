using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshFilter))]
public class Synthesizer : MonoBehaviour
{
  private SphereCollider _collider;
  [SerializeField] private LayerMask _layerMask;
  [SerializeField] private float _expansionDuration = 1f;
  [SerializeField] private float _contractionDuration = 0.5f;

  public SynthesizerState State { get; private set; } = SynthesizerState.OFF;

  private readonly List<WorldObject> _overlaps = new();
  private readonly List<ExplosiveFruit> _overlapsExplosiveFruits = new();

  private float ColliderRadius => _collider.radius * Mathf.Max(
    _collider.transform.lossyScale.x,
    _collider.transform.lossyScale.y,
    _collider.transform.lossyScale.z);

  private float _radius = 0;
  public float Radius => _radius;
  public Vector3 Center => _collider.transform.position;

  private void Awake()
  {
    _collider = GetComponent<SphereCollider>();
    _collider.isTrigger = true;
    GameManager.Instance.Synthesizer = this;
  }

  private IEnumerator StartSynthesisCoroutine()
  {
    State = SynthesizerState.ACTIVATING;

    float timer = 0;
    while (timer < _expansionDuration)
    {
      yield return null;
      _radius = Mathf.Lerp(0, ColliderRadius, timer / _expansionDuration);
      timer += Time.deltaTime;
    }

    State = SynthesizerState.ACTIVE;

    SynthesizeOvelaps();
  }

  private IEnumerator EndSynthesisCoroutine()
  {
    State = SynthesizerState.DEACTIVATING;

    float timer = 0;

    while (timer < _contractionDuration)
    {
      yield return null;
      _radius = Mathf.Lerp(ColliderRadius, 0, timer / _contractionDuration);
      timer += Time.deltaTime;
    }

    DeSynthetizeOverlaps();

    State = SynthesizerState.OFF;
  }

  public void SynthesizeOvelaps()
  {
    foreach (WorldObject worldObject in _overlaps)
    {
      Debug.Log("Synthesizing " + worldObject.name);
      worldObject.Synthetize(this);
      worldObject.IsSynthesized = true;
    }

    foreach (ExplosiveFruit explosiveFruit in _overlapsExplosiveFruits)
    {
      explosiveFruit.IsSynthesized = true;
    }
  }

  public void DeSynthetizeOverlaps()
  {
    foreach (WorldObject worldObject in _overlaps)
    {
      worldObject.DeSynthetize();
      worldObject.IsSynthesized = false;
    }
  }

  public void StartSynthesis()
  {
    Debug.Log("Starting Synthesis...");
    // CalculateOverlapedWorldObjects();
    StartCoroutine(StartSynthesisCoroutine());
  }

  public void EndSynthesis()
  {
    Debug.Log("Ending Synthesis...");
    StartCoroutine(EndSynthesisCoroutine());
  }

  /*
    private void CalculateOverlapedWorldObjects()
    {
      Vector3 center = _collider.transform.TransformPoint(_collider.center);
      int hitCount = Physics.OverlapSphereNonAlloc(center, ColliderRadius, _hits, _layerMask);

      _overlaps.Clear();
      for (int i = 0; i < hitCount; i++)
        if (_hits[i].gameObject.TryGetComponent(out WorldObject worldObject))
          _overlaps.Add(worldObject);
    }
  */
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.TryGetComponent(out WorldObject worldObject) && !_overlaps.Contains(worldObject))
      _overlaps.Add(worldObject);

    if (other.gameObject.TryGetComponent(out ExplosiveFruit explosiveFruit))
      if (State == SynthesizerState.ACTIVE)
        explosiveFruit.IsSynthesized = true;
      else
        _overlapsExplosiveFruits.Add(explosiveFruit);
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.TryGetComponent(out WorldObject worldObject))
      _overlaps.Remove(worldObject);

    if (other.gameObject.TryGetComponent(out ExplosiveFruit explosiveFruit))
    {
      _overlapsExplosiveFruits.Remove(explosiveFruit);
      explosiveFruit.IsSynthesized = false;
    }
  }
}
