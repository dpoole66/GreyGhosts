using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopTimeController : MonoBehaviour{
    //UI OBJECTS
    [Header("UI Menu Objects: ")]
    public GameObject o1, o2, o3, o4, o5, o6;
    //UI BUTTONS
    [Header("UI Buttons: ")]
    public Button b1, b2, b3, b4, b5, b6;
    //UI PANELS
    [Header("UI Text: ")]
    public Text p1, p2, p3, p4, p5, p6;
    //GET AR MODEL
    [Header("AR Object: ")]
    public GameObject popcorn;
    //UI ANIMATIONS
    [Header("UI Animations: ")]
    public GameObject UIAnim ;
    //BUTTON COLOR
    [Header("Button Color Block: ")]
    public ColorBlock colorBlock;
    ColorBlock newColorBlock;
    //AUDIO
    //[Header("Audio: ")]
    //public AudioSource audioFeedback;
    //public AudioClip beepUp;
    //STATE AND FEEDBACK: 
    [Header("Current State Update: ")]
    public popState currentState;
    public popState remainState;
    //UI BUTTON STATE ADVANCE
    public bool decision1 = false;
    public bool decision2 = false;
    //HIDDEN STATE TIMER
    [HideInInspector] public float stateTimeElapsed;

    private void Update() {

        //UPDATE STATE MACHINE STATUS
        currentState.UpdateState(this);

    }

    public void TransitionToState(popState nextState) {
        if (nextState != remainState) {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState() {
        stateTimeElapsed = 0;
    }


    //UI  BUTTON COLOR
    public void ButtonGreen(Button button){

        ColorBlock buttonColor = button.colors;
        buttonColor.normalColor = Color.green;
        buttonColor.highlightedColor = Color.white;
        button.colors = buttonColor;

    }
    public void ButtonYellow(Button button) {

        ColorBlock buttonColor = button.colors;
        buttonColor.normalColor = Color.yellow;
        buttonColor.highlightedColor = Color.white;
        button.colors = buttonColor;

    }

    public void ButtonRed(Button button) {

        ColorBlock buttonColor = button.colors;
        buttonColor.normalColor = Color.red;
        buttonColor.highlightedColor = Color.white;
        button.colors = buttonColor;

    }


}
