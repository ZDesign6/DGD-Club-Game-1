using UnityEngine;

public class MailCubbyBehavior : MonoBehaviour
{
    //A reference to the Game Manager so we can check for the active Mail
    public GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assign the Game Manager to the container
        gameManager = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        //Every frame, attempt to iterate over the Game Manager's list of Sonic Mails...
        for (int currentIndex = 0; currentIndex < gameManager.GetComponent<GameManagerInfo>().mailList.Count; currentIndex = currentIndex + 1)
        {
            //Checking if any of them are currently enabled...

        }
    }
}
