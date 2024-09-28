using UnityEngine;

public class LightEmitterScript : MonoBehaviour
{
    public float lightDistance = 10f; 
    public LayerMask reflectiveLayer; 
    public LayerMask portalLayer; 
    private LineRenderer lineRenderer;

    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.1f; 
        lineRenderer.endWidth = 0.1f;
        
        
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        
        
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
        EmitLight(); 
    }

    void EmitLight()
    {
        Vector2 direction = transform.right; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, lightDistance);

        
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position; 

        if (hit.collider != null)
        {
            positions[1] = hit.point; 
        }
        else
        {
            positions[1] = transform.position + (Vector3)direction * lightDistance; 
        }

        
        lineRenderer.SetPositions(positions);
    }
}
