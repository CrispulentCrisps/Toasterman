using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsMenu : MonoBehaviour
{
    enum DevScorePlanet
    {
        Planet1 = 0,
        Planet2 = 1,
        Planet3 = 2,
        Planet4 = 3,
        Planet5 = 4,
    }
    [SerializeField]
    DevScorePlanet DeveloperScore;

    private int[] PlanetAimScores = { 9900, 14920, 12220, 0, 0 };
    private int[] DeveloperAimScores = { 15000, 22600, 18500, 0, 0 };
    [SerializeField]
    TextMeshProUGUI MainTextBody;
    [SerializeField]
    Image BG;
    bool ShowText = false;
    bool Started = false;
    bool ChoseRank = false;
    [SerializeField]
    private GameObject Container;
    [SerializeField]
    private GameObject[] Ranks;
    string Ranktext = "";

    private void Start()
    {
        BG.color = new Color(0f, 0f, 0f, 0f);
        //StartStats();
    }

    public void StartStats()
    {
        Started = true;
        Container.active = true;
    }

    private void Update()
    {
        if (Started)
        {
            BG.color += new Color(0f, 0f, 0f, 0.25f);
            if (BG.color.a > 0.75f)
            {
                BG.color = new Color(0f, 0f, 0f, 0.75f);
                ShowText = true;
            }

            if (ShowText)
            {
                int Devscore = 0;
                int CurrentPlanetScore = 0;
                if (!ChoseRank)
                {
                    switch (DeveloperScore)
                    {
                        case DevScorePlanet.Planet1:
                            Devscore = 0;
                            UpdateStats(0);
                            break;
                        case DevScorePlanet.Planet2:
                            Devscore = 0;
                            UpdateStats(1);
                            break;
                        case DevScorePlanet.Planet3:
                            Devscore = 0;
                            UpdateStats(2);
                            break;
                        case DevScorePlanet.Planet4:
                            Devscore = 0;
                            UpdateStats(3);
                            break;
                        case DevScorePlanet.Planet5:
                            Devscore = 0;
                            UpdateStats(4);
                            break;
                        default:
                            break;
                    }
                    ChoseRank = true;
                }
                float HScore = PlanetTally.PlanetScore[CurrentPlanetScore];
                string AddText = "";
                if (Shooting.TargetScore >= HScore)
                {
                    AddText = " a new highscore!!";
                }

                MainTextBody.text = "Current score: " + Shooting.TargetScore + "\nHighScore: " + HScore + AddText + "\n\n Current rank:\n" + Ranktext;

            }
        }
    }

    void UpdateStats(int Index)
    {
        if (Shooting.TargetScore >= PlanetAimScores[Index])
        {
            PlanetTally.PlanetsDone[Index] = true;
            if (Shooting.TargetScore > DeveloperAimScores[Index])
            {
                Ranktext = " Master of the metal!";
                Ranks[0].active = true;
                Ranks[1].active = false;
                Ranks[2].active = false;
            }
            else if (Shooting.TargetScore > PlanetTally.PlanetScore[Index])
            {
                Ranktext = " Way to go, metal man!";
                Ranks[0].active = false;
                Ranks[1].active = true;
                Ranks[2].active = false;
            }
        }
        else
        {
            Ranktext = " Good try, could be better!";
            Ranks[0].active = false;
            Ranks[1].active = false;
            Ranks[2].active = true;
        }
        PlanetTally.PlanetScore[Index] = Shooting.TargetScore;
        PlanetTally.TimesCompleted[Index] += 1;
        PlanetTally pt = GameObject.FindGameObjectWithTag("TallyCounter").GetComponent<PlanetTally>();
        pt.SaveData();
    }
}
