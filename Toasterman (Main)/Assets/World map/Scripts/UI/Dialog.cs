using System;
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
    public bool ToastEntered = false;
    public bool OtherEntered = false;
    private bool StartAnimating = false;

    void Update()
    {
        if (index >= indexDone)
        {
            textDisplay.text = " ";
            ContinueButton.SetActive(false);
            SkipButton.SetActive(false);
            GotoButton.SetActive(true);
        }

        if (StartAnimating)
        {
            if (sentences[index].ToastIn && !ToastEntered)
            {
                ToastAnim.Play("In");
                ToastEntered = true;
            }
            else if (!sentences[index].ToastIn && ToastEntered)
            {
                ToastAnim.Play("Out");
            }
            
            if (sentences[index].AndrussIn && !OtherEntered)
            {
                OtherAnim.Play("In");
                OtherEntered = true;
            }
            else if (!sentences[index].AndrussIn && OtherEntered)
            {
                OtherAnim.Play("Out");
            }

            if (Started == true)
            {
                StartCoroutine(Type());
                Started = false;
            }
        }
    }

    IEnumerator Type()
    {
        ContinueButton.SetActive(false);
        SkipButton.SetActive(false);

        foreach (char letter in sentences[index].Words.ToCharArray())
        {
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
        StartAnimating = true;
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
        if (ToastEntered)
        {
            ToastAnim.Play("Out");
        }
        if (OtherEntered)
        {
            OtherAnim.Play("Out");
        }
        index = indexDone;
        textDisplay.text = " ";
        SkipButton.SetActive(false);
        GotoButton.SetActive(true);
    }
}