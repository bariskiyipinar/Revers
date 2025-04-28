using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GirlDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private Animator npcAnimator; // Kız karakterin Animator'u
    [SerializeField] private GameObject npcCharacter; // Kız karakter objesi

    private string[] sentences;
    private int index = 0;
    private bool isTyping = false;
    private bool isDialogueFinished = false;
    private bool isInTriggerZone = false; // Oyuncu trigger alanına girdi mi?


    private void Start()
    {
        sentences = new string[]
        {
            "Merhaba CYBORG!",
            "Burası bir zamanlar dengedeydi... her şey yerli yerindeydi.",
            "Sonra bir şey geldi, görünmeyen bir virüs... ve sessizce her şeyi tersine çevirdi.",
            "Şimdi yollar kırık, gerçekler bükük, zihinler kayıp.",
            "İlerledikçe fark edeceksin... Bazı şeyler geri dönmeyecek.",
            "Ve belki... sen de onlardan biri olacaksın." ,
            "Yolun açık olsun CYBORG !"
        };

    
        dialogueGameObject.SetActive(false); // Diyalog paneli gizli
    }

    private void Update()
    {
        if (isInTriggerZone && Input.GetMouseButtonDown(0) && !isTyping && !isDialogueFinished)
        {
            NextSentence();
            npcAnimator.SetBool("IsTalking", true); // Kız karakter konuşmaya başlar
        }
        else if (!isTyping)
        {
            npcAnimator.SetBool("IsTalking", false); // Konuşma bitince animasyonu durdur
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isDialogueFinished)
        {
            isInTriggerZone = true;
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInTriggerZone = false;
        }
    }

    private void StartDialogue()
    {
        dialogueGameObject.SetActive(true);
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueGameObject.SetActive(false);

        npcAnimator.SetBool("IsTalking", false); 
        npcAnimator.SetBool("IsIdle", true); 

        isDialogueFinished = true;

       
    }
}
