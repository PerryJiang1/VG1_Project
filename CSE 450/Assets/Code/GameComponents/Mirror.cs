using UnityEngine;

public class Mirror : MonoBehaviour
{
    private LightEmitter lightEmitter;
    // Speed of rotation
    public float rotationSpeed = 30f;
    public GameObject player;
    public float maxDistance = 5f;
    private Renderer mirrorRenderer;


    void Start()
    {
        lightEmitter = FindObjectOfType<LightEmitter>();
        mirrorRenderer = GetComponent<Renderer>();
        if (mirrorRenderer != null)
        {
            mirrorRenderer.material.color = Color.white;
        }
    }

    void Update()
    {
        // Calculate the distance between the player and the mirror
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Only rotate if the distance between the player and the mirror is less than maxDistance
        if (distance < maxDistance)
        {
            if (mirrorRenderer != null)
            {
                mirrorRenderer.material.color = Color.blue;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateMirror(-rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateMirror(rotationSpeed);
            }
        }
        else
        {
            if (mirrorRenderer != null)
            {
                mirrorRenderer.material.color = Color.white;
            }
        }

        //if (lightEmitter != null)
        //{
        //    lightEmitter.CheckPortalsActivation();
        //}
    }

    private void RotateMirror(float speed)
    {
         transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
