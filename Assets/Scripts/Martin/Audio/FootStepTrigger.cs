using UnityEngine;

public class FootStepTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private AudioClip[] footStepSounds;

    public void Footstep()
    {
        int random = Random.Range(0, footStepSounds.Length);
        var clip = footStepSounds[random];
        audioSource.PlayOneShot(clip);
    }


}
