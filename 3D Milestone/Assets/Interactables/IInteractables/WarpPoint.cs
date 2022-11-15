using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractManager pim, PlayerControl pc)
    {
        Debug.Log("Warp plz");
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
