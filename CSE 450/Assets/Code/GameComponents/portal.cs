using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    [SerializeField] private Transform destination;
    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivatePortal();
    }

    public void ActivatePortal()
    {
        spriteRenderer.color = Color.green;
        isActivated = true;
    }

    public void DeactivatePortal()
    {
        Debug.Log("123123");
        isActivated = false;
        spriteRenderer.color = Color.black;
    }


    // When object collides with portals
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated)
        {
            Debug.Log("Portal is not activated yet.");
            return;
        }

        // Prevent repeatedly being transported by the portal
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

    // If object exits the portal, then remove it from portalObjects
    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObjects.Remove(collision.gameObject);
    }

}