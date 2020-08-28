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

    public string[] sentences;
    public int[] ToastEmotion;
    public float TypingSpeed;

    public int index = -1;
    public int indexDone;

    public int LeftIn;
    public int RightIn;
    public int LeftOut;
    public int RightOut;

    public bool Started;

    private bool OtherIn;

    void Update()
    {
        if (index >= indexDone)
        {
            textDisplay.text = " ";
            ContinueButton.SetActive(false);
            SkipButton.SetActive(false);
            GotoButton.SetActive(true);

        }
        else if (LeftIn == index)
        {

            ToastAnim.Play("In");
            LeftIn = -1;

        }
        else if (LeftOut == index)
        {

            ToastAnim.Play("Out");
            LeftOut = -1;

        }
        else if (RightIn == index)
        {

            OtherAnim.Play("In");
            RightIn = -1;

        }
        else if (RightOut == index)
        {

            ToastAnim.Play("Out");
            RightOut = -1;

        }
        else if (Started == true)
        {

            StartCoroutine(Type());
            Started = false;

        }
        
    }

    IEnumerator Type()
    {

        ContinueButton.SetActive(false);
        SkipButton.SetActive(false);

        foreach (char letter in sentences[index].ToCharArray())
        {

            textDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
            
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
        textDisplay.text = " ";
        SkipButton.SetActive(false);
        GotoButton.SetActive(true);
    }

}
