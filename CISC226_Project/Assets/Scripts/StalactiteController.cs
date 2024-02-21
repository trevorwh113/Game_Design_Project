using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteController : MonoBehaviour
{   
    // Create an object referencce for sprite renderer.
    // This is what changes the sprites
    public SpriteRenderer spriteRenderer;

    // Create an array for the sprites, set current sprite to the first one
    public Sprite[] spriteArray;
    int currentSprite = 0;

    // Flag to only change sprite once per echolocation.
    bool wait = false;

    // The tile that the stalactite is on
    OverlayTile tile;

    // This is used in finding the overlay tile??
    public MouseController cursor;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer of the stalactite
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Start crystal as first sprite
        spriteRenderer.sprite = spriteArray[currentSprite];
    }

    void LateUpdate()
    {
        // Find the location of the stalactite and the tile under it
        Vector2 pos = transform.position;
        var hit = cursor.GetComponent<MouseController>().GetTileAtPos(pos);
        tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
    }

    // Update is called once per frame
    void Update()
    {
        // If echolocated change sprite to damaged version
        if(tile.echolocated)
        {
            // Make sure stalactite is not completely broken already
            // Make sure sprite has not already changed (wait)
            if(currentSprite < 4 && !wait)
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
    }
}
