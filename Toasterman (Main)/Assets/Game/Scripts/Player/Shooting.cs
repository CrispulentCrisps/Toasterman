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

    public int BulletType;
    public int[] BulletLevel;

    private float BulletSpreadMult;

    public Quaternion BulletRot;

    private string[] ProjectileNames;


    private void Start()
    {

        objectPooler = ObjectPools.Instance;

        ProjectileNames = new string[] { "Bullet", "Acid", "Bouncer", "Power" };

    }

    // Update is called once per frame
    void Update()
    {

        FireRate += Increment * Time.deltaTime;

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
        else if (BulletType > ProjectileNames.Length - 1)
        {

            BulletType = 0;

        }

        textDisplay.text = "Weapon:" + ProjectileNames[BulletType];

        if (Auto == true && FireRate >= ReloadTime)
        {

            Shoot();

        }

        
    }


    public void Shoot()
    {

        switch (BulletType)
        {
            case 0:
                Increment = 3f;
                for (int i = 0; i < BulletLevel[BulletType] + 1; i++)// spread shot
                {
                    //Spread
                    BulletSpreadMult = BulletLevel[BulletType] + 1;
                    BulletRot = Quaternion.Euler(0, 0, ((BulletLevel[BulletType] + (i - (BulletLevel[BulletType]) * 0.5f) * BulletSpreadMult) % 360));
                    objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, BulletRot);

                }
                break;
            case 1:
                Increment = 2f;
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
            case 2:
                Increment = 1.5f;
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
            case 3:
                Increment = 0.25f + (BulletLevel[BulletType] * 0.25f);
                objectPooler.SpawnFromPool(ProjectileNames[BulletType], tf.position, Quaternion.identity);
                break;
        }
        FireRate = 0;// reset firerate

    }

}