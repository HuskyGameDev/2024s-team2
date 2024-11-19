using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{
    public int slot;
    public int count;
    public int type;
    public Image item;
    public Sprite[] items;


    // Start is called before the first frame update
    void Start()
    {

        switch (slot)
        {
            case 1:
                type = PlayerPrefs.GetInt("slot1");
                count = PlayerPrefs.GetInt("slot1amount");
                break;
            case 2:
                type = PlayerPrefs.GetInt("slot2");
                count = PlayerPrefs.GetInt("slot2amount");
                break;
            case 3:
                type = PlayerPrefs.GetInt("slot3");
                count = PlayerPrefs.GetInt("slot3amount");
                break;
            default:
                PlayerPrefs.GetInt("slot1");
                count = PlayerPrefs.GetInt("slot1amount");
                break;
        }
        item.sprite = items[type - 1];
    }
    //slot 1 q slot 2 e slot 3 r

    // Update is called once per frame
    void Update()
    {
        
    }
}
