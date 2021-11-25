using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject Slot;
    private GameObject Candies;
    public GameObject WinPanel;
    public GameObject LosePanel;

    public Text WinScore;
    public Text WinTimer;
    public Text ScoreText;

    // 슬롯에 들어갈 수 있는 크기 제한
    public static int SlotChild = 0;
    public static int MaxSlot = 9;

    RawImage img;
    RawImage[] imgs = new RawImage[9];

    public AudioClip BombSound;
    public AudioClip NonBombSound;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    public AudioSource Audio;

    public bool isBombed = false;

    public static int score;
    public static float timer;

    bool isGame;
    // Start is called before the first frame update
    void Start()
    { 
        isGame= true;
        score = 0;
        timer = 0.0f;
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 슬롯의 갯수 확인
        SlotChild = Slot.transform.childCount;

        ScoreText.text = score.ToString();
        // 승리 확인
        GameObject[] ParentCol = this.GetComponent<MapMaker>().ParentCol;
        for (int i=0; i< ParentCol.Length; i++)
        {
            if(ParentCol[i].transform.childCount != 0)
            {
                break;
            }
            if (i == ParentCol.Length-1 && SlotChild == 0)
            {
                WinGame();
            }
        }
        // 패배 확인 (슬롯이 꽉 차있으면서 윗부분이 3스택 이하 면)
        int stack = 1;
        for (int i = SlotChild - 1; i >= 0; i--)
        {
            imgs[i] = (RawImage)Slot.transform.GetChild(i).gameObject.GetComponent<RawImage>();
        }
        for (int i = SlotChild - 1; i >= 0; i--)
        {
            if (i != 0 && imgs[i].texture.name == imgs[i - 1].texture.name)
            {
                stack++;
            }
            else if (i == 0 || imgs[i].texture.name != imgs[i - 1].texture.name || SlotChild == 0)
            {
                if (stack < 3)
                {
                    if (SlotChild == MaxSlot)
                    {
                        LoseGame();
                    }
                }
            }
        }
        for (int i = 0; i < MapMaker.MaxCol; i++) {
            if (this.GetComponent<MapMaker>().ParentCol[i].transform.childCount>0) {
                break;
            }
            else if(i== MapMaker.MaxCol-1 && SlotChild>0 && stack<3) // 보드 판이 다 비었지만 슬롯은 차 있는 경우
            {
                LoseGame();
            }
        }
        if (isGame == true)
            timer += Time.deltaTime;
    }

    // 슬롯 스폰
    public void SlotSpawn(GameObject results)
    {
        Candies = Instantiate(results, Slot.transform.position, Quaternion.identity, Slot.transform);
        Candies.GetComponent<BoxCollider2D>().enabled = true;
        Candies.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Destroy(results.gameObject); 
    }

    // 폭발
    public void CheckBomb()
    {
        int stack = 1;
        if (SlotChild >= 3) { 
        for (int i = SlotChild - 1; i >= 0; i--)
        {
            imgs[i] = (RawImage)Slot.transform.GetChild(i).gameObject.GetComponent<RawImage>();
        }
            for (int i = SlotChild - 1; i >= 0; i--)
            {
                
                if (i==0 || imgs[i].texture.name != imgs[i - 1].texture.name || SlotChild == 0)
                {
                    if (stack < 3)
                    {
                        break;
                    }
                    else
                    {
                        Bomb(stack);
                        break;
                    }
                }
                else if(i != 0 && imgs[i].texture.name == imgs[i - 1].texture.name)
                {
                    stack++;
                }
            }
        }
        if (!isBombed)
        {
            Audio.PlayOneShot(NonBombSound);
        }
        isBombed = false;
    }
    public void Bomb(int stack)
    {
        Audio.PlayOneShot(BombSound);
        isBombed = true;
        for (int j = 0; j < stack; j++)
            Destroy(Slot.transform.GetChild(SlotChild - j - 1).gameObject);
        score += (stack - 2) * 3;
        stack = 1;
    }
    public void UseMirror(GameObject Mirror)
    {
        if (SlotChild > 0)
        {
            // 슬롯의 마지막 캔디를 복사함
            Candies = Instantiate(Slot.transform.GetChild(SlotChild - 1).gameObject, Slot.transform.position, Quaternion.identity, Slot.transform);
            Candies.GetComponent<BoxCollider2D>().enabled = true;
            Candies.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            Mirror.SetActive(false);
        }
    }
    public void WinGame()
    {
        isGame = false;

        WinPanel.SetActive(true);
        Audio.PlayOneShot(WinSound);

        WinScore.text = score.ToString();
        WinTimer.text = timer.ToString("F1");

        // 데이터 저장
        if (!PlayerPrefs.HasKey("score" + MapMaker.level) || PlayerPrefs.GetInt("score" + MapMaker.level) < score)
        {
            PlayerPrefs.SetInt("score" + MapMaker.level, score);
            PlayerPrefs.SetString("timer" + MapMaker.level, timer.ToString("F1"));
            Debug.Log(MapMaker.level);
        }

    }
    public void LoseGame()
    {
        isGame = false;
        LosePanel.SetActive(true);
        Audio.PlayOneShot(LoseSound);
    }
}
