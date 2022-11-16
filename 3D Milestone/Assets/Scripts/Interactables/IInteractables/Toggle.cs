using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Toggle : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> Stuff;
    public void Interact(PlayerInteractManager pim, PlayerControl pc)
    {
        Debug.Log("Toggle plz");
        foreach(GameObject things in Stuff)
        {
            if (things.activeSelf)
            {
                things.SetActive(false);
            }
            else
            {
                things.SetActive(true);
            }
        }
    }

}
