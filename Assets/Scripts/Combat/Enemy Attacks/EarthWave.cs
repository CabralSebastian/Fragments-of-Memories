using System.Collections;
using System.Linq;
using UnityEngine;

public class EarthWave : MonoBehaviour
{
  [SerializeField] float _timeBetweenBlocksSeconds = 0.2f;
  [SerializeField] private EarthWaveBlock[] _blocks;
  public bool IsRunning => _blocks.Any(block => block.IsRunning);

  public void Attack()
  {
    if (IsRunning)
      return;
    
    StartCoroutine(Attacking());
  } 

  private IEnumerator Attacking()
  {
    foreach (EarthWaveBlock block in _blocks)
    {
      block.gameObject.SetActive(true);
      block.Raise();
      yield return new WaitForSeconds(_timeBetweenBlocksSeconds);
    }

    yield return new WaitForSeconds(0.2f);
  }
}
