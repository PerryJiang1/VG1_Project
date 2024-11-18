using UnityEngine;
using System.Collections.Generic;

public class LightEmitter : MonoBehaviour
{
    public float lightRange = 10f; // Range of light
    public LayerMask portalLayer;  
    public LayerMask mirrorLayer;
    public LayerMask groundAndPlayerLayer;
    public LayerMask actButtonLayer;
    private LineRenderer lineRenderer;

    private HashSet<Portal> activePortals = new HashSet<Portal>();
    private HashSet<ActButton> activeButtons = new HashSet<ActButton>();
    public int maxReflections = 20;
    private bool hasReflected = false;
    private Collider2D previousHitCollider = null;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

    }

    void Update()
    {
        EmitLight(transform.position, transform.right, 0);

    }

    public void EmitLight(Vector2 origin, Vector2 direction, int reflectionCount)
    {
        // Renew portal activation/deactivation
        CheckPortalsActivation(origin, direction);
        CheckButtonsActivation(origin, direction);
        
        if (reflectionCount == 0)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, origin);
        }

        hasReflected = false;

        if (reflectionCount >= maxReflections)
        {
            Debug.Log("Reached maximum reflection limit.");
            Debug.Log(maxReflections);
            return;
        }

        // Prevent raycast hit with the same mirror last time
        if (previousHitCollider != null)
        {
            previousHitCollider.enabled = false;
        }

        // Merge all layers needed for detection
        LayerMask combinedLayerMask = portalLayer | mirrorLayer | groundAndPlayerLayer | actButtonLayer;

        // Emit light and detect whether collide with any targets
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, lightRange, combinedLayerMask);


        if (previousHitCollider != null)
        {
            previousHitCollider.enabled = true;
        }



        if (hit.collider != null)
        {

            // Detecting portals
            if (hit.collider.CompareTag("Portal"))
            {
                Portal portal = hit.collider.GetComponent<Portal>();
                if (portal != null)
                {
                    // If portal is not activated, then activate
                    if (!activePortals.Contains(portal))
                    {
                        portal.ActivatePortal();
                        activePortals.Add(portal);  // Put portal into activePortals set
                        
                    }
                }
                //Debug.Log("hit point: " + hit.point);
                previousHitCollider = null;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                return;
            }


            // Detecting mirrors
            else if (hit.collider.CompareTag("Mirror") && !hasReflected)
            {
                //Debug.Log("Light hit mirror, reflecting light");
                Vector2 xAxisNormal = hit.transform.up;
                Vector2 normal = new Vector2(xAxisNormal.y, -xAxisNormal.x);
               
                Vector2 normalizedDirection = direction.normalized;
                Vector2 normalizedNormal = normal.normalized;
                Vector2 reflectedDirection = Vector2.Reflect(normalizedDirection, normalizedNormal);

                // Renew portal activation/deactivation
                CheckPortalsActivation(hit.point, reflectedDirection);
                CheckButtonsActivation(hit.point, reflectedDirection);
                //Debug.DrawRay(hit.point, normalizedNormal * lightRange, Color.blue, 1f);
                //Debug.Log("Normalized direction: " + normalizedDirection);
                //Debug.Log("Normalized normal: " + normalizedNormal);
                //Debug.Log("Hit point: " + hit.point);
                //Debug.Log("Reflected direction: " + reflectedDirection);

                // Add a reflection point
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                hasReflected = true;

                //Debug.Log("reflection Count: " + reflectionCount);
                //Debug.Log("origin: " + origin);
                //Debug.Log("hit point: " + hit.point);

                previousHitCollider = hit.collider;

                EmitLight(hit.point, reflectedDirection, reflectionCount + 1);
                return;
            }
            

            // Detecting ground/player, light stops there
            else if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Player"))
            {
                //Debug.Log("Raycast hit: " + hit.collider.name + "  at position: " + hit.point);
                // Renew portal activation/deactivation
                //CheckPortalsActivation();
                //Debug.Log("Light hit ground or player, stopping light.");
                previousHitCollider = null;
                // Add a collision point
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                return;
            }
            // Detecting buttons
            else if (hit.collider.CompareTag("ActButton"))
            {   

                ActButton button = hit.collider.GetComponent<ActButton>();
                if (button != null)
                {
                    
                    if (!activeButtons.Contains(button))
                    {   
                        print("Hello, Unity!");
                        button.Activate();
                        activeButtons.Add(button);  
                    }
                }
                previousHitCollider = null;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                return;
            }

            
            else
            {
                previousHitCollider = null;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
            }


        }
        else
        {
            // Renew portal activation/deactivation
            //CheckPortalsActivation();
            previousHitCollider = null;
            // If no collision, then draw a line with length of lightRange
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, origin + direction * lightRange);
        }
    }


    public void CheckPortalsActivation(Vector2 origin, Vector2 direction)
    {
        List<Portal> portalsToDeactivate = new List<Portal>();

        foreach (Portal portal in activePortals)
        {
            // Check whether portal gets collided with light
            Vector2 directionToPortal = (portal.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(origin, directionToPortal, lightRange, portalLayer | groundAndPlayerLayer);

            // If light doesn't detect portal or it hits ground/player, then deactivate it
            if (hit.collider == null || hit.collider.gameObject != portal.gameObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("divide");
                Debug.Log(portal.gameObject.name);
                portalsToDeactivate.Add(portal);
            }
        }

        // Deactivate portals in portalsToDeactivate set
        foreach (Portal portal in portalsToDeactivate)
        {
            portal.DeactivatePortal();
            activePortals.Remove(portal);
        }
    }
        public void CheckButtonsActivation(Vector2 origin, Vector2 direction)
    {
        List<ActButton> buttonsToDeactivate = new List<ActButton>();

        foreach (ActButton button in activeButtons)
        {
            Vector2 directionToButton = (button.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(origin, directionToButton, lightRange, actButtonLayer | groundAndPlayerLayer);

            
            if (hit.collider == null || hit.collider.gameObject != button.gameObject)
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("divide");
                Debug.Log(button.gameObject.name);
                buttonsToDeactivate.Add(button);  // 如果光线不再照射，标记为需要取消激活
            }
        }

        // 取消激活不再被光照射的按钮
        foreach (ActButton button in buttonsToDeactivate)
        {
            
            button.Deactivate();  // 调用按钮的取消激活方法
            activeButtons.Remove(button);  // 从激活的按钮集合中移除
        }
    }


}

