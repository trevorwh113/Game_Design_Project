using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageManager : MonoBehaviour
{
    // Store the dialogue trigger it is attached too.
    private DialogueTrigger button;

    private DialogueManager dialogueManager;

    // Store the two images.
    public Sprite locked;
    public Sprite unlocked;
    

    // The text field.
    public TextMeshProUGUI text_field;
    // The default text that shows on an unlocked button.
    public string message;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Update the locked buttons to be locked.
        button = gameObject.GetComponent<DialogueTrigger>();
    
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null) {

            // Updates the look of the buttons.
            if (!button.previousStoriesRead(dialogueManager)) {
                button.GetComponent<Image>().sprite = locked;       // Sets the image
                text_field.text = "Read others";                               // Clears the text
            }
            else if (!button.doesStoryUnlock(dialogueManager)) {
                button.GetComponent<Image>().sprite = locked;       // Sets the image
                text_field.text = "Item needed";                               // Clears the text
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Updates to unlock the story if needed.
        if (dialogueManager != null) {
            if (button.previousStoriesRead(dialogueManager)) {
                if (!button.doesStoryUnlock(dialogueManager)) {
                    button.GetComponent<Image>().sprite = locked;       // Sets the image
                    text_field.text = "Item needed"; 
                }
                else {
                    button.GetComponent<Image>().sprite = unlocked;    
                    text_field.text = message; 
                }
            }
            
        }
    }
}
