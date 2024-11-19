using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponDescription;
    public TextMeshProUGUI selectWeaponConfirm;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI selectItemConfirm;
    public TextMeshProUGUI slotNum;
    public int currentWeapon;
    public int currentItem;
    public Image item;
    public Sprite[] items;
    public RawImage weapon;
    public RenderTexture[] weapons;

    private int slot;

    void Start()
    {
        //initialize everything :)
        PlayerPrefs.SetInt("weaponNum", 1);
        PlayerPrefs.SetInt("itemNum", 1);
        slot = 1;
        weaponName.text = "Rolling Pin";
        weaponDescription.text = "Your rolling pin, now being used as a melee weapon\nAttack:\nRange:\nCooldown: ";
        itemName.text = "Goblin Steak";
        itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
        //amountText.text = "" + amount;
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
                weaponDescription.text = "Your rolling pin, now being used as a melee weapon\nAttack:\nRange:\nCooldown: ";
                weapon.texture = weapons[0];
                break;
            case 2:
                PlayerPrefs.SetInt("weaponNum", 2);
                weaponName.text = "Cleaver";
                weaponDescription.text = "Your cleaver, now being used as a melee weapon\nAttack:\nRange:\nCooldown: ";
                weapon.texture = weapons[1];
                break;
            case 3:
                PlayerPrefs.SetInt("weaponNum", 3);
                weaponName.text = "Weapon 3";
                weaponDescription.text = "Your _____, now being used as a _____ weapon\nAttack:\nRange:\nCooldown: ";
                weapon.texture = weapons[0];
                break;
            default:
                PlayerPrefs.SetInt("weaponNum", 1);
                weaponName.text = "Rolling Pin";
                weaponDescription.text = "Your rolling pin, now being used as a melee weapon\nAttack:\nRange:\nCooldown: ";
                weapon.texture = weapons[0];
                break;
        }

    }

    public void SelectItem()
    {
        if (PlayerPrefs.GetInt("craftNum") >= 1 && PlayerPrefs.GetInt("craftNum") <= 3)
        {
            slot = PlayerPrefs.GetInt("craftNum");
        }
        else
        {
            slot = 1;
            PlayerPrefs.SetInt("craftNum", slot);
        }
        //amountText.text = "" + amount;
        selectItemConfirm.text = "";

        switch (currentItem)
        {
            case 1:
                PlayerPrefs.SetInt("itemNum", 1);
                itemName.text = "Goblin Steak";
                itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                item.sprite = items[0];
                PlayerPrefs.SetInt("maxHold", 3);
                break;
            case 2:
                PlayerPrefs.SetInt("itemNum", 2);
                itemName.text = "Peanut Butter & Slime Jelly Sandwich";
                itemDescription.text = "Eating this dish will increase your walk speed for a bit, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("pbnjelly");
                item.sprite = items[1];
                PlayerPrefs.SetInt("maxHold", 3);
                break;
            case 3:
                PlayerPrefs.SetInt("itemNum", 3);
                itemName.text = "Peanut Butter & Boss Slime Jelly Sandwich";
                itemDescription.text = "Eating this dish will reduce the amount of damage you take for a bit, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("pbnbossjelly");
                item.sprite = items[2];
                PlayerPrefs.SetInt("maxHold", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("itemNum", 4);
                itemName.text = "Dish 4";
                itemDescription.text = "Eating this dish will _, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("dish4");
                item.sprite = items[0];
                PlayerPrefs.SetInt("maxHold", 3);
                break;
            case 5:
                PlayerPrefs.SetInt("itemNum", 5);
                itemName.text = "Dish 5";
                itemDescription.text = "Eating this dish will _, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("dish5");
                item.sprite = items[0];
                PlayerPrefs.SetInt("maxHold", 3);
                break;
            default:
                PlayerPrefs.SetInt("itemNum", 1);
                itemName.text = "Goblin Steak";
                itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                item.sprite = items[0];
                PlayerPrefs.SetInt("maxHold", 3);
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
                if (PlayerPrefs.GetInt("haveWeapon2") == 1)
                {
                    PlayerPrefs.SetInt("equippedWeapon", 3);
                    selectWeaponConfirm.text = "Weapon3 equipped";
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
        slot = PlayerPrefs.GetInt("craftNum");
        switch (PlayerPrefs.GetInt("itemNum"))
        {
            case 1:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Goblin Steak selected for slot " + slot;
                break;
            case 2:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 2);
                        if (PlayerPrefs.GetInt("pbnjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("pbnjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 2);
                        if (PlayerPrefs.GetInt("pbnjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("pbnjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 2);
                        if (PlayerPrefs.GetInt("pbnjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("pbnjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 2);
                        if (PlayerPrefs.GetInt("pbnjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("pbnjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Peanut Butter & Slime Jelly Sandwich selected for slot " + slot;
                break;
            case 3:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 3);
                        if (PlayerPrefs.GetInt("pbnbossjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("pbnbossjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 3);
                        if (PlayerPrefs.GetInt("pbnbossjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("pbnbossjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 3);
                        if (PlayerPrefs.GetInt("pbnbossjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("pbnbossjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 3);
                        if (PlayerPrefs.GetInt("pbnbossjelly") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("pbnbossjelly") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Peanut Butter & Boss Slime Jelly Sandwich selected for slot " + slot;
                break;
            case 4:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 4);
                        if (PlayerPrefs.GetInt("dish4") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dish4") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 4);
                        if (PlayerPrefs.GetInt("dish4") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("dish4") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 4);
                        if (PlayerPrefs.GetInt("dish4") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("dish4") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 4);
                        if (PlayerPrefs.GetInt("dish4") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dish4") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Dish 4 selected for slot " + slot;
                break;
            case 5:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 5);
                        if (PlayerPrefs.GetInt("dish5") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dish5") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 5);
                        if (PlayerPrefs.GetInt("dish5") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("dish5") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 5);
                        if (PlayerPrefs.GetInt("dish5") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("dish5") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 5);
                        if (PlayerPrefs.GetInt("dish5") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("dish5") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Dish 5 selected for slot " + slot;
                break;
            default:
                switch (slot)
                {
                    case 1:
                        PlayerPrefs.SetInt("slot1", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 2:
                        PlayerPrefs.SetInt("slot2", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot2amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    case 3:
                        PlayerPrefs.SetInt("slot3", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot3amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                    default:
                        PlayerPrefs.SetInt("slot1", 1);
                        if (PlayerPrefs.GetInt("goblinSteak") >= PlayerPrefs.GetInt("maxHold"))
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold"));
                        }
                        else
                        {
                            PlayerPrefs.SetInt("slot1amount", PlayerPrefs.GetInt("maxHold") - PlayerPrefs.GetInt("goblinSteak"));
                        }
                        break;
                }
                selectItemConfirm.text = "Goblin Steak selected for slot " + slot;
                break;
        }
    }

    public void IncrAmount()
    {
        slot++;
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
        PlayerPrefs.SetInt("craftNum", slot);
    }

    public void DecrAmount()
    {
        slot--;
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
        PlayerPrefs.SetInt("craftNum", slot);
    }

    public void NextLevel()
    {
        switch (PlayerPrefs.GetInt("lastLevel"))
        {
            case 1:
                SceneManager.LoadScene("Level Two");
                break;
            case 2:
                SceneManager.LoadScene("Level Three");
                break;
            case 3:
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
