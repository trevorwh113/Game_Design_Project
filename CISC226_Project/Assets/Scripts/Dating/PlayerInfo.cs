using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    // Tracks the player's affection level with each character.
    public int affection_worm = 0;
    public int affection_bird = 0;
    public int affection_croc = 0;

    // Tracks the coins that the player has.
    public int coins = 0;


    private void Start() {
      
        // Make sure it does not get unloaded.
        DontDestroyOnLoad(gameObject);

        // Transition to another scene.
        SceneManager.LoadScene("intro_lvl");

    }


}
