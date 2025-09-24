using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public Camera MainCamera;
  [HideInInspector] public CharacterController Player;
  [HideInInspector] public float IsAstralWorld = 0;
  [HideInInspector] public PeepSkill Peep;
  [HideInInspector] public Synthesizer Synthesizer;
  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private GameObject _worldRoot;
  private List<WorldObject> _worldObjects;

  private void Awake()
  {
    transform.SetParent(null);
    if (Instance && Instance != this)
    {
      GameManager.Instance.UpdateWorldObjects(_worldRoot);
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  public void UpdateWorldObjects(GameObject worldRoot)
  {
    _worldObjects = new List<WorldObject>(worldRoot.GetComponentsInChildren<WorldObject>());
  }

  private void Start()
  {
    _worldObjects = new List<WorldObject>(_worldRoot.GetComponentsInChildren<WorldObject>());
  }

  public void Astralize()
  {
    IsAstralWorld = 1;
    foreach (WorldObject worldObject in _worldObjects)
      worldObject.ResetMeshCollider();

    if (Synthesizer && Synthesizer.State == SynthesizerState.ACTIVE)
      Synthesizer.SynthesizeOvelaps();
  }

  public void UnAstralize()
  {
    IsAstralWorld = 0;
    foreach (WorldObject worldObject in _worldObjects)
      worldObject.ResetMeshCollider();

    if (Synthesizer && Synthesizer.State == SynthesizerState.ACTIVE)
      Synthesizer.SynthesizeOvelaps();
  }

  public void LockCursor()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  public void UnlockCursor()
  {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }

  public void Reset()
  {
    Player.TeleportTo(_spawnPoint.position);
  }
}
