using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{


    public AudioClip UIClick;
    public GameObject Fade;


    Animator myAnimatorFade;

    // Start is called before the first frame update
    void Start()
    {
        myAnimatorFade = Fade.GetComponent<Animator>();
    }

    // Update is called once per frame


    public void PlayAgain()
    {
        StartCoroutine(GameAgain());
    }

    public void Menu()
    {
        StartCoroutine(MenuAgain());    
    }

    IEnumerator GameAgain()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        FindObjectOfType<Game>().Heal();
        SceneManager.LoadScene("New Level");
    }

    IEnumerator MenuAgain()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
    }
}
