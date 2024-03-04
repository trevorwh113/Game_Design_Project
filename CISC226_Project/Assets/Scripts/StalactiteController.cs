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

    // Used to get the tile the player is on and reset the scene.
    private CharacterInfo character;
    private LevelManager levelManager;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer of the stalactite
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Start crystal as first sprite
        spriteRenderer.sprite = spriteArray[currentSprite];

        // Get the levelManager and the character so that
        // Each instance of stalactite doesn't need to save it.
        character = FindObjectOfType<CharacterInfo>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    void LateUpdate()
    {
        // Find the location of the stalactite and the tile under it
        Vector2 pos = transform.position;
        var hit = cursor.GetComponent<MouseController>().GetTileAtPos(pos);
        if (hit.HasValue) {
            tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If echolocated change sprite to damaged version
        if(tile != null && tile.echolocated)
        {
            // Make sure stalactite is not completely broken already
            // Make sure sprite has not already changed (wait)
            if(currentSprite < 4 && !wait)
            {
                changeSprite();
                wait = true;

                // Kill the player if the sprite updated to 4 and player is on tile.
                if (currentSprite == 4 && tile == character.onTile) {
                    levelManager.ResetLevel();
                }
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
        if (currentSprite == 4)
        {
            spriteRenderer.sortingLayerID = SortingLayer.NameToID("character");
            tile.isBlocked = true;
        }
    }
}
