using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public SceneChange scenechange;

    public GameObject PauseMenuUI;
    
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
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
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
