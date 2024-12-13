using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SeedSelectorCubbyBehavior : MonoBehaviour
{
    //reference to the Mail Cubby. Assigned on Start()
    GameObject mailCubby;
    /* These variables allow us to manipulate where Seeds
     * should start spawning through the editor.
     * Offsets refer to how far left and how far up Seeds 
     * should spawn in comparison to the Cubby's coords */
    public float seedSpawnXOffset;
    public float seedSpawnYOffset;
    public float distanceBetweenSeeds;
    public float distanceBetweenRows;
    //List of all the Seeds in the SeedSelector
    public List<GameObject> seedsInSelector = new List<GameObject>();
    /* This fct takes in all the Seeds that are in the SonicMail 
     * stored in the Mail Cubby. Also sets their pos's. */
    public void intakeSeeds()
    {
        //Loop over the seeds in the Mail from the MailCubby...
        for (int currentIndex = 0; currentIndex < mailCubby.GetComponent<MailCubbyBehavior>().mailInCubby.GetComponent<SonicMailBehavior>().seedsInMail.Count; currentIndex = currentIndex + 1)
        {
            //And assign the index at seedsInSelector equal to a copy of that Seed
            seedsInSelector.Add(mailCubby.GetComponent<MailCubbyBehavior>().mailInCubby.GetComponent<SonicMailBehavior>().seedsInMail[currentIndex]);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assign the Mail Cubby object into the local reference
        mailCubby = GameObject.Find("Mail Cubby");
    }

    // Update is called once per frame
    void Update()
    {
        //Representation of the X where Seeds will appear
        float currentX = this.GetComponent<Transform>().position.x - seedSpawnXOffset;
        //Representation of the Y where Seeds will appear
        float currentY = this.GetComponent<Transform>().position.y + seedSpawnYOffset;
        //then operate over all the new Seeds
        for (int currentIndex = 0; currentIndex < seedsInSelector.Count; currentIndex = currentIndex + 1)
        {
            //turning on their SpriteRenderers
            seedsInSelector[currentIndex].GetComponent<SpriteRenderer>().enabled = true;
            //turning on their Colliders
            seedsInSelector[currentIndex].GetComponent<PolygonCollider2D>().enabled = true;
            //And moving them to currentX, currentY
            seedsInSelector[currentIndex].GetComponent<Transform>().position = new Vector3(currentX, currentY, -5);
            //Then increasing currentX before next loop
            currentX = currentX + distanceBetweenSeeds;
            //And if we are on the 4th Seed, decrease currentY and reset currentX before next loop
            if (currentIndex == 3)
            {
                currentX = this.GetComponent<Transform>().position.x - seedSpawnXOffset;
                currentY = currentY - distanceBetweenRows;
            }
            //Debug.Log("SEED SELECTOR TOOK IN " + seedsInSelector[currentIndex]);
        }
    }
}
