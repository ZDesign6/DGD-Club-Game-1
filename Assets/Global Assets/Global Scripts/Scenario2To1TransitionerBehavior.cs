using UnityEngine;
using UnityEngine.InputSystem;

public class Scenario2To1TransitionerBehavior : MonoBehaviour
{
    //A container to hold the Camera that this Transitioner will switch to when activated
    public Camera targetCamera;
    //a reference to the Game Manager. Allows us to reference the active Mail.
    GameObject gameManager;
    /*This fct activates on Player Interact. Changes active Camera so we can
     * switch 'Scenarios' */
    public void changeScenario(InputAction.CallbackContext actionInfo)
    {
        //An abstraction of the mouse's screenspace coords as passed by the callback context
        float currentMouseX = actionInfo.ReadValue<Vector2>().x;
        float currentMouseY = actionInfo.ReadValue<Vector2>().y;
        //Debug.Log("Callback context carried " + actionInfo.ReadValue<Vector2>() + " as mouse pos");
        //An abstraction of the current active Main Camera
        Camera activeCam = Camera.main;
        //The current Main Camera's conversion of mouse screenpoint to worldpoint
        Vector3 currentMouseWorldPoint = activeCam.ScreenToWorldPoint(new Vector3(currentMouseX, currentMouseY, 0));
        //If mouse is colliding with this Transitioner at the time of click...
        if (this.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)))
        {
            //Disable Camera component of the active Cam
            activeCam.GetComponent<Camera>().enabled = false;
            //And disable the Audio Listener component of the active Cam
            activeCam.GetComponent<AudioListener>().enabled = false;
            //And enable the Camera component of targetCam
            targetCamera.GetComponent<Camera>().enabled = true;
            //And enable the Audio Listener component of the targetCam
            targetCamera.GetComponent<AudioListener>().enabled = true;

            //EMPTY PLANTERS

            //iterate over all the planters in the planterList
            for (int currentIndex = 0; currentIndex < gameManager.GetComponent<GameManagerInfo>().planterList.Count; currentIndex =currentIndex + 1)
            {
                //temp var for referencing the current Planter being operated on
                GameObject currentPlanter = gameManager.GetComponent<GameManagerInfo>().planterList[currentIndex];
                //empty the currentPlanter
                currentPlanter.GetComponent<PlanterBehavior>().emptyPlanter();
            }

            //TURN ON THE VICTORY WINDOW OF THE ACTIVE MAIL

            //set the Victory Window to active
            gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().victoryWindowSegment.SetActive(true);
            //set its worldspace position equal to the home pos that was stored by Scenario1To2 Transitioner
            gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().victoryWindowSegment.GetComponent<Transform>().position = gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowHomePos;
            //set its scale equal to the home scale that was stored by Scenario1To2 Transitioner
            gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().victoryWindowSegment.GetComponent<Transform>().localScale = gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowHomeScale;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assign reference to Game Manager
        gameManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        //A temp var that calculates EVERY FRAME how many matching Instruments we have
        int correctInstruments = 0;
        //Every frame, check if the Planters contain all 4 Instruments the Song contains

        //IF there is an activeMail (prevents Console from spamming with NullRef errors)
        if (gameManager.GetComponent<GameManagerInfo>().activeMail != null)
        {
            //First iterate over the instrumentsInSong in the activeMail's song
            for (int currentIndex = 0; currentIndex < gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().song.GetComponent<SongBehavior>().instrumentsInSong.Length; currentIndex = currentIndex + 1)
            {
                //A temp var for storing the songInstrument we're examining
                GameObject songInstrument = gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().song.GetComponent<SongBehavior>().instrumentsInSong[currentIndex];
                //A temp flag for letting us know if we counted this songInstrument toward our total yet
                bool counted = false;
                //Next iterate over every Planter, checking if its instrumentInPot is equal to this songInstrument
                for (int currentIndex2 = 0; currentIndex2 < gameManager.GetComponent<GameManagerInfo>().planterList.Count; currentIndex2 = currentIndex2 + 1)
                {
                    //A temp var for storing the instrument in this Planter
                    GameObject planterInstrument = gameManager.GetComponent<GameManagerInfo>().planterList[currentIndex2].GetComponent<PlanterBehavior>().instrumentInPot;
                    //As long as we haven't already counted a planter instrument towards this songInstrument
                    if(counted == false)
                    {
                        //Check if the Instrument in this Pot matches the current songInstrument
                        if (planterInstrument == songInstrument)
                        {
                            /*then flip the flag to let us know we have counted this songInstrument.
                             * This prevents duplicate Instruments from counting more than once*/
                            counted = true;
                            //And increase the correctInstruments count
                            correctInstruments = correctInstruments + 1;
                        }
                    }
                }
            }
        }
        //Debug.Log("Number of correct instruments counted this frame was " +  correctInstruments);
        //If, after counting all correct instruments, we have reached 4...
        if (correctInstruments >= 4)
        {
            //Then the Transitioner should turn its Sprite Renderer and Collider on
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
        //else, if the correctInstruments is less than 4...
        else
        {
            //Then the Transitioner should turn its Sprite Renderer and Collider off
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
