using System.Collections;
using UnityEngine;

public class GrayBearAttack : MonoBehaviour
{
  private readonly static WaitForSeconds WaitForSeconds0_15 = new(0.15f);
  [SerializeField] private Hitbox _hitbox;

  public void Attack()
  {
    _hitbox.gameObject.SetActive(true);
    StartCoroutine(DisableHitbox());
  }

  private IEnumerator DisableHitbox()
  {
    yield return WaitForSeconds0_15;

    _hitbox.gameObject.SetActive(false);
  }
}
