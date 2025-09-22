using Parabox.CSG;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ResultCollider : MonoBehaviour
{
  private MeshCollider _colldier;
  private MeshFilter _filter;
  private MeshRenderer _renderer;

  [SerializeField] private GameObject _leftGO;
  [SerializeField] private GameObject _rightGO;

  private void Start()
  {
    _filter = GetComponent<MeshFilter>();
    _colldier = GetComponent<MeshCollider>();
    _renderer = GetComponent<MeshRenderer>();

    Model model = CSG.Subtract(_leftGO, _rightGO);

    Mesh optimizedMesh = CleanMesh(model.mesh);
    _filter.sharedMesh = optimizedMesh;
    _colldier.sharedMesh = optimizedMesh;

    Material defaultMat = new(Shader.Find("Universal Render Pipeline/Lit")) { color = Color.gray };
    _renderer.material = defaultMat;
  }

  private Mesh CleanMesh(Mesh mesh)
  {
    Debug.Log("Before Optimization");
    Debug.Log("Vertices: " + mesh.vertexCount);
    Debug.Log("Triangles: " + (mesh.triangles.Length / 3));

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
}

