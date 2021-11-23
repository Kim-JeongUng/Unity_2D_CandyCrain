using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.EventSystems;
using System;

public class TouchEvent : MonoBehaviour
{

    public Canvas m_canvas;
    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    RawImage img;

    public GameObject SlotSpawnPoint;

    private GameObject Candies;

    void Start()
    {
        m_gr = m_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);
    }

    void Update()
    {
        m_ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_gr.Raycast(m_ped, results);


        if (Input.GetMouseButtonDown(0))
        {
            img = (RawImage)results[0].gameObject.GetComponent<RawImage>();
            Debug.Log(img.texture.name);
            if (results[0].gameObject.CompareTag("Candies")){
                SlotSpawn(results[0].gameObject);
                Destroy(results[0].gameObject);
            }
        }
    }

    public void SlotSpawn(GameObject results)
    {
        // 삭제 후 슬롯에서 스폰되게
        Candies = Instantiate(results, SlotSpawnPoint.transform.position, Quaternion.identity, SlotSpawnPoint.transform);
        Candies.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        //Candies.transform.SetParent(SlotSpawnPoint.transform);
    }
}
