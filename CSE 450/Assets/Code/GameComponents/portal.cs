using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>(); 
    [SerializeField] private Transform destination; 
    private bool isActivated = false; 

    
    public void TriggerPortal(Collider2D collision)
    {
        
        if (!isActivated)
        {
            Debug.Log("Portal is not activated yet.");
            return;
        }

        
        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }

        
        if (destination.TryGetComponent(out Portal destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        
        collision.transform.position = destination.position;
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObjects.Remove(collision.gameObject);
    }

    
    public void ActivatePortal()
    {
        isActivated = true; 
        Debug.Log("Portal activated by light!");
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        TriggerPortal(collision);
    }
}
