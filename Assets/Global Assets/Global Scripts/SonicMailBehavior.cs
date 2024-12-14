using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SonicMailBehavior : MonoBehaviour
{
    //This is the Song that this Sonic Mail contains
    public GameObject song;
    /*This is a reference to this SonicMail's Window.
     * Used for turning rendering on/off through obj activation */
    public GameObject windowSegment;
    /*This is a reference to THIS Sonic Mail's Play Button,
     * used for checking collision during PlaySong()*/
    public GameObject playButton;
    //A ref to the 'Victory' version of the Window Segment. Used for turning on and off rendering
    public GameObject victoryWindowSegment;
    //A ref to the Listen Button from the Victory WIndow
    public GameObject listenButton;
    //A ref to the Send Button from the Victory Window
    public GameObject sendButton;
    /* A reference to the starting state of the Windows before they get dragged around the Scene.
     * Assigned by Scenario1To2Transioner before moving the Window.
     * Used by Scenario2To1Transitioner to reset the Window when coming back to Scenario 1*/
    public Vector3 windowHomePos;
    public Vector3 windowHomeScale;
    //This is the List of Seeds that this Sonic Mail contains
    public List<GameObject> seedsInMail = new List<GameObject>();
    /*This fct calls the playSong() fct of this Mail's Song.
     Called by Player Input 'Interact'.*/
    public void playSong(InputAction.CallbackContext actionInfo )
    {
        //An abstraction of the mouse's screenspace coords as passed by the callback context
        float currentMouseX = actionInfo.ReadValue<Vector2>().x;
        float currentMouseY = actionInfo.ReadValue<Vector2>().y;
        //Debug.Log("Callback context carried " + actionInfo.ReadValue<Vector2>() + " as mouse pos");
        //An abstraction of the current active Main Camera
        Camera activeCam = Camera.main;
        //The current Main Camera's conversion of mouse screenpoint to worldpoint
        Vector3 currentMouseWorldPoint = activeCam.ScreenToWorldPoint(new Vector3(currentMouseX, currentMouseY, 0));
        
        //CLICKING PLAY BUTTON

        //If the Play Button's Collider is overlapping with the mouse's Vector2...
        if (playButton.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)) == true)
        {
            //Grab the specific instance of the Song in this Mail and use its PlayInstruments() method
            song.GetComponent<SongBehavior>().playInstruments();
        }

        //CLICKING LISTEN BUTTON (VICTORY WINDOW)

        //If Listen Button's Collider is overlapping with mouse's World Pos
        if(listenButton.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)) == true)
        {
            //If the audio is not playing...
            if(listenButton.GetComponent<AudioSource>().isPlaying == false)
            {
                listenButton.GetComponent<AudioSource>().Play();
            }
            //Else if the audio IS playing...
            else
            {
                listenButton.GetComponent<AudioSource>().Stop();
            }
        }

        //CLICKING SEND BUTTON (VICTORY WINDOW)

        if (sendButton.GetComponent<BoxCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)) == true)
        {
            Debug.Log("CLICKED SEND BUTTON");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
