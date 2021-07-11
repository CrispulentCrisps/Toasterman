using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public EnemySet[] Waves; // sets up the variables for easy wave spawning

    ObjectPools objectPooler;

    private double Count;

    public int i = 0; // i is for the amount of Waves in the level

    public float Space = 0; // has spacing added on to spread enemies out
    public float TriPos;
    private float SinePos;
    private float WallSpace;
    private float EnemyAmount;
    public float RotSpace; //Seperates enemies in a circle
    public static int PowerNum;
    private string Name;


    public bool start = false;
    private bool DoneUp = false;

    public Transform tf;

    private void Start()
    {
        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        if (start == true)
        {
            Count +=  Time.deltaTime;
        }

        if (Count >= Waves[i].Time) // checks if enough time has past
        {
            Name = Waves[i].EnemyName;
            EnemyAmount = Waves[i].Amount;
            WaveStart();
        }
    }

    public void WaveStart()
    {
        PowerNum = Waves[i].PowerType;
        switch (Waves[i].WaveType)
        {
            case 1:
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {
                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x + Space, tf.position.y + Waves[i].StartYpos, tf.position.z), Quaternion.identity);
                    Space += Waves[i].Spacing; // spaces the enemies out
                }
                break;

            case 2:
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {

                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x + Space, (tf.position.y + TriPos) + Waves[i].StartYpos, tf.position.z), Quaternion.identity);
                    Space += Waves[i].Spacing; // spaces the enemies out

                    if (DoneUp == false)
                    {
                        TriPos += Waves[i].Increment;
                    }
                    else TriPos -= Waves[i].Increment;

                    if (TriPos >= Waves[i].MinMax)
                    {
                        DoneUp = true;
                    }
                    else if (TriPos <= Waves[i].MinMax * -1)
                    {
                        DoneUp = false;
                    }

                }
                break;

            case 3:
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {

                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x, tf.position.y - 5f + ((WallSpace / Waves[i].Amount) * 10) + Waves[i].StartYpos, tf.position.z), Quaternion.identity);
                    Space += Waves[i].Spacing; // spaces the enemies out
                    WallSpace++;

                }
                break;
            case 4:
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {
                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x + Waves[i].Radius + Waves[i].Radius * Mathf.Sin(RotSpace * (2 * Mathf.PI) / Waves[i].Amount), tf.position.y + Waves[i].Radius * Mathf.Cos(RotSpace * (2 * Mathf.PI) / Waves[i].Amount) + Waves[i].StartYpos, tf.position.z), Quaternion.identity);
                    Space += Waves[i].Spacing; // spaces the enemies out
                    RotSpace++;
                }
                break;
            case 5:
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {
                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x + Space, tf.position.y * Waves[i].Radius * Mathf.Sin((tf.position.x - 16f) / Waves[i].Amount * Waves[i].RotateSpeed) + Waves[i].StartYpos, tf.position.z), Quaternion.identity);//Radius is amplitude, rot speed is Frequency
                    Space += Waves[i].Spacing; // spaces the enemies out
                    RotSpace++;
                }
                break;
            case 6://Unfinished
                for (int A = 0; A < Waves[i].Amount; A++) // enemy spawning
                {
                    objectPooler.SpawnFromPool(Name, new Vector3(tf.position.x + Mathf.Max(Mathf.Abs(Waves[i].Radius + Waves[i].Radius * Mathf.Sin(RotSpace * (2 * Mathf.PI) / Waves[i].Amount))), 
                        tf.position.y + Mathf.Max(Mathf.Abs(Waves[i].Radius + Waves[i].Radius * Mathf.Cos(RotSpace * (2 * Mathf.PI) / Waves[i].Amount))), tf.position.z), Quaternion.identity);
                    Space += Waves[i].Spacing; // spaces the enemies out
                    RotSpace++;
                }
                break;
        }
        i++; // goes to the next wave
        Count = 0; // resets the counter
        TriPos = 0;
        Space = 0;
        RotSpace = 0;
    }
}