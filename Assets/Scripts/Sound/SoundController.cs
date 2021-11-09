using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource1.Play();
        audioSource2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
