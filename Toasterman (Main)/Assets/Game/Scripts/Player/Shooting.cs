using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{ 
    public Transform tf;

    ObjectPools objectPooler;

    public TextMeshProUGUI textDisplay;

    private float FireRate = 0f;
    public float ReloadTime = 10f;
    public float Increment;

    private bool Auto = false;

    public static int BulletType;
    public static int[] BulletLevel;
    [Range(0,5)]
    public int MaxProjec;
    [Range(0,5)]
    public int MaxBulletLevel;

    private int RecursionCounter;

    private float BulletSpreadMult;

    public Quaternion BulletRot;

    private string[] ProjectileNames;


    private void Start()
    {

        objectPooler = ObjectPools.Instance;

        ProjectileNames = new string[] { "Bullet", "Acid", "Bouncer", "Power" };//Text cannot be greater than 7-8 characters
        BulletLevel = new int[] {0,0,0,0,0};

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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyUp(KeyCode.M))// shooting input
        {

            Auto = true;

        }
        else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.M))
        {

            Auto = false;

        }
        
        if (Input.GetKeyUp(KeyCode.C))
        {

            BulletType++;
            
        }
        else if (Input.GetKeyUp(KeyCode.X))
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

            Shoot();

        }
    }


    public void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shoot regular");
        switch (BulletType)
        {
            case 0:
                Increment = 3f;
                for (int i = 0; i < BulletLevel[BulletType] + 1; i++)// spread shot
                {
                    //Spread
                    BulletSpreadMult = BulletLevel[BulletType] + 1;
                    BulletRot = Quaternion.Euler(0, 0, ((BulletLevel[BulletType] + (i - (BulletLevel[BulletType]) * 0.5f) * BulletSpreadMult) % 360));
                    objectPooler.SpawnFromPool(ProjectileNames[0], tf.position, BulletRot);

                }
                break;
            case 1:
                Increment = 1.5f;
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
            case 2:
                Increment = 1.25f;
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
            case 3:
                Increment = 0.25f + (BulletLevel[BulletType] * 0.25f);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
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
                ShootCircle(Bulletmount, "BreadZookaBullet", tf, (1.681f /*using 1.681 as it is aprox golden ratio*/ * RecursionCounter) * (360f / Bulletmount));
                TimeElapsedTwo = 0f;
                RecursionCounter++;
            }
            yield return new WaitForSeconds(SpaceBetween);
        }
    }

}