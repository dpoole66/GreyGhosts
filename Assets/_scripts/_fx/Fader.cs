using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

    //FADEING
    [Header("Meshes to Fade: ")]
    public SkinnedMeshRenderer[] fadeRenderers = new SkinnedMeshRenderer[7];
    SkinnedMeshRenderer fadeMe;
    public float alphaFade = 0f;

    //FADE TIMER AND BOOL
    [Header("Fade Timer and Bool: ")]
    public float fadeTime = 3f;
    public bool isVisiable;
    //public bool isFadeing;          
    [HideInInspector] public Material _material;
    [HideInInspector] public Color _color;
    [HideInInspector] public float startOpacity;


    //GHOSTING FX
    [Header("Grey Ghost FX:")]
    public ParticleSystem psBody;
    public ParticleSystem psTop;
    public ParticleSystem psBottom;
    public ParticleSystem psVapor;

    //RING FX
    [Header("Ring Particles: ")]
    public ParticleSystem psRing;

    private void Awake() {

        StartCoroutine(MakeFade());

    }

    void Update()
    {

        //StartCoroutine(Visible());

        if (Input.GetKeyDown(KeyCode.F1)) {

            StartCoroutine(FadeTo(1f, fadeTime));

        }

        if (Input.GetKeyDown(KeyCode.F2)) {

            StartCoroutine(FadeTo( 0f, fadeTime));

        }

        if (Input.GetKey(KeyCode.F3)) {

            StartCoroutine(MakeFade());

        }

        if (Input.GetKey(KeyCode.F4)) {

            StartCoroutine(MakeOpaque());

        }

        if (Input.GetKey(KeyCode.Space)) {


        }



    }


    IEnumerator FadeIn(float time) {

        Debug.Log("Fadeing In");

        while (true) {

            //PLAY VAPOR PARTICLES
            psVapor.Play();
            //SET ALPHA FOR FADE
            //alphaFade = 0f;

            foreach (SkinnedMeshRenderer fadeMe in fadeRenderers) {

                Debug.Log("Gathering Renderers");
                //SET RENDER MODE TO "FADE" 
                _material = fadeMe.material;
                _material.SetFloat("_Mode", 2f);
                _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                _material.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                _material.SetInt("_ZWrite", 0);
                _material.EnableKeyword("_ALPHABLEND_ON");

                //Debug.Log("Setting Colors");
                //SET COLORS
                // _color = new Color(1, 1, 1, Mathf.Lerp(from, to, time));
                //alphaFade = 0f;
                _color = fadeMe.material.color;
                _color.a = alphaFade;
                fadeMe.material.color = _color;

            }

            alphaFade = Mathf.Lerp(1f, 0f, time - Time.time);
            Debug.Log("Alpha Fade:  " + alphaFade);

            if (alphaFade == 1f) {

                //isFadeing = false;
                yield return StartCoroutine(MakeOpaque());
                psVapor.Stop();
                Debug.Log("Breaking from Visible");
                yield break;

            }

            yield return null;


        }


    }

    IEnumerator FadeTo(float targetOpacity, float duration) {

        foreach (SkinnedMeshRenderer fadeMe in fadeRenderers) {

            Debug.Log("Gathering Renderers");
            //SET RENDER MODE TO "FADE" 
            _material = fadeMe.material;
            _material.SetFloat("_Mode", 2f);
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _material.SetInt("_ZWrite", 0);
            _material.EnableKeyword("_ALPHABLEND_ON");

            Debug.Log("Setting Colors");
            startOpacity = 0f;
            ////SET COLORS   
            _color = fadeMe.material.color;
            _color.a = startOpacity;
            fadeMe.material.color = _color;

        }

        Debug.Log("FadeTo");
        float t = 0f;

        while (t < duration) {

            t += Time.deltaTime;

            float blend = Mathf.Clamp01(t / duration);
            Debug.Log("Blend: " + blend);

            _color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
            Debug.Log("Alpha: " + _color.a);

            _material.color = _color;

            yield return null;

        }

    }

    IEnumerator MakeFade() {

        Debug.Log("Fade");

        foreach (SkinnedMeshRenderer fadeMe in fadeRenderers) {

            Debug.Log("Gathering Renderers to make FADE");
            //SET RENDER MODE TO "FADE" 
            _material = fadeMe.material;
            _material.SetFloat("_Mode", 2f);
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _material.SetInt("_ZWrite", 0);
            _material.EnableKeyword("_ALPHABLEND_ON");

            Debug.Log("Setting Colors");
            //_color = _material.color;
            startOpacity = 0f;
            //_color.a = startOpacity;
            ////SET COLORS   
            _color = fadeMe.material.color;
            _color.a = startOpacity;
            fadeMe.material.color = _color;

        }

        yield break;


    }


    IEnumerator MakeOpaque() {

        Debug.Log("Opaque");

        foreach (SkinnedMeshRenderer fadeMe in fadeRenderers) {

            Debug.Log("Gathering Renderers to make OPAQUE");
            //SET RENDER MODE TO "OPAQUE" 
            _material = fadeMe.material;
            _material.SetFloat("_Mode", 1f);
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            _material.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            _material.SetInt("_ZWrite", 1);
            _material.DisableKeyword("_ALPHABLEND_ON");

            Debug.Log("Setting Colors");
            //SET COLORS
            _color = fadeMe.material.color;
            _color.a = alphaFade;
            fadeMe.material.color = _color;

        }

        yield break;


    }


   
}
