using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject GameMenu;
    public GameObject GameCredits;
    public GameObject Loading;
    public GameObject Fade;
    public GameObject Audio;
    public AudioClip UIClick;

    Animator myAnimatorFade;
    Animator myAnimatorMusic;

    void Start()
    {
        myAnimatorFade = Fade.GetComponent<Animator>();
        myAnimatorMusic = Audio.GetComponent<Animator>();
    }

    public void MainMenuButton()
    {
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        myAnimatorFade.SetBool("Go", false);
        GameCredits.SetActive(false);
        GameMenu.SetActive(true);
    }

    public void StartGameButton()
    {
        myAnimatorMusic.SetBool("Fade", true);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        GameMenu.SetActive(false);
        Loading.SetActive(true);
        yield return new WaitForSeconds(1);
        myAnimatorFade.SetBool("Go", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameMenuuButton()
    {
        StartCoroutine(MenuGame());
    }

    IEnumerator MenuGame()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CreditsButton()
    {
        StartCoroutine(Credits());
    }

    IEnumerator Credits()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        myAnimatorFade.SetBool("Go", false);
        GameMenu.SetActive(false);
        GameCredits.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }













}
