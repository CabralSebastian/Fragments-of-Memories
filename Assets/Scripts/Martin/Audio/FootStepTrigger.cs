using UnityEngine;

public class FootStepTrigger : MonoBehaviour
{
    [Header("Components")]
    private Animator animator;
    private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private AudioClip[] footStepSounds;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Footstep()
    {
        int random = Random.Range(0, footStepSounds.Length);
        var clip = footStepSounds[random];
        audioSource.PlayOneShot(clip);
    }


}
