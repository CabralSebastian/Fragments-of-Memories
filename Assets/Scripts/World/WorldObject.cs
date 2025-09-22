using Parabox.CSG;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Material))]
public class WorldObject : MonoBehaviour
{
  [SerializeField] private bool _hasPhysicalPresence = true;
  [SerializeField] private bool _hasAstralPresence = true;
  public bool IsSynthesized { get; set; } = false;
  public bool IsAstralized => IsSynthesized ^ (GameManager.Instance.IsAstralWorld == 1);

  protected MeshCollider _collider;
  private Mesh _mainMesh;
  private MeshRenderer _renderer;
  private MeshFilter _filter;
  private Material _material;

  private void Start()
  {
    _collider = GetComponent<MeshCollider>();
    _renderer = GetComponent<MeshRenderer>();
    _filter = GetComponent<MeshFilter>();
    _mainMesh = _collider.sharedMesh;
    _material = _renderer.material;

    if (!IsValidCSGTarget())
      Debug.LogError($"{gameObject.name} has no mesh assigned to MeshCollider!");

    ResetMeshCollider();
  }

  private void Update()
  {
    SetWorldState();
    HandlePeepEffect();
    HandleSynthetisisEffect();
  }

  private void SetWorldState()
  {
    _material.SetFloat("_IsAstralWorld", GameManager.Instance.IsAstralWorld);
  }

  private void HandlePeepEffect()
  {
    
    PeepSkill peep = GameManager.Instance.Peep;

    _material.SetFloat("_IsCylinderActive", peep.IsActive);
    _material.SetVector("_CylinderOrigin", peep.Center);
    _material.SetVector("_CylinderDirection", peep.Direction);
    _material.SetFloat("_CylinderRadius", peep.Radius);
  }

  public void HandleSynthetisisEffect()
  {
    Synthesizer synthesizer = GameManager.Instance.Synthesizer;

    _material.SetFloat("_IsSphereActive", 1);
    _material.SetFloat("_SphereRadius", synthesizer.Radius);
    _material.SetVector("_SphereOrigin", synthesizer.Center);
  }

  public virtual void Synthetize(Synthesizer synthesizer)
  {
    if (_hasPhysicalPresence && _hasAstralPresence)
      return;

    bool isAstralWorld = GameManager.Instance.IsAstralWorld == 1f;

    if (isAstralWorld && _hasAstralPresence || !isAstralWorld && _hasPhysicalPresence)
      CutCollider(synthesizer.gameObject);
    else
      Materialize(synthesizer.gameObject);
  }

  public void DeSynthetize()
  {
    ResetMeshCollider();
  }

  private bool IsAstralWorld => GameManager.Instance.IsAstralWorld == 1f;
  private bool HasPresense => (IsAstralWorld && !_hasAstralPresence) || (!IsAstralWorld && !_hasPhysicalPresence);

  public virtual void ResetMeshCollider()
  {
    _collider.sharedMesh = _mainMesh;
    _collider.convex = true;
    _collider.isTrigger = HasPresense;
  }

  private bool IsValidCSGTarget()
  {
    if (_filter == null || _filter.sharedMesh == null)
        return false;

    if (_renderer == null || !_renderer.enabled)
        return false;

    return _filter.sharedMesh.vertexCount > 2 && _filter.sharedMesh.triangles.Length > 0;
  }

  private void CutCollider(GameObject cutter)
  {
    Model model;

    try
    {
      model = CSG.Subtract(gameObject, cutter);
    }
    catch (System.Exception e)
    {
      Debug.LogWarning($"{name}: CSG.Subtract failed with exception:\n{e}");
      _collider.sharedMesh = _mainMesh;
      _collider.convex = true;
      _collider.isTrigger = true;

      return;
    }

    Mesh localMesh = TransformMeshFromWorldToLocal(model.mesh, transform);
    Mesh optimizedMesh = CleanMesh(localMesh);

    _collider.convex = false;
    _collider.isTrigger = false;
    _collider.sharedMesh = optimizedMesh;
  }

  private void Materialize(GameObject materializer)
  {
    Model model = CSG.Intersect(gameObject, materializer);

    Mesh localMesh = TransformMeshFromWorldToLocal(model.mesh, transform);
    Mesh optimizedMesh = CleanMesh(localMesh);

    _collider.sharedMesh = optimizedMesh;
    _collider.convex = false;
    _collider.isTrigger = false;
  }

  private Mesh CleanMesh(Mesh mesh)
  {
    Mesh cleaned = new()
    {
      indexFormat = mesh.indexFormat,
      vertices = mesh.vertices,
      triangles = mesh.triangles
    };

    cleaned.RecalculateNormals();
    cleaned.RecalculateBounds();

    return cleaned;
  }
  
  private Mesh TransformMeshFromWorldToLocal(Mesh mesh, Transform toTransform)
  {
    Vector3[] vertices = mesh.vertices;

    for (int i = 0; i < vertices.Length; i++)
      vertices[i] = toTransform.InverseTransformPoint(vertices[i]);

    Mesh transformedMesh = new()
    {
      indexFormat = mesh.indexFormat,
      vertices = vertices,
      triangles = mesh.triangles
    };

    transformedMesh.RecalculateNormals();
    transformedMesh.RecalculateBounds();

    return transformedMesh;
  }
}
