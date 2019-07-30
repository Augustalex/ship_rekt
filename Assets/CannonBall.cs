﻿using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private LowPolyWater.LowPolyWater _water;
    private bool _floating;
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        _water = GameObject.FindGameObjectWithTag("water").GetComponent<LowPolyWater.LowPolyWater>();
        _body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
    }

    private void FixedUpdate()
    {
        if (_water.transform.position.y > transform.position.y)
        {
            _floating = true;
            _body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ
                                                                     | RigidbodyConstraints.FreezeRotationX |
                                                                     RigidbodyConstraints.FreezeRotationY |
                                                                     RigidbodyConstraints.FreezeRotationZ;
            _body.isKinematic = true;
            var hitBox = transform.Find("HitBox").gameObject;
            hitBox.SetActive(true);
        }

        if (_floating)
        {
            var nearestVertecies = _water.NearestVertexTo(transform.position);
            var position = transform.position;
            transform.position = new Vector3(position.x, nearestVertecies[0].y, position.z);
        }
    }
}