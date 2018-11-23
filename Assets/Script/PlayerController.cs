using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    // Use this for initialization

    public float turningspeed = 400f;   // origin : 400
    public float revspeed;
    public float DashForce = 3f;

    public Transform target;

    public float dashspeed;
    public float MaxDashtime = 0.5f;
    public float dashtime;
    public float StartDashtime = 0.2f;   
    public bool canIDash;

    Rigidbody2D MyRigidbody;
    public float walkingspeed = 1;


    private float RecoverTime;
    private float NormalisePlayer =0.5f;

    private bool PlayergotHit;
    public bool SpeedPowerUp;
    private float speedboostTimer;
    public GameObject SpeedLightningEffect;


    private bool TeleportUp;
    _2dxFX_Hologram2 hologram;
    private float hologramTimer;
    TrailRenderer trail;
    BoxCollider2D mycollider;
   
    //Controls//

    float Horizontal = 0f;



    public bool isDead;
    public GameObject explosion;
    SpriteRenderer mySprite;

    float speedMilestoneCount;
    float speedIncreaseMilestone = 150;
    float speedMultiplier =1.2f;

    GameObject OutsideBoundaries;
    GameController game;


    //Audio

    public AudioSource SpaceLaunch;
    public AudioSource SpaceExplosion;
    public AudioSource WarpDash;

    // control
    float InputUI;
    bool performdash;
    

    void Start () {
        canIDash = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        hologram = GetComponent<_2dxFX_Hologram2>();
        mycollider = GetComponent<BoxCollider2D>();
        hologram.enabled = false;
        dashtime = StartDashtime;
        speedMilestoneCount = speedIncreaseMilestone;
        
        OutsideBoundaries = GameObject.Find("PlayerDestructionPoint");
        SpaceLaunch.Play();
         game = FindObjectOfType<GameController>();
    }
	// Update is called once per frame
	void Update () {
       
        if (transform.position.y< OutsideBoundaries.transform.position.y && isDead ==false)  // so it wont update all the time to make the sound distruptive
        {
            Death();
            game.ConfirmPlayerKill();
        }
    
        if(PlayergotHit)
        {
           
            RecoverTime -= Time.deltaTime;

            if(RecoverTime <= 0)
            {
                                  // Recovering from collision
                                                                   //MyRigidbody.bodyType = RigidbodyType2D.Kinematic;
                                                                   //MyRigidbody.useFullKinematicContacts = true;

                MyRigidbody.bodyType = RigidbodyType2D.Static;// Normalise players physics 
                NormalisePlayer -= Time.deltaTime;
                if (NormalisePlayer<= 0)
                {
                    
                    RecoverTime = 1;
                    NormalisePlayer = 0.5f;
                    MyRigidbody.bodyType = RigidbodyType2D.Dynamic;
                    PlayergotHit = false;
                }
             
            }
        }

        if(TeleportUp)
        {
            
            trail = GetComponent<TrailRenderer>();
            hologram.enabled = true;
            trail.enabled = false;
            hologramTimer -= Time.deltaTime;

            if(hologramTimer <= 0)
            {
                TeleportUp = false;
                trail.enabled = true;
                hologram.enabled = false;
            }
        }
        if(SpeedPowerUp)
        {

            speedboostTimer -= Time.deltaTime;
            SpeedLightningEffect.SetActive(true);
            mycollider.enabled = false;
            
            if (speedboostTimer<= 0)
            {
                mycollider.enabled = true;
                SpeedPowerUp = false;
                SpeedLightningEffect.SetActive(false);
                
            }
        }
        // speed acceleration test


        //transform.position = Vector2.up * currentSpeed * Time.deltaTime;
        ////Control

        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

      
        if(dir.sqrMagnitude>1)
        {
            dir.Normalize();
        }
        if (transform.position.y > speedMilestoneCount)              // if player position reach speed milestone 
        {
            SpaceLaunch.Play();
            speedMilestoneCount += speedIncreaseMilestone;
            
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;      // increase the distance to the next milestone by the speed milestone
            walkingspeed = walkingspeed * speedMultiplier;             // inicreased speedmultiplier.
        }
        if (isDead == false)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");  // Window
            InputUI = CrossPlatformInputManager.GetAxisRaw("Horizontal");  //phone
            //-1 left 0 still 1+ right;
            transform.Translate(Vector2.up * walkingspeed * Time.deltaTime, Space.Self);
           
            transform.Rotate(Vector3.forward *-InputUI * turningspeed * Time.deltaTime * 10);   // turning rate rotation need more turn rate so you dont injure your wrist 18
        }
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

       
        if (screenPos.x < 0)
        {
            TeleportRight(screenPos);
        }
        if (screenPos.x>Screen.width)
        {
            TeleportLeft(screenPos);
        }

       // Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")
        performdash = CrossPlatformInputManager.GetButtonDown("Dash");  //phone

        if (performdash && canIDash == true && isDead == false)        // Input.GetKeyDown(KeyCode.Space)
        {

            if (dashtime <= MaxDashtime)
            {
                //MyRigidbody.velocity = Vector3.up * dashspeed;
                transform.Translate(Vector2.up * dashspeed);  // Dash effect
                WarpDash.Play();

                dashtime++;
                canIDash = false;
            }
            if (StartDashtime <= 0)
            {

                StartDashtime = 0.2f;
                dashtime = 0;
                
                canIDash = true;
            }
        }
        StartDashtime -= Time.deltaTime * 5;
    }
 
    void TeleportRight(Vector3 screenPos)
    {
        Vector3 goalScrPos = new Vector3(Screen.width, screenPos.y, screenPos.z);
        Vector3 ScreenToWorldPos = Camera.main.ScreenToWorldPoint(goalScrPos);
        transform.position = ScreenToWorldPos;
    }

    void TeleportLeft(Vector3 screenPos)
    {

        Vector3 goalScrPos = new Vector3(0, screenPos.y, screenPos.z);
        Vector3 ScreenToWorldPos = Camera.main.ScreenToWorldPoint(goalScrPos);
        transform.position = ScreenToWorldPos;
    }
    public void SpeedBoostOn(float timer, bool Activate)
    {

        SpeedPowerUp = Activate;
        speedboostTimer = timer;
    }
    public void TeleportEffectOn(float timer, bool Activate)
    {

        TeleportUp = Activate;
        hologramTimer = timer;
    }

    public void CooltimeTimer(float recovery, bool playerGotHit)
    {
        RecoverTime = recovery;
        PlayergotHit = playerGotHit;
        
    }
    public void Death()
    {
        
        isDead = true;
        walkingspeed = 0;
        mySprite.enabled = false;
        explosion.SetActive(true);
        SpaceExplosion.Play();
        mycollider.enabled = false;
        StartCoroutine("CheckIfAlive");
    }
    IEnumerator CheckIfAlive()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

}
