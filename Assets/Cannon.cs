using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public bool currentCannon;
    
    private bool _rotates;
    private int _rotationDirection;
    private const float RotationSpeed = 20;
    private const float MaxAngle = 45;
    private const float CannonBallSpeed = 90;
    private const float CannonBallEndurance = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rotationDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotates)
        {
            var diff = RotationSpeed * _rotationDirection * Time.deltaTime;
            SetCannonAngle(GetCannonAngle() + diff);
            if (GetCannonAngle() < -MaxAngle)
            {
                SetCannonAngle(-MaxAngle);
                _rotationDirection = 1;
            }
            else if (GetCannonAngle() > MaxAngle)
            {
                SetCannonAngle(MaxAngle);
                _rotationDirection = -1;
            }
        }
    }

    public void Fire()
    {
        var ball = Instantiate(cannonBall);
        var rigidBody = ball.GetComponent<Rigidbody>();
        var muzzle = transform.Find("Muzzle");
        ball.transform.position = muzzle.position;
        rigidBody.AddForce(muzzle.forward * CannonBallSpeed, ForceMode.Impulse);
    }

    public void ToggleTurning()
    {
        _rotates = !_rotates;
    }
    
    private float GetCannonAngle()
    {
        var angle = gameObject.transform.localRotation.eulerAngles.y;
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
        var rotation = transform.localRotation;
        var eulerAngles = rotation.eulerAngles;
        eulerAngles.y = angle;
        rotation.eulerAngles = eulerAngles;
        transform.localRotation = rotation;
    }
}