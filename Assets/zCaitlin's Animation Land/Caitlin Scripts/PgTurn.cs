using System;
using UnityEngine;

public class PgTurn : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private AlmanacScript _almanacScript;
    
    private void OnMouseDown()
    {
       
            if (isLeft &&  _almanacScript.pgNum < 3)
            {
                _almanacScript.TurnLeft();
            }
            else if (!isLeft &&  _almanacScript.pgNum > 0)
            {
                _almanacScript.TurnRight();
            }
        
        
       
       
    }
}
