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
    //A container for this Seed's "home coords", set when the Seed enters the Seed Selector
    public Vector3 homePos;
   /*This fct activates on Player Clicking */
   public void onClick(InputAction.CallbackContext actionInfo)
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
            Debug.Log("CLICKED A SEED");
            //If this Seed is not currently grabbed, make it grabbed
            if (gameManager.GetComponent<GameManagerInfo>().grabbedSeed != this.gameObject)
            {
                //Make this Seed the grabbed Seed
                gameManager.GetComponent<GameManagerInfo>().grabbedSeed = this.gameObject;
            }
            //else if it is already the grabbed Seed...
            else
            {
                //Mark this Seed as no longer grabbed
                gameManager.GetComponent<GameManagerInfo>().grabbedSeed = null;
                //And return this Seed to its homePos
                this.gameObject.GetComponent<Rigidbody2D>().position = homePos;
            }
            
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
    }
}
