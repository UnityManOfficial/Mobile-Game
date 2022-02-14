using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject GameMenu;
    public GameObject GameCredits;
    public GameObject Difficulty;
    public GameObject Loading;
    public GameObject Fade;
    public GameObject Audio;
    public GameObject Player;
    public AudioClip UIClick;
    public AudioClip Death;

    Animator myAnimatorFade;
    Animator myAnimatorMusic;
    Animator myAnimatorPlayer;

    void Start()
    {
        myAnimatorFade = Fade.GetComponent<Animator>();
        myAnimatorMusic = Audio.GetComponent<Animator>();
        myAnimatorPlayer = Player.GetComponent<Animator>();
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
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        GameMenu.SetActive(false);
        Difficulty.SetActive(true);
        myAnimatorFade.SetBool("Go", false);

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

    public void EasyButton()
    {
        myAnimatorPlayer.SetBool("Run", true);
        StartCoroutine(StartGameNow());
    }

    public void MediumButton()
    {
        myAnimatorPlayer.SetBool("Run", true);
        StartCoroutine(StartGameNow());
    }

    public void HardButton()
    {
        myAnimatorPlayer.SetBool("Hard", true);
        AudioSource.PlayClipAtPoint(Death, Camera.main.transform.position, 0.1f);
        StartCoroutine(StartGameNow());
    }

    IEnumerator StartGameNow()
    {
        AudioSource.PlayClipAtPoint(UIClick, Camera.main.transform.position, 0.1f);
        yield return new WaitForSeconds(2);
        myAnimatorMusic.SetBool("Fade", true);
        myAnimatorFade.SetBool("Go", true);
        yield return new WaitForSeconds(1);
        Difficulty.SetActive(false);
        Fade.SetActive(false);
        Loading.SetActive(true);
        SceneManager.LoadScene("Level Prototype");
    }











}
