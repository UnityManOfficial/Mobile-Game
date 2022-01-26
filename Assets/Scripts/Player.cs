using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Settings in Unity

    [Header ("Player's Movement")]
    [Tooltip ("How fast can the player go?")] public float MovementSpeed = 1f;
    [Tooltip("How high can the player jump?")] public float JumpVelocity = 1f;
    [Tooltip("Is the player touching the ground?")] public bool Grounded = false;

    [Header("Player's Health System")]
    [Tooltip("How much should the player's HP max health is?")] public int HealthMax = 10;
    [Tooltip("How much should the player's HP health is?")] public int HealthCurrent = 10;
    [Tooltip("Is the player poisoned?")] public bool IsPoisoned = false;
    [Tooltip("Is the player on fire? (Not a killing spree. Like literally on fire)")] public bool IsOnFire = false;
    [Tooltip("Is the player being an asshole?")] public bool IsBeingAnAsshole = true;

    [Header ("Player's Sounds")]
    [Tooltip("Player's taking damage sounds")] public AudioClip[] DamageSounds;
    [Tooltip ("How loud would sounds be?")] [SerializeField] [Range(0, 1)] float Volume = 1.0f;

    [Header("Cheats")]
    [Tooltip("Grants the player to have Infinite Health or just invincible")] public bool InfiniteHealth = false;
    [Tooltip("Grants the player to NoClip")] public bool NoClip = false;
    [Tooltip("Grants the player to Fly")] public bool Fly = false;
    [Tooltip("Grants the player a BFG From Doom")] public bool BFG = false;
    [Tooltip("Give the player Dietz Nuts")] public bool DietzNuts = false;

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
    }

    //Movement Settings

    private void Run()
    {
        float moveX = Input.GetAxis("Horizontal");
        var velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * MovementSpeed;
        myRigidBody.velocity = velocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            myRigidBody.velocity = Vector2.up * JumpVelocity;
        }
    }

    //Sounds

    private AudioClip GetRandomDamageClip()
    {
        return DamageSounds[UnityEngine.Random.Range(0, DamageSounds.Length)];
    }

    //Tiggers and Miscs

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Grounded = true;
        }
        if (collision.gameObject.tag == "Death")
        {
            gameObject.transform.position = LastCheckpoint;
            AudioClip DamageTake = GetRandomDamageClip();
            AudioSource.PlayClipAtPoint(DamageTake, Camera.main.transform.position, Volume);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
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
