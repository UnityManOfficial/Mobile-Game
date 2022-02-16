using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject healthBarTurn;

    [SerializeField] [Range(1, 3)] int Difficulty = 1;
    public int HealthMax = 10;
    public int HealthCurrent = 1;
    public int Lives = 1;

    void Awake()
    {
        dontDestroy();
    }

    private void dontDestroy()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void update()
    {

    }

    // Update is called once per frame

    public void HealthOn()
    {
        healthBarTurn.SetActive(true);
    }

    public void HealthOff()
    {
        healthBarTurn.SetActive(false);
    }

    public void Heal()
    {
        HealthCurrent = HealthMax;
        Lives = 1;
        healthBar.SetHealth(HealthCurrent);
    }

    public void DifficultyEasy()
    {
        Difficulty = 1;
        HealthMax = 10;
        HealthCurrent = HealthMax;
        healthBar.SetMaxHealth(HealthMax);
    }

    public void DifficultyMed()
    {
        Difficulty = 2;
        HealthMax = 5;
        HealthCurrent = HealthMax;
        healthBar.SetMaxHealth(HealthMax);
    }

    public void DifficultyHard()
    {
        Difficulty = 3;
        HealthMax = 1;
        HealthCurrent = HealthMax;
        healthBar.SetMaxHealth(HealthMax);
    }

    public void TakeDamage()
    {
        HealthCurrent -= 1;
        healthBar.SetHealth(HealthCurrent);
        Death();
    }

    public void Death()
    {
        if (HealthCurrent <= 0)
        {
            StartCoroutine(DeathStart());
        }
    }

    public void DeathGameOver()
    {
        if (Lives <= 0)
        {
            FindObjectOfType<PlatformChecker>().GameOver();
            FindObjectOfType<Player>().DeathAnimation();
        }
    }

    public void Respawn()
    {
        if (Lives > 0)
        {
            FindObjectOfType<Player>().DeathStart();
            healthBar.SetHealth(HealthCurrent);
        }
    }

    IEnumerator DeathStart()
    {
        HealthCurrent = HealthMax;
        Lives -= 1;
        DeathGameOver();
        yield return new WaitForSeconds(1);
        Respawn();
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }
}
