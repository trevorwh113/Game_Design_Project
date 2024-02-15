using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Tracks the player's affection level with each character.
    public int affection_worm = 0;
    public int affection_bird = 0;
    public int affection_croc = 0;

    // Tracks the coins that the player has.
    public int coins = 0;


    // // Ensure the object this is attached to is never cleared.
    // private void Start() {
    //     // Self-destruct if there is a duplicate copy.
    //     PlayerInfo instance = FindObjectOfType<PlayerInfo>();
    //     if (instance != null && instance != this) {
    //         Destroy(gameObject);
    //     }
        
    //     // Otherwise, make sure it does not get unloaded.
    //     DontDestroyOnLoad(gameObject);

    // }


}
