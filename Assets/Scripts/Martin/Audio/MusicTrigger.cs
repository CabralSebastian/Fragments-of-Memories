using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot onEnterSnapshot;
    [SerializeField] private AudioMixerSnapshot onExitSnapshot;
    [SerializeField] private float transitionTime = 1.5f;

    private void Update()
    {
        if (GameManager.Instance.IsCombat)
        {
            EnterCombat();
        }
        else if (!GameManager.Instance.IsCombat)
        {
            ExitCombat();
        }
    }

    private void EnterCombat()
    {
        onEnterSnapshot.TransitionTo(transitionTime);
    }

    private void ExitCombat()
    {
        onExitSnapshot.TransitionTo(transitionTime);
    }
}

