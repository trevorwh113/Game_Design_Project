using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CondtionalGoTo : MonoBehaviour
{
    // Scene to go to
    public string regularDest;
    public string sometimesDest;
    private PlayerInfo player;



    // Function to transition to the scene.

    public void chooseScene() {
        player = FindObjectOfType<PlayerInfo>();
        // Does not attempt to ensure that the destination is a valid scene name.
        if (player.tutLvl.Contains(sometimesDest)){
            SceneManager.LoadScene(regularDest);
        } else {
            player.tutLvl.Add(sometimesDest);
            SceneManager.LoadScene(sometimesDest);
        }
        
    }

    public void quitGame() {
        Application.Quit();
    }
}
