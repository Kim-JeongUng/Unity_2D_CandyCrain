using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour
{
    public Texture[] AllCandies;
    public Texture[] ChooseCandies;
    public GameObject pf_Candies;
    public GameObject Panel;
    public Text Mode;
    public Text Score;


    private RawImage img;

    // 맵 크기
    private static int MaxCol = 6;
    private static int MaxRow = 9;
    public GameObject[] ParentCol = new GameObject[MaxCol];

    // 랜덤 CandiesImg 설정
    private int RanIndex;

    private GameObject Candies;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        //선택한 레벨에 맞는 텍스쳐 생성 1레벨 : 4가지종류, 2레벨 : 5가지종류, 3레벨 : 6가지종류
        level = PlayerPrefs.GetInt("level")+3;
        Debug.Log(level);
        int[] RanImgIndex = getRandomInt(level, 0, AllCandies.Length);
        ChooseCandies = new Texture[level];
        for (int i = 0; i < level; i++)
        {
            ChooseCandies[i] = AllCandies[RanImgIndex[i]];
        }
        switch (level)
        {
            case 6:
                Mode.text = "HARD";
                break;
            case 5:
                Mode.text = "NORMAL";
                break;
            default:
                Mode.text = "EASY";
                break;

        }

        // 캔디 생성
        for (int i = 0; i < MaxRow; i++)
        {
            for (int j = 0; j < MaxCol; j++)
            {
                RanIndex = Random.Range(0, ChooseCandies.Length);

                Candies = Instantiate(pf_Candies, new Vector2(ParentCol[j].transform.position.x, ParentCol[j].transform.position.y + (i * 142)), Quaternion.identity);
                
                img = (RawImage)Candies.GetComponent<RawImage>();
                img.texture = (Texture)ChooseCandies[RanIndex]; 

                Candies.transform.SetParent(ParentCol[j].transform,false);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
