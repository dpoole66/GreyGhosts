// SunLightCS (C) Mark Hessburg
// IMPORTANT: always make sure that the intensity of ALL other directional lights is below 8! Unfortunately Unity currently does not offer to set the input of the skybox from a script yet, if don't set manually it will take the brightest directional light.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARSunLight : MonoBehaviour 
{
	private TextAsset JSONFile;
	private string  JSONString;
	public ARSunLightCalculation SunLightCalculation;

	// mandatory settings:
	public float latitude; 
	public float longitude; 
	public float offsetUTC; 
	public int dayOfYear; 
	public float timeInHours;
	public bool progressTime; 
	public float timeProgressFactor; 
	public bool leapYear;
    //voodoo
    public GPS gps;
    public Text gpsUI;

	// New in version 1.1:
		public float northDirection;
	//

	// layerMasks:
	public LayerMask sceneLightLayerMask;// = -1; // layer mask of the scene light – set as required
	[HideInInspector]
	public LayerMask skyboxLightLayerMask = 0; // layer mask of the skybox light – usually this should be set to "nothing"
		
	[HideInInspector]
	public int selectedLocationPreset;
	[HideInInspector]
	public Material SkyboxMaterial;

	[HideInInspector]
	public bool SunLightInitialised;
	[HideInInspector]
	public bool inspectorChanged;


	void Awake() 
	{
		if(SunLightInitialised==false) InitialiseSunLight();
        //gps = GameObject.FindGameObjectWithTag("gps").GetComponent<GPS>();
    }
		
	public void InitialiseSunLight() {
        //Import precalculated data
        //JSONFile = Resources.Load("Hessburg/SunDataResource") as TextAsset;
        //JSONString = JSONFile.text;
        //SunLightCalculation = SunLightCalculation.CreateFromJSON(JSONString);

        // Creates SceneLight, a new directional light for lighting the scene – unlike the one used for the skybox this one won't go deep below the horizon during twilight to avoid any unrealistically lighting from below
        SunLightCalculation.CreateSceneLight(this.gameObject.transform.position, sceneLightLayerMask, this.gameObject);
		SunLightCalculation.sceneLightTransform.SetParent(this.transform, false);

		//SunLightCalculation.SkyboxMaterial = RenderSettings.skybox;
		SunLightInitialised = true;
	}
		
	public void ChangeSceneLightLayer(LayerMask sceneLightLayerMask)
	{
		if(Application.isPlaying) SunLightCalculation.sceneLight.cullingMask = sceneLightLayerMask;
	}	

	public Light GetSceneLight()
	{
		return SunLightCalculation.sceneLight;
	}	

    ////Original ToD setup
    //public void SetTime(int HH, int MM, int SS) {
    //    timeInHours = HH + 1.0f / 60.0f * MM + 1.0f / 60.0f / 60.0f * SS;
    //}

    //Voodoo
    public void SetTime(float HH, float MM, float SS) {
        timeInHours = HH + gps._dateTimeHour * gps._dateTimeMin * gps._dateTimeSecond;
    }

    public int  GetHours()
	{
		return (int)Mathf.Floor(timeInHours);
	}	

	public int GetMinutes()
	{
		return (int)Mathf.Floor((timeInHours-Mathf.Floor(timeInHours))*60.0f);
	}	

	public int GetSeconds()
	{
		return (int)Mathf.Floor((timeInHours-Mathf.Floor(timeInHours))*3600.0f - Mathf.Floor((timeInHours-Mathf.Floor(timeInHours))*60.0f) * 60.0f);
    }
        
    //Done with getting ToD

    public float GetSunAltitude()
	{
		return SunLightCalculation.sunAltitude;
	}	

	public float GetSunAzimuth()
	{
		return SunLightCalculation.sunAzimuth;
	}	

	public float GetStarDomeDeclination()
	{
		if(dayOfYear==1)
		{
			return latitude+Mathf.Rad2Deg*Mathf.Lerp(SunLightCalculation.DeclinationOfTheSun(365), SunLightCalculation.DeclinationOfTheSun(1), timeInHours/24.0f);
		}
		else
		{	
			return latitude+Mathf.Rad2Deg*Mathf.Lerp(SunLightCalculation.DeclinationOfTheSun(dayOfYear-1), SunLightCalculation.DeclinationOfTheSun(dayOfYear), timeInHours/24.0f);
		}
	}	

	void Start()
	{
		// Changed in version 1.1:
			SunLightCalculation.SetSun(latitude, longitude, offsetUTC, dayOfYear, timeInHours, northDirection);
			//SunLightCalculation.SetSun(latitude, longitude, offsetUTC, dayOfYear, timeInHours, northDirection, overcastFactor, artisticSunAzimuth, overrideAzimuth);
	}

	void Update()
	{
		UpdateLight();
	}

	bool checkSettings()
	{
		return true;
	}	

	public void UpdateLight() 
	{
		if(progressTime==true && Application.isPlaying==true)
		{	
			timeProgressFactor=Mathf.Max(timeProgressFactor, 0.0f);
			timeInHours+=Time.deltaTime/3600.0f*timeProgressFactor;
			if(timeInHours>=24.0f)
			{
				timeInHours=0.0f;
				dayOfYear=dayOfYear+1;
				if(leapYear==false && dayOfYear==366)
				{
					dayOfYear=1;
				}
				else
				{
					if(leapYear==true && dayOfYear==367)
					{
						dayOfYear=1;
					}	
				}	
			}	
		}

		//Changed in version 1.1:
			SunLightCalculation.SetSun(latitude, longitude, offsetUTC, dayOfYear, timeInHours, northDirection);
            gpsUI.text = latitude.ToString() + longitude.ToString() + offsetUTC.ToString() + dayOfYear.ToString() + timeInHours.ToString();
			//SunLightCalculation.SetSun(latitude, longitude, offsetUTC, dayOfYear, timeInHours, northDirection, overcastFactor, artisticSunAzimuth, overrideAzimuth);

		
	}
}

