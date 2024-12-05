using JetBrains.Annotations;
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
                //Assign that Seed to seedInPot
                seedInPot = gameManager.GetComponent<GameManagerInfo>().grabbedSeed;
                Debug.Log("PUT " + seedInPot + " IN THE PLANTER");
            }
            else
            {
                Debug.Log("PLANTER DID NOT SEE A SEED BEING GRABBED");
            }
        }
    }
    // Update is called once per frame
    void Start()
    {
        //Assign reference to Game Manager
        gameManager = GameObject.Find("Game Manager");
    }
    void Update()
    {
      
          /*
          if (seedInPot==null)
          {
          //never run this void event again lol
          return
          }
          */
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
                    //grabs the information of the instrumentin the seed in the seed controller
                    GameObject instrumentLocal=seedInPot.GetComponent<SeedController>().instrumentinseed;
                    //move that Instrument to the Planter's pos
                    instrumentLocal.GetComponent<Transform>().position = planterPosition;
                    //Then turn its Sprite Renderer on
                    instrumentLocal.GetComponent<SpriteRenderer>().enabled = true;
                    //And set its volume to 1
                    instrumentLocal.GetComponent<AudioSource>().volume = 1.0f;
                    //destroys the seed in the pot
                    Destroy(seedInPot);
                }
          }
    }

}
