using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public List<DialogueLine> dialogue;
    public List<DialogueLine> fufilledDialogue;
    public bool fufilled = false;

    public void StartConversation()
    {
        if(fufilled)
        {
            DialogueManager.instance.StartDialogue(fufilledDialogue);
        }
        else
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}