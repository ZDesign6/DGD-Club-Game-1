using UnityEngine;
using UnityEngine.InputSystem;

public class Scenario1To2TransitionerBehavior : MonoBehaviour
{
    //A reference to the Game Manger so we can check the active Sonic Mail.
    GameObject gameManager;
    //A reference to the Mail Cubby so we can move the active Sonic Mail to it.
    GameObject mailCubby;
    //a reference to the SeedSelector Cubby so we can tell it to intake the new Seeds
    GameObject seedSelectorCubby;
    //A container to hold the Camera that this Transitioner will switch to when activated
    public Camera targetCamera;
    /*This fct activates on Player Interact. Changes active Camera
     * and passes the activeMail into the Mail Cubby in Scneario 2.*/
    public void changeScenario(InputAction.CallbackContext actionInfo)
    {
        //An abstraction of the mouse's screenspace coords as passed by the callback context
        float currentMouseX = actionInfo.ReadValue<Vector2>().x;
        float currentMouseY = actionInfo.ReadValue<Vector2>().y;
        //Debug.Log("Callback context carried " + actionInfo.ReadValue<Vector2>() + " as mouse pos");
        //Grab the current main Camera
        Camera activeCam = Camera.main;
        //The current Main Camera's conversion of mouse screenpoint to worldpoint
        Vector3 currentMouseWorldPoint = activeCam.ScreenToWorldPoint(new Vector3(currentMouseX, currentMouseY, 0));
        //If mouse is colliding with this Transitioner at the time of click...
        if (this.GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(currentMouseWorldPoint.x, currentMouseWorldPoint.y)))
        {
            //pass the activeMail from Game Manager to Mail Cubby
            passMailToCubby();
            //Let the Seed Selector know to intake the Seeds from the new Mail
            seedSelectorCubby.GetComponent<SeedSelectorCubbyBehavior>().intakeSeeds();
            //Disable Camera component of the active Cam
            activeCam.GetComponent<Camera>().enabled = false;
            //And disable the Audio Listener component of the active Cam
            activeCam.GetComponent<AudioListener>().enabled = false;
            //And enable the Camera component of targetCam
            targetCamera.GetComponent<Camera>().enabled = true;
            //And enable the Audio Listener component of the targetCam
            targetCamera.GetComponent<AudioListener>().enabled = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assign reference to the Game Manager
        gameManager = GameObject.Find("Game Manager");
        //Assign reference to the Mail Cubby
        mailCubby = GameObject.Find("Mail Cubby");
        //Assign reference to the Seed Selector
        seedSelectorCubby = GameObject.Find("Seed Selector Cubby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Handles passing the activeMail in Scenario 1 to the Mail Cubby in Scenario 2
    void passMailToCubby()
    {
        //Store the Window's current Worldspace Pos in the activeMail so it can be reset later by Scenario2To1Transitioner
        gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowHomePos = new Vector3(gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().position.x, gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().position.y, gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().position.z);
        //Store the Window's current Scale in the activeMail so it can be reset later by Sceario2To1 Transitioner
        gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowHomeScale = new Vector3(gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().localScale.x, gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().localScale.y, gameManager.GetComponent<GameManagerInfo>().activeMail.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().localScale.z);
        //set the mailInCubby container in the Mail Cubby equal to the activeMail in the Game Manager
        mailCubby.GetComponent<MailCubbyBehavior>().mailInCubby = gameManager.GetComponent<GameManagerInfo>().activeMail;
        //Then disable its Window
        mailCubby.GetComponent<MailCubbyBehavior>().mailInCubby.GetComponent<SonicMailBehavior>().windowSegment.SetActive(false);
        Debug.Log("PASSED " + gameManager.GetComponent<GameManagerInfo>().activeMail + " TO MAIL CUBBY");
    }
}
