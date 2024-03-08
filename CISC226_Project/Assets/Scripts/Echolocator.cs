using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Echolocator : MonoBehaviour
{
    // Tracks whether the script is enabled or not.
    public bool is_enabled;
    
    // Constant for the mouse input button.
    private int RIGHT_CLICK = 1;

    // Variable to decide how long the echolocation will be.
    public int echo_length;

    // The amount of time the echo lasts, in seconds.
    public float echo_duration;

    private MouseController mouseController;


    // Varialbe used to store the list of tiles most recently echolocated.
    private RaycastHit2D[] echoTarget;

    // Constants for the echolocation cases.
    private int STRAIGHT = 0;
    private int DIAGONAL = 1;
    private int DOT = 2;
        

    void Start() {
        Enable();

        mouseController = FindObjectOfType<MouseController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Only do anything if the echolocator is enabled.
        if (is_enabled) {
            // On right-click, shoot out the echolocation beam and mark those tiles.
            if (Input.GetMouseButtonDown(RIGHT_CLICK)) {
                
                // Get the new list of tiles to ligth up.
                (RaycastHit2D[] tileList, int echoCase) focusedTileHitList = GetEchoBeamTiles();
                echoTarget = focusedTileHitList.tileList;

                // Mark the tiles are echolocated.
                MarkEchoedTiles(echoTarget);

                StartCoroutine("Freeze");
                 
            }

            // // When right-click is released, hide the tiles of echolocated.
            // if (Input.GetMouseButtonUp(RIGHT_CLICK)) {
            //     UnmarkEchoedTiles(echoTarget);
            // }
        }
    }


    private IEnumerator Freeze() {
        yield return new WaitForEndOfFrame();
        mouseController.Disable();
        yield return new WaitForSeconds(echo_duration);
        mouseController.Enable();
    }

    public void MarkEchoedTiles(RaycastHit2D[] tileList) {
        // Marks all the tiles in the list.
        foreach (RaycastHit2D? hit in tileList) {
            if(hit.HasValue){
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                
                // Mark the tiles neighbours.
                tile.MarkAdjacentAsEchoed(tile);
            }   
        }
    }


    public void UnmarkEchoedTiles(RaycastHit2D[] tileList) {
        // Unmarks all the tiles in the list.
        foreach (RaycastHit2D? hit in tileList) {
            if(hit.HasValue){
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                
                // Unmark the tiles neighbours.
                tile.UnmarkAdjacentAsEchoed(tile);
            }   
        }
    }


    // Generates the list of tiles under echolocation.
    public (RaycastHit2D[] , int) GetEchoBeamTiles(){
        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //convert mouse position vector into 2d object
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        // get the character position.
        Vector2 charPos2d = transform.position;

        // Get the direction vector.
        Vector2 difference = mousePos2d - charPos2d;

        // Use it to sort into a category.
        Vector2 echoDir;
        int echoCase;
        // Right
        if (difference.x > 0 && difference.y > -1 && difference.y < 1) {
            echoDir = new Vector2(1, 0);
            echoCase = STRAIGHT;
        }
        // Left
        else if (difference.x < 0 && difference.y > -1 && difference.y < 1) {
            echoDir = new Vector2(-1, 0);
            echoCase = STRAIGHT;
        }
        // Up
        else if (difference.x > -1 && difference.x < 1 && difference.y > 0) {
            echoDir = new Vector2(0, 1);
            echoCase = STRAIGHT;
        }
        // Down
        else if (difference.x > -1 && difference.x < 1 && difference.y < 0) {
            echoDir = new Vector2(0, -1);
            echoCase = STRAIGHT;
        }
        // Upper-left
        else if (difference.x > 0 && difference.y > 0) {
            echoDir = new Vector2(1, 1);
            echoCase = DIAGONAL;
        }
        // Lower-left
        else if (difference.x < 0 && difference.y < 0) {
            echoDir = new Vector2(-1, -1);
            echoCase = DIAGONAL;
        }
        // Upper-right
        else if (difference.x < 0 && difference.y > 0) {
            echoDir = new Vector2(-1, 1);
            echoCase = DIAGONAL;
        }
        // Lower-right
        else if (difference.x > 0 && difference.y < 0) {
            echoDir = new Vector2(1, -1);
            echoCase = DIAGONAL;
        }
        // On the character.
        else {
            echoDir = new Vector2(0,0);
            echoCase = DOT;
        }

        // list of objects in raycast
        // If the case is STRAIGHET, extend the beam by 1.
        if (echoCase == STRAIGHT) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length+1);
            return (hits, echoCase);
        }
        else {
            RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length);
            return (hits, echoCase);
        }


    }


    // Disables the echolocator.
    public void Disable() {
        is_enabled = false;
    }

    // Enables the echolocator.
    public void Enable() {
        is_enabled = true;
    }

}
