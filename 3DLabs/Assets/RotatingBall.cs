using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBall : MonoBehaviour, IInteractable
{
    [SerializeField] Animator animator;
    
    public void Interact(PlayerInteractManager pim, PlayerController pc)
    {
        animator.SetTrigger("interacted");
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
