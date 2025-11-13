using UnityEngine;

public class BrownBearAttack : MonoBehaviour
{
  [SerializeField] private EarthWave _earthwave;

  public void Attack()
  {
    _earthwave.Attack();
  }
}

