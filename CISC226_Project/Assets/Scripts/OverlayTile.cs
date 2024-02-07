using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;

    public int F {get { return G+H;}}

    public bool isBlocked;
    public OverlayTile previous;
    public Vector3Int gridLocation; 

    void Update()
    {
        
    }
    public void showTile(){
        // sets transparency to full
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
    public void hideTile(){
        // sets transparency to none; invisible
        gameObject.GetComponent<SpriteRenderer>().color =new Color(1,1,1,0);
    }
}
