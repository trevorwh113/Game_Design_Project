using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Echolocator : MonoBehaviour
{
    // Tracks whether the script is enabled or not.
    private bool is_enabled;
    
    // Constant for the mouse input button.
    private int RIGHT_CLICK = 1;

    // Variable to decide how long the echolocation will be.
    public int echo_length;
    // Varialbe used to store the list of tiles most recently echolocated.
    private List<OverlayTile> echoTarget;

    // Constants for the echolocation cases.
    private int UP = 0;
    private int DOWN = 1;
    private int DOT = 2;
    private int LEFT = 3;
    private int RIGHT = 4;
    private int UP_RIGHT = 5;
    private int DOWN_RIGHT = 6;
    private int UP_LEFT = 7;
    private int DOWN_LEFT = 8;
        

    // Variable for the times to pause between echos and lighting up tiles.
    public float light_propagate_speed;
    public float light_linger;

    void Start() {
        Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Only do anything if the echolocator is enabled.
        if (is_enabled) {
            // On right-click, shoot out the echolocation beam and mark those tiles.
            if (Input.GetMouseButtonDown(RIGHT_CLICK)) {
                
                // Get the new list of tiles to ligth up.
                (List<OverlayTile> tileList, int echoCase) TileHitList = GetEchoBeamTiles();
                echoTarget = TileHitList.tileList;

                // Mark the tiles are echolocated.
                StartCoroutine("MarkEchoedTiles", echoTarget);
                // MarkEchoedTiles(echoTarget);

                StartCoroutine("DelayUnmarking", echoTarget);
                            
            }

            // // When right-click is released, hide the tiles of echolocated.
            // if (Input.GetMouseButtonUp(RIGHT_CLICK)) {
            //     StartCoroutine("UnmarkEchoedTiles", echoTarget);
            //     // UnmarkEchoedTiles(echoTarget);
            // }
        }
    }


    // public void MarkEchoedTiles(RaycastHit2D[] tileList) {
    //     // Marks all the tiles in the list.
    //     foreach (RaycastHit2D? hit in tileList) {
    //         if(hit.HasValue){
    //             OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                
    //             // Mark the tiles neighbours.
    //             tile.MarkAdjacentAsEchoed(tile);
    //         }   
    //     }
    // }

    private IEnumerator DelayUnmarking(List<OverlayTile> tileList) {
        yield return new WaitForSeconds(light_linger * echo_length);
        StartCoroutine("UnmarkEchoedTiles", tileList);
    }
    
    private IEnumerator MarkEchoedTiles(List<OverlayTile> tileList) {
        // Marks all the tiles in the list.
        foreach (OverlayTile tile in tileList) {
            if(tile != null){
                // Mark the tiles neighbours.
                yield return new WaitForSeconds(light_propagate_speed);
                tile.MarkAdjacentAsEchoed(tile);
                // tile.MarkTileAsEchoed();
            }   
        }
    }


    private IEnumerator UnmarkEchoedTiles(List<OverlayTile> tileList) {
        // Unmarks all the tiles in the list.
        foreach (OverlayTile tile in tileList) {
            if(tile != null){                
                // Unmark the tiles neighbours.
                yield return new WaitForSeconds(light_propagate_speed);
                tile.UnmarkAdjacentAsEchoed(tile);
                // tile.UnmarkTileAsEchoed();
            }   
        }
    }



    // OLD
    // // Generates the list of tiles under echolocation.
    // public (RaycastHit2D[] , int) GetEchoBeamTiles(){
    //     // get mouse position
    //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     //convert mouse position vector into 2d object
    //     Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

    //     // get the character position.
    //     Vector2 charPos2d = transform.position;

    //     // Get the direction vector.
    //     Vector2 difference = mousePos2d - charPos2d;

    //     // Use it to sort into a category.
    //     Vector2 echoDir;
    //     int echoCase;
    //     // Right
    //     if (difference.x > 0 && difference.y > -1 && difference.y < 1) {
    //         echoDir = new Vector2(1, 0);
    //         echoCase = STRAIGHT;
    //     }
    //     // Left
    //     else if (difference.x < 0 && difference.y > -1 && difference.y < 1) {
    //         echoDir = new Vector2(-1, 0);
    //         echoCase = STRAIGHT;
    //     }
    //     // Up
    //     else if (difference.x > -1 && difference.x < 1 && difference.y > 0) {
    //         echoDir = new Vector2(0, 1);
    //         echoCase = STRAIGHT;
    //     }
    //     // Down
    //     else if (difference.x > -1 && difference.x < 1 && difference.y < 0) {
    //         echoDir = new Vector2(0, -1);
    //         echoCase = STRAIGHT;
    //     }
    //     // Upper-left
    //     else if (difference.x > 0 && difference.y > 0) {
    //         echoDir = new Vector2(1, 1);
    //         echoCase = DIAGONAL;
    //     }
    //     // Lower-left
    //     else if (difference.x < 0 && difference.y < 0) {
    //         echoDir = new Vector2(-1, -1);
    //         echoCase = DIAGONAL;
    //     }
    //     // Upper-right
    //     else if (difference.x < 0 && difference.y > 0) {
    //         echoDir = new Vector2(-1, 1);
    //         echoCase = DIAGONAL;
    //     }
    //     // Lower-right
    //     else if (difference.x > 0 && difference.y < 0) {
    //         echoDir = new Vector2(1, -1);
    //         echoCase = DIAGONAL;
    //     }
    //     // On the character.
    //     else {
    //         echoDir = new Vector2(0,0);
    //         echoCase = DOT;
    //     }

    //     // list of objects in raycast
    //     // If the case is STRAIGHET, extend the beam by 1.
    //     if (echoCase == STRAIGHT) {
    //         RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length+1);
    //         return (hits, echoCase);
    //     }
    //     else {
    //         RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length);
    //         return (hits, echoCase);
    //     }


    // }

    // NEW
    // Generates the list of tiles under echolocation.
    public (List<OverlayTile> , int) GetEchoBeamTiles(){
        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //convert mouse position vector into 2d object
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        // get the character position.
        Vector2 charPos2d = transform.position;

        // Get the direction vector.
        Vector2 difference = mousePos2d - charPos2d;

        // Use it to sort into a category.
        Vector2Int echoDir;
        int echoCase;

        // Right
        if (difference.x > 0 && difference.y > -1 && difference.y < 1) {
            echoDir = new Vector2Int(1, 0);
            echoCase = RIGHT;
        }
        // Left
        else if (difference.x < 0 && difference.y > -1 && difference.y < 1) {
            echoDir = new Vector2Int(-1, 0);
            echoCase = LEFT;
        }
        // Up
        else if (difference.x > -1 && difference.x < 1 && difference.y > 0) {
            echoDir = new Vector2Int(0, 1);
            echoCase = UP;
        }
        // Down
        else if (difference.x > -1 && difference.x < 1 && difference.y < 0) {
            echoDir = new Vector2Int(0, -1);
            echoCase = DOWN;
        }
        // Upper-left
        else if (difference.x > 0 && difference.y > 0) {
            echoDir = new Vector2Int(1, 1);
            echoCase = UP_LEFT;
        }
        // Lower-left
        else if (difference.x < 0 && difference.y < 0) {
            echoDir = new Vector2Int(-1, -1);
            echoCase = DOWN_LEFT;
        }
        // Upper-right
        else if (difference.x < 0 && difference.y > 0) {
            echoDir = new Vector2Int(-1, 1);
            echoCase = UP_RIGHT;
        }
        // Lower-right
        else if (difference.x > 0 && difference.y < 0) {
            echoDir = new Vector2Int(1, -1);
            echoCase = DOWN_RIGHT;
        }
        // On the character.
        else {
            echoDir = new Vector2Int(0,0);
            echoCase = DOT;
        }

        
        // Creates a list of tiles in the appropriate direction.
        OverlayTile start_tile = gameObject.GetComponent<CharacterInfo>().onTile;
        OverlayTile tile = start_tile;
        
        var map = MapManager.Instance.map;

        List<OverlayTile> echo_beam = new List<OverlayTile>();
        Vector2Int locationToCheck;
        echo_beam.Add(start_tile);


        // Shorten the range for diagonal cases by 1.
        int range = echo_length;
        if (echoCase == UP_RIGHT || echoCase == UP_LEFT 
            || echoCase == DOWN_RIGHT || echoCase == DOWN_LEFT) {
                range -= 1;
            }
        int i = 0;
        int x = echoDir.x;
        int y = echoDir.y;
        
        // Loop over all tiles in a line described by the length and the direction.
        while (!tile.isBlocked && i < range) {
            locationToCheck = new Vector2Int(start_tile.gridLocation.x + x, start_tile.gridLocation.y + y);
            
            if (map.ContainsKey(locationToCheck)) {
                    tile = map[locationToCheck];
                    echo_beam.Add(tile);
            }
            
            // Widen the brean on diagonals.
            if (echoCase == UP_RIGHT || echoCase == DOWN_RIGHT) {
                locationToCheck = new Vector2Int(start_tile.gridLocation.x + x + 1, start_tile.gridLocation.y + y);
            
                if (map.ContainsKey(locationToCheck)) {
                        echo_beam.Add(map[locationToCheck]);
                }
            }
            else if (echoCase == UP_LEFT || echoCase == DOWN_LEFT) {
                locationToCheck = new Vector2Int(start_tile.gridLocation.x + x - 1, start_tile.gridLocation.y + y);
            
                if (map.ContainsKey(locationToCheck)) {
                        echo_beam.Add(map[locationToCheck]);
                }
            }


            x += echoDir.x;
            y += echoDir.y;
            i++;
            
        }

        return (echo_beam, echoCase);


        // // list of objects in raycast
        // // If the case is STRAIGHET, extend the beam by 1.
        // if (echoCase == STRAIGHT) {
        //     RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length+1);
        //     return (hits, echoCase);
        // }
        // else {
        //     RaycastHit2D[] hits = Physics2D.RaycastAll(charPos2d, echoDir, distance:echo_length);
        //     return (hits, echoCase);
        // }


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
