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

    public GameObject ModeEndPanel;

    public Text WinScore;
    public Text WinTimer;
    public Text PanelScore;
    public Text PanelTimer;

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

    MapMaker mapMaker;

    // Start is called before the first frame update
    void Start()
    { 
        isGame= true;
        score = 0;
        timer = 0.0f;
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        ModeEndPanel.SetActive(false);
        mapMaker = this.GetComponent<MapMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // 슬롯의 갯수 확인
        SlotChild = Slot.transform.childCount;

        ScoreText.text = score.ToString();
        // 승리 확인
        GameObject[] ParentCol = mapMaker.ParentCol;
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
        if (!mapMaker.RunningTimeAttack && PlayerPrefs.GetInt("Mode") == 3) // 타임어택 모드 게임종료
        {
            LoseGame();
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

        GameObject Presult = results.transform.parent.gameObject;
        Destroy(results.gameObject, 0.3f);

        if (PlayerPrefs.GetInt("Mode") == 2) // 인피니티모드 슬롯 스폰
        {
            int RanIndex = Random.Range(0, mapMaker.ChooseCandies.Length);
            foreach(Transform tf in Presult.transform.GetComponentInChildren<RectTransform>())
            {
                tf.localPosition += new Vector3(0, 143.0f, 0);
                
            }
            Candies = Instantiate(mapMaker.pf_Candies, Presult.transform.position, Quaternion.identity, Presult.transform);

            img = (RawImage)Candies.GetComponent<RawImage>();
            img.texture = (Texture)mapMaker.ChooseCandies[RanIndex];
        }

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

    // 게임 종료 체크
    public void WinGame()
    {
        isGame = false;

        WinPanel.SetActive(true);
        Audio.PlayOneShot(WinSound);

        WinScore.text = score.ToString();
        WinTimer.text = timer.ToString("F1");

        // 데이터 저장
        if(PlayerPrefs.GetInt("Mode")==1)
        { 
            if (!PlayerPrefs.HasKey("score" + MapMaker.level) || PlayerPrefs.GetInt("score" + MapMaker.level) < score)
            {
                PlayerPrefs.SetInt("score" + MapMaker.level, score);
                PlayerPrefs.SetString("timer" + MapMaker.level, timer.ToString("F1"));
            }
        }
    }
    public void LoseGame()
    {
        isGame = false;
        Audio.PlayOneShot(LoseSound);
        if (PlayerPrefs.GetInt("Mode") == 1) // 클래식모드
        {
            LosePanel.SetActive(true);
        }
        else 
        {
            isGame = false;
            ModeEndPanel.SetActive(true);

            PanelScore.text = score.ToString();
            PanelTimer.text = timer.ToString("F1");

            if (PlayerPrefs.GetInt("Mode") == 2) // 무한모드
            {
                InfinityEnd(); 
            }
            else if (PlayerPrefs.GetInt("Mode") == 3) // 타임어택모드
            {
                TimeAttackEnd();
                mapMaker.StopCoroutine("TimeAttack");
            }
        }
    }

    public void InfinityEnd()
    {
        if (!PlayerPrefs.HasKey("scoreInfinity") || PlayerPrefs.GetInt("scoreInfinity") < score)
        {
            PlayerPrefs.SetInt("scoreInfinity", score);
            PlayerPrefs.SetString("timerInfinity", timer.ToString("F1"));
        }
    }

    public void TimeAttackEnd()
    {
        if (!PlayerPrefs.HasKey("timerTimeAttack") || PlayerPrefs.GetInt("timerTimeAttack") > timer)
        {
            PlayerPrefs.SetInt("scoreTimeAttack", score);
            PlayerPrefs.SetString("timerTimeAttack", timer.ToString("F1"));
        }
    }

    // 아이템
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

}
