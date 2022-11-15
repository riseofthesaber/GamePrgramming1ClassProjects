using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class WarpPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string destination;
    public void Interact(PlayerInteractManager pim, PlayerControl pc)
    {
        Debug.Log("Warp plz");
        GameManager.SwitchScene(destination);
    }


}
