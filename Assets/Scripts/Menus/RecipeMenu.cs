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
        PlayerPrefs.SetInt("dish1", 0); //for default cases
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
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry";
                break;
            case 4:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom";
                break;
            case 5:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly";
                break;
            case 6:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 7:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                break;
            case 8:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                break;
            case 9:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                break;
            case 10:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount)
                    + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
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
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry";
                break;
            case 4:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom";
                break;
            case 5:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly";
                break;
            case 6:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 7:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                break;
            case 8:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                break;
            case 9:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                break;
            case 10:
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount)
                    + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
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
        int dNeeded = 0; //minotaur horn
        int eNeeded = 0; //mushrooms
        int fNeeded = 0; //blood berry
        int gNeeded = 0; //bones

        amount = PlayerPrefs.GetInt("craftNum");

        switch (PlayerPrefs.GetInt("recipeNum"))
        {
            case 1://goblin steak, a
                aNeeded = 1 * amount;
                break;
            case 2://slime jelly sandwich, bc
                bNeeded = 3 * amount;
                cNeeded = 1 * amount;
                break;
            case 3://blood berry slime jelly sandwich, bcf
                bNeeded = 3 * amount;
                cNeeded = 1 * amount;
                fNeeded = 1 * amount;
                break;
            case 4://fried mushroom, e
                eNeeded = 2 * amount;
                break;
            case 5://blood berry soup, bf
                bNeeded = 2 * amount;
                fNeeded = 3 * amount;
                break;
            case 6://royal blood berry soup, cf
                cNeeded = 2 * amount;
                fNeeded = 1 * amount;
                break;
            case 7://boss drink, bdf
                bNeeded = 2 * amount;
                dNeeded = 3 * amount;
                fNeeded = 1 * amount;
                break;
            case 8://roasted bone marrow, g
                gNeeded = 3 * amount;
                break;
            case 9://dungeon dinner, aeg
                aNeeded = 2 * amount;
                eNeeded = 3 * amount;
                gNeeded = 1 * amount;
                break;
            case 10://dungeon feast, abcdefg
                aNeeded = 2 * amount;
                bNeeded = 3 * amount;
                cNeeded = 1 * amount;
                dNeeded = 1 * amount;
                eNeeded = 1 * amount;
                fNeeded = 1 * amount;
                gNeeded = 1 * amount;
                break;
            default:
                aNeeded = 1 * amount;
                bNeeded = 1 * amount;
                break;
        }

        if (PlayerPrefs.GetInt("goblinMeat") < aNeeded || PlayerPrefs.GetInt("slimeJelly") < bNeeded || PlayerPrefs.GetInt("bossSlimeJelly") < cNeeded 
            || PlayerPrefs.GetInt("horn") < dNeeded || PlayerPrefs.GetInt("mushroom") < eNeeded 
            || PlayerPrefs.GetInt("bloodBerry") < fNeeded || PlayerPrefs.GetInt("bone") < gNeeded)
        {
            craftResult.text = "You do not have enough ingredients";
        }
        else
        {
            switch (PlayerPrefs.GetInt("recipeNum"))
            {
                case 1://goblinSteak
                    PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") - aNeeded);
                    PlayerPrefs.SetInt("goblinSteak", PlayerPrefs.GetInt("goblinSteak") + amount);
                    recipeDescription.text = "Goblin Steak: Eating this dish will let you regain a bit of health\nOwned: " + PlayerPrefs.GetInt("goblinSteak");
                    break;
                case 2://jellysand
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("jellysand", PlayerPrefs.GetInt("jellysand") + amount);
                    recipeDescription.text = "Slime Jelly Sandwich: Eating this dish will increase your walk speed for a bit\nOwned: " + PlayerPrefs.GetInt("jellysand");
                    break;
                case 3://bbsand
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bbsand", PlayerPrefs.GetInt("bbsand") + amount);
                    recipeDescription.text = "Blood Berry Slime Jelly Sandwich: Eating this dish will reduce the amount of damage you take for a bit\nOwned: " + PlayerPrefs.GetInt("bbsand");
                    break;
                case 4://friedmush
                    PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") - eNeeded);
                    PlayerPrefs.SetInt("friedmush", PlayerPrefs.GetInt("friedmush") + amount);
                    recipeDescription.text = "Fried Mushroom: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("friedmush");
                    break;
                case 5://bbsoup
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bbsoup", PlayerPrefs.GetInt("bbsoup") + amount);
                    recipeDescription.text = "Blood Berry Soup: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("bbsoup");
                    break;
                case 6://royalbbsoup
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("royalbbsoup", PlayerPrefs.GetInt("royalbbsoup") + amount);
                    recipeDescription.text = "Royal Blood Berry Soup: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("royalbbsoup");
                    break;
                case 7://bossdrink
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("horn", PlayerPrefs.GetInt("horn") - dNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bossdrink", PlayerPrefs.GetInt("bossdrink") + amount);
                    recipeDescription.text = "Boss Drink: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("bossdrink");
                    break;
                case 8://roastbone
                    PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") - gNeeded);
                    PlayerPrefs.SetInt("roastbone", PlayerPrefs.GetInt("roastbone") + amount);
                    recipeDescription.text = "Roasted Bone Marrow: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("roastbone");
                    break;
                case 9://dundinner
                    PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") - aNeeded);
                    PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") - eNeeded);
                    PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") - gNeeded);
                    PlayerPrefs.SetInt("dundinner", PlayerPrefs.GetInt("dundinner") + amount);
                    recipeDescription.text = "Dungeon Dinner: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dundinner");
                    break;
                case 10://dunfeast
                    PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") - aNeeded);
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("horn", PlayerPrefs.GetInt("horn") - dNeeded);
                    PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") - eNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") - gNeeded);
                    PlayerPrefs.SetInt("dunfeast", PlayerPrefs.GetInt("dunfeast") + amount);
                    recipeDescription.text = "Dungeon Feast: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dunfeast");
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
            case 1://goblinSteak
                PlayerPrefs.SetInt("recipeNum", 1);
                recipeDescription.text = "Goblin Steak: Eating this dish will let you regain a bit of health\nOwned: " + PlayerPrefs.GetInt("goblinSteak"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t"; 
                break;
            case 2://jellysand
                PlayerPrefs.SetInt("recipeNum", 2);
                recipeDescription.text = "Slime Jelly Sandwich: Eating this dish will increase your walk speed for a bit\nOwned: " + PlayerPrefs.GetInt("jellysand"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 3://bbsand
                PlayerPrefs.SetInt("recipeNum", 3);
                recipeDescription.text = "Blood Berry Slime Jelly Sandwich: Eating this dish will reduce the amount of damage you take for a bit\nOwned: " + PlayerPrefs.GetInt("bbsand"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry"; 
                break;
            case 4://friedmush
                PlayerPrefs.SetInt("recipeNum", 4);
                recipeDescription.text = "Fried Mushroom: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("friedmush"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom"; 
                break;
            case 5://bbsoup
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Blood Berry Soup: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("bbsoup"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly"; 
                break;
            case 6://royalbbsoup
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Royal Blood Berry Soup: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("royalbbsoup");
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                break;
            case 7://bossdrink
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Boss Drink: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("bossdrink");
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                break;
            case 8://roastbone
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Roasted Bone Marrow: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("roastbone");
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                break;
            case 9://dundinner
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Dungeon Dinner: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dundinner");
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                break;
            case 10://dunfeast
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Dungeon Feast: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dunfeast");
                ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount) 
                    + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
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
