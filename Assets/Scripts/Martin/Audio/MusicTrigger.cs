using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    public AudioMixerSnapshot onEnterSnapshot;
    public AudioMixerSnapshot onExitSnapshot;
    public float transitionTime = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onEnterSnapshot.TransitionTo(transitionTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            onExitSnapshot.TransitionTo(transitionTime);
    }
}

