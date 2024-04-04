using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FindUIPosition : MonoBehaviour
{
    // Main Camera
    private UnityEngine.Camera cam;

    // The tile that this UI element is above.
    public OverlayTile onTile;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the camera.
        cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the tile that this UI element is under each time.
        if (cam != null) {
            var UIhit = GetTileAtPos(transform.position);
            if (UIhit != null) {
                OverlayTile UItile = UIhit.Value.collider.gameObject.GetComponent<OverlayTile>();

                onTile = UItile;
            }
        }
    }


    // Duplicate of a function in mouse controller.
    public RaycastHit2D? GetTileAtPos(Vector2 pos){
        // list of objects in raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending( i => i.collider.transform.position.z).First();
        }

        return null;
    }

}
