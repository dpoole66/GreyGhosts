using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudGPS : MonoBehaviour {

    //LONG & LAT
    public float latitude;
    public float longitude;
    public float north;
    public Quaternion northRotation;
    //TIME
    DateTime dateTimeHour;
    public float _dateTimeHour;
    //public Transform compass;
    //FEEDBACK UI
    Text statusUI, latUI, longUI, timeUI, compassUI;

    public void Awake() {

        StartCoroutine(Start());
        statusUI = GameObject.FindGameObjectWithTag("feedbackA").GetComponent<Text>();
        compassUI = GameObject.FindGameObjectWithTag("feedbackB").GetComponent<Text>();
        timeUI = GameObject.FindGameObjectWithTag("feedbackC").GetComponent<Text>();
        //latUI = GameObject.FindGameObjectWithTag("feedbackA").GetComponent<Text>();
        //longUI = GameObject.FindGameObjectWithTag("feedbackB").GetComponent<Text>();
        //Text feedbackAtlas, feedbackApollo;

    }

    private void Update() {

        if (Input.location.isEnabledByUser) {

            northRotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);

        }

    }

    IEnumerator Start() {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1) {
            print("Timed out");
            statusUI.text = "Timed Out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed) {
            print("Unable to determine device location");
            statusUI.text = "Unable to determine device location";
            yield break;
        } else {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
            north = Input.compass.trueHeading;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            _dateTimeHour = dateTimeHour.Hour;
            //UI

            statusUI.text = "GPS Running";           
            compassUI.text = north.ToString();
            timeUI.text = System.DateTime.UtcNow.ToString("HH:mm dd MMM, yyy");
            //latUI.text = latitude.ToString();
            //longUI.text = longitude.ToString();


        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}
                                                                                                                                                                 