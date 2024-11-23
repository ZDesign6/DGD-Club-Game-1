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

    /*This fct iterates over this Song's instrumentsInSong,
     * playing the audio file located in each of their 
     * Audio Source Components */
    public void playInstruments()
    {
        //iterate over Instruments in instrumentsInSong...
        for (int songIndex = 0; songIndex < instrumentsInSong.Length; songIndex = songIndex + 1)
        {
            //play the Audio Resource of this Instrument's Audio Source Component
            instrumentsInSong[songIndex].GetComponent<AudioSource>().Play();
            //should probably add a waiting period before the loop recurs so we don't stack sounds
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
