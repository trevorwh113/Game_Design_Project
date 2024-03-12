using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    // Create an object referencce for sprite renderer.
    // This is what changes the sprites
    public SpriteRenderer spriteRenderer;

    // Create an array for the sprites, set current sprite to the first one
    public Sprite[] spriteArray;
    int currentSprite = 0;

    // Flag to only change sprite once per echolocation.
    bool wait = false;

    bool broken = false;

    // The tile that the crystal is on
    OverlayTile tile;

    // This is used in finding the overlay tile??
    public MouseController cursor;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer of the crystal
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Start crystal as first sprite
        spriteRenderer.sprite = spriteArray[currentSprite];
        
    }

    void LateUpdate()
    {   
        // Find the location of the crystal and the tile under it
        Vector2 pos = transform.position;
        var hit = cursor.GetComponent<MouseController>().GetTileAtPos(pos);
        if (hit.HasValue) {
            tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
        }

        
        // Safety guard to make sure there is always a tile we're working with.
        if (tile != null) {
            tile.isBlocked = true;
            if(broken)
            {
                tile.isBlocked = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        // If echolocated change sprite to damaged version
        if(tile != null && tile.echolocated)
        {
            // Make sure crystal is not completely broken already
            // Make sure sprite has not already changed (wait)
            if(currentSprite < 3 && !wait)
            {
                changeSprite();
                wait = true;
            }
        }
        // Once echolocation stops, sprite can change again
        else
        {
            wait = false;
        }
    }

    // When called, go to the next sprite in the array
    void changeSprite()
    {
        currentSprite++;
        spriteRenderer.sprite = spriteArray[currentSprite];
        if (currentSprite == 3)
        {
            spriteRenderer.sortingLayerID = SortingLayer.NameToID("ground");
            broken = true;

            // Break it in the levelManager.
            FindObjectOfType<LevelManager>().breakCrystal();
        }
    }
}
