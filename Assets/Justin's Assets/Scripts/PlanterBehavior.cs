using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlanterBehavior : MonoBehaviour
{
    //represents the seed that is in this pot
    public GameObject seedInPot;
    //represents the Text for the Timer
    public GameObject TimerText;
    //grabs the information of the instrumentin the seed in the seed controller
        GameObject instrument;
    // Update is called once per frame
    void Start()
    {
      //TimerText.GetComponent<RectTransform>()=seedInPot.GetComponent<Transform>();
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
      //if this thing does exist
      if (seedInPot != null)
      {
        //raises the currentgrowth variable by one every frame
        seedInPot.GetComponent<SeedController>().currentgrowth+=1;
        //turns the frame count into second count for players
        float growthInSeconds = seedInPot.GetComponent<SeedController>().currentgrowth / 60;
        //Connects the current growth to HUD timer
        TimerText.GetComponent<TextMeshProUGUI>().text=growthInSeconds.ToString();
        //if current growth variable is greater or equal to the set growth time do these things
        if (seedInPot.GetComponent<SeedController>().currentgrowth==seedInPot.GetComponent<SeedController>().growthtime)
        {
          //the seeds transform is equal the position of the seed in the pot
          Vector3 seedposition=seedInPot.GetComponent<Transform>().position;
          //grabs the information of the instrumentin the seed in the seed controller
          GameObject instrumentLocal=seedInPot.GetComponent<SeedController>().instrumentinseed;
          //creates the instrument in the seed and creates it in the same position of the pot
          Instantiate(instrumentLocal, seedposition, Quaternion.identity);

          instrument.GetComponent<SpriteRenderer>().enabled=true;
          
          //destroys the seed in the pot
          Destroy(seedInPot);
        }
      }
    }

}
