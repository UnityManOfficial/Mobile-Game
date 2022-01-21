using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player's Movement")]
    [Tooltip ("How fast can the player go?")] public float MovementSpeed = 1f;
    [Tooltip("How high can the player jump?")] public float JumpVelocity = 1f;

    [Header("Player's Health System")]
    [Tooltip("How much should the player's HP max health is?")] public int HealthMax = 10;
    [Tooltip("How much should the player's HP health is?")] public int HealthCurrent = 10;
    [Tooltip("Is the player poisoned?")] public bool IsPoisoned = false;
    [Tooltip("Is the player on fire? (Not a killing spree. Like literally on fire)")] public bool IsOnFire = false;
    [Tooltip("Is the player being an asshole?")] public bool IsBeingAnAsshole = true;

    [Header("Cheats")]
    [Tooltip("Grants the player to have Infinite Health or just invincible")] public bool InfiniteHealth = false;
    [Tooltip("Grants the player to NoClip")] public bool NoClip = false;
    [Tooltip("Grants the player to Fly")] public bool Fly = false;
    [Tooltip("Grants the player a BFG From Doom")] public bool BFG = false;
    [Tooltip("Give the player Dietz Nuts")] public bool DietzNuts = false;

    public static Vector2 LastCheckpoint = new Vector2(0, 0);

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;

    void Start()
    {
        myRigidBody = transform.GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        HealthCurrent = HealthMax;
    }

    void Update()
    {
        Run();
    }


    private void Run()
    {
        float moveX = Input.GetAxis("Horizontal");
        var velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * MovementSpeed;
        myRigidBody.velocity = velocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }
}
