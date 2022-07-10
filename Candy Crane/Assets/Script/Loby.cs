using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class Loby : MonoBehaviour
{
    public GameObject RecordPanel;
    public AudioSource ClickSound;
    public Canvas canvas;
    GraphicRaycaster gr;
    PointerEventData ped;
    PointerEventData past_ped;
    RawImage img;

    public Text[] ScoreText;
    public Text[] TimerText;

    public GameObject[] LevelText;

    Game GameManager;

    void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        past_ped = new PointerEventData(null);
        GameManager = this.GetComponent<Game>();
    }

    void Update()
    {
        try
        {
            ped.position = Input.mousePosition;
            past_ped.position = ped.position;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);

            if (results.Count > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClickSound.Play();
                    Debug.Log(results[0]);
                    if (results[0].gameObject.CompareTag("ClassicMode"))
                    {
                        if (!LevelText[0].gameObject.activeSelf) // 클릭 false 상태 일 때
                        {
                            LevelText[0].gameObject.SetActive(true);
                            LevelText[1].gameObject.SetActive(true);
                            LevelText[2].gameObject.SetActive(true);
                            LevelText[3].gameObject.SetActive(true);
                        }
                        else
                        {
                            LevelText[0].gameObject.SetActive(false);
                            LevelText[1].gameObject.SetActive(false);
                            LevelText[2].gameObject.SetActive(false);
                            LevelText[3].gameObject.SetActive(false);
                        }
                    }
                    if (results[0].gameObject.CompareTag("Level1"))
                    {
                        PlayerPrefs.SetInt("Mode", 1);
                        PlayerPrefs.SetInt("level", 1);
                        SceneManager.LoadScene("GameScene");
                    }
                    if (results[0].gameObject.CompareTag("Level2"))
                    {
                        PlayerPrefs.SetInt("Mode", 1);
                        PlayerPrefs.SetInt("level", 2);
                        SceneManager.LoadScene("GameScene");
                    }
                    if (results[0].gameObject.CompareTag("Level3"))
                    {
                        PlayerPrefs.SetInt("Mode", 1);
                        PlayerPrefs.SetInt("level", 3);
                        SceneManager.LoadScene("GameScene");
                    }
                    if (results[0].gameObject.CompareTag("InfinityMode"))
                    {
                        PlayerPrefs.SetInt("Mode", 2);
                        PlayerPrefs.SetInt("level", 3);
                        SceneManager.LoadScene("GameScene");
                    }
                    if (results[0].gameObject.CompareTag("TimeAttack"))
                    {
                        PlayerPrefs.SetInt("Mode", 3);
                        PlayerPrefs.SetInt("level", 2);
                        SceneManager.LoadScene("GameScene");
                    }

                    if (results[0].gameObject.CompareTag("Record"))
                    {
                        RecordPanel.SetActive(true);
                        PanelWrite();
                    }
                    if (results[0].gameObject.CompareTag("RecordPanel"))
                    {
                        RecordPanel.SetActive(false);
                    }
                    if (results[0].gameObject.CompareTag("Reset"))
                    {
                        ResetData();
                        PanelWrite();
                    }
                    if (results[0].gameObject.CompareTag("Help"))
                    {
                        Application.OpenURL("https://youtu.be/d8S2b3hhZXc");
                    }
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }
    public void PanelWrite()
    {
        for (int i = 0; i < ScoreText.Length; i++)
        {
            if (i<3)
            {
                ScoreText[i].text = PlayerPrefs.GetInt("score" + (i + 4)).ToString();
                TimerText[i].text = PlayerPrefs.GetString("timer" + (i + 4)).ToString();
            }
            else if (i == 3) // 무한 모드
            {
                ScoreText[i].text = PlayerPrefs.GetInt("scoreInfinity").ToString();
                TimerText[i].text = PlayerPrefs.GetString("timerInfinity").ToString();
            }
            else if (i == 4) // 타임어택 모드
            {
                ScoreText[i].text = PlayerPrefs.GetInt("scoreTimeAttack").ToString();
                TimerText[i].text = PlayerPrefs.GetString("timerTimeAttack").ToString();
            }
            if(ScoreText[i].text == 0.ToString())
            {
                ScoreText[i].text = "No";
                TimerText[i].text = " Data";
            }
        }
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
