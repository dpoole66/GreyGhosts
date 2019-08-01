using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class AudioObs : MonoBehaviour
{

    AudioSource _audioSource;

    [Header("Get Spectrum Data Function")]
    public static float[] samples = new float[512];      //512 roughly 20,000 Hz

    private void Start() {

        _audioSource = GetComponent<AudioSource>();

    }

    private void Update() {

        GetSpectrumAudioSource();

    }

    void GetSpectrumAudioSource(){

        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
 
}
