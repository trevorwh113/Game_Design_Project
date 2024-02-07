using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            hideTile();
        }
    }
    public void showTile(){
        // sets transparency to full
        gameObject.GetComponent<SpriteRenderer>().color =new Color(1,1,1,1);
    }
    public void hideTile(){
        // sets transparency to none; invisible
        gameObject.GetComponent<SpriteRenderer>().color =new Color(1,1,1,0);
    }
}
