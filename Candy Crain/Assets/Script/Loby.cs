using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Loby : MonoBehaviour
{
    public Canvas canvas;
    GraphicRaycaster gr;
    PointerEventData ped;
    RawImage img;

    Game GameManager;

    public AudioSource ClickSound;


    void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        GameManager = this.GetComponent<Game>();
    }

    void Update() {
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        if (results.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickSound.Play();
                if (results[0].gameObject.CompareTag("Level1"))
                {
                    PlayerPrefs.SetInt("level", 1);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("Level2"))
                {
                    PlayerPrefs.SetInt("level", 2);
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.CompareTag("Level3"))
                {
                    PlayerPrefs.SetInt("level", 3);
                    SceneManager.LoadScene("GameScene");
                }

            }
        }


    }
}
