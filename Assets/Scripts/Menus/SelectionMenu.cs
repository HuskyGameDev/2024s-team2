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
    public int currentWeapon;
    public int currentItem;
    public Image item;
    public Sprite[] items;
    public RawImage weapon;
    public RenderTexture[] weapons;

    private int amount;

    void Start()
    {
        //ingredient and dish values set here for testing purposes
        //PlayerPrefs.SetInt("ingredientA", 4);
        //PlayerPrefs.SetInt("ingredientB", 2);
        PlayerPrefs.SetInt("dish1", 0); //for default cases
        PlayerPrefs.SetInt("dish4", 0);
        PlayerPrefs.SetInt("dish5", 0);
        //initialize everything :)
        PlayerPrefs.SetInt("weaponNum", 1);
        PlayerPrefs.SetInt("itemNum", 1);
        amount = 1;
        weaponName.text = "Rolling Pin";
        weaponDescription.text = "Your rolling pin, now being used as a melee weapon\nAttack:\nRange:\nCooldown: ";
        itemName.text = "Goblin Steak";
        itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
        //amountText.text = "" + amount;
    }

    public void SelectWeapon()
    {
        amount = 1;
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
                weapon.texture = weapons[0];
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
        amount = 1;
        //amountText.text = "" + amount;
        selectItemConfirm.text = "";

        switch (currentItem)
        {
            case 1:
                PlayerPrefs.SetInt("itemNum", 1);
                itemName.text = "Goblin Steak";
                itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                item.sprite = items[0];
                break;
            case 2:
                PlayerPrefs.SetInt("itemNum", 2);
                itemName.text = "Peanut Butter & Slime Jelly Sandwich";
                itemDescription.text = "Eating this dish will increase your walk speed for a bit, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("pbnjelly");
                item.sprite = items[1];
                break;
            case 3:
                PlayerPrefs.SetInt("itemNum", 3);
                itemName.text = "Peanut Butter & Boss Slime Jelly Sandwich";
                itemDescription.text = "Eating this dish will reduce the amount of damage you take for a bit, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("pbnbossjelly");
                item.sprite = items[2];
                break;
            case 4:
                PlayerPrefs.SetInt("itemNum", 4);
                itemName.text = "Dish 4";
                itemDescription.text = "Eating this dish will _, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("dish4");
                item.sprite = items[0];
                break;
            case 5:
                PlayerPrefs.SetInt("itemNum", 5);
                itemName.text = "Dish 5";
                itemDescription.text = "Eating this dish will _, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("dish5");
                item.sprite = items[0];
                break;
            default:
                PlayerPrefs.SetInt("itemNum", 1);
                itemName.text = "Goblin Steak";
                itemDescription.text = "Eating this dish will let you regain a bit of health, you can carry __ at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                item.sprite = items[0];
                break;
        }

    }

    public void NextLevel()
    {
        switch (PlayerPrefs.GetInt("lastLevel"))
        {
            case 1:
                SceneManager.LoadScene("Level One");
                break;
            case 2:
                SceneManager.LoadScene("Level Two");
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
