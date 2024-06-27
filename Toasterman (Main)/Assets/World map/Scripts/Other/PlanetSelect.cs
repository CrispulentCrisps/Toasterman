using UnityEngine;

public class PlanetSelect : MonoBehaviour
{
    public int PlanetNum;
    public int IndexStart;
    public int IndexEnd;
    public int LevelIndex;
    public int TargetScore;

    public GameObject DialogManager;

    public Transform StatsBox;
    public Transform Particle;
    public AnimationCurve StatSizeCurve;

    public SceneChange scenechange;

    public Dialog dialog;
    
    public Transform tf;
    public Transform UITARGET;
    
    public Vector3 Scale;

    public GameObject[] Ailments;
    public GameObject[] Locks;
    public GameObject PlanetBefore;

    public float ResetSize;
    private float T;

    public static bool Selected = false;
    public bool Locked;
    public bool Completed;
    private bool Hovered = false;

    void Start()
    {
        StatsBox.localScale = new Vector2(0f, 1f);
        Selected = false;
        dialog = GameObject.Find("Dialogmanager").GetComponent<Dialog>();
        Completed = PlanetTally.PlanetsDone[PlanetNum-1];
        if (PlanetBefore != null)
        {
            Locked = !PlanetTally.PlanetsDone[PlanetBefore.GetComponent<PlanetSelect>().PlanetNum-1];
        }
        //Check if level is Locked
        if (Locks.Length != 0)
        {
            if (!Locked)
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

    private void Update()
    {
        if (Hovered)
        {
            T += Time.deltaTime * 5f;
        }
        else
        {
            T -= Time.deltaTime * 5f;
        }

        T = Mathf.Clamp(T, 0f, 1f);

        if (Locked == false)
        {
            StatsBox.localScale = new Vector2(StatSizeCurve.Evaluate(T), 1f);
            Particle.localScale = new Vector2(StatSizeCurve.Evaluate(T), 1f);
        }
    }

    private void OnMouseEnter()
    {
        Hovered = true;
    }
    private void OnMouseExit()
    {
        Hovered = false;
    }
}
