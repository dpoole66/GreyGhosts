using UnityEngine;
using System.Collections;
using GoogleARCore;


[System.Serializable]

public class ARSunLightCalculation
{

    Transform AR_Scenelight;

    void Awake()
	{
		lastTimeInHours=-9999.0f;
        GameObject AR_Scenelight = GameObject.FindGameObjectWithTag("ar_scenelight");
    }
			


	public void CreateSceneLight(Vector3 pos, int sceneLightLayerMask, GameObject SunLightGO)
	{
		bool found = false;
		foreach (Transform t in SunLightGO.transform)
		{
			    if(t.name == "AR_SceneLight")
			    {
                //LightController lc = GameObject.FindGameObjectWithTag("lightController").GetComponent<LightController>();
			    GameObject instance = t.transform.gameObject;
			    sceneLightTransform = instance.transform;
			    sceneLightTransform.position = pos;
			    sceneLight = instance.GetComponent<Light>();
			    sceneLight.type = LightType.Directional;

                sceneLight.cullingMask = sceneLightLayerMask;
			    found=true;
			    }
		} 

		if(found==false)
		{

            //LightController lc = GameObject.FindGameObjectWithTag("lightController").GetComponent<LightController>();
            GameObject AR_Scenelight = GameObject.FindGameObjectWithTag("ar_scenelight");
            //GameObject instance = GameObject.Instantiate(AR_Scenelight);
            AR_Scenelight.name="AR_SceneLight";
			sceneLightTransform = AR_Scenelight.transform;
		    sceneLightTransform.position = pos;
		    sceneLight = AR_Scenelight.GetComponent<Light>();
		    sceneLight.type = LightType.Directional;


            sceneLight.cullingMask = sceneLightLayerMask;
		}
	}	



	// Changed in version 1.1:
	//public void SetSun(float latitude, float longitude, float offsetUTC, int dayOfYear, float timeInHours, float overcastFactor, float artisticSunAzimuth, bool overrideAzimuth)
    public float overcastFactor = 0.0f;
    public void SetSun(float latitude, float longitude, float offsetUTC, int dayOfYear, float timeInHours, float northDirection)
	{
		// New in version 1.2.1:
		if(lastTimeInHours != timeInHours || lastLatitude != latitude || lastLongitude != longitude || lastDayOfYear != dayOfYear || lastOffsetUTC != offsetUTC || lastNorthDirection != northDirection) 
		{
            GPS gps = GameObject.FindGameObjectWithTag("gps").GetComponent<GPS>();
            lastLatitude = gps.latitude;
            lastLongitude = gps.longitude;
            lastOffsetUTC =offsetUTC; 
			lastDayOfYear=dayOfYear; 
			lastTimeInHours=timeInHours;
            ////added this voodoo
            timeInHours = gps._dateTimeHour;
            ////end voodoo
			//lastOvercastFactor=overcastFactor; 
			//lastOverrideAzimuth=overrideAzimuth; 
			//lastArtisticSunAzimuth=artisticSunAzimuth;
			lastNorthDirection=northDirection;	
			// end of New in version 1.2.1:

			//ensure inputs stay within required limits
			latitude=Mathf.Clamp(latitude, -90.0f, 90.0f);
			longitude=Mathf.Clamp(longitude, -180.0f, 180.0f);
			offsetUTC=Mathf.Clamp(offsetUTC, -12.0f, 14.0f);
			dayOfYear=Mathf.Clamp(dayOfYear, 1, 366);
			timeInHours=Mathf.Clamp(timeInHours, 0.0f, 24.00f);
			overcastFactor=Mathf.Clamp(overcastFactor, 0.0f, 1.0f);
			//artisticSunAzimuth=Mathf.Clamp(artisticSunAzimuth, 0.0f, 360.0f);

			// calculate azimut & altitute
			cosLatitude = Mathf.Cos(latitude*Mathf.Deg2Rad);
			sinLatitude = Mathf.Sin(latitude*Mathf.Deg2Rad);		
			D=DeclinationOfTheSun(dayOfYear);
			sinDeclination=Mathf.Sin(D);
			cosDeclination=Mathf.Cos(D);
			W = 360.0f/365.24f;
			A = W * (dayOfYear + 10.0f);
			B = A + (360.0f/Mathf.PI) * 0.0167f * Mathf.Sin(W * (dayOfYear-2) * Mathf.Deg2Rad);
			C = (A - (Mathf.Atan(Mathf.Tan(B*Mathf.Deg2Rad)/Mathf.Cos(23.44f*Mathf.Deg2Rad))*Mathf.Rad2Deg)) / 180.0f;		
			EquationOfTime = 720.0f * (C - Mathf.Round(C)) / 60.0f;
			hourAngle = (timeInHours + longitude / (360.0f / 24.0f) - offsetUTC - 12.0f + EquationOfTime) * (1.00273790935f-1.0f/365.25f)*(360.0f / 24.0f)*Mathf.Deg2Rad;

			azimuth = Mathf.Atan2(-cosDeclination * Mathf.Sin(hourAngle), sinDeclination * cosLatitude - cosDeclination * Mathf.Cos(hourAngle) * sinLatitude);	
			if (azimuth<0) azimuth = azimuth + 6.28318530717959f; 		
			altitude = Mathf.Asin(sinDeclination * sinLatitude + cosDeclination * Mathf.Cos(hourAngle) * cosLatitude );		
				
		
			azimuth = azimuth / Mathf.Deg2Rad; // Azimuth (in degrees): 0 = North, East = 90, South = 180, West = 270

			//New in version 1.1: North adjustment
				azimuth = azimuth + Mathf.Clamp(northDirection, 0.0f, 360.0f);
				if(azimuth>=360f) azimuth = azimuth - 360.0f;
			//

			//if(overrideAzimuth==true) azimuth = artisticSunAzimuth;

			altitude = (altitude) / Mathf.Deg2Rad; // Height of the center of the sun (in degrees) - 0 = horizon - 90 = full zenit

			if(altitude<0.0f) altitude=360.0f+altitude;

			//Set rotations	
			if(altitude>90.0f || altitude<0.0f) // Scene Light altitude angle is limited to avoid objects being lit from below the horizon during twilight conditions.
			{
				sceneLightTransform.eulerAngles = new Vector3(0.0f, azimuth, 0.0f); 
			}
			else
			{
				sceneLightTransform.eulerAngles = new Vector3(altitude, azimuth, 0.0f);
			}

            sceneLight.color = Color.white;


			sunAltitude=altitude;
			sunAzimuth=azimuth;

            if(AR_Scenelight){

                Quaternion sunRotation = Quaternion.Euler(sunAltitude, sunAzimuth, 0f);
                AR_Scenelight.transform.rotation = sunRotation;

            }

        }	
	}
		
	
	public float DeclinationOfTheSun(int dayOfYear)
	{
		WD = 360.0f/365.24f;
		AD = WD * (dayOfYear + 10.0f);
		BD = AD + (360.0f/Mathf.PI) * 0.0167f * Mathf.Sin(WD * (dayOfYear-2) * Mathf.Deg2Rad);
		return -Mathf.Asin(Mathf.Sin(23.44f * Mathf.Deg2Rad) * Mathf.Cos(BD * Mathf.Deg2Rad) );
	}

	void OnApplicationQuit()
	{
		RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1.0f);
	}	

	[HideInInspector]
	public Color[] sunColorClear;
	[HideInInspector]
	public Color[] sunColorOC;
	//[HideInInspector]
	//public Light skyboxLight; // This directional light will be instantiated from the prefab of the resources folder at runtime and it is used as input for the skybox
	[HideInInspector]
	public Light sceneLight; // This directional light will be instantiated from the prefab of the resources folder at runtime and it is used for lighting the scene only
	//[HideInInspector]
	//public Transform skyboxLightTransform;
	[HideInInspector]
	public Transform sceneLightTransform;
	//[HideInInspector]
	//public LensFlare lensFlare; // The lensFlare will be instantiated from the prefab of the resources folder at runtime 
	//[HideInInspector]
	//public Transform lensFlareTransform;
	//[HideInInspector]
	//public Material SkyboxMaterial;

	private float cosLatitude;
	private float sinLatitude;
	private float cosDeclination;
	private float sinDeclination;
	private float EquationOfTime;		
	private float hourAngle;  	
	private float azimuth;
	// Refraction removed in version 1.2 (see readme):
	//private float refraction;
	//private float refA; 
	//private float refB;
	private float altitude;
	private float colorAltitude;
	private int floorAlt;
	private Color clearColor; 
	private Color OCColor;
	private float W;
	private float A;
	private float B;	
	private float C;
	private float D;
	private float WD;
	private float AD;
	private float BD;	

	[HideInInspector]
	public float sunAltitude;
	[HideInInspector]
	public float sunAzimuth;

	// New in version 1.2.1
		private float lastLatitude; 
		private float lastLongitude; 
		private float lastOffsetUTC; 
		private int lastDayOfYear; 
		private float lastTimeInHours;

		private float lastOvercastFactor; 
		private bool lastOverrideAzimuth; 
		private float lastArtisticSunAzimuth;
		private float lastNorthDirection;
}
