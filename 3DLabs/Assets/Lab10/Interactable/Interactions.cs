using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Interactions : MonoBehaviour, IInteractable
{

    [Header("Which option is active")]
    [SerializeField] private int configuration;
    enum Config { Debug, Sound, Animate };

    [SerializeField] Config config;

    [Header("Option 1 debug.log")]
    [SerializeField] private string text;
    [Header("Option 2 play sound")]
    [SerializeField] private AudioClip clipToPlay;
    [SerializeField] private AudioSource audioSource;
    [Header("Option 3 start or stop an animation")]
    [SerializeField] Animator animator;

    public void Interact(PlayerInteractManager pim, PlayerController pc)
    {
        if (config== Config.Debug)
        {
            Debug.Log(text);
        }
        else if (configuration == 2)
        {
            audioSource.PlayOneShot(clipToPlay);
        } 
        else if (configuration == 3)
        {
            animator.SetTrigger("interacted");
        }
        else
        {
            Debug.LogError("Plaease set configuration to a valid option");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


