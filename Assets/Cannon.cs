using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    private bool _rotates;
    private int _rotationDirection;
    private const float RotationSpeed = 10;
    private const float MaxAngle = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        _rotationDirection = 1;
        _rotates = true;
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

        if (Input.GetButtonDown("2_player_toggleTurning"))
        {
            _rotates = !_rotates;
        }
        
        if (Input.GetButtonDown("2_player_fire"))
        {
            var ball = Instantiate(cannonBall);
            var rigidBody = ball.GetComponent<Rigidbody>();
            var muzzle = transform.Find("Muzzle");
            ball.transform.position = muzzle.position;
            rigidBody.AddForce(transform.forward * 10, ForceMode.Impulse);
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
