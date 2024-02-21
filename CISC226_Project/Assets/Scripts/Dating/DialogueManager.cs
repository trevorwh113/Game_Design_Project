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


    // The Dialogue speed used to determine the pause between characters.
    [SerializeField]
    public float display_speed;


    // Stores references to the 3 other story trigger panels.
    public GameObject story_1;
    public GameObject story_2;
    public GameObject story_3;
    public GameObject story_4;

    // Stores a reference to the continue button for dialogue.
    public GameObject continue_button;
    

    // Holds the strings to display in a queue.
    private Queue<string> sentences;
    // Holds the dialogues in a queue.
    private Queue<Dialogue> dialogues;

    // Stores reference to the player and NPC object.
    public GameObject player;
    public GameObject npc;

    // The NPC in question.
    public string npc_name;

    
    // Start is called before the first frame update
    void Start()
    {
        // Initializes the queue of strings.
        sentences = new Queue<string>();

        // Initializes the queue of dialogues.
        dialogues = new Queue<Dialogue>();

    }

    // Starts a full conversation composed of many character dialogues.
    public void StartConversation(List<Dialogue> conversation) {
        // Takes a list of dialogues and adds them to a queue.
        foreach (Dialogue dialogue in conversation) {
            dialogues.Enqueue(dialogue);
        }

        // Launches the first dialogue.
        if (dialogues.Count != 0) {
            StartDialogue(dialogues.Dequeue());
        }
    }

    // Starts a single dialogue (1 character).
    public void StartDialogue(Dialogue dialogue) {
        // Set the name of the dialogue panel.
        name_text.text = dialogue.name;

        // Activate the player/NPC based on the dialogue.
        if (dialogue.is_npc) {
            player.SetActive(false);
            npc.SetActive(true);
        }
        else {
            npc.SetActive(false);
            player.SetActive(true);
        }

        // Clear previous dialogue that was there.
        sentences.Clear();

        // Loads all strings from 'dialogue' into the queue.
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        // Displays the next sentences.
        DisplayNextSentence();

    }

    // Displays the next sentence in a dialogue. 
    public void DisplayNextSentence() {
        // If there are no sentences left, either end the conversation OR
        // move on to the next dialogue.
        if (sentences.Count == 0) {
            if (dialogues.Count == 0) {
                EndConversation();
            }
            else {
                StartDialogue(dialogues.Dequeue());
            }
            return;     // Stops before doing the next part.
        }

        // If there is more for the NPC to say, get the line.
        string sentence = sentences.Dequeue();
        // Makes sure nothing is still animating in case the 
        // Player skips ahead.
        StopAllCoroutines();
        // Updates the UI with that line.
        StartCoroutine(TypeSentence(sentence));
        
    }

    // Types the sentence one character at a time.
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


    void EndConversation() {
        // Clear the name and text.
        dialogue_text.text = "";
        name_text.text = "";
        
        // Disactivate the continue button.
        continue_button.SetActive(false);
        
        // Activate all the conversation buttons.
        story_1.SetActive(true);
        // Disactivate all the other buttons.
        story_2.SetActive(true);
        story_3.SetActive(true);
        story_4.SetActive(true);

        // Disactivate the player and activate the npc.
        player.SetActive(false);
        npc.SetActive(true);
        
    }
}
