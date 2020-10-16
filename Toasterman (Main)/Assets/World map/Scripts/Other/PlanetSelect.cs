using UnityEngine;

public class PlanetSelect : MonoBehaviour
{
    public int PlanetNum;
    public int IndexStart;
    public int IndexEnd;
    public int LeftIn;
    public int RightIn;
    public int LeftOut;
    public int RightOut;
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

    private bool Selected = false;
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

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && Selected == false && Locked == false)
        {
            dialog.index = IndexStart;
            dialog.indexDone = IndexEnd;
            dialog.LeftIn = LeftIn;
            dialog.RightIn = RightIn;
            dialog.LeftOut = LeftOut;
            dialog.RightOut = RightOut;
            scenechange.SceneNumber = LevelIndex;
            dialog.StartCoroutine(dialog.BoxIn(1f));
            Selected = true;
        }
        else if (Selected == false && Locked == false)
        {
            tf.localScale = new Vector3(Scale.x, Scale.y, Scale.z);
        }
    }

    void OnMouseExit()
    {
        tf.localScale = new Vector3(1f, 1f, 1f); 
    }

}
