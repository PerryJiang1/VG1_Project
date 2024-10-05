using UnityEngine;

public class Mirror : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 100f;
    public GameObject player;
    public float maxDistance = 3f;

    void Update()
    {
        // Calculate the distance between the player and the mirror
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Only rotate if the distance between the player and the mirror is less than maxDistance
        if (distance < maxDistance)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateMirror(-rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateMirror(rotationSpeed);
            }
        }
    }

    private void RotateMirror(float speed)
    {
         transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
