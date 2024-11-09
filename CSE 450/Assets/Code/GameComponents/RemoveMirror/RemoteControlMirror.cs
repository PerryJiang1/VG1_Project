using System.Collections;
using UnityEngine;

public class RemoteControlMirror : MonoBehaviour
{
    public bool activeClockwise = true;  
    public float activeAngle = 90f;      
    public bool inactiveClockwise = true;  
    public float inactiveAngle = 90f;      
    private bool isRotating = false;

    [SerializeField] private float rotationSpeed = 30f;  
    private Coroutine currentRotationCoroutine;

    void Start()
    {
        
    }
    public bool IsRotating(){
        return isRotating;
    }
    public void Activate()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);  
        }
        currentRotationCoroutine = StartCoroutine(RotateMirror(activeClockwise, activeAngle));
    }

    public void Deactivate()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);  
        }
        currentRotationCoroutine = StartCoroutine(RotateMirror(inactiveClockwise, inactiveAngle));
    }

    private IEnumerator RotateMirror(bool clockwise, float angleToRotate)
    {
        isRotating = true;
        float direction = clockwise ? -1f : 1f;  
        float currentRotation = transform.rotation.eulerAngles.z;  
        float targetRotation = currentRotation + (direction * angleToRotate);  

        while (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) > 0.01f)
        {
            currentRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, targetRotation);  
        isRotating = false;
    }
}
