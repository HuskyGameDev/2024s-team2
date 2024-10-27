using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    public TextMeshProUGUI myIngredients;
    public TextMeshProUGUI recipeDescription;
    public TextMeshProUGUI ingredientsNeeded;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI craftResult;
    public int currentWeapon;
    public int currentItem;

    private int amount;

    public void SelectWeapon()
    {
        amount = 1;
        amountText.text = "" + amount;
        craftResult.text = "";

        switch (currentWeapon)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                
                break;
            default:

                break;
        }

    }

    public void SelectItem()
    {
        amount = 1;
        amountText.text = "" + amount;
        craftResult.text = "";

        switch (currentItem)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                
                break;
            default:
                
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
