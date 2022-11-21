using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SFXplayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    public void PlaySFX(AudioClip clipToPlay)
    {
        // if already playing, dont play again
        // dosnt let you play 2 clips back to back
        //if (clipToPlay == audioSource.clip) return;

        audioSource.Stop();
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
