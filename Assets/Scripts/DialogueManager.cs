using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText; 
    [SerializeField] private GameObject dialogueGameObject; 
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Animator CharacterAnim;
    private string[] sentences;
    private int index = 0;
    private bool isTyping = false;
    private Character character;
   
    private void Start()
    {
       
        sentences = new string[]
        {
            "Burasi... garip. Her sey olmasi gerektigi gibi degil.",
            "Ne olursa olsun, ilerlemeliyim. Bozuk kurallari cozmek zorundayim.",
            "Saga gitmek icin (→), sola gitmek icin (←) tusuna bas.",
            "Bazi yerlerde yonler ters calisabilir. Hazir ol!",
            "Ve unutma... Bazen dogru sandigin adim, tam tersine cikar."
        };
        character=FindAnyObjectByType<Character>();
        StartCoroutine(TypeSentence());
        character.enabled = false;
        Menu.instance.BGMusic.Stop();
        CharacterAnim.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            NextSentence();
        }
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
            dialogueText.gameObject.SetActive(false); 
            dialogueGameObject.SetActive(false);
            character.enabled=true;    
            CharacterAnim.enabled = true;
        }
        Menu.instance.BGMusic.Play();
    }
}
