using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGoToScene : MonoBehaviour
{
    // Scene to go to
    public string destination;

    // Function to transition to the scene.
    public void GoToScene() {
        // Does not attempt to ensure that the destination is a valid scene name.
        SceneManager.LoadScene(destination);
    }

    public void quitGame() {
        Application.Quit();
    }
}
