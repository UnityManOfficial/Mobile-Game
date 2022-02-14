using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformChecker : MonoBehaviour
{
    public GameObject Touch;
    public GameObject PauseMenu;
    public GameObject Fade;
    public GameObject Loading;
    public GameObject Health;
    public GameObject PauseButton;
    private bool GamePaused = false;

    Animator MyAnimatorFade;

    public void Start()
    {
        MyAnimatorFade = Fade.GetComponent<Animator>();
#if UNITY_STANDALONE_WIN
        Touch.SetActive(false);

#elif UNITY_ANDROID
        Touch.SetActive(true);

#elif UNITY_IOS
        Touch.SetActive(true);
#endif

    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && GamePaused == false)
        {
            GamePaused = true;
            if (GamePaused)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if (Input.GetButtonDown("Cancel") && GamePaused == true)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;
        }
    }

    public void MenuOpen()
    {
        StartCoroutine(MenuStart());
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        Touch.SetActive(false);
        Health.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void Unpause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Health.SetActive(true);
        PauseButton.SetActive(true);

#if UNITY_ANDROID
        Touch.SetActive(true);
#endif
    }

    IEnumerator MenuStart()
    {
        Time.timeScale = 1f;
        MyAnimatorFade.SetBool("Go", true);
        PauseMenu.SetActive(false);
        yield return new WaitForSeconds(1);
        Touch.SetActive(false);
        PauseButton.SetActive(false);
        Loading.SetActive(true);
        MyAnimatorFade.SetBool("Go", false);
        SceneManager.LoadScene("Main Menu");
    }
}
