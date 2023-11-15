using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // UI를 담당하는 GameObject
    public GameObject gameOverUI;

    private void Awake()
    {
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

    public void GameOver()
    {
        Debug.Log("게임 오버!");

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
}
