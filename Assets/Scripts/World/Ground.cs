using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Material))]
public class Ground : MonoBehaviour
{
  private Material _material;

  private void Start()
  {
    MeshRenderer _renderer = GetComponent<MeshRenderer>();
    _material = _renderer.material;
  }

  private void Update()
  {
    SetWorldState();
    HandlePeepEffect();
    HandleSynthetisisEffect();
  }

  private void SetWorldState()
  {
    float isAstralWorld = GameManager.Instance.IsAstralWorld;
    
    _material.SetFloat("_IsAstralWorld", isAstralWorld);
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
}
