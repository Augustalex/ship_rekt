using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool Rotates { get; set; }
    private int RotationDirection { get; set; }
    private const float RotationSpeed = 100;
    private const float MaxAngle = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        RotationDirection = 1;
        Rotates = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotates)
        {
            var diff = RotationSpeed * RotationDirection * Time.deltaTime;
            SetCannonAngle(GetCannonAngle() + diff);
            if (GetCannonAngle() < -MaxAngle)
            {
                SetCannonAngle(-MaxAngle);
                RotationDirection = 1;
            }
            else if (GetCannonAngle() > MaxAngle)
            {
                SetCannonAngle(MaxAngle);
                RotationDirection = -1;
            } 
        }
    }

    private float GetCannonAngle()
    {
        var angle = gameObject.transform.rotation.eulerAngles.y;
        if (angle > 180)
        {
            return angle - 360;
        }
        else
        {
            return angle;
        }
    }
    
    private void SetCannonAngle(float angle)
    {
        var rotation = transform.rotation;
        var eulerAngles = rotation.eulerAngles;
        eulerAngles.y = angle;
        rotation.eulerAngles = eulerAngles;
        transform.rotation = rotation;
    }
}
