using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SonicMailBehavior : MonoBehaviour
{
    //This is the Song that this Sonic Mail contains
    public GameObject song;
    /*This is a reference to THIS Sonic Mail's Play Button,
     * used for checking collision during PlaySong()*/
    public GameObject playButton;
    /*This is a reference to this SonicMail's Window.
     * Used for turning rendering on/off through obj activation */
    public GameObject windowSegment;
    //This is the List of Seeds that this Sonic Mail contains
    public List<GameObject> seedsInMail = new List<GameObject>();
    /*This fct calls the playSong() fct of this Mail's Song.
     Called by Player Input 'Interact'.*/
    public GameObject Text;
    //The text that will show up
    public GameObject Seed;
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
        //Debug.Log("The current active cam converted that to " + currentMouseWorldPoint);
        
        //If the Play Button's Collider is overlapping with the mouse's Vector2...
        if (playButton.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)) == true)
        {
            Debug.Log("MOUSE IS ON PLAY BUTTON");
            //Grab the specific instance of the Song in this Mail and use its PlayInstruments() method
            song.GetComponent<SongBehavior>().playInstruments();
            Text.SetActive(true);
            Seed.SetActive(true);
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
