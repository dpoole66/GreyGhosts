using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class GreySpeech : MonoBehaviour
{
    //SOURCE
    [Header("Audio Source")]
    public AudioSource audioSource;

    //CLIPS
    [Header("Violation")]
    public AudioClip violate;
    [Header("Attention")]
    public AudioClip attention;
    [Header("Comment")]
    public AudioClip comment1;

    private void Start() {

        audioSource = GetComponent<AudioSource>();

    }

    public void Violation(){

        audioSource.PlayOneShot(violate, 1.0f);

    }

    public void Attention() {

        audioSource.PlayOneShot(attention, 1.0f);

    }

    public void Comment1() {

        audioSource.PlayOneShot(comment1, 1.0f);

    }

}
