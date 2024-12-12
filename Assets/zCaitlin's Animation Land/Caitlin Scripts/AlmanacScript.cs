using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AlmanacScript : MonoBehaviour
{

    //Descriptions for the left and right pages
    [SerializeField] private List<String> descriptions;
    [SerializeField] private List<String> descriptions2;
    //Images to correspond with the descriptions
    [SerializeField] private List<Sprite> seedImg;
    [SerializeField] private List<Sprite> seedImg2;
    //Pg you are on
    public int pgNum = 0;
    private Animator anim;

    [SerializeField] private TextMeshPro desc1;
    [SerializeField] private TextMeshPro desc2;
    
    [SerializeField] SpriteRenderer  img1;
    [SerializeField] SpriteRenderer  img2;

    [SerializeField] private GameObject content;
    
    public void Start()
    {
        anim = GetComponent<Animator>();
        UpdateContent();
    }

   public void TurnLeft()
    {
        anim.Play("RightPage");
        pgNum++;
        UpdateContent();
    }

    public void TurnRight()
    {
        anim.Play("LeftPage");
        pgNum--;
        UpdateContent();
    }
    
    public void ShowContent(bool show)
    {
        content.SetActive(show);
    }
    public void UpdateContent()
    {
        //I don't understand unity 6 and my ternaries are failing me please dont look at this
     
        
        //update all the descriptions
        desc1.text = descriptions[pgNum];
        desc2.text = descriptions2[pgNum];

        img1.sprite = seedImg[pgNum];
        img2.sprite = seedImg2[pgNum];
    }
    
  
    
}
