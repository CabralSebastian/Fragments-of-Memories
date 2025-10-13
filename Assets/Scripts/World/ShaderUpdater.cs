using UnityEngine;

public class ShaderEffectUpdater : MonoBehaviour
{
    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        UpdatePeepEffect();
        UpdateSynthesisEffect();
        UpdateWorldState();
    }

    private void UpdateWorldState()
    {
        _material.SetFloat("_IsAstralWorld", GameManager.Instance.IsAstralWorld);
    }

    private void UpdatePeepEffect()
    {
        PeepSkill peep = GameManager.Instance.Peep;
        _material.SetFloat("_IsCylinderActive", peep.IsActive);
        _material.SetVector("_CylinderOrigin", peep.Center);
        _material.SetVector("_CylinderDirection", peep.Direction);
        _material.SetFloat("_CylinderRadius", peep.Radius);
    }

    private void UpdateSynthesisEffect()
    {
        Synthesizer s = GameManager.Instance.Synthesizer;
        _material.SetFloat("_IsSphereActive", 1);
        _material.SetFloat("_SphereRadius", s.Radius);
        _material.SetVector("_SphereOrigin", s.Center);
    }
}
