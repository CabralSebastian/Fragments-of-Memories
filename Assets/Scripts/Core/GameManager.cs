using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  [HideInInspector] public Camera MainCamera;
  [HideInInspector] public CharacterController Player;
  [HideInInspector] public StaffController Staff;
  [HideInInspector] public float IsAstralWorld = 0;
  public PeepSkill Peep => Player.AstralSkills.Peep;
  public Synthesizer Synthesizer => Player.Staff.Synthesizer;
  [SerializeField] private Transform _spawnPoint;
  [SerializeField] private GameObject _worldRoot;
  private List<WorldObject> _worldObjects;

  private void Awake()
  {
    transform.SetParent(null);
    if (Instance && Instance != this)
    {
      GameManager.Instance.UpdateGameManager(_worldRoot, _spawnPoint);
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  public void UpdateGameManager(GameObject worldRoot, Transform spawnPoint)
  {
    _worldObjects = new List<WorldObject>(worldRoot.GetComponentsInChildren<WorldObject>());
    _spawnPoint = spawnPoint;
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

  public void WaterReset()
  {
    Player.TeleportTo(_spawnPoint.position);
  }

  public void DeathReset()
  {
    Player.Health.FillToMax();
    Player.TeleportTo(_spawnPoint.position);
  }
}
