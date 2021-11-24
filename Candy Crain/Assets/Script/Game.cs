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

    // 슬롯에 들어갈 수 있는 크기 제한
    public static int SlotChild = 0;
    public static int MaxSlot = 9;

    RawImage img;
    RawImage[] imgs = new RawImage[9];

    public AudioClip BombSound;
    public AudioClip NonBombSound;

    public AudioSource Audio;

    public bool isBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 슬롯의 갯수 확인
        SlotChild = Slot.transform.childCount;

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
        // 패배 확인
        if (SlotChild == MaxSlot)
        {
            
        }
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
    public void Bomb()
    {
        int stack = 1;
        if (SlotChild >= 3) { 
        for (int i = SlotChild - 1; i >= 0; i--)
        {
            imgs[i] = (RawImage)Slot.transform.GetChild(i).gameObject.GetComponent<RawImage>();
        }
            for (int i = SlotChild - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (stack < 3)
                    {
                        break;
                    }
                    else // 3개 이상 판별
                    {
                        Audio.PlayOneShot(BombSound);
                        isBomb = true;
                        for (int j = 0; j < stack; j++)
                            Destroy(Slot.transform.GetChild(SlotChild - j - 1).gameObject);
                        stack = 1;

                    }
                }
                else if (imgs[i].texture.name == imgs[i - 1].texture.name)
                {
                    stack++;
                }
                else if (imgs[i].texture.name != imgs[i - 1].texture.name || SlotChild == 0)
                {
                    if (stack < 3)
                    {
                        break;
                    }
                    else
                    {
                        Audio.PlayOneShot(BombSound);
                        isBomb = true;
                        for (int j = 0; j < stack; j++)
                            Destroy(Slot.transform.GetChild(SlotChild - j - 1).gameObject);
                        stack = 1;
                    }
                }
            }
        }
        if (!isBomb)
        {
            Audio.PlayOneShot(NonBombSound);
        }
        isBomb = false;
    }
    public void WinGame()
    {
        WinPanel.SetActive(true);
    }

}
