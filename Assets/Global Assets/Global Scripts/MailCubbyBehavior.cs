using UnityEngine;
using UnityEngine.InputSystem;

public class MailCubbyBehavior : MonoBehaviour
{
    /*A container for the current active SonicMail from Scenario 1.
     * Do not ever fill this manually, it should be set by the 
     * Scene1To2TransitionerBehavior Script*/
    public GameObject mailInCubby;
    //Keeps track of if a Mail is currently showing or not
    bool showingMail = false;
    //A reference to the SeedSelectorCubby so we can tell it to IntakeSeeds
    GameObject seedCubby;
    /*This fct is called when Player clicks. */

    public Animator windowAnim;
    
    public void OnClick(InputAction.CallbackContext actionInfo)
    {
        //COLLISION
        //First we grab the active Camera so it can convert Screen to World for us
        Camera activeCam = Camera.main;
        //Then we store the Mouse's Screenspace X/Y as passed by CallbackContext
        float mouseScreenX = actionInfo.ReadValue<Vector2>().x;
        float mouseScreenY = actionInfo.ReadValue<Vector2>().y;
        //Next we convert the Screen coords to World coords and store them
        Vector3 mouseWorldPos = activeCam.ScreenToWorldPoint(new Vector3(mouseScreenX, mouseScreenY, 0));
        //Now we check if the Cubby's Collider is colliding with Mouse in Worlspace
        if (this.GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPos) == true)
        {
            //If a mail is not currently showing...
            if (showingMail == false)
            {
                Debug.Log("Showing Mail");
                //Show the Mail at the given pos
                mailInCubby.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x + 5, this.gameObject.GetComponent<Transform>().position.y + 5, this.gameObject.GetComponent<Transform>().position.z - 5);
                //With scale of 1
                //mailInCubby.GetComponent<SonicMailBehavior>().windowSegment.GetComponent<Transform>().localScale = new Vector3(10.65f, 6.75f, 1);
                //If Player clicks on the Mail Cubby, turn the Window component of the Sonic Mail on
                mailInCubby.GetComponent<SonicMailBehavior>().windowSegment.SetActive(true);
                //Finally, flip showingMail
                showingMail = true;
                //Play animation
                windowAnim.Play("WindowAgain");
            }
            //else if a mail IS showing
            else
            {
                Debug.Log("Hiding Mail");
                //If Player clicks on the Mail Cubby, turn the Window component of the Sonic Mail off
                mailInCubby.GetComponent<SonicMailBehavior>().windowSegment.SetActive(false);
                //Finally, flip showingMail
                showingMail = false;
            }
        }
            
    }
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assigns ref to the Seed Selector Cubby
        seedCubby = GameObject.Find("Seed Selector Cubby");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
