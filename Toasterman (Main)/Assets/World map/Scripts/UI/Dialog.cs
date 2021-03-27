﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public GameObject ContinueButton;
    public GameObject GotoButton;
    public GameObject SkipButton;

    public Animator ToastAnim;
    public Animator OtherAnim;
    public Animator BoxAnim;
    public Animator TxtAnim;

    public SentencesRec[] sentences;
    public float TypingSpeed;

    public int index = -1;
    public int indexDone;

    public bool Started;

    void Update()
    {
        if (index >= indexDone)
        {
            textDisplay.text = " ";
            ContinueButton.SetActive(false);
            SkipButton.SetActive(false);
            GotoButton.SetActive(true);
        }
        else if (sentences[index].ToastIn)
        {
            ToastAnim.Play("In");
        }
        else if (!sentences[index].ToastIn)
        {
            ToastAnim.Play("Out");
        }
        else if (sentences[index].AndrussIn)
        {
            OtherAnim.Play("In");
        }
        else if (!sentences[index].AndrussIn)
        {
            OtherAnim.Play("Out");
        }
        
        if (Started == true)
        {
            StartCoroutine(Type());
            Started = false;
        }
    }

    IEnumerator Type()
    {
        ContinueButton.SetActive(false);
        SkipButton.SetActive(false);

        foreach (char letter in sentences[index].Words.ToCharArray())
        {
            switch (letter)
            {
                default:
                    break;
                case '{':
                    textDisplay.text += "<i>";
                    break;
                case '}':
                    textDisplay.text += "</i>";
                    break;
                case '[':
                    textDisplay.text += "<b>";
                    break;
                case ']':
                    textDisplay.text += "</b>";
                    break;
                case '_':
                    textDisplay.text += "<b>";
                    break;
                case '~':
                    textDisplay.text += "</b>";
                    break;
            }
            if (letter == '{') //Reserve { and } for italics, [ and ] for bold, _ and ~ for underline
            {
                textDisplay.text += "<i>";
            }
            else if (letter == '}')
            {
                textDisplay.text += "</i>";
            }
            else if (letter == '[')
            {
                textDisplay.text += "<b>";
            }
            else if (letter == ']')
            {
                textDisplay.text += "</b>";
            }
            else if (letter == '_')
            {
                textDisplay.text += "<u>";
            }
            else if (letter == '~')
            {
                textDisplay.text += "</u>";
            }
            else
            {
                textDisplay.text += letter;
                AudioManager.instance.Play("Text1");
                yield return new WaitForSeconds(TypingSpeed);
            }
        }
        ContinueButton.SetActive(true);
        SkipButton.SetActive(true);
    }

    public IEnumerator BoxIn(float seconds)
    {
        BoxAnim.Play("In");
        yield return new WaitForSeconds(seconds);
        TxtAnim.Play("TextBox");
        Started = true;
        index = 0;  
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            ContinueButton.SetActive(true);
            SkipButton.SetActive(true);
            textDisplay.text = "";
        }
    }

    public void SkipText()
    {
        index = indexDone + 1;
        ToastAnim.Play("Out");
        OtherAnim.Play("Out");
        textDisplay.text = " ";
        SkipButton.SetActive(false);
        GotoButton.SetActive(true);
    }
}