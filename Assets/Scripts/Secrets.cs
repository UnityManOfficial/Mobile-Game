using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Secrets : MonoBehaviour
{

    [Tooltip("Where scene should the player go?")] public int SceneBuild = 1;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            SceneManager.LoadScene(SceneBuild);
        }
    }

}
