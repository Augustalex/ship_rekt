using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody _body;

    public string PlayerId = "1";

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_body.velocity.magnitude < 10)
        {
            var acceleration = 150;
            _body.AddForce(transform.forward * acceleration * Time.deltaTime, ForceMode.Acceleration);
        }

        var axis = Input.GetAxis(PlayerId + "_player_horizontal");
        Debug.Log("axis: " + axis);
        if (axis > .5f || axis < -.5f)
        {
            var heading = axis < -.5f
                ? -transform.up
                : transform.up;
            heading = heading * Time.deltaTime * 100;
            _body.transform.Rotate(heading);
//        _body.MoveRotation(Quaternion.Euler(heading));
        }
    }
}