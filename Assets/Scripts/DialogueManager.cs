using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float typeSpeed;

    Queue<DialogueLine> lines = new Queue<DialogueLine>();

    void Awake()
    {
        if(instance != null)
        Destroy(this);

        instance = this;
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(List<DialogueLine> dialogue)
    {
        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach (var line in dialogue)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char character in line)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
    
    void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();
        speakerText.text = line.speaker;
        TypeLine(line.text);
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}