using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public List<DialogueLine> dialogue;

    void OnMouseDown()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}