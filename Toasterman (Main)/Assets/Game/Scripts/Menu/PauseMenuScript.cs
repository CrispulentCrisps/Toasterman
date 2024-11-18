using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
    public SceneChange scenechange;

    public GameObject PauseMenuUI;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;

    public Animator anim;

    public static bool GameIsPaused = false;
    public static bool Pausable = true;

    public void Start()
    {
        scenechange = FindObjectOfType<SceneChange>().GetComponent<SceneChange>();
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
                Debug.Log("IS RESUMED");
            }
            else
            {
                Pause();
                Debug.Log("IS PAUSED");
            }
        }   
    }

    public IEnumerator ExitMenu()
    {
        anim.Play("UnPause");
        yield return new WaitForSeconds(0.15f);
        PauseMenuUI.SetActive(false);
        GameIsPaused = true;
    }

    public void OptionsToPause()
    {
        PauseMenu.active = true;
        OptionsMenu.active = false;
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
