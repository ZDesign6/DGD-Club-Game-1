using System.Collections.Generic;
using UnityEngine;

public class GameManagerInfo : MonoBehaviour
{
    //Keeps track of current frame. App runs at 60fps.
    public int frameCounter = 0;
    //A list for keeping track of all Seeds in the game. Used for referencing, instantiating, etc.
    public List<GameObject> seedList = new List<GameObject>();
    //A list of all Instruments in the game. Used for referencing, instantiating, etc.
    public List<GameObject> instrumentList = new List<GameObject>();
    //A List of all Sonic Mails in the game. used for referencing, instantiating, etc.
    public List<GameObject> mailList = new List<GameObject>();
    //A List of all Planters. Used by Scenario2To1Transitioner to check Planter contents.
    public List<GameObject> planterList = new List<GameObject>();
    /*A reference to the current active SonicMail. 
     * Used to know which Mail to bring into Scenario 2 */
    public GameObject activeMail;
    //Keeps track of which seed the Player is grabbing
    public GameObject grabbedSeed;
    //Keeps track of the last frame that a Seed was clicked on
    public int clickFrame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //every frame, update the frameCounter so it maintains accuracy.
        frameCounter = frameCounter + 1;
        //A sample condition for making a Sonic Mail active, activates every second
        if (frameCounter % 60 == 0)
        {
            //activate the Mail at Index 0
            activateMail(0);
        }
        //Debug.Log("grabbed Seed is currently" + grabbedSeed);

    }
    /*This fct grabs the indicated SonicMail out of the mailList and 
     * activates its 'Window' child object so it starts rendering.
     * It also ensures that activeMail updates with a reference to the Mail*/
    void activateMail(int mailIndex)
    {
        //activate the Window obj of the given Mail
        mailList[mailIndex].GetComponent<SonicMailBehavior>().windowSegment.SetActive(true);
        //Then ensure that activeMail becomes a reference to the given Mail
        activeMail = mailList[mailIndex];
    }
}
