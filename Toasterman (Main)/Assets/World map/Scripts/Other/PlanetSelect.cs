using UnityEngine;

public class PlanetSelect : MonoBehaviour
{
    public int PlanetNum;
    public int IndexStart;
    public int IndexEnd;
    public int LevelIndex;

    public GameObject DialogManager;

    public PlanetTally planetTally;

    public SceneChange scenechange;

    public Dialog dialog;
    
    public Transform tf;
    
    public Vector3 Scale;

    public GameObject[] Ailments;
    public GameObject[] Locks;
    public GameObject PlanetBefore;

    public float ResetSize;

    public bool MouseOverPlanet = false; //Make private, public for debugging
    public static bool Selected = false;
    public bool Locked;
    public bool Completed;

    void Start()
    {
        dialog = GameObject.Find("Dialogmanager").GetComponent<Dialog>();
        planetTally = GameObject.Find("PlanetCompletionCounter").GetComponent<PlanetTally>();
        if (planetTally.PlanetsDone[0] == true)
        {
            Completed = true;
        }
        else if (PlanetBefore != null && PlanetBefore.GetComponent<PlanetSelect>().Completed == true)
        {
            Locked = false;
        }
        if (Locks.Length != 0 && Locked == false)
        { 
            for (int i = 0; i < Locks.Length; i++)
            {
                Locks[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Locks.Length; i++)
            {
                Locks[i].SetActive(true);
            }
        }
        if (Completed == true)
        {
            for (int i = 0; i < Ailments.Length; i++)
            {
                Ailments[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Ailments.Length; i++)
            {
                Ailments[i].SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        if (MouseOverPlanet == false)
        {
            float Velocity = 0.0f;
            tf.localScale = new Vector3(Mathf.SmoothDamp(tf.localScale.x, ResetSize, ref Velocity, 0.05f), Mathf.SmoothDamp(tf.localScale.y, ResetSize, ref Velocity, 0.05f), 0f);
        }
    }
    void OnMouseOver()
    {
        MouseOverPlanet = true;
        if (Input.GetMouseButtonDown(0) && Selected == false && Locked == false)
        {
            dialog.index = IndexStart;
            dialog.indexDone = IndexEnd;
            scenechange.SceneNumber = LevelIndex;
            dialog.StartCoroutine(dialog.BoxIn(1f));
            Selected = true;
            MouseOverPlanet = false;
            PlanetSelectMouseUI.Selected = true;
        }
        else if (Selected == false && Locked == false && MouseOverPlanet == true)
        {
            float Velocity = 0.0f;
            tf.localScale = new Vector3(Mathf.SmoothDamp(tf.localScale.x,Scale.x, ref Velocity, 0.05f), Mathf.SmoothDamp(tf.localScale.y, Scale.y, ref Velocity, 0.05f));
        }
    }

    void OnMouseExit()
    {
        MouseOverPlanet = false;
    }

}
