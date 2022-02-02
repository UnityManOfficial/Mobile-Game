using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Settings in Unity

    [Header("Player's Movement")]
    [Tooltip("How fast can the player go?")] public float MovementSpeed = 1f;
    [Tooltip("How high can the player jump?")] public float JumpVelocity = 1f;
    [Tooltip("Minimun fall distance till taking fall damage?")] public float MinFallDistance = -20;
    [Tooltip("Is the player touching the ground?")] public bool Grounded = false;


    [Header("Player's Health System")]
    [Tooltip("How much should the player's HP max health is?")] public int HealthMax = 10;
    [Tooltip("How much should the player's HP health is?")] public int HealthCurrent = 10;
    [Tooltip("Maximum Fall Damage Taken")] public int MaxFallDamage = 1;
    [Tooltip("Player will take Fall Damage")] public bool WillFallDamage = false;
    [Tooltip("Make the player not taking damage")] public bool NoDamage = false;

    [Header("Player's Sounds")]
    [Tooltip("Player's taking damage sounds")] public AudioClip[] DamageSounds;
    [Tooltip("Player's Steping on Grass Sounds")] public AudioClip[] GrassStepSounds;
    [Tooltip("How loud would sounds be?")] [SerializeField] [Range(0, 1)] float Volume = 1.0f;

    [Header("Cheats")]
    [Tooltip("Grants the player to have Infinite Health or just invincible")] public bool InfiniteHealth = false;
    [Tooltip("Grants the player to NoClip")] public bool NoClip = false;
    [Tooltip("Grants the player to Fly")] public bool Fly = false;

    //Cached Things

    public static Vector2 LastCheckpoint = new Vector2(0, 0);

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;

    //Default Unity Code

    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        HealthCurrent = HealthMax;
        LastCheckpoint = transform.position;
    }

    void Update()
    {
        Run();
        Jump();
        Falling();
        Health();
        FallDamage();
    }

    //Health Settings

    private void Health()
    {
        if (HealthCurrent <= 0)
        {
            Death();
        }
    }

    IEnumerator Death()
    {
        HealthCurrent = HealthMax;
        gameObject.transform.position = LastCheckpoint;
        NoDamage = true;
        yield return new WaitForSeconds(1);
        NoDamage = false;
    }


    private void FallDamage()
    {
        if (myRigidBody.velocity.y <= MinFallDistance)
        {
            WillFallDamage = true;
        }
        else if (Grounded && WillFallDamage)
        {
            HealthCurrent -= MaxFallDamage;
            WillFallDamage = false;
            StartCoroutine(InvincibleDamage());
        }
    }


    private void TakeDamage()
    {
        AudioClip DamageTake = GetRandomDamageClip();
        AudioSource.PlayClipAtPoint(DamageTake, Camera.main.transform.position, Volume);
        StartCoroutine(InvincibleDamage());
    }

    IEnumerator InvincibleDamage()
    {
        NoDamage = true;
        myAnimator.SetBool("Inv", true);
        yield return new WaitForSeconds(1);
        myAnimator.SetBool("Inv", false);
        NoDamage = false;
    }

    //Movement Settings

    private void Run()
    {
        float moveX = Input.GetAxis("Horizontal");
        var velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * MovementSpeed;
        myRigidBody.velocity = velocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
        FlipCharacter();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            myRigidBody.velocity = Vector2.up * JumpVelocity;
            myAnimator.SetBool("Jumping", true);
        }
    }

    private void Falling()
    {
        if (myRigidBody.velocity.y <= -2.5)
        {
            myAnimator.SetBool("Falling", true);
        }
        else if (myRigidBody.velocity.y <= 0)
        {
            myAnimator.SetBool("Falling", false);
        }
    }

    //Sounds

    private void GrassStep()
    {
        AudioClip GrassSteps = GetRandomGrassStepClip();
        AudioSource.PlayClipAtPoint(GrassSteps, Camera.main.transform.position, Volume);
    }

    private AudioClip GetRandomDamageClip()
    {
        return DamageSounds[UnityEngine.Random.Range(0, DamageSounds.Length)];
    }

    private AudioClip GetRandomGrassStepClip()
    {
        return GrassStepSounds[UnityEngine.Random.Range(0, GrassStepSounds.Length)];
    }

    //Tiggers and Miscs

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
            myAnimator.SetBool("Ground", true);
            myAnimator.SetBool("Jumping", false);
        }

        if(collision.gameObject.tag == "Enemy" && !NoDamage)
        {
            TakeDamage();
        }

        if (collision.gameObject.tag == "Death" && !NoDamage)
        {
            Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Grounded = false;
            myAnimator.SetBool("Ground", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Grounded = true;
        }
    }

    private void FlipCharacter()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

}
