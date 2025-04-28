using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
    private bool isDialogueFinished = false;

    private void Start()
    {
        sentences = new string[]
        {
            "Burasi... garip. Her sey olmasi gerektigi gibi degil.",
            "Ne olursa olsun, ilerlemeliyim. Bozuk kurallari cozmek zorundayim.",
            "Saga gitmek icin (→), sola gitmek icin (←) tusuna bas.",
            "Yer çekimine meydan okumak için (space) tusuna bas.",
            "Bazi yerlerde yonler ters calisabilir. Hazir ol!",
            "Ve unutma... Bazen dogru sandigin adim, tam tersine cikar."
        };
        character = FindAnyObjectByType<Character>();
        StartCoroutine(TypeSentence());
        character.enabled = false;
        Menu.instance.BGMusic.Stop();
        CharacterAnim.SetBool("IsTalking", false); 
        CharacterAnim.SetBool("IsIdle", false);
        CharacterAnim.SetBool("IsWalking", false);
        CharacterAnim.SetBool("IsFalling", false);
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !isTyping && !isDialogueFinished)
        {
            NextSentence();
            CharacterAnim.SetBool("IsTalking", true); 
        }
        else
        {
            CharacterAnim.SetBool("IsTalking", false);
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
            // Diyalog bittiğinde
            dialogueText.gameObject.SetActive(false);
            dialogueGameObject.SetActive(false);
            character.enabled = true;

            // Diyalog bitmeden önce "IsTalking" animasyonunu kapat
            CharacterAnim.SetBool("IsTalking", false);

            // Animasyonları sıfırlayarak doğru durumu ayarla
            CharacterAnim.SetBool("IsIdle", true);
            CharacterAnim.SetBool("IsWalking", false);
            CharacterAnim.SetBool("IsFalling", false);

            // Müzik tekrar başlasın
            Menu.instance.BGMusic.Play();
            isDialogueFinished = true;
        }
    }

}
