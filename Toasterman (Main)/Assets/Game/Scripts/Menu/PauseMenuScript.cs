using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
    public SceneChange scenechange;

    public GameObject PauseMenuUI;

    public Animator anim;

    public static bool GameIsPaused = false;
    public static bool Pausable = true;

    public void Start()
    {
        Pausable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Pausable)
        {
            if (GameIsPaused)
            {
                Time.timeScale = 1f;
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

    public IEnumerator ExitMenu()
    {
        Time.timeScale = 1f;
        anim.Play("UnPause");
        yield return new WaitForSeconds(0.15f);
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        StartCoroutine(ExitMenu());
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        scenechange.SceneNumber = 2;
        scenechange.ChangeScene();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    public void QuitGame()
    {
        Resume();
        scenechange.SceneNumber = 1;
        scenechange.ChangeScene();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
