using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    [SerializeField] [Range(1, 3)] int Difficulty = 1;


    public int GameDifficulty()
    {
        return Difficulty;
    }

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

    // Update is called once per frame
    void Update()
    {
    DifficultyChanger();
    }

    private void DifficultyChanger()
    {

    }



}
