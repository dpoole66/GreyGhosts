using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIO : MonoBehaviour
{
    //OBJECT AND RENDERER
    [Header("Renderer: ")]
    public SkinnedMeshRenderer[] fadeMe = new SkinnedMeshRenderer[1];
    [Header("Fade Time: ")]
    public float fadeTime;

    //GET MATERIALS COLORS AND SET START OPACITY
    Material mat;
    Color fadeColor;
    float startOpacity;


    private void Start() {

        Debug.Log("Started");

    }


    void Update() {

        if (Input.GetKeyDown(KeyCode.F1)) {

            StartCoroutine(FadeTo(1f, fadeTime));

        }

        if (Input.GetKeyDown(KeyCode.F2)) {

            StartCoroutine(FadeTo(0f, fadeTime));

        }


    }


    IEnumerator FadeTo(float targetOpacity, float duration) {

        Debug.Log("FadeTo");

        float t = 0f;

        while (true && t < duration) {

            foreach (SkinnedMeshRenderer fade in fadeMe) {

                mat = fade.material;
                mat.SetFloat("_Mode", 2f);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 1);
                mat.EnableKeyword("_ALPHABLEND_ON");

                fadeColor = mat.color;
                startOpacity = fadeColor.a;

                //INCREMENT TIMER
                t += Time.deltaTime;

                //BLEND CALC
                float blend = Mathf.Clamp01(t / duration);
                Debug.Log("Blend: " + blend);

                //FADE LERP
                fadeColor.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
                Debug.Log("Alpha: " + fadeColor.a);

                //MATERIAL COLOR IS FADED COLOR
                mat.color = fadeColor;


            }


            //IF FULL ALPHA BREAK
            if (fadeColor.a == 1f) {

                StartCoroutine(MakeOpaque());
                yield break;

            }


            yield return null;

        }

        IEnumerator MakeOpaque() {

            Debug.Log("Opaque");

            foreach (SkinnedMeshRenderer fadeMe in fadeMe) {

                Debug.Log("Gathering Renderers to make OPAQUE");
                //SET RENDER MODE TO "OPAQUE" 
                mat = fadeMe.material;
                mat.SetFloat("_Mode", 1f);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DistBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHABLEND_ON");

                Debug.Log("Setting Colors");
                //SET COLORS
                fadeColor = fadeMe.material.color;
                fadeColor.a = 1f;
                fadeMe.material.color = fadeColor;

            }

            yield break;


        }


    }

}
