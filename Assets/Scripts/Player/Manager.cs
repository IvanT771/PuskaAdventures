using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Высота конца игры")]
    public float hightGameOver = -5f;
    [Header("UI panels")]
    public GameObject[] panels; //0 - GameOver, 1 - Win, 2-Pause
    [Header("body player")]
    public Transform body;

    private bool isPause = false;

    private void Start()
    {
        GameResume();
    }
    private void CursorSetVisable(bool set)
    {
        if (set)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        CursorSetVisable(true);
        panels[0].SetActive(true);
    }

    public void GameWin()
    {
        CursorSetVisable(true);
        panels[1].SetActive(true);
    }

    public void GamePause()
    {
        CursorSetVisable(true);
        panels[2].SetActive(true);
        isPause = true;
    }

    public void GameResume()
    {
        CursorSetVisable(false);
        for (int i = 0; i < panels.Length; i++) { panels[i].SetActive(false); }
        isPause = false;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        if (body.position.y <= hightGameOver) { GameOver(); }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause) { GamePause();  isPause = true; } else { GameResume(); isPause = false; }
        }
    }
}
