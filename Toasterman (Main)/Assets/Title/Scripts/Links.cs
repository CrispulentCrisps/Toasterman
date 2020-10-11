using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Links : MonoBehaviour
{

    public void LinkOpen()
    {
        Application.OpenURL("https://discord.gg/ZymUwN4");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
