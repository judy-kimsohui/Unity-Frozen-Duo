using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    // UI를 담당하는 GameObject
    public GameObject gameOverUI;
    public GameObject nextstageUI;
    public GameObject pauseUI;
    public GameObject pauseButton;
    public GameObject startUI;
    public GameObject endUI;
    public GameObject infoUI;

    private bool isGamePaused = false;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        // 게임 매니저 싱글톤 설정
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void StartGame()
    {
        Debug.Log("게임 시작");
        pauseButton.SetActive(true);
        startUI.SetActive(false);
        NextStage();
    }

    public void EndGame()
    {
        Debug.Log("게임 끝");
        pauseButton.SetActive(false);
        GameClear();
        NextStage();
        endUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
        startUI.SetActive(true);
        endUI.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("게임 오버!");
        Time.timeScale = 0f;

        // 게임 오버 시 UI를 활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverUI가 할당되지 않았습니다.");
        }
    }

    public void Retry()
    {
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void GameClear()
    {
        Debug.Log("스테이지 클리어!");
        Time.timeScale = 0f;

        // 게임 오버 시 UI를 활성화
        if (nextstageUI != null)
        {
            nextstageUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("nextstageUI 할당되지 않았습니다.");
        }
    }


    public void NextStage()
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;


        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            nextstageUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogWarning("There is no next scene in the build settings!");
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Debug.Log("paused");

        if (isGamePaused)
        {
            Time.timeScale = 0f; // 시간을 멈춥니다.
            pauseUI.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f; // 시간을 다시 실행합니다.
            pauseUI.SetActive(false);
            pauseButton.SetActive(true);

        }
    }

    public void ToggleInfo()
    {
        isGamePaused = !isGamePaused;
        Debug.Log("paused");

        if (isGamePaused)
        {
            Time.timeScale = 0f; // 시간을 멈춥니다.
            infoUI.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f; // 시간을 다시 실행합니다.
            infoUI.SetActive(false);
            pauseButton.SetActive(true);

        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}