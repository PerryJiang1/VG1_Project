using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActButton : MonoBehaviour
{
    public FloatingBlock targetBlock;  

    
    [SerializeField] private Vector3 moveDirection = Vector3.up;  
    [SerializeField] private float moveDistance = 2f;  

    
    public void Activate()
    {
        if (targetBlock != null)
        {
            print("111");
            targetBlock.Move(moveDirection, moveDistance);
        }
    }

    
    public void Deactivate()
    {
        if (targetBlock != null)
        {
            targetBlock.Deactivate();  
        }
    }
}
