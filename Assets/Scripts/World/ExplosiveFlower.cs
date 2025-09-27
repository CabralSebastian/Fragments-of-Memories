using UnityEngine;

public class ExplosiveFlower : MonoBehaviour
{
  [SerializeField] private GameObject _explosiveFruitPrefab;
  [SerializeField] private Transform _fruitSpawnPosition;

  private void Start()
  {
    SpawnExplosiveFruit();
  }

  public void SpawnExplosiveFruit()
  {
    GameObject fruitObject = Instantiate(_explosiveFruitPrefab, _fruitSpawnPosition.position, _fruitSpawnPosition.rotation);
    ExplosiveFruit fruit = fruitObject.GetComponent<ExplosiveFruit>();
    fruit.SetExplosiveFlower(this);
  }
} 