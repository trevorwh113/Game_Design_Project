using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // References to UI elements.
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI dialogue_text;

    // The Dialogue speed used to determien the pause between characters.
    [SerializeField]
    public float display_speed;
    

    // Holds the strings to display in a queue.
    private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initializes the queue of strings.
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        // Set the name of the dialogue panel.
        name_text.text = dialogue.name;

        // Clear previous dialogue that was there.
        sentences.Clear();

        // Loads all strings from 'dialogue' into the queue.
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        // Displays the next sentences.
        DisplayNextSentence();

    }

    public void DisplayNextSentence() {
        // Check if there are more sentences in the queue.
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        // If there is more for the NPC to say, get the line.
        string sentence = sentences.Dequeue();
        // Makes sure nothing is still animating in case the 
        // Player skips ahead.
        StopAllCoroutines();
        // Updates the UI with that line.
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence) {
        // Starts the UI as empty.
        dialogue_text.text = "";

        // Add in one character at a time.
        foreach (char letter in sentence.ToCharArray()) {
            dialogue_text.text += letter;
            // Waits a small amout of time.
            // Divides the speed by 100 to get the pause time.
            yield return new WaitForSeconds(display_speed / 100);  
        }
    }


    void EndDialogue() {
        Debug.Log("End conversation.");
    }
}
