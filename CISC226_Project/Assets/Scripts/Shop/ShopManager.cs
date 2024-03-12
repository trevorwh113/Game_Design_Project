using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // Reference to the player info (for affection levels).
    private PlayerInfo playerInfo;
    
    // Reference to the textbox name and item fields.
    public TextMeshProUGUI text_field;
    public TextMeshProUGUI name_field;

    public GameObject confirm_button;



    // Various default messages.
    public string greeting_message;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the player info instance.
        playerInfo = FindObjectOfType<PlayerInfo>();

        // Display a greeting message. Hide what shouldn't be there.
        name_field.text = "";
        text_field.text = greeting_message;  
        confirm_button.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
