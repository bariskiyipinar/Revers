using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Diyalog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diyalogText;
    [SerializeField] private string[] diyalogs;
    [SerializeField] private GameObject NextButton;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject DiyologBubble;
    [SerializeField] private GameObject PlayExit›mage;
    [SerializeField] private GameObject Play, Exit;
    [SerializeField] private Animator ReversText;
    [SerializeField] private Animator CharacterFall;
    [SerializeField] private AudioSource RetroMusic;
    private int index;
    public static bool isMusic = false;
    void Start()
    {
        ReversText.enabled = false;
        StartCoroutine(DiyalogsTime());
        NextButton.SetActive(false);
    }
    void Update()
    {
      if(diyalogText.text == diyalogs[index])
        {
            NextButton.SetActive(true);

        }
      
    }

    IEnumerator DiyalogsTime()
    {
        foreach(char harf in diyalogs[index].ToCharArray())
        {
            diyalogText.text += harf;
            yield return new WaitForSeconds(Speed);
        }
    }

    public void Next()
    {
        NextButton.SetActive(false);

        if (index < diyalogs.Length-1)
        {
            index++;
            diyalogText.text = "";
           

            StartCoroutine(DiyalogsTime());
        }
        else
        {
            diyalogText.text = "";
            DiyologBubble.SetActive(false);
            PlayExit›mage.SetActive(true);
            Play.SetActive(true);
            Exit.SetActive(true);

            ReversText.enabled = true;
            ReversText.Play("ReversFlip");

            CharacterFall.SetBool("IsFalling", true);
            CharacterFall.SetBool("IsIdle", false);

            isMusic = true; 
            RetroMusic.Play();
        }

      
    }


}
