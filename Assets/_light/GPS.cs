 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour {

    //public static GPS Instance{ set; get; }
    public float latitude, longitude, north;

    DateTime dateTimeHour;
    public float _dateTimeHour;
    public float _dateTimeMin;
    public float _dateTimeSecond;

    public Text gpsUI;

    void Start () {

        //Instance = this;
        //DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());

    }

    private IEnumerator StartLocationService() {

        if (!Input.location.isEnabledByUser){

            gpsUI.text = "GPS Not Enabled by User ";
            yield break;

        }

        //Start Location Services
        Input.location.Start();

        //Feedback
        gpsUI.text = "GPS Starting... ";

        //Countdown to let LS launch
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0){

            yield return new WaitForSeconds(1);
            maxWait--;
            gpsUI.text = "Starting LocService " + maxWait.ToString();

         }

         if(maxWait <= 1){

            //Timing out
            gpsUI.text = "GPS Timeing Out " ;
            yield break;

         }

         if(Input.location.status == LocationServiceStatus.Failed){

            //Unable to determine location
            gpsUI.text = "GPS Failed ";
            yield break;

         } else{

            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            gpsUI.text = "GPS Access Granted ";
            yield return new WaitForSeconds(1);
            //SUN
            north = Input.compass.trueHeading;
            ////voodoo
            _dateTimeHour = dateTimeHour.Hour;
            _dateTimeMin = dateTimeHour.Minute;
            _dateTimeSecond = dateTimeHour.Second;
            gpsUI.text = north.ToString();
            yield return new WaitForSeconds(2);

         }

        Input.location.Stop();
        gpsUI.text = "GPS Stopping ";
        yield return new WaitForSeconds(1);  

    }
}
