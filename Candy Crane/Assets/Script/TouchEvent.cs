using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class TouchEvent : MonoBehaviour
{

    public AudioSource ClickSound;
    public Canvas canvas;
    GraphicRaycaster gr;
    PointerEventData ped;
    public GameObject Mirror;
    Game GameManager;

    private RectTransform RectTransform;

    public Admob admob;
    void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        GameManager = this.GetComponent<Game>();
        Mirror.SetActive(true);
        admob = GetComponent<Admob>();
    }

    void Update()
    {
        try
        {
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);


            if (results.Count > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(results[0]);
                    if (!results[0].gameObject.CompareTag("Bomb"))
                    {
                        ClickSound.Play();
                    }
                    // 캔디 삭제
                
                    if (results[0].gameObject.CompareTag("Candies") && Game.SlotChild < Game.MaxSlot)
                    {
                        //GameObject topResult = results[0].gameObject.transform.parent.GetChild(0).gameObject;
    /*
                        for (int i = 0; i < results[0].gameObject.transform.parent.childCount-1; i++) {
                            Debug.Log(results[0].gameObject.transform.parent.GetChild(i).rotation.z);
                            if (results[0].gameObject.transform.parent.GetChild(i).rotation.z == 0)
                            {
                                topResult = results[0].gameObject.transform.parent.GetChild(i).gameObject;
                                // 클릭된 칼럼의 상단에 있는 캔디 제거
                                break;
                            }
                        }*/
                        //RectTransform = topResult.GetComponent<RectTransform>();
                        //StartCoroutine(RectRotation());
                        GameManager.SlotSpawn(results[0].gameObject.transform.parent.GetChild(0).gameObject);
                    }
                    if (results[0].gameObject.CompareTag("Bomb"))
                    {
                        RectTransform = results[0].gameObject.GetComponent<RectTransform>();
                        StartCoroutine(RectRotation());
                        GameManager.CheckBomb();
                    }
                    if (results[0].gameObject.CompareTag("Mirror")) // 아이템
                    {
                        RectTransform = results[0].gameObject.GetComponent<RectTransform>();
                        StartCoroutine(RectRotation());
                        this.gameObject.GetComponent<Game>().UseMirror(results[0].gameObject);
                    }
                    if (results[0].gameObject.name == "Reload")
                    {
                        RectTransform = results[0].gameObject.GetComponent<RectTransform>();
                        StartCoroutine(RectRotation());
                        SceneManager.LoadScene("GameScene");
                    }
                    if (results[0].gameObject.CompareTag("GoMenu"))
                    {
                    
                        admob.ReplayGame();
                    
                    }


                }
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene("LobyScene");
        }
    }

    IEnumerator RectRotation()
    {
        RectTransform.rotation = new Quaternion(0, 0, 1, 20);
        yield return new WaitForSeconds(0.15f);
        RectTransform.rotation = new Quaternion(0, 0, 1, -20);
    }
}
