using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavyText : MonoBehaviour
{
    public TMP_Text text;

    Mesh mesh;
    Vector3[] Vertices;

    public float amp1, amp2, freq1, freq2;

    char[] characters;

    public void Start()
    {
        characters = text.text.ToCharArray();
    }

    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (characters == null) characters = text.text.ToCharArray();
            text.ForceMeshUpdate();
            mesh = text.mesh;
            Vertices = mesh.vertices;

            for (int i = 0; i < text.textInfo.characterCount; i++)
            {
                if (characters[i] != ' ')
                {
                    TMP_CharacterInfo c = text.textInfo.characterInfo[i];
                    int index = c.vertexIndex;

                    Vector3 offset = SineMove(amp1, amp2, freq1, freq2, Time.time + i + index);

                    for (int j = 0; j < 4; j++)
                    {
                        Vertices[index + j] += offset;
                    }
                }
            }

            mesh.vertices = Vertices;
            text.canvasRenderer.SetMesh(mesh);
        }
    }

    Vector2 SineMove(float A1, float A2, float F1, float F2, float t)
    {
        Vector2 offset = new Vector2(0, 0);
        if (A1 == 0 || F1 == 0)
        {
            return new Vector2(0, A2 * Mathf.Cos(F2 * t));
        }
        if (A2 == 0 || F2 == 0)
        {
            return new Vector2(A1 * Mathf.Sin(F1 * t),0);
        }
        else
        {
            return new Vector2(A1 * Mathf.Sin(F1 * t), A2 * Mathf.Cos(F2 * t));
        }
    }
}
