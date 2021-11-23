using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapMaker : MonoBehaviour
{
    public Texture[] CandiesImg;
    public GameObject pf_Candies;
    public GameObject Panel;

    private RawImage img;

    // 맵 크기
    private int MaxRow = 6;
    private int MaxCol = 9;

    // 랜덤 CandiesImg 설정
    private int RanIndex;

    private GameObject Candies;

    // Start is called before the first frame update
    void Start()
    {
        // 캔디 생성
        for (int i = 0; i < MaxCol; i++)
        {
            for (int j = 0; j < MaxRow; j++)
            {
                RanIndex = Random.Range(0, CandiesImg.Length);

                Candies = Instantiate(pf_Candies, new Vector2(j * 48 + 24, (48 * i) + 160), Quaternion.identity);
                
                img = (RawImage)Candies.GetComponent<RawImage>();
                img.texture = (Texture)CandiesImg[RanIndex]; 

                Candies.transform.SetParent(Panel.transform);
            }
        }
        /*for (int i = 0; i < MaxCol; i++)
        {
            for (int j = 0; j < MaxRow; j++)
            {
                RanIndex = Random.Range(0, CandiesImg.Length);
                Candies.transform.position = new Vector2(j * 49 + 24, (49 * i) + 172);
                img = (RawImage)Candies.GetComponent<RawImage>();
                img.texture = (Texture)CandiesImg[RanIndex];

                PreFabCandies = Instantiate(Candies);
                PreFabCandies.transform.SetParent(Panel.transform);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
