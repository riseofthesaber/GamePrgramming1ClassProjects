using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip landSFX;
    [SerializeField] private AudioClip hurtSFX;
    [SerializeField] private AudioClip defeatSFX;
    [SerializeField] private AudioSource audioSource;


    private void PlaySFX(AudioClip clipToPlay)
    {
        // if already playing, dont play again
        // dosnt let you play 2 clips back to back
        //if (clipToPlay == audioSource.clip) return;

        audioSource.Stop();
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }

    public void playLanding()
    {
        PlaySFX(landSFX);
    }

    public void playDefeat()
    {
        PlaySFX(defeatSFX);
    }
    public void playHurt()
    {
        PlaySFX(hurtSFX);
    }


}
