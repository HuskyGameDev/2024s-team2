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
            case 2: //pbnjelly
                value = PlayerPrefs.GetInt("pbnjelly");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 3: //pbnbossjelly
                value = PlayerPrefs.GetInt("pbnbossjelly");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 4: //dish4
                value = PlayerPrefs.GetInt("dish4");
                maxVal = PlayerPrefs.GetInt("maxHold");
                break;
            case 5: //dish5
                value = PlayerPrefs.GetInt("dish5");
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
            case 2: //use pbnjelly
                PlayerPrefs.SetInt("pbnjelly", PlayerPrefs.GetInt("pbnjelly") - 1);
                break;
            case 3: //pbnbossjelly
                PlayerPrefs.SetInt("pbnbossjelly", PlayerPrefs.GetInt("pbnbossjelly") - 1);
                break;
            case 4: //use dish4
                PlayerPrefs.SetInt("dish4", PlayerPrefs.GetInt("dish4") - 1);
                break;
            case 5: //use dish5
                PlayerPrefs.SetInt("dish5", PlayerPrefs.GetInt("dish5") - 1);
                break;
            default: //use goblinSteak
                PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak") - 1);
                break;
        }
    }
}
