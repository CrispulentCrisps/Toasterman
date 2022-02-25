using UnityEngine;

public class PlanetSelect : MonoBehaviour
{
    public int PlanetNum;
    public int IndexStart;
    public int IndexEnd;
    public int LevelIndex;
    public int TargetScore;
    public GameObject DialogManager;

    public SceneChange scenechange;

    public Dialog dialog;
    
    public Transform tf;
    public Transform UITARGET;
    
    public Vector3 Scale;

    public GameObject[] Ailments;
    public GameObject[] Locks;
    public GameObject PlanetBefore;

    public float ResetSize;

    public static bool Selected = false;
    public bool Locked;
    public bool Completed;

    void Start()
    {
        Selected = false;
        dialog = GameObject.Find("Dialogmanager").GetComponent<Dialog>();
        if (PlanetTally.PlanetsDone[0] == true)
        {
            Completed = true;
        }
        
        else if (PlanetBefore != null && PlanetBefore.GetComponent<PlanetSelect>().Completed == true)
        {
            Locked = false;
        }
        //Check if level is Locked
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
        //Check planets ailments
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

    private void OnMouseDown()
    {
        if (Selected == false && Locked == false)
        {
            dialog.index = IndexStart;
            dialog.indexDone = IndexEnd;
            scenechange.SceneNumber = LevelIndex;
            dialog.StartCoroutine(dialog.BoxIn(1f));
            Selected = true;
            PlanetSelectMouseUI.Selected = true;
        }
    }
}
