using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RecipeMenu : MonoBehaviour
{
    public TextMeshProUGUI myIngredients;
    public TextMeshProUGUI recipeDescription;
    public TextMeshProUGUI ingredientsNeeded;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI craftResult;
    public int currentRecipe;

    private int amount;

    // Start is called before the first frame update
    void Start()
    {
        //ingredient and dish values set here for testing purposes
        //PlayerPrefs.SetInt("ingredientA", 4);
        //PlayerPrefs.SetInt("ingredientB", 2);
        PlayerPrefs.SetInt("dish1", 0); //for default cases
        PlayerPrefs.SetInt("dish4", 0);
        PlayerPrefs.SetInt("dish5", 0);
        //initialize everything :)
        PlayerPrefs.SetInt("recipeNum", 1);
        amount = 1;
        myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("goblinMeat") + " Goblin Meat\t" + PlayerPrefs.GetInt("slimeJelly") + " Slime Jelly\n" + PlayerPrefs.GetInt("bossSlimeJelly") + " Boss Slime Jelly\t"; 
        recipeDescription.text = "Goblin Steak: Eating this dish will let you regain a bit of health\nOwned: " + PlayerPrefs.GetInt("goblinSteak"); 
        ingredientsNeeded.text = "Required Ingredients:\n" + 1 + " Goblin Meat\t"; 
        amountText.text = "" + amount;
        Cursor.lockState = CursorLockMode.None;
    }

    //increase amount of dishes to craft
    public void IncrAmount()
    {
        amount++;
        if(amount > 0)
        {
            craftResult.text = "";
        }
        amountText.text = "" + amount;

        switch (PlayerPrefs.GetInt("recipeNum"))
        {
            case 1:
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t"; 
                break;
            case 2:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t"; 
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly"; 
                break;
            case 4:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
                break;
            case 5:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (3 * amount) + " ingredientB"; 
                break;
            default:
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB";
                break;
        }
        PlayerPrefs.SetInt("craftNum", amount);
    }

    //decrease amount of dishes to craft
    public void DecrAmount()
    {
        amount--;
        if(amount <= 0)
        {
            amount = 1;
            craftResult.text = "Cannot craft less than 1 dish";
        }
        else
        {
            craftResult.text = "";
        }
        amountText.text = "" + amount;

        switch (PlayerPrefs.GetInt("recipeNum"))
        {
            case 1:
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t"; 
                break;
            case 2:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t"; 
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly"; 
                break;
            case 4:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
                break;
            case 5:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (3 * amount) + " ingredientB"; 
                break;
            default:
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
        }
        PlayerPrefs.SetInt("craftNum", amount);
    }

    //try to craft a dish
    public void Craft()
    {
        int aNeeded = 0; //goblin meat
        int bNeeded = 0; //slime jelly
        int cNeeded = 0; //boss slime jelly

        amount = PlayerPrefs.GetInt("craftNum");

        switch (PlayerPrefs.GetInt("recipeNum"))
        {
            case 1://goblin steak
                aNeeded = 1 * amount;
                break;
            case 2://pb&slime jelly sandwich
                bNeeded = 3 * amount;
                break;
            case 3://pb&boss slime jelly sandwich
                bNeeded = 3 * amount;
                cNeeded = 1 * amount;
                break;
            case 4:
                aNeeded = 2 * amount;
                bNeeded = 2 * amount;
                break;
            case 5:
                aNeeded = 2 * amount;
                bNeeded = 3 * amount;
                break;
            default:
                aNeeded = 1 * amount;
                bNeeded = 1 * amount;
                break;
        }

        if (PlayerPrefs.GetInt("goblinMeat") < aNeeded || PlayerPrefs.GetInt("slimeJelly") < bNeeded || PlayerPrefs.GetInt("bossSlimeJelly") < cNeeded)
        {
            craftResult.text = "You do not have enough ingredients";
        }
        else
        {
            switch (PlayerPrefs.GetInt("recipeNum"))
            {
                case 1:
                    PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") - aNeeded);
                    PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak") + amount);
                    recipeDescription.text = "Goblin Steak: Eating this dish will let you regain a bit of health\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                    break;
                case 2:
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("pbnjelly", PlayerPrefs.GetInt("pbnjelly") + amount);
                    recipeDescription.text = "Peanut Butter & Slime Jelly Sandwich: Eating this dish will increase your walk speed for a bit\nOwned: " + PlayerPrefs.GetInt("pbnjelly");
                    break;
                case 3:
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("pbnbossjelly", PlayerPrefs.GetInt("pbnbossjelly") + amount);
                    recipeDescription.text = "Peanut Butter & Boss Slime Jelly Sandwich: Eating this dish will reduce the amount of damage you take for a bit\nOwned: " + PlayerPrefs.GetInt("pbnbossjelly");
                    break;
                case 4:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish4", PlayerPrefs.GetInt("dish4") + amount);
                    recipeDescription.text = "Recipe 4: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish4");
                    break;
                case 5:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish5", PlayerPrefs.GetInt("dish5") + amount);
                    recipeDescription.text = "Recipe 5: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish5");
                    break;
                default:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish1", PlayerPrefs.GetInt("dish1") + amount);
                    recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1");
                    break;
            }

            myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("goblinMeat") + " Goblin Meat\t" + PlayerPrefs.GetInt("slimeJelly") + " Slime Jelly\n" + PlayerPrefs.GetInt("bossSlimeJelly") + " Boss Slime Jelly\t";
            craftResult.text = "Successfully crafted";
        }

    }

    //update descriptions for a recipe
    public void SelectRecipe()
    {
        if (PlayerPrefs.GetInt("craftNum") >= 1)
        {
            amount = PlayerPrefs.GetInt("craftNum");
        }
        else
        {
            amount = 1;
            PlayerPrefs.SetInt("craftNum", amount);
        }
        amountText.text = "" + amount;
        craftResult.text = "";

        switch (currentRecipe)
        {
            case 1:
                PlayerPrefs.SetInt("recipeNum", 1);
                recipeDescription.text = "Goblin Steak: Eating this dish will let you regain a bit of health\nOwned: " + PlayerPrefs.GetInt("goblinSteak"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t"; 
                break;
            case 2:
                PlayerPrefs.SetInt("recipeNum", 2);
                recipeDescription.text = "Peanut Butter & Slime Jelly Sandwich: Eating this dish will increase your walk speed for a bit\nOwned: " + PlayerPrefs.GetInt("pbnjelly"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t"; 
                break;
            case 3:
                PlayerPrefs.SetInt("recipeNum", 3);
                recipeDescription.text = "Peanut Butter & Boss Slime Jelly Sandwich: Eating this dish will reduce the amount of damage you take for a bit\nOwned: " + PlayerPrefs.GetInt("pbnbossjelly"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly"; 
                break;
            case 4:
                PlayerPrefs.SetInt("recipeNum", 4);
                recipeDescription.text = "Recipe 4: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish4"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
                break;
            case 5:
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Recipe 5: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish5"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " ingredientA\t" + (3 * amount) + " ingredientB"; 
                break;
            default:
                PlayerPrefs.SetInt("recipeNum", 1);
                recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
        }

    }

    public void ToSelection()
    {
        SceneManager.LoadScene("SelectionMenu");
    }
}
