using UnityEngine;
using UnityEngine.Audio;

public class AstralizeTriggerAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private void Update()
    {
        if (GameManager.Instance.IsAstralWorld == 1f)
        {
            onAstralize();
        }
        else if(GameManager.Instance.IsAstralWorld == 0f)
        {
            onExitAstralize();
        }
    }

    private void onAstralize()
    {
        audioMixer.SetFloat("MasterFreq", 5000f);
    }

    private void onExitAstralize()
    {
        audioMixer.SetFloat("MasterFreq", 22000f);

    }
}
