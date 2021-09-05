﻿using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public GameObject ContinueButton;
    public GameObject GotoButton;
    public GameObject SkipButton;

    public RectTransform ContTf;
    public RectTransform SkipTf;

    public Animator ToastAnim;
    public Animator OtherAnim;
    public Animator BoxAnim;
    public Animator TxtAnim;

    public SentencesRec[] sentences;
    
    public float TypingSpeed;

    public int index;
    public int indexDone;
    public int SelectState;

    public bool Started = false;
    public bool ToastEntered = false;
    public bool OtherEntered = false;
    private bool StartAnimating = false;
    private bool Continue = false;
    private bool Changed = false;

    void Update()
    {
        //Move arrow
        PauseMenuScript.GameIsPaused = false; //Stops game from being paused
        if (index >= indexDone)
        {
            textDisplay.text = " ";
            ContinueButton.SetActive(false);
            SkipButton.SetActive(false);
            GotoButton.SetActive(true);
        }

        //Controller Input
        if (Input.GetAxisRaw("Horizontal") > 0f && !Changed)
        {
            SelectState++;
            Changed = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f && !Changed)
        {
            SelectState--;
            Changed = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Changed = false;
        }

        if (SelectState < 0)
        {
            SelectState = 0;
        }

        if (SelectState > 1)
        {
            SelectState = 1;
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
            //Makes sure portraits don't stay on after text it done
            if (index < indexDone)
            {
                //Toast emotion
                if (sentences[index].ToastIn && ToastEntered)
                {
                    switch (sentences[index].ToastEmote)
                    {
                        case 1:
                            ToastAnim.Play("Normal");
                            break;
                        case 2:
                            ToastAnim.Play("Happy");
                            break;
                        case 3:
                            ToastAnim.Play("Sad");
                            break;
                        case 4:
                            ToastAnim.Play("Angry");
                            break;
                        case 5:
                            ToastAnim.Play("Confused");
                            break;
                        case 6:
                            ToastAnim.Play("Electrified");
                            break;
                        case 7:
                            ToastAnim.Play("Suspicious");
                            break;
                        case 8:
                            ToastAnim.Play("Sick");
                            break;
                    }
                }
                //Other emotion
                if (sentences[index].AndrussIn && OtherEntered)
                {
                    switch (sentences[index].AndrussEmote)
                    {
                        case 0:
                            OtherAnim.Play("AndrussNoTalk");
                            break;
                        case 1:
                            OtherAnim.Play("AndrussNormal");
                            break;
                        case 2:
                            OtherAnim.Play("AndrussAngry");
                            break;
                        case 3:
                            OtherAnim.Play("AndrussAngryNoTalk");
                            break;
                        case 4:
                            OtherAnim.Play("AndrussWorry");
                            break;
                        case 5:
                            OtherAnim.Play("AndrussWorryNoTalk");
                            break;
                        case 6:
                            OtherAnim.Play("AndrussHappy");
                            break;
                        case 7:
                            OtherAnim.Play("AndrussHappyNoTalk");
                            break;
                    }
                }
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
        //actual typing
        ContinueButton.SetActive(false);
        SkipButton.SetActive(false);
        Continue = false;
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
        Continue = true;
        ContinueButton.SetActive(true);
        SkipButton.SetActive(true);
    }

    public IEnumerator BoxIn(float seconds)
    {
        BoxAnim.Play("In");
        yield return new WaitForSeconds(seconds);
        TxtAnim.Play("TextBox");
        Started = true;
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