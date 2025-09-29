using UnityEngine;

public enum Element
{
  Astral,
  Fire,
  Frost,
}

[CreateAssetMenu(menuName = "Skills/SkillData")]
public class SkillData : ScriptableObject
{
  [SerializeField] private GameObject _proyectilePrefab;
  public GameObject ProjectilePrefab => _proyectilePrefab;

  [SerializeField] private string _name;
  public string Name => _name;

  [SerializeField] private string _description;
  public string Description => _description;

  [SerializeField] private Element _element = Element.Astral;
  public Element Element => _element;

  [SerializeField] private float _cooldownooldown;
  public float Cooldown => _cooldownooldown;
}