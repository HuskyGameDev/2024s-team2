using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponDescription;
    public TextMeshProUGUI selectWeaponConfirm;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    //public TextMeshProUGUI selectItemConfirm;
    //public TextMeshProUGUI slotNum;
    public TextMeshProUGUI levelMessage;
    public Image item;
    public GameObject itemPreviewHide;
    public Image slot1;
    public Image slot1sprite;
    public Image slot2;
    public Image slot2sprite;
    public Image slot3;
    public Image slot3sprite;
    public Sprite[] items;
    public RawImage weapon;
    public GameObject weaponPreviewHide;
    public RenderTexture[] weapons;
    public int currentWeapon;
    public int currentItem;
    public int currentSlot;
    public TextMeshProUGUI itemTitle;
    public GameObject itemHide;
    public TextMeshProUGUI weaponTitle;
    public GameObject weaponHide;

    private int slot;
    private bool canEquip = true;

    void Start()
    {
        //initialize everything :)
        PlayerPrefs.SetInt("weaponNum", 1);
        PlayerPrefs.SetInt("itemNum", 1);
        PlayerPrefs.SetInt("craftNum", 1);
        slot = 1;

        //hide unfound weapons
        switch (currentWeapon)
        {
            case 1:
                PlayerPrefs.SetInt("weaponNum", 1);
                weaponName.text = "Rolling Pin";
                weaponDescription.text = "Your rolling pin, a slow but hard-hitting weapon\nDamage: 12\nCooldown: 1.25";
                weapon.texture = weapons[0];
                break;
            case 2:
                if (PlayerPrefs.GetInt("haveCleaver") != 1)
                {
                    weaponTitle.text = "???";
                    weaponHide.SetActive(true);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("havePan") != 1)
                {
                    weaponTitle.text = "???";
                    weaponHide.SetActive(true);
                }
                break;
            default:
                PlayerPrefs.SetInt("weaponNum", 1);
                weaponName.text = "Rolling Pin";
                weaponDescription.text = "Your rolling pin, a slow but hard-hitting weapon\nDamage: 12\nCooldown: 1.25";
                weapon.texture = weapons[0];
                break;
        }

        //hide unknown dishes
        switch (currentItem)
        {
            case 1:
                if (PlayerPrefs.GetInt("found1") != 1)
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                    itemPreviewHide.SetActive(true);
                    PlayerPrefs.SetInt("canEquip", 0);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                }
                else
                {
                    itemName.text = "Goblin Steak";
                    itemDescription.text = "Eating this dish will let you regain 10 hp (5 second cooldown), you can carry 10 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                    PlayerPrefs.SetInt("canEquip", 1);
                }
                break;
            case 2:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 3:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("found5") != 1)
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 5:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 6:
                if ((PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 7:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found4") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 8:
                if (PlayerPrefs.GetInt("found7") != 1)
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 9:
                if ((PlayerPrefs.GetInt("found1") != 1) || (PlayerPrefs.GetInt("found5") != 1) || (PlayerPrefs.GetInt("found7") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            case 10:
                if ((PlayerPrefs.GetInt("found1") != 1) || (PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found4") != 1) || (PlayerPrefs.GetInt("found5") != 1) ||
                    (PlayerPrefs.GetInt("found6") != 1) || (PlayerPrefs.GetInt("found7") != 1))
                {
                    itemTitle.text = "???";
                    itemHide.SetActive(true);
                }
                break;
            default:
                break;
        }
        
        
        //amountText.text = "" + amount;
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MenuMusic>().PlayMusic();
        if (currentWeapon == 0)
        {
            int type = PlayerPrefs.GetInt("slot1");
            if (type != 0)
            {
                slot1sprite.sprite = items[type - 1];
            }
            type = PlayerPrefs.GetInt("slot2");
            if (type != 0)
            {
                slot2sprite.sprite = items[type - 1];
            }
            type = PlayerPrefs.GetInt("slot3");
            if (type != 0)
            {
                slot3sprite.sprite = items[type - 1];
            }
            currentSlot = 1;
            PlayerPrefs.SetInt("CurrentSlot", 1);
            slot1.color = new Color32(0, 115, 32, 255);
        }

    }

    public void SelectSlot(int changeSlot)
    {
        currentSlot = changeSlot;
        switch (changeSlot)
        {
            case 1:
                PlayerPrefs.SetInt("CurrentSlot", 1);
                slot1.color = new Color32(0, 115, 32, 255);
                slot2.color = new Color32(66, 66, 66, 255);
                slot3.color = new Color32(66, 66, 66, 255);
                break;
            case 2:
                PlayerPrefs.SetInt("CurrentSlot", 2);
                slot2.color = new Color32(0, 115, 32, 255);
                slot1.color = new Color32(66, 66, 66, 255);
                slot3.color = new Color32(66, 66, 66, 255);
                break;
            case 3:
                PlayerPrefs.SetInt("CurrentSlot", 3);
                slot3.color = new Color32(0, 115, 32, 255);
                slot1.color = new Color32(66, 66, 66, 255);
                slot2.color = new Color32(66, 66, 66, 255);
                break;
            default:
                PlayerPrefs.SetInt("CurrentSlot", 1);
                slot1.color = new Color32(66, 66, 66, 255);
                slot2.color = new Color32(66, 66, 66, 255);
                slot3.color = new Color32(66, 66, 66, 255);
                break;
        }
    }

    public void SelectWeapon()
    {
        slot = 1;
        selectWeaponConfirm.text = "";

        switch (currentWeapon)
        {
            case 1:
                PlayerPrefs.SetInt("weaponNum", 1);
                weaponName.text = "Rolling Pin";
                weaponDescription.text = "Your rolling pin, a slow but hard-hitting weapon\nDamage: 12\nCooldown: 1.25";
                weapon.texture = weapons[0];
                weaponPreviewHide.SetActive(false);
                break;
            case 2:
                if (PlayerPrefs.GetInt("haveCleaver") == 1)
                {
                    PlayerPrefs.SetInt("weaponNum", 2);
                    weaponName.text = "Cleaver";
                    weaponDescription.text = "Your cleaver, a light and fast weapon\nDamage: 6\nCooldown: 0.5";
                    weapon.texture = weapons[1];
                    weaponPreviewHide.SetActive(false);
                }
                else
                {
                    PlayerPrefs.SetInt("weaponNum", 2);
                    weaponName.text = "???";
                    weaponDescription.text = "Weapon not unlocked yet";
                    weapon.texture = weapons[1];
                    weaponPreviewHide.SetActive(true);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("havePan") == 1)
                {
                    PlayerPrefs.SetInt("weaponNum", 3);
                    weaponName.text = "Weapon 3";
                    weaponDescription.text = "Your Frying Pan, a _____ weapon\nDamage: _\nCooldown: _";
                    weapon.texture = weapons[0];
                    weaponPreviewHide.SetActive(false);
                }
                else
                {
                    PlayerPrefs.SetInt("weaponNum", 3);
                    weaponName.text = "???";
                    weaponDescription.text = "Weapon not unlocked yet";
                    weapon.texture = weapons[0];
                    weaponPreviewHide.SetActive(true);
                }
                break;
            default:
                PlayerPrefs.SetInt("weaponNum", 1);
                weaponName.text = "Rolling Pin";
                weaponDescription.text = "Your rolling pin, a slow but hard-hitting weapon\nDamage: 12\nCooldown: 1.25";
                weapon.texture = weapons[0];
                weaponPreviewHide.SetActive(false);
                break;
        }

    }

    public void SelectItem()
    {
        /*if (PlayerPrefs.GetInt("craftNum") >= 1 && PlayerPrefs.GetInt("craftNum") <= 3)
        {
            slot = PlayerPrefs.GetInt("craftNum");
        }
        else
        {
            slot = 1;
            PlayerPrefs.SetInt("craftNum", slot);
        }*/
        //amountText.text = "" + amount;
        //selectItemConfirm.text = "";

        switch (currentItem)
        {
            case 1:
                if (PlayerPrefs.GetInt("found1") != 1)
                {
                    canEquip = false;
                }
                break;
            case 2:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1))
                {
                    canEquip = false;
                }
                break;
            case 3:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    canEquip = false;
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("found5") != 1)
                {
                    canEquip = false;
                }
                break;
            case 5:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    canEquip = false;
                }
                break;
            case 6:
                if ((PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    canEquip = false;
                }
                break;
            case 7:
                if ((PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found4") != 1) || (PlayerPrefs.GetInt("found6") != 1))
                {
                    canEquip = false;
                }
                break;
            case 8:
                if (PlayerPrefs.GetInt("found7") != 1)
                {
                    canEquip = false;
                }
                break;
            case 9:
                if ((PlayerPrefs.GetInt("found1") != 1) || (PlayerPrefs.GetInt("found5") != 1) || (PlayerPrefs.GetInt("found7") != 1))
                {
                    canEquip = false;
                }
                break;
            case 10:
                if ((PlayerPrefs.GetInt("found1") != 1) || (PlayerPrefs.GetInt("found2") != 1) || (PlayerPrefs.GetInt("found3") != 1) || (PlayerPrefs.GetInt("found4") != 1) || (PlayerPrefs.GetInt("found5") != 1) ||
                    (PlayerPrefs.GetInt("found6") != 1) || (PlayerPrefs.GetInt("found7") != 1))
                {
                    canEquip = false;
                }
                break;
            default:
                break;
        }

        if (!canEquip)
        {
            PlayerPrefs.SetInt("canEquip", 0);
        }
        else
        {
            PlayerPrefs.SetInt("canEquip", 1);
        }

        switch (currentItem)
        {
            case 1:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 1);
                    itemName.text = "Goblin Steak";
                    itemDescription.text = "Eating this dish will let you regain 10 hp (5 second cooldown), you can carry 10 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[0];
                    PlayerPrefs.SetInt("maxHold1", 10);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 1);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[0];
                    PlayerPrefs.SetInt("maxHold1", 10);
                }
                break;
            case 2:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 2);
                    itemName.text = "Slime Jelly Sandwich";
                    itemDescription.text = "Eating this dish will increase your movement speed for 15 seconds, you can carry 8 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("jellysand");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[1];
                    PlayerPrefs.SetInt("maxHold2", 8);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 2);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[1];
                    PlayerPrefs.SetInt("maxHold2", 8);
                }
                break;
            case 3:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 3);
                    itemName.text = "Blood Berry Slime Jelly Sandwich";
                    itemDescription.text = "Eating this dish will reduce the damage you take by 20% for 30 seconds, you can carry 5 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("bbsand");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[2];
                    PlayerPrefs.SetInt("maxHold3", 5);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 3);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[2];
                    PlayerPrefs.SetInt("maxHold3", 5);
                }
                break;
            case 4:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 4);
                    itemName.text = "Fried Mushroom";
                    itemDescription.text = "Eating this dish will increase the damage you deal by 30% for 15 seconds, you can carry 5 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("friedmush");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[3];
                    PlayerPrefs.SetInt("maxHold4", 5);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 4);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[3];
                    PlayerPrefs.SetInt("maxHold4", 5);
                }
                break;
            case 5:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 5);
                    itemName.text = "Blood Berry Soup";
                    itemDescription.text = "Eating this dish will let you regain 15 hp (8 second cooldown), you can carry 8 at a time\nOwned: " + PlayerPrefs.GetInt("bbsoup");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[4];
                    PlayerPrefs.SetInt("maxHold5", 8);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 5);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[4];
                    PlayerPrefs.SetInt("maxHold5", 8);
                }
                break;
            case 6:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 6);
                    itemName.text = "Royal Blood Berry Soup";
                    itemDescription.text = "Eating this dish will increase your movement speed for 25 seconds, you can carry 5 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("royalbbsoup");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[5];
                    PlayerPrefs.SetInt("maxHold6", 5);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 6);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[5];
                    PlayerPrefs.SetInt("maxHold6", 5);
                }
                break;
            case 7:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 7);
                    itemName.text = "Boss Drink";
                    itemDescription.text = "Eating this dish will increase your total health by 50, you can carry 3 at a time. The effect will only last for 1 level\nOwned: " + PlayerPrefs.GetInt("bossdrink");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[6];
                    PlayerPrefs.SetInt("maxHold7", 3);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 7);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[6];
                    PlayerPrefs.SetInt("maxHold7", 3);
                }
                break;
            case 8:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 8);
                    itemName.text = "Roasted Bone Marrow";
                    itemDescription.text = "Eating this dish will reduce the damage you take by 30% for 40 seconds, you can carry 5 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("roastbone");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[7];
                    PlayerPrefs.SetInt("maxHold8", 5);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 8);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[7];
                    PlayerPrefs.SetInt("maxHold8", 5);
                }
                break;
            case 9:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 9);
                    itemName.text = "Dungeon Dinner";
                    itemDescription.text = "Eating this dish will increase the damage you deal by 50% for 25 seconds, you can carry 5 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("dundinner");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[8];
                    PlayerPrefs.SetInt("maxHold9", 5);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 9);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[8];
                    PlayerPrefs.SetInt("maxHold9", 5);
                }
                break;
            case 10:
                if (canEquip)
                {
                    PlayerPrefs.SetInt("itemNum", 10);
                    itemName.text = "Dungeon Feast";
                    itemDescription.text = "Eating this dish will completely recover your health (75 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("dunfeast");
                    itemPreviewHide.SetActive(false);
                    item.sprite = items[9];
                    PlayerPrefs.SetInt("maxHold10", 3);
                }
                else
                {
                    PlayerPrefs.SetInt("itemNum", 10);
                    itemName.text = "Unknown Dish";
                    itemDescription.text = "Discover more ingredients to unlock \nOwned: 0";
                    itemPreviewHide.SetActive(true);
                    item.sprite = items[9];
                    PlayerPrefs.SetInt("maxHold10", 3);
                }
                break;
            default:
                PlayerPrefs.SetInt("itemNum", 1);
                itemName.text = "Goblin Steak";
                itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry 10 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                item.sprite = items[0];
                PlayerPrefs.SetInt("maxHold1", 10);
                break;
        }

    }

    public void EquipWeapon()
    {
        switch (PlayerPrefs.GetInt("weaponNum"))
        {
            case 1:
                PlayerPrefs.SetInt("equippedWeapon", 1);
                selectWeaponConfirm.text = "Rolling pin equipped";
                break;
            case 2:
                if (PlayerPrefs.GetInt("haveCleaver") == 1)
                {
                    PlayerPrefs.SetInt("equippedWeapon", 2);
                    selectWeaponConfirm.text = "Cleaver equipped";
                }
                else
                {
                    selectWeaponConfirm.text = "Weapon not unlocked yet";
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("havePan") == 1)
                {
                    PlayerPrefs.SetInt("equippedWeapon", 3);
                    selectWeaponConfirm.text = "Frying Pan equipped";
                }
                else
                {
                    selectWeaponConfirm.text = "Weapon not unlocked yet";
                }
                break;
            default:
                PlayerPrefs.SetInt("equippedWeapon", 1);
                selectWeaponConfirm.text = "Rolling pin equipped";
                break;
        }
    }

    public void EquipItem() 
    {
        slot = PlayerPrefs.GetInt("CurrentSlot");

        if (PlayerPrefs.GetInt("canEquip") == 0)
        {
            canEquip = false;
        }
        else
        {
            canEquip = true;
        }


        if (canEquip)
        {

            switch (PlayerPrefs.GetInt("itemNum"))
            {
                case 1:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Goblin Steak selected for slot " + slot;
                    break;
                case 2:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 2);
                            if (PlayerPrefs.GetInt("jellysand") >= PlayerPrefs.GetInt("maxHold2"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold2"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("jellysand"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 2);
                            if (PlayerPrefs.GetInt("jellysand") >= PlayerPrefs.GetInt("maxHold2"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold2"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("jellysand"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 2);
                            if (PlayerPrefs.GetInt("jellysand") >= PlayerPrefs.GetInt("maxHold2"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold2"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("jellysand"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 2);
                            if (PlayerPrefs.GetInt("jellysand") >= PlayerPrefs.GetInt("maxHold2"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold2"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("jellysand"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Slime Jelly Sandwich selected for slot " + slot;
                    break;
                case 3:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 3);
                            if (PlayerPrefs.GetInt("bbsand") >= PlayerPrefs.GetInt("maxHold3"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold3"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bbsand"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 3);
                            if (PlayerPrefs.GetInt("bbsand") >= PlayerPrefs.GetInt("maxHold3"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold3"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("bbsand"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 3);
                            if (PlayerPrefs.GetInt("bbsand") >= PlayerPrefs.GetInt("maxHold3"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold3"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("bbsand"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 3);
                            if (PlayerPrefs.GetInt("bbsand") >= PlayerPrefs.GetInt("maxHold3"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold3"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bbsand"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Blood Berry Slime Jelly Sandwich selected for slot " + slot;
                    break;
                case 4:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 4);
                            if (PlayerPrefs.GetInt("friedmush") >= PlayerPrefs.GetInt("maxHold4"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold4"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("friedmush"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 4);
                            if (PlayerPrefs.GetInt("friedmush") >= PlayerPrefs.GetInt("maxHold4"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold4"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("friedmush"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 4);
                            if (PlayerPrefs.GetInt("friedmush") >= PlayerPrefs.GetInt("maxHold4"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold4"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("friedmush"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 4);
                            if (PlayerPrefs.GetInt("friedmush") >= PlayerPrefs.GetInt("maxHold4"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold4"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("friedmush"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Fried Mushroom selected for slot " + slot;
                    break;
                case 5:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 5);
                            if (PlayerPrefs.GetInt("bbsoup") >= PlayerPrefs.GetInt("maxHold5"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold5"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bbsoup"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 5);
                            if (PlayerPrefs.GetInt("bbsoup") >= PlayerPrefs.GetInt("maxHold5"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold5"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("bbsoup"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 5);
                            if (PlayerPrefs.GetInt("bbsoup") >= PlayerPrefs.GetInt("maxHold5"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold5"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("bbsoup"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 5);
                            if (PlayerPrefs.GetInt("bbsoup") >= PlayerPrefs.GetInt("maxHold5"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold5"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bbsoup"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Blood Berry Soup selected for slot " + slot;
                    break;
                case 6:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 6);
                            if (PlayerPrefs.GetInt("royalbbsoup") >= PlayerPrefs.GetInt("maxHold6"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold6"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("royalbbsoup"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 6);
                            if (PlayerPrefs.GetInt("royalbbsoup") >= PlayerPrefs.GetInt("maxHold6"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold6"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("royalbbsoup"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 6);
                            if (PlayerPrefs.GetInt("royalbbsoup") >= PlayerPrefs.GetInt("maxHold6"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold6"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("royalbbsoup"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 6);
                            if (PlayerPrefs.GetInt("royalbbsoup") >= PlayerPrefs.GetInt("maxHold6"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold6"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("royalbbsoup"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Royal Blood Berry Soup selected for slot " + slot;
                    break;
                case 7:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 7);
                            if (PlayerPrefs.GetInt("bossdrink") >= PlayerPrefs.GetInt("maxHold7"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold7"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bossdrink"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 7);
                            if (PlayerPrefs.GetInt("bossdrink") >= PlayerPrefs.GetInt("maxHold7"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold7"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("bossdrink"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 7);
                            if (PlayerPrefs.GetInt("bossdrink") >= PlayerPrefs.GetInt("maxHold7"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold7"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("bossdrink"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 7);
                            if (PlayerPrefs.GetInt("bossdrink") >= PlayerPrefs.GetInt("maxHold7"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold7"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("bossdrink"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Boss Drink selected for slot " + slot;
                    break;
                case 8:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 8);
                            if (PlayerPrefs.GetInt("roastbone") >= PlayerPrefs.GetInt("maxHold8"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold8"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("roastbone"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 8);
                            if (PlayerPrefs.GetInt("roastbone") >= PlayerPrefs.GetInt("maxHold8"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold8"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("roastbone"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 8);
                            if (PlayerPrefs.GetInt("roastbone") >= PlayerPrefs.GetInt("maxHold8"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold8"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("roastbone"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 8);
                            if (PlayerPrefs.GetInt("roastbone") >= PlayerPrefs.GetInt("maxHold8"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold8"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("roastbone"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Roasted Bone Marrow selected for slot " + slot;
                    break;
                case 9:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 9);
                            if (PlayerPrefs.GetInt("dundinner") >= PlayerPrefs.GetInt("maxHold9"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold9"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dundinner"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 9);
                            if (PlayerPrefs.GetInt("dundinner") >= PlayerPrefs.GetInt("maxHold9"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold9"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("dundinner"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 9);
                            if (PlayerPrefs.GetInt("dundinner") >= PlayerPrefs.GetInt("maxHold9"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold9"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("dundinner"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 9);
                            if (PlayerPrefs.GetInt("dundinner") >= PlayerPrefs.GetInt("maxHold9"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold9"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dundinner"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Dungeon Dinner selected for slot " + slot;
                    break;
                case 10:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 10);
                            if (PlayerPrefs.GetInt("dunfeast") >= PlayerPrefs.GetInt("maxHold10"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold10"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dunfeast"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 10);
                            if (PlayerPrefs.GetInt("dunfeast") >= PlayerPrefs.GetInt("maxHold10"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold10"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("dunfeast"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 10);
                            if (PlayerPrefs.GetInt("dunfeast") >= PlayerPrefs.GetInt("maxHold10"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold10"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("dunfeast"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 10);
                            if (PlayerPrefs.GetInt("dunfeast") >= PlayerPrefs.GetInt("maxHold10"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold10"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dunfeast"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Dungeon Feast selected for slot " + slot;
                    break;
                default:
                    switch (slot)
                    {
                        case 1:
                            PlayerPrefs.SetInt("slot1", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        case 2:
                            PlayerPrefs.SetInt("slot2", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        case 3:
                            PlayerPrefs.SetInt("slot3", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                        default:
                            PlayerPrefs.SetInt("slot1", 1);
                            if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold1"))
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold1"));
                            }
                            else
                            {
                                PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("goblinSteak"));
                            }
                            break;
                    }
                    //selectItemConfirm.text = "Goblin Steak selected for slot " + slot;
                    break;
            }


            int type = 0;
            switch (slot)
            {
                case 1:
                    type = PlayerPrefs.GetInt("slot1");
                    UnityEngine.Debug.Log("slot 1 item " + type);
                    slot1sprite.sprite = items[type - 1];
                    break;
                case 2:
                    type = PlayerPrefs.GetInt("slot2");
                    UnityEngine.Debug.Log("slot 2 item " + type);
                    slot2sprite.sprite = items[type - 1];
                    break;
                case 3:
                    type = PlayerPrefs.GetInt("slot3");
                    UnityEngine.Debug.Log("slot 3 item " + type);
                    slot3sprite.sprite = items[type - 1];
                    break;
                default:
                    type = PlayerPrefs.GetInt("slot1");
                    UnityEngine.Debug.Log("slot 1 item " + type);
                    slot1sprite.sprite = items[type - 1];
                    break;
            }
        }
    }

    public void IncrAmount()
    {
        /*slot++;
        if (slot > 3)
        {
            selectItemConfirm.text = "Cannot choose a higher slot";
            slot = 3;
        }
        else
        {
            selectItemConfirm.text = "";
        }
        slotNum.text = "" + slot;
        PlayerPrefs.SetInt("craftNum", slot);*/
    }

    public void DecrAmount()
    {
        /*slot--;
        if (slot <= 0)
        {
            slot = 1;
            selectItemConfirm.text = "Cannot choose a lower slot";
        }
        else
        {
            selectItemConfirm.text = "";
        }
        slotNum.text = "" + slot;
        PlayerPrefs.SetInt("craftNum", slot);*/
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level One");
    }

    public void LevelTwo()
    {
        if (PlayerPrefs.GetInt("haveCleaver") == 1)
        {
            SceneManager.LoadScene("Level Two");
        }
        else
        {
            levelMessage.text = "Level not unlocked!";
        }
    }

    public void LevelThree()
    {
        //if (PlayerPrefs.GetInt("havePan") == 1)
        //{
            SceneManager.LoadScene("Level Three");
        /*}
        else
        {
            levelMessage.text = "Level not unlocked!";
        }*/
    }
}
