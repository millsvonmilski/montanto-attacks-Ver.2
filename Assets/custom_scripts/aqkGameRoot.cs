using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aqkGameRoot : MonoBehaviour
{
    //--------------------------------------------------------------
    // Public Properties
    //--------------------------------------------------------------

    // Static instance of aqkGameRoot which allows it to be accessed by any other script.
    public static aqkGameRoot instance = null;

    //--------------------------------------------------------------
    // Accessors
    //--------------------------------------------------------------

    public GameObject getGobj ()
    {
        return transform.gameObject;
    }

    //--------------------------------------------------------------
    // Initialization
    //--------------------------------------------------------------

    void Awake ()
    {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    //--------------------------------------------------------------
    // Application Lifecycle
    //--------------------------------------------------------------

    //public void init () {
    //  Debug.Log (transform.name + ":" + GetType () + ":init");
    //}

    //public void recall (string sceneName) {
    //  Debug.Log (transform.name + ":" + GetType () + ":recall  scene name: " + sceneName);
    //}

    public void pauseGame (float state)
    {
        Debug.Log("pauseGame: " + state);
        Time.timeScale = (state == 0) ? 1 : 0.01f;
    }
}