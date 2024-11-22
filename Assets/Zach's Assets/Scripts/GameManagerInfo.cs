using System.Collections.Generic;
using UnityEngine;

public class GameManagerInfo : MonoBehaviour
{
    //Keeps track of current frame. App runs at 60fps.
    int frameCounter = 0;
    //A list for keeping track of all Seeds in the game. Used for referencing, instantiating, etc.
    List<GameObject> seedList = new List<GameObject>();
    //A list of all Instruments in the game. Used for referencing, instantiating, etc.
    List<GameObject> instrumentList = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //every frame, update the frameCounter so it maintains accuracy.
        frameCounter = frameCounter + 1;
    }
}
