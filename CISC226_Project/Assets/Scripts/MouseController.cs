using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // usinh late update so it occurs after overlay update but this is a lazy way
    // so we might need to make an event handler system in the future
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();

        if(focusedTileHit.HasValue){
            GameObject overlayTile = focusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            
            if (Input.GetMouseButtonDown(0)){
                // should be this?: dk how to fix: overlayTile.GetComponent<Overlay>().showTile();
                // just copied and pasted code from the function here lol
                overlayTile.GetComponent<SpriteRenderer>().color =new Color(1,1,1,1);
            }
        }

        
    }

    public RaycastHit2D? GetFocusedOnTile(){
        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //convert mouse position vector into 2d object
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        // list of objects in raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending( i => i.collider.transform.position.z).First();
        }

        return null;
    }
}
