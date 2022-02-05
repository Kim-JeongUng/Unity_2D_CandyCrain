using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapMaker : MonoBehaviour
{
    public Texture[] AllCandies;
    public Texture[] ChooseCandies;
    public GameObject pf_Candies;
    public GameObject Panel;
    public Text ModeText;

    // 맵 크기
    public static int MaxCol = 6;
    public static int MaxRow = 9;
    public GameObject[] ParentCol = new GameObject[MaxCol];

    // 랜덤 CandiesImg 설정
    private int RanIndex;

    private GameObject Candies;
    private RawImage img;

    public static int level = 1;

    public bool RunningTimeAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        //선택한 레벨에 맞는 텍스쳐 생성 1레벨 : 4가지종류, 2레벨 : 5가지종류, 3레벨 : 6가지종류
        level = PlayerPrefs.GetInt("level") + 3;
        int[] RanImgIndex = getRandomInt(level, 0, AllCandies.Length);
        ChooseCandies = new Texture[level];
        for (int i = 0; i < level; i++)
        {
            ChooseCandies[i] = AllCandies[RanImgIndex[i]];
        }
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            switch (level)
            {
                case 6:
                    ModeText.text = "HARD";
                    break;
                case 5:
                    ModeText.text = "NORMAL";
                    break;
                default:
                    ModeText.text = "EASY";
                    break;
            }
        }
        else if(PlayerPrefs.GetInt("Mode") == 2)
        {
            ModeText.text = "INFINITY";
        }
        else if (PlayerPrefs.GetInt("Mode") == 3)
        {
            ModeText.text = "TIME ATTACK";
        }


        if (PlayerPrefs.GetInt("Mode") != 3) {
            // 맵 생성
            int[] MinCheckCnt = new int[level];
            for (int i = 0; i < level; i++)
            {
                MinCheckCnt[i] = 0;
            }
            for (int i = MaxRow - 1; i >= 0; i--)
            {
                for (int j = MaxCol - 1; j >= 0; j--)
                {
                    RanIndex = Random.Range(0, ChooseCandies.Length);

                    Candies = Instantiate(pf_Candies, new Vector2(ParentCol[j].transform.position.x, ParentCol[j].transform.position.y + (i * 143)), Quaternion.identity);

                    img = (RawImage)Candies.GetComponent<RawImage>();
                    img.texture = (Texture)ChooseCandies[RanIndex];

                    Candies.transform.SetParent(ParentCol[j].transform, false);
                    for (int k = 0; k < level; k++)
                    {
                        if (img.texture.name == ChooseCandies[k].name)
                        {
                            MinCheckCnt[k]++;
                        }
                    }
                }
            }
            for (int i = 0; i < level; i++)
            {
                if (MinCheckCnt[i] < 3) // 최소한 같은 이미지가 3개 이상 씩 나와야함
                {
                    SceneManager.LoadScene("GameScene");
                }
            }
        }
        else if(PlayerPrefs.GetInt("Mode") == 3) { // 타임 어택 모드
            RunningTimeAttack = true;
            StartCoroutine("TimeAttack");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TimeAttack()
    {
        float speedLevel = 3.3f;
        while (RunningTimeAttack)
        {
            for (int i = MaxCol - 1; i >= 0; i--)
            {
                if (ParentCol[i].transform.childCount >= MaxRow-1)
                {
                    RunningTimeAttack = false;
                }

                int RanIndex = Random.Range(0, ChooseCandies.Length);

                foreach (Transform tf in ParentCol[i].transform.GetComponentInChildren<RectTransform>())
                {
                    tf.localPosition += new Vector3(0, 143.0f, 0);

                }
                Candies = Instantiate(pf_Candies, ParentCol[i].transform.position, Quaternion.identity, ParentCol[i].transform);

                img = (RawImage)Candies.GetComponent<RawImage>();
                img.texture = (Texture)ChooseCandies[RanIndex];
            }
            if ( speedLevel > 2.5f)
                speedLevel *= 0.97f;

            yield return new WaitForSeconds(speedLevel);
        }
    }

    public int[] getRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max);
                isSame = false;

                for (int j = 0; j < i; ++j)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArray;
    }

}
