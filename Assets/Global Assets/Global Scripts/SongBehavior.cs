using UnityEngine;

public class SongBehavior : MonoBehaviour
{
    /*TO DO:
    1) Figure out how to add in a wait period between sounds in playSong() */
    
    /* This array contains the 4 Instruments that make
     * up this Song. This way we can reference a Song's
     * Instruments, their order, and all their Instrument data
     * whenever we need */
    public GameObject[] instrumentsInSong = new GameObject[4];
    //This is a reference to the GameManager to simplify referencing
    GameObject GameManager;
    //This is a bool flag that keeps track of if the Song in the Mail is playing.
    bool isPlaying = false;
    /*This fct iterates over this Song's instrumentsInSong,
     * playing the audio file located in each of their 
     * Audio Source Components */
    public void playInstruments()
    {
        //If the SOng is not currently playing, play it
        if (isPlaying == false)
        {
            //set isPlaying to true
            isPlaying = true;
            //iterate over Instruments in instrumentsInSong...
            for (int songIndex = 0; songIndex < instrumentsInSong.Length; songIndex = songIndex + 1)
            {
                //setting the volume in the Audio Component to 1 so we hear them
                instrumentsInSong[songIndex].GetComponent<AudioSource>().volume = 1.0f;

            }
        }
        //else song must be playing, so stop it
        else
        {
            //set isPlaying to false
            isPlaying = false;
            //iterate over Instruments in instrumentsInSong...
            for (int songIndex = 0; songIndex < instrumentsInSong.Length; songIndex = songIndex + 1)
            {
                //setting the volume in the AUdio COmponent to 0 so they mute
                instrumentsInSong[songIndex].GetComponent<AudioSource>().volume = 0;
           
            }
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Ensures that this Song has a reference to the Game Manager
        GameManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
