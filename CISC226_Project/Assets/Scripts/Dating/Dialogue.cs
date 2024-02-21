using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    // This field stores whether the player or npc is speaking.
    public bool is_npc;

    // This field tracks whether there is a branching choice option.
    public bool is_choice;

    [TextArea(3, 10)]               // Min, Max lines used.
    public string[] sentences;
}
