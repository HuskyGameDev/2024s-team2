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
    public TextMeshProUGUI displayCount;


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
        if (type != 0)
        {
            item.sprite = items[type - 1];
        }
        else{
            item.enabled = false;
        }
        syncAmount();
        displayCount.text = "" + count;
    }
    //slot 1 q slot 2 e slot 3 r

    void syncAmount()
    {
        int value;
        int maxVal;
        int final;
        switch (type)
        {
            case 1: //goblinSteak
                value = PlayerPrefs.GetInt("goblinSteak");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 2: //jellysand
                value = PlayerPrefs.GetInt("jellysand");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 3: //bbsand
                value = PlayerPrefs.GetInt("bbsand");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 4: //friedmush
                value = PlayerPrefs.GetInt("friedmush");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 5: //bbsoup
                value = PlayerPrefs.GetInt("bbsoup");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 6: //royalbbsoup
                value = PlayerPrefs.GetInt("royalbbsoup");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 7: //bossdrink
                value = PlayerPrefs.GetInt("bossdrink");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 8: //roastbone
                value = PlayerPrefs.GetInt("roastbone");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 9: //dundinner
                value = PlayerPrefs.GetInt("dundinner");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 10: //dunfeast
                value = PlayerPrefs.GetInt("dunfeast");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            default: //goblinSteak
                value = PlayerPrefs.GetInt("goblinSteak");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
        }
        Debug.Log(value);
        switch (slot)
        {
            case 1:
                if (value >= maxVal)
                {
                    PlayerPrefs.SetInt("slot1amount", maxVal);
                    final = maxVal;
                }
                else
                {
                    PlayerPrefs.SetInt("slot1amount", value);
                    final = value;
                }
                //PlayerPrefs.SetInt("slot1amount", value);
                break;
            case 2:
                if (value >= maxVal)
                {
                    PlayerPrefs.SetInt("slot2amount", maxVal);
                    final = maxVal;
                }
                else
                {
                    PlayerPrefs.SetInt("slot2amount", value);
                    final = value;
                }
                //PlayerPrefs.SetInt("slot2amount", value);
                break;
            case 3:
                if (value >= maxVal)
                {
                    PlayerPrefs.SetInt("slot3amount", maxVal);
                    final = maxVal;
                }
                else
                {
                    PlayerPrefs.SetInt("slot3amount", value);
                    final = value;
                }
                //PlayerPrefs.SetInt("slot3amount", value);
                break;
            default:
                if (value >= maxVal)
                {
                    PlayerPrefs.SetInt("slot1amount", maxVal);
                    final = maxVal;
                }
                else
                {
                    PlayerPrefs.SetInt("slot1amount", value);
                    final = value;
                }
                //PlayerPrefs.SetInt("slot1amount", value);
                break;
        }
        displayCount.text = "" + final;
        count = final;
    }

    // Update is called once per frame
    void Update()
    {
        switch (slot)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Q) && (count != 0))
                {
                    PlayerPrefs.SetInt("slot1amount", count-1);
                    count = count - 1;
                    useItem();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.E) && (count != 0))
                {
                    PlayerPrefs.SetInt("slot2amount", count - 1);
                    count = count - 1;
                    useItem();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.R) && (count != 0))
                {
                    PlayerPrefs.SetInt("slot3amount", count - 1);
                    count = count - 1;
                    useItem();
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.E) && (count != 0))
                {
                    PlayerPrefs.SetInt("slot1amount", count - 1);
                    count = count - 1;
                    useItem();
                }
                break;
        }
        displayCount.text = "" + count;
    }

    void useItem()
    {
        switch (type)
        {
            case 1: //use goblinSteak
                PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak")-1);
                break;
            case 2: //use jellysand
                PlayerPrefs.SetInt("jellysand", PlayerPrefs.GetInt("jellysand") - 1);
                break;
            case 3: //use bbsand
                PlayerPrefs.SetInt("bbsand", PlayerPrefs.GetInt("bbsand") - 1);
                break;
            case 4: //use friedmush
                PlayerPrefs.SetInt("friedmush", PlayerPrefs.GetInt("friedmush") - 1);
                break;
            case 5: //use bbsoup
                PlayerPrefs.SetInt("bbsoup", PlayerPrefs.GetInt("bbsoup") - 1);
                break;
            case 6: //use royalbbsoup
                PlayerPrefs.SetInt("royalbbsoup", PlayerPrefs.GetInt("royalbbsoup") - 1);
                break;
            case 7: //use bossdrink
                PlayerPrefs.SetInt("bossdrink", PlayerPrefs.GetInt("bossdrink") - 1);
                break;
            case 8: //use roastbone
                PlayerPrefs.SetInt("roastbone", PlayerPrefs.GetInt("roastbone") - 1);
                break;
            case 9: //use dundinner
                PlayerPrefs.SetInt("dundinner", PlayerPrefs.GetInt("dundinner") - 1);
                break;
            case 10: //use dunfeast
                PlayerPrefs.SetInt("dunfeast", PlayerPrefs.GetInt("dunfeast") - 1);
                break;
            default: //use goblinSteak
                PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak") - 1);
                break;
        }
    }
}
