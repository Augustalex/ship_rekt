using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Wind : MonoBehaviour
    {
        private float target = 0;

        private void Update()
        {
            if (Random.value < .002f)
            {
                target = Random.value * 360;
            }

            Quaternion newRotation = Quaternion.AngleAxis(target, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .5f * Time.deltaTime);
        }
    }
}