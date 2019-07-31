using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool Rowing;

    private Animation _animation;

    // Start is called before the first frame update
    void Start()
    {
        Rowing = false;
        _animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Rowing)
        {
            _animation.Play();
        }
        else
        {
            _animation.Stop();
        }
    }
}