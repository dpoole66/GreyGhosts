using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

namespace GoogleARCore {

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    public class ARPopCorn : MonoBehaviour {

        //GET UI
        public Text feedBack;

        //GET COMPONENTS FOR AR
        public Camera FirstPersonCamera;
        public GameObject DetectedPlanePrefab;
        public GameObject VerticalPlanePrefab;
        public GameObject HorizontalPlanePrefab;
        public GameObject StagePrefab;

        //PLACEMENT ROTATION
        private const float StageRotation = 180.0f;

        //TAPS
        public int touchLimit;
        int touchCount = 0;

        //ARCORE QUIT
        private bool IsQuitting = false;

        //AR ROOT SCALE
        public float arScaleFactor;


        //UPDATE FOR AR
        public void Update() {

            //RUN APP LIFECYCLE UPDATE
            _UpdateApplicationLifecycle();

            //TOUCH
            Touch touch;

            // If the player has not touched the screen, we are done with this update.
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {

                return;

            }

            // Should not handle input if the player is pointing on UI.
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {

                return;

            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {       //&& touchCount < touchLimit

                if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0) {

                    Debug.Log("Hit at back of the current DetectedPlane");

                } else {

                    GameObject prefab;

                    if (hit.Trackable is FeaturePoint) {

                        prefab = StagePrefab;

                    } else if (hit.Trackable is DetectedPlane) {

                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;

                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical) {

                            prefab = VerticalPlanePrefab;

                        } else {

                            prefab = HorizontalPlanePrefab;

                        }
                    } else {

                        prefab = HorizontalPlanePrefab;

                    }

                    // Instantiate Stage model at the hit pose.
                    var stageObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                    // Compensate for the hitPose rotation facing away from the raycast (i.e.
                    // camera).
                    stageObject.transform.Rotate(0, StageRotation, 0, Space.Self);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make Stage model a child of the anchor.
                    stageObject.transform.parent = anchor.transform;

                    //INCREMENT TOUCHCOUNT TO PREVENT FURTHER PLACEMENT
                    touchCount++;
                    feedBack.text = touchCount.ToString() + "  Prefab:  " + prefab.ToString();
                }
            }
        }


        //ARCORE UPDATE APP LIFECYCLE   :   BOILERPLATE ARCORE - Copyright 2017 Google Inc. All Rights Reserved.
        private void _UpdateApplicationLifecycle() {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape)) {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking) {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            } else {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (IsQuitting) {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            } else if (Session.Status.IsError()) {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        //ARCORE ANDROID TOAST
        private void _ShowAndroidToastMessage(string message) {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null) {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }

        //ARCORE QUIT
        private void _DoQuit() {
            Application.Quit();
        }

    }
    //NAMESPACE
}
