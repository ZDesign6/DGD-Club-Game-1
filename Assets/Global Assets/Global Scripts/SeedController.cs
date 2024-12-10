using UnityEngine;
using UnityEngine.InputSystem;

public class SeedController : MonoBehaviour
{
   //A reference to the Instrument associated with this Seed.
   public GameObject instrumentinseed;
   //A representation of how long (in frames) this Seed should take to mature
   public float growthtime;
   /*A representation of how long (in frames) this Seed has been growing.
    * Modified by Planters while Seeds are inside of them */
   public float currentgrowth;
    //A reference to the Game Manager used to communicate when Seeds are grabbed.
    GameObject gameManager;
    //This boolean determines if a Click actuates anything
    bool isClickable = false;
   /*This fct activates on Player Clicking */
   public void onClick(InputAction.CallbackContext actionInfo)
    {
        //If the Seed is not on click cooldown...
        if (isClickable == true)
        {
            //COLLISION CHECK

            //First we grab the active Camera so it can convert Screen to World for us
            Camera activeCam = Camera.main;
            //Then we store the Mouse's Screenspace X/Y as passed by CallbackContext
            float mouseScreenX = actionInfo.ReadValue<Vector2>().x;
            float mouseScreenY = actionInfo.ReadValue<Vector2>().y;
            //Next we convert the Screen coords to World coords and store them
            Vector3 mouseWorldPos = activeCam.ScreenToWorldPoint(new Vector3(mouseScreenX, mouseScreenY, 0));
            //Now we check if this Seed's Collider is colliding with Mouse in Worlspace
            if (this.GetComponent<PolygonCollider2D>().OverlapPoint(mouseWorldPos) == true)
            {
                //turn on click cooldown
                isClickable = false;
                //and store the current frame in the GameManager
                gameManager.GetComponent<GameManagerInfo>().clickFrame = gameManager.GetComponent<GameManagerInfo>().frameCounter;
                Debug.Log("CLICKED A SEED");
                //If this Seed is not currently grabbed, make it grabbed
                if (gameManager.GetComponent<GameManagerInfo>().grabbedSeed != this.gameObject)
                {
                    //Make this Seed the grabbed Seed
                    gameManager.GetComponent<GameManagerInfo>().grabbedSeed = Instantiate(this.gameObject);
                }
                //else if it is already the grabbed Seed...
                else
                {
                    //exile the grabbed Seed to the shadow realm
                    gameManager.GetComponent<GameManagerInfo>().grabbedSeed.GetComponent<Transform>().position = new Vector3(1000, 1000, 1000);
                    //Mark this Seed as no longer grabbed
                    gameManager.GetComponent<GameManagerInfo>().grabbedSeed = null;
                }

            }
        }
        else
        {
            Debug.Log(this.gameObject.name + " is on click cooldown");
        }
    }
    public void Start()
    {
        gameManager = GameObject.Find("Game Manager");
    }
    public void Update()
    {
        //If the currently grabbed Seed is this one...
        if (gameManager.GetComponent<GameManagerInfo>().grabbedSeed == this.gameObject)
        {
            //First grab the active Camera for Screen to World conversion
            Camera activeCam = Camera.main;
            //Then set this Seed's pos equal to mouse's world pos
            this.gameObject.GetComponent<Rigidbody2D>().position = activeCam.ScreenToWorldPoint(Input.mousePosition);
        }
        //Every frame, check if 10 frames have elapsed since the last click
        if ( gameManager.GetComponent<GameManagerInfo>().frameCounter == gameManager.GetComponent<GameManagerInfo>().clickFrame + 10)
        {
            //If they have, set isClickable to true
            isClickable = true;
        }
    }
}
