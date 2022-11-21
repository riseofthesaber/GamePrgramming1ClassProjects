using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractManager : MonoBehaviour
{


    List<GameObject> interactableObjects = new List<GameObject>();

    // Tooltip "event called when we go from 0 to at least one interactable object in range"
    public UnityEvent OnInteractablesExist;
    // Tooltip "event called when we go from at least one interactable object in range to 0"
    public UnityEvent OnInteractablesDoNotExist;

    [SerializeField] private PlayerController playerController;


    private void TrackObject(GameObject objectToTrack)
    {
        interactableObjects.Add(objectToTrack);


        if (interactableObjects.Count == 1)
        {
            OnInteractablesExist.Invoke();
        }
    }

    public void UnTrackObject(GameObject trackedObject)
    {
        if (interactableObjects.Contains(trackedObject))
        {
            interactableObjects.Remove(trackedObject);

            if (interactableObjects.Count == 1)
            {
                OnInteractablesDoNotExist.Invoke();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            TrackObject(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // if interactable leaves
        if (other.CompareTag("Interactable"))
        {
            // untrack it
            UnTrackObject(other.gameObject);
        }
    }
    public void Interact()
    {
        // only interact if there is something to interact with
        //clean up the list in case there are nulls
        while (interactableObjects.Count > 0 && interactableObjects[0] == null)
        {
            UnTrackObject(interactableObjects[0]);
        }

        if (interactableObjects.Count > 0)
        {

            // interact with only the first one
            interactableObjects[0].GetComponent<IInteractable>().Interact(this, playerController);
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
