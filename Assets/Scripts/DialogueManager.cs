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
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextMessage();
        }
    }

    public void StartDialogue(List<DialogueLine> dialogue)
    {
        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach (DialogueLine line in dialogue)
            lines.Enqueue(line);

        DisplayNextMessage();
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char character in line)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(typeSpeed);
        }

        if(dialogueText.text != line)
        {
            StopAllCoroutines();
            StartCoroutine(TypeLine("You talk too fast..."));
        }
    }
    
    void DisplayNextMessage()
    {
        dialogueText.text = "";
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine message = lines.Dequeue();
        speakerText.text = message.speaker;
        StartCoroutine(TypeLine(message.text));
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}