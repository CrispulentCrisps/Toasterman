using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{ 
    public Transform tf;

    ObjectPools objectPooler;

    public TextMeshProUGUI textDisplay;

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
        objectPooler = ObjectPools.Instance;
        ProjectileNames = new string[] { "Bullet", "Acid", "Bouncer", "Power" };//Text cannot be greater than 7-8 characters
        BulletLevel = new int[] {0,0,0,0,0,0};
    }

    // Update is called once per frame
    void Update()
    {
        FireRate += Increment * Time.deltaTime;
        if (BulletLevel[4] >= 1)
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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Greater))// shooting input
        {
            Auto = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Greater))
        {
            Auto = false;
        }
        
        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.Less))
        {
            BulletType++;  
        }
        else if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.M))
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
                Increment = 3f;
                for (int i = 0; i < BulletLevel[BulletType] + 1; i++)// spread shot
                {
                    //Spread
                    BulletSpreadMult = BulletLevel[BulletType] + 1;
                    BulletRot = Quaternion.Euler(0, 0, ((BulletLevel[BulletType] + (i - (BulletLevel[BulletType]) * 0.5f) * BulletSpreadMult) % 360));
                    objectPooler.SpawnFromPool(ProjectileNames[0], ShootPos.position, BulletRot);
                }
                break;
            case 1:
                Increment = 1.5f;
                BulletPatternsModule.ShootArc(BulletLevel[BulletType] * 90, BulletLevel[BulletType], ProjectileNames[BulletType],tf,-90f);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], ShootPos.position, Quaternion.identity);
                break;
            case 2:
                Increment = 0.75f;
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], ShootPos.position, Quaternion.identity);
                break;
            case 3:
                Increment = 0.25f + (BulletLevel[BulletType] * 0.25f);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], ShootPos.position, Quaternion.identity);
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