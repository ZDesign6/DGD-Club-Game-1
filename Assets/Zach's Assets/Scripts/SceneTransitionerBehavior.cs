using UnityEngine;
using UnityEngine.InputSystem;

public class SceneTransitionerBehavior : MonoBehaviour
{
    //A container to hold the Camera that this Transitioner will switch to when activated
    public Camera targetCamera;
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
            Debug.Log("MOUSE IS IN THE TRANSITIONER");
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
