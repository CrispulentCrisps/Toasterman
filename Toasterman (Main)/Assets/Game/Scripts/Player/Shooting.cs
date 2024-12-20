﻿using System.Collections;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{ 
    public Transform tf;

    ObjectPools objectPooler;

    PlayerMovement Pm;

    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI ScoreDisplay;

    public float Score = 0;
    public static float TargetScore = 0;

    public Color AddColour;
    public Color TakeColour;

    public float FireRate = 0f;
    public float ReloadTime = 10f;
    public float Increment;

    public bool Auto = false;

    public static int BulletType;
    public static int[] BulletLevel;
    [Range(0,5)]
    public int MaxProjec;
    [Range(0,20)]
    public int MaxBulletLevel;

    private int RecursionCounter;

    private float BulletSpreadMult;

    public Quaternion BulletRot;

    private string[] ProjectileNames;

    private void Start()
    {
        Pm = GetComponent<PlayerMovement>();
        objectPooler = ObjectPools.Instance;
        ProjectileNames = new string[] { "Bullet", "Acid", "Bouncer", "Pierce" };//Text cannot be greater than 7-8 characters
        BulletLevel = new int[] {0,0,0,0,0,0};
        Score = 0;
        TargetScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //score
        ScoreDisplay.text = "Score: " + Score;
        ScoreDisplay.color += new Color(255f, 255f, 255f, 255f) * Time.deltaTime * 0.1f;

		#region ScoreDisplay
		if (TargetScore - Score <= 1000 && TargetScore - Score > 100 || TargetScore - Score >= -1000 && TargetScore - Score < -100)
        {
            if (Score < TargetScore)
            {
                Score += 100;
                ScoreDisplay.color = AddColour;
            }
            if (Score > TargetScore)
            {
                Score -= 100;
                ScoreDisplay.color = TakeColour;
            }
        }
        else if (TargetScore - Score <= 100 && TargetScore - Score > 10 || TargetScore - Score >= -100 && TargetScore - Score < -10)
        {
            if (Score < TargetScore)
            {
                Score += 10;
                ScoreDisplay.color = AddColour;
            }
            if (Score > TargetScore)
            {
                Score -= 10;
                ScoreDisplay.color = TakeColour;
            }
        }
        else if (TargetScore - Score <= 10)
        {
            if (Score < TargetScore)
            {
                Score++;
                ScoreDisplay.color = AddColour;
            }
            if (Score > TargetScore)
            {
                Score--;
                ScoreDisplay.color = TakeColour;
            }
        }
		#endregion ScoreDisplay
		
        FireRate += Increment * Time.deltaTime;
        //special bullets
        if (BulletLevel[4] >= 1)//BulletLevel[4] is for the 'fuck everything on screen' attack
        {
            StartCoroutine(BreadzookaBlast(2.25f, 12, 0.05f));
            AudioManager.instance.Play("BreadzookaBlastTheme");
            BulletLevel[4] = 0;
        }
        else if (BulletLevel[5] >= 1)
        {
            BulletLevel[5] = 0;
            for (int i = 0; i < 2; i++)
            {
                objectPooler.SpawnFromPool("ShooterHelper", tf.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Fire1"))// shooting input
        {
            Auto = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.B) || Input.GetButtonUp("Fire1"))
        {
            Auto = false;
        }
        
        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.M) || Input.GetButtonUp("Fire3"))
        {
            BulletType++;  
        }
        else if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.N) || Input.GetButtonUp("Fire2"))
        {
            BulletType--;  
        }

        if (BulletType < 0)
        {
            BulletType = ProjectileNames.Length - 1;
        }
        
        if (BulletType > ProjectileNames.Length - 1 || BulletType > MaxProjec - 1)
        {
            BulletType = 0;
        }
        
        for (int i = 0; i < ProjectileNames.Length; i++)
        {
            if (BulletLevel[i] > MaxBulletLevel)
            {
                BulletLevel[i] = MaxBulletLevel;
            }
        }

        textDisplay.text = "Weapon:" + ProjectileNames[BulletType];

        if (Auto == true && FireRate >= ReloadTime)
        {
            Shoot(tf);
        }
    }


    public void Shoot(Transform ShootPos)
    {
        AudioManager.instance.Play("Shoot regular");
        switch (BulletType)
        {
            case 0:
                //Spread Shot
                Increment = 3f;
                for (int i = 0; i < BulletLevel[BulletType] + 1; i++)
                {
                    //Spread
                    BulletSpreadMult = BulletLevel[BulletType] + 2.5f;
                    BulletRot = Quaternion.Euler(0, 0, (((BulletLevel[BulletType] + (i - (BulletLevel[BulletType]) * 0.5f) * BulletSpreadMult)) % 360)+ (180 * Pm.ShipRot));
                    objectPooler.SpawnFromPool(ProjectileNames[0], ShootPos.position, BulletRot);
                }
                break;
                //Close Range Acid
            case 1:
                Increment = 4f * ((BulletLevel[BulletType]*0.33f)+1);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], ShootPos.position, Quaternion.EulerAngles(Quaternion.identity.x, Quaternion.identity.y, 135 * Pm.ShipRot));
                break;
                //Bouncer
            case 2:
                Increment = 0.75f;
                BulletPatternsModule.ShootArc(360f, (BulletLevel[BulletType]+1) * 2, ProjectileNames[BulletType], ShootPos, 0f);
                break;
                //Laser
            case 3:
                Increment = 0.25f + (BulletLevel[BulletType] * 0.075f);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], ShootPos.position, Quaternion.EulerAngles(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z+(180*Pm.ShipRot)));
                break;
        }
        FireRate = 0;// reset firerate
    }

    public void ShootCircle(int BulletAmount, string BulletName, Transform tf, float Offset)
    {
        float angle = Offset;
        for (int i = 0; i < BulletAmount; i++)
        {
            float AngleStep = 360f / BulletAmount;
            angle += AngleStep;
            objectPooler.SpawnFromPool(BulletName, tf.position, Quaternion.Euler(0, 0, angle));
        }
    }

    //IEnumerators

    public IEnumerator BreadzookaBlast(float Length, int Bulletmount, float SpaceBetween)
    {
        float TimeElapsed = 0;
        float TimeElapsedTwo = 0;
        while (TimeElapsed < Length)
        {
            TimeElapsed += Time.deltaTime;
            TimeElapsedTwo += Time.deltaTime;

            if (TimeElapsedTwo >= SpaceBetween)
            {
                ShootCircle(Bulletmount, "BreadZookaBullet", tf, (1.681f /*using 1.681 as it is aproximatly the golden ratio*/ * RecursionCounter) * (360f / Bulletmount));
                TimeElapsedTwo = 0f;
                RecursionCounter++;
            }
            yield return new WaitForSeconds(SpaceBetween);
        }
    }

}