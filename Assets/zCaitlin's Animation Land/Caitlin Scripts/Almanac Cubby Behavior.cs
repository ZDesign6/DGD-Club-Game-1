using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NewMonoBehaviourScript : MonoBehaviour
{

   [SerializeField] private Animator almanacAnimator;
    //Is the almanac on screen?
   private bool isAwake = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //should I enter or exit the screen?
        var clip = isAwake ? "AlmanacExit" : "AlmanacEntrance";
        almanacAnimator.Play(clip);
        //update whether or not I am on screen
        isAwake = !isAwake;
    }
}
