using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip LoopClip;
    
    private AudioSource _audioSource;
    private float _duration;

    // Start is called before the first frame update
    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        _duration = _audioSource.clip.length;
        StartCoroutine(WaitForSound());
    }

    private IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(_duration);
        _audioSource.Stop();
        _audioSource.clip = LoopClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}