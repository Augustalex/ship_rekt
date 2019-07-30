using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Build;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public string PlayerId = "1";

    private Rigidbody _body;
    private Sail _sail;
    private bool _controllingSail = true;

    private const float SailSpeed = 1.5f;
    private const float ShipDragForce = 1;
    private const float ShipRotationSpeed = 800;
    private const float SailRotationSpeed = 100;
    private const int MaxVelocity = 5;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _sail = transform.Find("Body/Sail").GetComponent<Sail>();
    }

    void Update()
    {
        if (Input.GetButtonDown(PlayerId + "_player_switch"))
        {
            _controllingSail = !_controllingSail;
        }


        if (_body.velocity.magnitude < MaxVelocity)
        {
            var speedAhead = GetSailVector() + transform.forward * ShipDragForce;
            _body.AddForce(speedAhead, ForceMode.Acceleration);
        }

        var axis = Input.GetAxis(PlayerId + "_player_horizontal");
        if (axis > .5f || axis < -.5f)
        {
            if (_controllingSail)
            {
                var heading = axis < -.5f
                    ? -_sail.transform.up
                    : _sail.transform.up;
                heading = heading * Time.deltaTime * SailRotationSpeed;
                _sail.transform.Rotate(heading);
            }
            else
            {
                var heading = axis < -.5f
                    ? -transform.up
                    : transform.up;
                _body.AddTorque(heading * ShipRotationSpeed * Time.deltaTime);
            }
        }
    }

    private Vector3 GetSailVector()
    {
        return _sail.transform.forward * SailSpeed * _sail.GetNormalizedEffeciencyAgainstWind();
    }
}