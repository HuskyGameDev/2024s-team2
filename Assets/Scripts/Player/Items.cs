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
    public PlayerHealth playerHealth;
    public Animator animatorUI;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("speedBoostWalk", 7);
        PlayerPrefs.SetInt("speedBoostSprint", 14);

        switch (slot)
        {
            case 1:
                type = PlayerPrefs.GetInt("slot1");
                count = PlayerPrefs.GetInt("slot1amount");
                PlayerPrefs.SetInt("coolDown1", 0);
                animatorUI = GameObject.Find("itemCooldown1").GetComponent<Animator>();
                break;
            case 2:
                type = PlayerPrefs.GetInt("slot2");
                count = PlayerPrefs.GetInt("slot2amount");
                PlayerPrefs.SetInt("coolDown2", 0);
                animatorUI = GameObject.Find("itemCooldown2").GetComponent<Animator>();
                break;
            case 3:
                type = PlayerPrefs.GetInt("slot3");
                count = PlayerPrefs.GetInt("slot3amount");
                PlayerPrefs.SetInt("coolDown3", 0);
                animatorUI = GameObject.Find("itemCooldown3").GetComponent<Animator>();
                break;
            default:
                PlayerPrefs.GetInt("slot1");
                count = PlayerPrefs.GetInt("slot1amount");
                PlayerPrefs.SetInt("coolDown1", 0);
                animatorUI = GameObject.Find("itemCooldown1").GetComponent<Animator>();
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
                if (Input.GetKeyDown(KeyCode.Q) && (count != 0) && (PlayerPrefs.GetInt("coolDown1") != 1))
                {
                    PlayerPrefs.SetInt("slot1amount", count-1);
                    count = count - 1;
                    useItem();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.E) && (count != 0) && (PlayerPrefs.GetInt("coolDown2") != 1))
                {
                    PlayerPrefs.SetInt("slot2amount", count - 1);
                    count = count - 1;
                    useItem();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.R) && (count != 0) && (PlayerPrefs.GetInt("coolDown3") != 1))
                {
                    PlayerPrefs.SetInt("slot3amount", count - 1);
                    count = count - 1;
                    useItem();
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.E) && (count != 0) && (PlayerPrefs.GetInt("coolDown1") != 1))
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
        int timeCool = 0;
        switch (type)
        {
            case 1: //use goblinSteak, regain some health  (carry 3 max, 10 hp? cooldown of 5 seconds)
                PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak")-1);
                PlayerPrefs.SetInt("healingVal", 10);
                playerHealth.AddHealth();
                animatorUI.Play("itemCooldown5sec");
                timeCool = 5;
                break;
            case 2: //use jellysand, increase movement speed (carry 3 max, ~15 seconds?) (move speed 10 sprint speed 17)
                PlayerPrefs.SetInt("jellysand", PlayerPrefs.GetInt("jellysand") - 1);
                PlayerPrefs.SetInt("speedBoostWalk", 10);
                PlayerPrefs.SetInt("speedBoostSprint", 17);
                animatorUI.Play("itemCooldown15sec");
                timeCool = 15;
                break;
            case 3: //use bbsand, reduce damage taken (carry 3 max, ~30 seconds?)
                PlayerPrefs.SetInt("bbsand", PlayerPrefs.GetInt("bbsand") - 1);
                PlayerPrefs.SetFloat("defBuff", 0.8f);
                animatorUI.Play("itemCooldown30sec");
                timeCool = 30;
                break;
            case 4: //use friedmush, increase damage dealt (carry 3 max, ~15 seconds?)
                PlayerPrefs.SetInt("friedmush", PlayerPrefs.GetInt("friedmush") - 1);
                PlayerPrefs.SetFloat("attBuff", 1.3f);
                animatorUI.Play("itemCooldown15sec");
                timeCool = 15;
                break;
            case 5: //use bbsoup, regain some health  (carry 3 max, 15 hp? cooldown of 8 seconds)
                PlayerPrefs.SetInt("bbsoup", PlayerPrefs.GetInt("bbsoup") - 1);
                PlayerPrefs.SetInt("healingVal", 15);
                playerHealth.AddHealth();
                animatorUI.Play("itemCooldown8sec");
                timeCool = 8;
                break;
            case 6: //use royalbbsoup, increase movement speed (carry 3 max, ~25 seconds?) (move speed 10 sprint speed 17)
                PlayerPrefs.SetInt("royalbbsoup", PlayerPrefs.GetInt("royalbbsoup") - 1);
                PlayerPrefs.SetInt("speedBoostWalk", 10);
                PlayerPrefs.SetInt("speedBoostSprint", 17);
                animatorUI.Play("itemCooldown25sec");
                timeCool = 25;
                break;
            case 7: //use bossdrink, increase total health for a level by 50? (carry 2 max)
                PlayerPrefs.SetInt("bossdrink", PlayerPrefs.GetInt("bossdrink") - 1);
                playerHealth.IncMaxHP(50);
                timeCool = 0;
                break;
            case 8: //use roastbone, reduce damage taken (carry 3 max, ~40 seconds?)
                PlayerPrefs.SetInt("roastbone", PlayerPrefs.GetInt("roastbone") - 1);
                PlayerPrefs.SetFloat("defBuff", 0.7f);
                animatorUI.Play("itemCooldown40sec");
                timeCool = 40;
                break;
            case 9: //use dundinner, increase damage dealt (carry 3 max, ~25 seconds?)
                PlayerPrefs.SetInt("dundinner", PlayerPrefs.GetInt("dundinner") - 1);
                PlayerPrefs.SetFloat("attBuff", 1.5f);
                animatorUI.Play("itemCooldown30sec");
                timeCool = 30;
                break;
            case 10: //use dunfeast, completely recover health (carry 2 max, cooldown of 90 seconds?)
                PlayerPrefs.SetInt("dunfeast", PlayerPrefs.GetInt("dunfeast") - 1);
                playerHealth.FullRecover();
                animatorUI.Play("itemCooldown75sec");
                timeCool = 75;
                break;
            default: //use goblinSteak, regain some health  (10 hp? cooldown of 5 seconds)
                PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak") - 1);
                PlayerPrefs.SetInt("healingVal", 10);
                playerHealth.AddHealth();
                animatorUI.Play("itemCooldown5sec");
                timeCool = 5;
                break;
        }
        StartCoroutine(ItemCooldown(timeCool));
    }

    IEnumerator ItemCooldown(int time)
    {
        switch (slot)
        {
            case 1:
                PlayerPrefs.SetInt("coolDown1", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("coolDown2", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("coolDown3", 1);
                break;
            default:
                PlayerPrefs.SetInt("coolDown1", 1);
                break;

        }

        //could start animations here?
        
        yield return new WaitForSeconds(time);

        switch (slot)
        {
            case 1:
                PlayerPrefs.SetInt("coolDown1", 0);
                break;
            case 2:
                PlayerPrefs.SetInt("coolDown2", 0);
                break;
            case 3:
                PlayerPrefs.SetInt("coolDown3", 0);
                break;
            default:
                PlayerPrefs.SetInt("coolDown1", 0);
                break;

        }

        switch (type)
        {
            case 1: //used goblinSteak
                PlayerPrefs.SetInt("healingVal", 0);
                break;
            case 2: //used jellysand
                PlayerPrefs.SetInt("speedBoostWalk", 7);
                PlayerPrefs.SetInt("speedBoostSprint", 14);
                break;
            case 3: //used bbsand
                PlayerPrefs.SetFloat("defBuff", 1f);
                break;
            case 4: //used friedmush
                PlayerPrefs.SetFloat("attBuff", 1f);
                break;
            case 5: //used bbsoup
                PlayerPrefs.SetInt("healingVal", 0);
                break;
            case 6: //used royalbbsoup
                PlayerPrefs.SetInt("speedBoostWalk", 7);
                PlayerPrefs.SetInt("speedBoostSprint", 14);
                break;
            case 7: //used bossdrink
                break;
            case 8: //used roastbone
                PlayerPrefs.SetFloat("defBuff", 1f);
                break;
            case 9: //used dundinner
                PlayerPrefs.SetFloat("attBuff", 1f);
                break;
            case 10: //used dunfeast
                break;
            default: //used goblinSteak
                PlayerPrefs.SetInt("healingVal", 0);
                break;
        }
    }
}
