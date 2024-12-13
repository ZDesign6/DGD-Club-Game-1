using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanterBehavior : MonoBehaviour
{
    //represents the seed that is in this pot
    public GameObject seedInPot;
    //represents the Text for the Timer
    public GameObject TimerText;
    //Reference to Game Manager for checking grabbed Seeds
    GameObject gameManager;
    //This is a container that should only be filled when the Seed in the Pot has matured into an Instrument
    public GameObject instrumentInPot;
    /*This fct is called when Player clicks. */
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
        //Now we check if this Planter's Collider is colliding with Mouse in Worlspace
        if (this.GetComponent<PolygonCollider2D>().OverlapPoint(mouseWorldPos) == true)
        {
            Debug.Log("CLICKED ON A PLANTER");
            //As long as there is a Seed being currently grabbed
            if (gameManager.GetComponent<GameManagerInfo>().grabbedSeed != null)
            {
                //Check if there is already an instrument in the Pot...
                if (instrumentInPot != null) 
                {
                    //Turn off the Sprite Renderer for the Instrument that might be in the Pot
                    instrumentInPot.GetComponent<SpriteRenderer>().enabled = false;
                    //Set the Instrument in the Pot's volume to 0
                    instrumentInPot.GetComponent<AudioSource>().volume = 0f;
                    //Finally, clear out any instrument that might have been in the Pot
                    instrumentInPot = null;
                }
                //Assign the grabbed Seed to seedInPot
                seedInPot = gameManager.GetComponent<GameManagerInfo>().grabbedSeed;
                Debug.Log("PUT " + seedInPot + " IN THE PLANTER");
            }
            //Else if there is no grabbed Seed
            else
            {
                //Turn off the Sprite Renderer for the Instrument in the Pot
                instrumentInPot.GetComponent<SpriteRenderer>().enabled = false;
                //Set the Instrument in the Pot's volume to 0
                instrumentInPot.GetComponent<AudioSource>().volume = 0f;
                //clear out the instrument from the Pot
                instrumentInPot = null;
                //And the Seed from the Pot
                seedInPot = null;
            }

        }
    }
    //Start is called once on instantiation
    void Start()
    {
        //Assign reference to Game Manager
        gameManager = GameObject.Find("Game Manager");
        //Add self to the Game Manager's list of Planters
        gameManager.GetComponent<GameManagerInfo>().planterList.Add(this.gameObject);
    }
    void Update()
    {
          //if there is a Seed in this Planter
          if (seedInPot != null)
          {
                //raises the currentgrowth variable by one every frame
                seedInPot.GetComponent<SeedController>().currentgrowth+=1;
                //turns the frame count into second count for players
                float growthInSeconds = seedInPot.GetComponent<SeedController>().currentgrowth / 60;
                //Show the growth progress through the Text Mesh
                TimerText.GetComponent<TextMeshProUGUI>().text=growthInSeconds.ToString() + " / " + seedInPot.GetComponent<SeedController>().growthtime / 60;
                //if current growth variable is greater or equal to the set growth time do these things
                if (seedInPot.GetComponent<SeedController>().currentgrowth==seedInPot.GetComponent<SeedController>().growthtime)
                {
                    //Store this Planter's current pos
                    Vector3 planterPosition = this.gameObject.GetComponent<Transform>().position;
                    //grabs the Instrument from the Seed and store it in the Pot
                    instrumentInPot=seedInPot.GetComponent<SeedController>().instrumentinseed;
                    //move the Instrument to an offest relative to the planter's Position
                    instrumentInPot.GetComponent<Transform>().position = new Vector3(planterPosition.x, planterPosition.y + 1.5f, planterPosition.z - 1);
                    //Then turn its Sprite Renderer on
                    instrumentInPot.GetComponent<SpriteRenderer>().enabled = true;
                    //And set its volume to 1
                    instrumentInPot.GetComponent<AudioSource>().volume = 1.0f;
                }
          }
    }

}
