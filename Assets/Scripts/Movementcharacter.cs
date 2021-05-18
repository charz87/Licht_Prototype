using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Movementcharacter : MonoBehaviour

{

    public float speedMagic;

    public static bool aliveMagic = false;

    private Transform myTransform;

    public GameObject magicAttack1;

    public GameObject magicAura;

    private GameObject shooter;

    private GameObject spawner;

    private GameObject auraSpawner;

    public float RunSpeed = 9;

    //standing Timer for Idle Looking behind animation
    private float standingTimer = 15f;
    private float timerIdle = 3f;
    public static bool activeMagic = false;
    public static float magicDelay;

    // public float JumpSpeed = 9;
    public float TurnSpeed = 25f;
    public float Gravity = 20;
    public float ThumbStickDeadZone = 0.1f;

    //Animator
    private Animator anim;
    private bool animControlRun = false;
    private bool animControlIdle;

    //Shoot Magic
    public static bool shooting;
    public static float shootingDelay;

    // [HideInInspector]
    public float Speed = 0;

    [HideInInspector]
    public Vector3 MoveDirection;

    [HideInInspector]
    public Transform Transform;

    [HideInInspector]
    public CharacterController Controller;

    public float X = 0f;
    public float Y = 0f;

    void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Transform = transform;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Player");
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        auraSpawner = GameObject.FindGameObjectWithTag("AuraSpawner");

        myTransform = auraSpawner.gameObject.transform;

    }

    void Update()
    {

        Debug.Log("Shooting = " + shooting);

        magicDelay = shootingDelay;


        //Animations 
        anim.SetBool("IsRunning", animControlRun);
        anim.SetBool("IsStanding", animControlIdle);

        if (!shooting && MagicMovement.aliveMagic == false)
        {
            //Move Inputs
            X = Input.GetAxis("Horizontal");
            Y = Input.GetAxis("Vertical");
            //If i am moving
            if (X != 0 || Y != 0)
            {

                animControlRun = true;
                standingTimer = 25f;
                shooting = false;

            }
            else//If not moving
            {
                standingTimer -= Time.deltaTime;
                animControlRun = false;

                ShooterManager(); // Manejador de los estados de shoot de hechizos

                //Am i standing too much
                if (standingTimer <= 0f)
                {
                    animControlIdle = true;
                    timerIdle = 3f;
                    standingTimer = 25f;
                }

                // Management of duration of Idle looking behind animation

                if (animControlIdle == true)
                {
                    timerIdle -= Time.deltaTime;

                    if (timerIdle <= 0)
                    {
                        animControlIdle = false;
                    }
                }
            }

        }
        else if (shooting && MagicMovement.aliveMagic == false)
        {
            X = 0f;
            Y = 0f;
            ShooterManager();
            ShootDelay();

            if (animControlRun == true)
            {

                animControlRun = false;
                anim.SetBool("IsRunning", animControlRun); // Llamada a la animacion Run cuando se presiona una tecla
              
            }
        }


    }

    void FixedUpdate()
    {
        Move(X, Y);

    }

    void SetSpeed(float rs, float ts)
    {
        RunSpeed = rs;
        TurnSpeed = ts;
    }

    void Move(float h, float v)
    {
        Speed = 0;
        MoveDirection.y -= Gravity * Time.deltaTime;
        if (Mathf.Abs(v) > ThumbStickDeadZone || (Mathf.Abs(h) > ThumbStickDeadZone))
        {
            var lookRotation = new Vector3();
            lookRotation.Set(h, 0, v);


            var rotation = Quaternion.LookRotation(lookRotation, Vector3.up);
            if (lookRotation.magnitude > ThumbStickDeadZone)
            {
                Speed = RunSpeed;
                transform.rotation = Quaternion.Slerp(Transform.rotation, rotation, Time.deltaTime * TurnSpeed);
            }
        }
        if (Controller.isGrounded && MagicMovement.aliveMagic == false)
        {
            MoveDirection = Transform.TransformDirection(Vector3.forward).normalized;
            MoveDirection *= Speed * Time.deltaTime;




            //if (Input.GetButton("Fire1"))
            //      MovementProperties.MoveDirection.y = JumpSpeed;
        }
        Controller.Move(MoveDirection);
    }

    void ShooterManager()
    {
        //Debug.Log (shooting);
        //Control de Input letra M-------------------------------


        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Fire1") || Input.GetKey(KeyCode.M) || Input.GetButton("Fire1") && shooting == false)
        {
            Debug.Log("Dispare con M");
            shooting = true;
            anim.SetTrigger("IsAttack1");
            StartCoroutine(MagicReleased());
            standingTimer = 25f;
            shootingDelay = 2.1f;
           


        }

        else if (Input.GetKeyDown(KeyCode.N) || Input.GetButtonDown("Fire2") || Input.GetKey(KeyCode.N) || Input.GetButton("Fire2") && shooting == false)
        {
            Debug.Log("Dispare con M");
            shooting = true;
            anim.SetTrigger("IsAttack2");
            standingTimer = 25f;
            shootingDelay = 2.1f;



        }

        else if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Fire3") || Input.GetKey(KeyCode.K) || Input.GetButton("Fire3") && shooting == false)
        {
            Debug.Log("Dispare con M");
            shooting = true;
            anim.SetTrigger("IsStrongAttack");
            standingTimer = 25f;
            shootingDelay = 2.1f;



        }
        else {
           // StartCoroutine(ChangeAnimState());
            //StartCoroutine(DestroyShooting());




        }


    }
    void ShootDelay()
    {
     
        shootingDelay -= Time.deltaTime;

        if (shootingDelay <= 0)
        {
            shootingDelay = 0;
        }



    }
    IEnumerator DestroyShooting()
    {
        yield return new WaitForSeconds(1);
        shooting = false;

    }
    IEnumerator ChangeAnimState()
    {
        yield return new WaitForSeconds(1);
      


    }
    IEnumerator MagicReleased()
    {

        if (shooting && MagicMovement.aliveMagic == false)
        {

            Debug.Log("Debe aparecer");
            activeMagic = true;
            Instantiate(magicAura, auraSpawner.transform.position, myTransform.rotation);
            shooting = false;
            MagicMovement.aliveMagic = true;
            yield return new WaitForSeconds(1);
            Instantiate(magicAttack1, spawner.transform.position, shooter.transform.rotation);
            StartCoroutine(WaitforDestroy());

        }
    }
    
    void MagicDelay()
    {
        //Debug.Log ("Entre a ShootDelay");



        magicDelay -= Time.deltaTime;

        if (magicDelay <= 0)
        {
            magicDelay = 0;
        }
        if (magicDelay == 0)
        {
            activeMagic = false;
        }

    }

    IEnumerator WaitforDestroy()
    {
        shooting = false;
        yield return new WaitForSeconds(1);
        Destroy(GameObject.FindWithTag("Magic"));
        MagicMovement.aliveMagic = false;

        //Shooter.activeMagic = false;
        //aliveMagic = false;

    }
    
} 