using UnityEngine;

public class TreeLogAudio : MonoBehaviour
{
    private Speedometer _speedometer;
    private Grabbable _tree;
    private AudioSource _audioSource;

    [SerializeField] private float _thresholdSpeed = 5;

    private void Awake()
    {
        _speedometer = GetComponent<Speedometer>();
        _tree = GetComponent<Grabbable>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _speedometer.CalculateSpeed(_tree.IsGrabbed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_speedometer.Speed > _thresholdSpeed)
        {
            _audioSource.pitch = Random.Range(0f, 2f);

            _audioSource.Play();
        }
    }
}
