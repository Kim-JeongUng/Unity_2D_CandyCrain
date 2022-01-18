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
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
        past_ped.position = ped.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        if (results.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickSound.Play();
                if (results[0].gameObject.CompareTag("ClassicMode"))
                {
                    if (!results[0].gameObject.transform.Find("Panel").gameObject.activeSelf) // 클릭 false 상태 일 때
                    {
                        results[0].gameObject.transform.Find("Panel").gameObject.SetActive(true);
                        canvas.transform.Find("Infinity").localPosition = new Vector3(-408, -236, 0);
                        canvas.transform.Find("TimeAttack").localPosition = new Vector3(-408, -385, 0);


                    }
                    else
                    {
                        results[0].gameObject.transform.Find("Panel").gameObject.SetActive(false);
                        canvas.transform.Find("Infinity").localPosition = new Vector3(-408, 21, 0);
                        canvas.transform.Find("TimeAttack").localPosition = new Vector3(-408, -128, 0);
                    }
                }
                if (results[0].gameObject.CompareTag("Level1"))
                {
                    PlayerPrefs.SetInt("level", 1);
                    PlayerPrefs.SetInt("Mode", 1);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("Level2"))
                {
                    PlayerPrefs.SetInt("level", 2);
                    PlayerPrefs.SetInt("Mode", 1);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("Level3"))
                {
                    PlayerPrefs.SetInt("level", 3);
                    PlayerPrefs.SetInt("Mode", 1);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("InfinityMode"))
                {
                    PlayerPrefs.SetInt("level", 3);
                    PlayerPrefs.SetInt("Mode", 2);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("TimeAttack"))
                {
                    PlayerPrefs.SetInt("level", 2);
                    PlayerPrefs.SetInt("Mode", 3);
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
        }
    }
    public void ResetData()
    {
        for (int i = 0; i < ScoreText.Length; i++)
        {
            PlayerPrefs.SetInt("score" + (i + 4), 0);
            PlayerPrefs.SetString("timer" + (i + 4), 0.0f.ToString("F1"));
        }
    }
}
