using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IPooledObject
{

    public Transform tf;

    public int Inverse;

    private float Health = 100f;
    private float TargetHealth = 100f;
    public float RegenRate;

    private float Velocity = 10;
    public float Normalspeed;

    private float timer;
    private float Timer2;

    private float DashTimer;
    public float DashTimerSpeed;

    public float DashLength;
    public float DashLengthRet;
    public float DashSpeed;

    public bool Dashin = false;

    ObjectPools objectPooler;

    public CameraShake camerashake;

    public Slider HealthSlider;
    public Slider DashSlider;

    public Animator DashAnim;

    public Animator HealthAnim;

    public Vector3 Movement;

    // Start is called before the first frame update
    void Start()
    {
        Movement = new Vector3(0,0,0);
        timer = 0f;
        Timer2 = 0.5f;
        DashTimer = 0f;
        objectPooler = ObjectPools.Instance;
        DashAnim = DashSlider.GetComponent<Animator>();
    }

    public void OnObjectSpawn()
    {

        Movement = new Vector3(0, 0, 0);
        timer = 0f;
        Timer2 = 0.5f;
        DashTimer = 0f;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("NotSoGoodThing") && Dashin == false && coll.gameObject.GetComponent<DamageScript>().Damage > 0f)
        {

            Health -= coll.gameObject.GetComponent<DamageScript>().Damage;
            HealthAnim.SetTrigger("HealthDown");
            StartCoroutine(camerashake.Shake(0.1f, 0.1f));
            StartCoroutine(camerashake.AbberationChange(1f, 1f));
            timer = 5f;
        }

    }

    // Update is called once per frame
    void Update()
    {


        Movement.x = Input.GetAxis("Horizontal") * Velocity;
        Movement.y = Input.GetAxis("Vertical") * Velocity;

        HealthSlider.value = Health / 100f;//health slider
        DashSlider.value += DashTimerSpeed * Time.deltaTime;//dash

        timer -= 1f * Time.deltaTime;//shooting

        Timer2 -= 2f * Time.deltaTime;//smoke

        if (Input.GetKeyUp(KeyCode.Space) && DashSlider.value >= 1f)
        {
            Dash();

        }else if (DashLength > 0)
        {
            DashLength -= 1 * Time.deltaTime;
        }
        else if (DashLength <= 0f && Dashin == true)
        {
            Velocity = Normalspeed;
            Dashin = false;
        }

        if (Health <= TargetHealth / 3)
        {

            SmokeHeavy();

        }
        else if (Health <= TargetHealth / 1.5)
        {

            SmokeLight();

        }

        if (Health < TargetHealth && timer <= 0)
        {

            Health += RegenRate;

        } else if (Health > TargetHealth)
        {

            Health -= RegenRate;

        }

        if (DashSlider.value == 1f)
        {

            DashAnim.Play("DashFull");

        }

    }

    void FixedUpdate()
    {

        tf.Translate(Movement * Time.deltaTime);

        if (Inverse != 0)
        {

            Movement *= -Inverse;

        }

    }


    //-------------------------------------------------|=========|-------------------------------------------------------\\
    //-------------------------------------------------|Functions|-------------------------------------------------------\\
    //-------------------------------------------------|=========|-------------------------------------------------------\\

    public void Dash()
    {

        DashSlider.value = 0f;
        DashLength = DashLengthRet;
        DashTimer = DashLength;
        Velocity = DashSpeed;
        Dashin = true;

    }


    public void SmokeLight()
    {

        if (Timer2 <= 0)
        {

            objectPooler.SpawnFromPool("LightSmoke", tf.position, Quaternion.identity);
            Timer2 = 0.25f;
        }

    }

    public void SmokeHeavy()
    {

        if (Timer2 <= 0)
        {

            objectPooler.SpawnFromPool("Smoke", tf.position, Quaternion.identity);
            Timer2 = 0.25f;
        }

    }

}
