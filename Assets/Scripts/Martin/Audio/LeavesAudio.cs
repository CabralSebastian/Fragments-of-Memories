using System.Collections;
using UnityEngine;

public class LeavesAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private TreeLeaves _leaves;

    private void Awake()
    {
        _leaves = GetComponent<TreeLeaves>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_leaves.IsGrabbed)
        {
            _audioSource.enabled = true;
        }
        else
        {
            _audioSource.enabled = false;
        }
    }

    /*private IEnumerator WiggleSound()
    {
        _audioSource.Play();

        return WaitUntil
 
    }
    */
}
