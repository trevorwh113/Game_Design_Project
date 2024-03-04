using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTile : MonoBehaviour
{
    // The tile this is on.
    private OverlayTile tile;

    // This is used in finding the overlay tile??
    public MouseController cursor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Avoid doing this over and over again.
        if (tile == null) {

            // Find the location of the goal tile on the grid.
            Vector2 pos = transform.position;
            var hit = cursor.GetComponent<MouseController>().GetTileAtPos(pos);
            if (hit.HasValue) {
                tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();

                // Set it to a goal tile.
                tile.is_goal_tile = true;
            }
        }
    }
}
