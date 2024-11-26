using System.Collections.Generic;
using UnityEngine;

public class SonicMailBehavior : MonoBehaviour
{
    //This is the Song that this Sonic Mail contains
    public GameObject song;
    //This is the List of Seeds that this Sonic Mail contains
    List<GameObject> seedsInMail = new List<GameObject>();
    /*This fct calls the playSong() fct of this Mail's Song.
     Called by Player Input 'Interact'.*/
    public void playSong()
    {
        //Grab the specific instance of the Song in this Mail and use its PlayInstruments() method
        song.GetComponent<SongBehavior>().playInstruments();
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
