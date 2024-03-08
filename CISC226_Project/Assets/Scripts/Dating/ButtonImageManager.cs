using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageManager : MonoBehaviour
{
    // Store the dialogue trigger it is attached too.
    private DialogueTrigger button;

    // Store the two images.
    public Sprite locked;
    public Sprite unlocked;
    

    // The text field.
    public TextMeshProUGUI text_field;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Update the locked buttons to be locked.
        button = gameObject.GetComponent<DialogueTrigger>();
        
        DialogueManager dialogueManager= FindObjectOfType<DialogueManager>();
        if (dialogueManager != null) {

            // Updates the look of the buttons.
            if (!button.doesStoryUnlock(dialogueManager)) {
                button.GetComponent<Image>().sprite = locked;       // Sets the image
                text_field.text = "-------";                               // Clears the text
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
