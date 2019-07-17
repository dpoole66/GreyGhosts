using UnityEngine;
using System.Collections;


// This script is necessary to to preview the light based on inspector settings and to set back Atmosphere thickness if the users leaves playmode during night times
[ExecuteInEditMode]
public class ARSunLightEditorHelper : MonoBehaviour 
{	
	#if UNITY_EDITOR
	private Material SkyboxOriginalMaterial;
	private ARSunLight SunLight;

    //void Awake() {
    //    SunLight = this.GetComponent<SunLight>();
    //    if (SunLight.SunLightInitialised == false) SunLight.InitialiseSunLight();
    //    SkyboxOriginalMaterial = UnityEngine.RenderSettings.skybox;
    //}
    void Awake() {
        SunLight = this.GetComponent<ARSunLight>();
        if(SunLight.SunLightInitialised == false) SunLight.InitialiseSunLight();
    }


    void Update() 
	{
		//if(Application.isPlaying==false && SkyboxOriginalMaterial.GetFloat("_AtmosphereThickness")!=1.0f) 
		//{
		//	SkyboxOriginalMaterial.SetFloat("_AtmosphereThickness", 1.0f);
		//	RenderSettings.skybox=SkyboxOriginalMaterial;
		////}

		//if(Application.isPlaying==false && SunLight.inspectorChanged==true)
		//{
		//	SunLight.UpdateLight();
		//	SunLight.inspectorChanged=false;
		//}				
	}
	#endif

}


