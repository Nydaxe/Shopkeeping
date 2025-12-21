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
    [SerializeField] AudioClip talkAudio;

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
        bool audioPlayed = false;

        dialogueText.text = "";

        foreach (char character in line)
        {
            dialogueText.text += character;
            
            if(Random.Range(1,4) >= 2 && audioPlayed == false)
            {
                if(speakerText.text == "You")
                {
                   AudioManager.instance.PlaySoundEffect(talkAudio, .6f, .7f, 1.1f); 
                }
                else
                {
                    AudioManager.instance.PlaySoundEffect(talkAudio, .6f, .9f, 1.3f);
                }

                audioPlayed = true;           
            }
            else
            {
                audioPlayed = false;
            }

            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        if(dialogueText.text != line)
        {
            lines.Clear();
            StopAllCoroutines();
            StartCoroutine(TypeLine("You talk too fast... say that again?"));
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
        Time.timeScale = 1;
    }
}