using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class RecipeMenu : MonoBehaviour
{
    //public TextMeshProUGUI myIngredients;
    public TextMeshProUGUI recipeDescription;
    //public TextMeshProUGUI ingredientsNeeded;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI craftResult;
    public int currentRecipe;

    public TextMeshProUGUI ingr1;
    public TextMeshProUGUI ingr2;
    public TextMeshProUGUI ingr3;
    public TextMeshProUGUI ingr4;
    public TextMeshProUGUI ingr5;
    public TextMeshProUGUI ingr6;
    public TextMeshProUGUI ingr7;

    private int amount;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("dish1", 0); //for default cases
        //initialize everything :)
        PlayerPrefs.SetInt("recipeNum", 1);
        amount = 1;
        //myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("goblinMeat") + " Goblin Meat\t" + PlayerPrefs.GetInt("slimeJelly") + " Slime Jelly\n" + PlayerPrefs.GetInt("bossSlimeJelly") + " Boss Slime Jelly\t"; 
        recipeDescription.text = "Goblin Steak: Eating this dish will let you regain 10 hp (5 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak") + "\nRequired Ingredients:";
        //ingredientsNeeded.text = "Required Ingredients:\n" + 1 + " Goblin Meat\t"; 
        amountText.text = "" + amount;
        ingr1.text = "1(" + PlayerPrefs.GetInt("goblinMeat") + ")";
        ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
        ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
        ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
        ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
        ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
        ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
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
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t";
                ingr1.text = (1 * amount) + "(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                break;
            case 2:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                break;
            case 3:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 4:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom";
                ingr4.text = (2 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                break;
            case 5:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 6:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 7:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                ingr3.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                break;
            case 8:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                ingr7.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 9:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr4.text = (3 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 10:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount)
                 //   + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = (1 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            default:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB";
                ingr2.text = (1 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
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
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t";
                ingr1.text = (1 * amount) + "(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                break;
            case 2:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                break;
            case 3:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 4:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom";
                ingr4.text = (2 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                break;
            case 5:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 6:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                break;
            case 7:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                ingr3.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr5.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                break;
            case 8:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                ingr7.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 9:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr4.text = (3 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 10:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount)
                //    + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = (1 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            default:
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB";
                ingr2.text = (1 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
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
                    recipeDescription.text = "Goblin Steak: Eating this dish will let you regain 10 hp (5 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak") + "\nRequired Ingredients:";
                    break;
                case 2://jellysand
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("jellysand", PlayerPrefs.GetInt("jellysand") + amount);
                    recipeDescription.text = "Slime Jelly Sandwich: Eating this dish will increase your walk speed for a bit\nOwned: " + PlayerPrefs.GetInt("jellysand") + "\nRequired Ingredients:";
                    break;
                case 3://bbsand
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bbsand", PlayerPrefs.GetInt("bbsand") + amount);
                    recipeDescription.text = "Blood Berry Slime Jelly Sandwich: Eating this dish will reduce the damage you take by 20% for 30 seconds, you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("bbsand") + "\nRequired Ingredients:";
                    break;
                case 4://friedmush
                    PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") - eNeeded);
                    PlayerPrefs.SetInt("friedmush", PlayerPrefs.GetInt("friedmush") + amount);
                    recipeDescription.text = "Fried Mushroom: Eating this dish will increase the damage you deal by 30% for 15 seconds, you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("friedmush") + "\nRequired Ingredients:";
                    break;
                case 5://bbsoup
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bbsoup", PlayerPrefs.GetInt("bbsoup") + amount);
                    recipeDescription.text = "Blood Berry Soup: Eating this dish will let you regain 15 hp (8 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("bbsoup") + "\nRequired Ingredients:";
                    break;
                case 6://royalbbsoup
                    PlayerPrefs.SetInt("bossSlimeJelly", PlayerPrefs.GetInt("bossSlimeJelly") - cNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("royalbbsoup", PlayerPrefs.GetInt("royalbbsoup") + amount);
                    recipeDescription.text = "Royal Blood Berry Soup: Eating this dish will increase your movement speed for 25 seconds, you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("royalbbsoup") + "\nRequired Ingredients:";
                    break;
                case 7://bossdrink
                    PlayerPrefs.SetInt("slimeJelly", PlayerPrefs.GetInt("slimeJelly") - bNeeded);
                    PlayerPrefs.SetInt("horn", PlayerPrefs.GetInt("horn") - dNeeded);
                    PlayerPrefs.SetInt("bloodBerry", PlayerPrefs.GetInt("bloodBerry") - fNeeded);
                    PlayerPrefs.SetInt("bossdrink", PlayerPrefs.GetInt("bossdrink") + amount);
                    recipeDescription.text = "Boss Drink: Eating this dish will increase your total health by 50, you can carry 2 at a time\nOwned: " + PlayerPrefs.GetInt("bossdrink") + "\nRequired Ingredients:";
                    break;
                case 8://roastbone
                    PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") - gNeeded);
                    PlayerPrefs.SetInt("roastbone", PlayerPrefs.GetInt("roastbone") + amount);
                    recipeDescription.text = "Roasted Bone Marrow: Eating this dish will reduce the damage you take by 30% for 40 seconds, you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("roastbone") + "\nRequired Ingredients:";
                    break;
                case 9://dundinner
                    PlayerPrefs.SetInt("goblinMeat", PlayerPrefs.GetInt("goblinMeat") - aNeeded);
                    PlayerPrefs.SetInt("mushroom", PlayerPrefs.GetInt("mushroom") - eNeeded);
                    PlayerPrefs.SetInt("bone", PlayerPrefs.GetInt("bone") - gNeeded);
                    PlayerPrefs.SetInt("dundinner", PlayerPrefs.GetInt("dundinner") + amount);
                    recipeDescription.text = "Dungeon Dinner: Eating this dish will increase the damage you deal by 50% for 25 seconds, you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("dundinner") + "\nRequired Ingredients:";
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
                    recipeDescription.text = "Dungeon Feast: Eating this dish will completely recover your health (75 second cooldown), you can carry 2 at a time\nOwned: " + PlayerPrefs.GetInt("dunfeast") + "\nRequired Ingredients:";
                    break;
                default:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish1", PlayerPrefs.GetInt("dish1") + amount);
                    recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1") + "\nRequired Ingredients:";
                    break;
            }

            //myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("goblinMeat") + " Goblin Meat\t" + PlayerPrefs.GetInt("slimeJelly") + " Slime Jelly\n" + PlayerPrefs.GetInt("bossSlimeJelly") + " Boss Slime Jelly\t";
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
                recipeDescription.text = "Goblin Steak: Eating this dish will let you regain 10 hp (5 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("goblinSteak") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " Goblin Meat\t";
                ingr1.text = (1 * amount) + "(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 2://jellysand
                PlayerPrefs.SetInt("recipeNum", 2);
                recipeDescription.text = "Slime Jelly Sandwich: Eating this dish will increase your movement speed for 15 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("jellysand") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 3://bbsand
                PlayerPrefs.SetInt("recipeNum", 3);
                recipeDescription.text = "Blood Berry Slime Jelly Sandwich: Eating this dish will reduce the damage you take by 20% for 30 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("bbsand") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Slime Jelly\t" + (1 * amount) + " Boss Slime Jelly" + (1 * amount) + " Blood Berry";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 4://friedmush
                PlayerPrefs.SetInt("recipeNum", 4);
                recipeDescription.text = "Fried Mushroom: Eating this dish will increase the damage you deal by 30% for 15 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("friedmush") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Mushroom";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = (2 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 5://bbsoup
                PlayerPrefs.SetInt("recipeNum", 5);
                recipeDescription.text = "Blood Berry Soup: Eating this dish will let you regain 15 hp (8 second cooldown), you can carry 3 at a time\nOwned: " + PlayerPrefs.GetInt("bbsoup") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Blood Berry\t" + (3 * amount) + " Slime Jelly";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 6://royalbbsoup
                PlayerPrefs.SetInt("recipeNum", 6);
                recipeDescription.text = "Royal Blood Berry Soup: Eating this dish will increase your movement speed for 25 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("royalbbsoup") + "\nRequired Ingredients:";
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + "Blood Berry \t" + (1 * amount) + " Boss Slime Jelly";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 7://bossdrink
                PlayerPrefs.SetInt("recipeNum", 7);
                recipeDescription.text = "Boss Drink: Eating this dish will increase your total health by 50, you can carry 2 at a time. The effect will only last for 1 level\nOwned: " + PlayerPrefs.GetInt("bossdrink") + "\nRequired Ingredients:";
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Boss Slime Jelly\t" + (3 * amount) + " Blood Berry" + (1 * amount) + " Minotaur Horn";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (2 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = "0(" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 8://roastbone
                PlayerPrefs.SetInt("recipeNum", 8);
                recipeDescription.text = "Roasted Bone Marrow: Eating this dish will reduce the damage you take by 30% for 40 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("roastbone") + "\nRequired Ingredients:";
                //ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " Bone";
                ingr1.text = "0(" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = "0(" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = (3 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 9://dundinner
                PlayerPrefs.SetInt("recipeNum", 9);
                recipeDescription.text = "Dungeon Dinner: Eating this dish will increase the damage you deal by 50% for 25 seconds, you can carry 3 at a time. The effect does not stack\nOwned: " + PlayerPrefs.GetInt("dundinner") + "\nRequired Ingredients:";
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Mushroom" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = "0(" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = "0(" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = (3 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = "0(" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = "0(" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            case 10://dunfeast
                PlayerPrefs.SetInt("recipeNum", 10);
                recipeDescription.text = "Dungeon Feast: Eating this dish will completely recover your health (75 second cooldown), you can carry 2 at a time\nOwned: " + PlayerPrefs.GetInt("dunfeast") + "\nRequired Ingredients:";
                //ingredientsNeeded.text = "Required Ingredients:\n" + (2 * amount) + " Goblin Meat\t" + (3 * amount) + " Slime Jelly" + (1 * amount) 
                 //   + " Boss Slime Jelly" + (1 * amount) + " Minotaur Horn" + (1 * amount) + " Mushroom" + (1 * amount) + " Blood Berry" + (1 * amount) + " Bone";
                ingr1.text = (2 * amount) + " (" + PlayerPrefs.GetInt("goblinMeat") + ")";
                ingr2.text = (3 * amount) + " (" + PlayerPrefs.GetInt("slimeJelly") + ")";
                ingr3.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bossSlimeJelly") + ")";
                ingr4.text = (1 * amount) + " (" + PlayerPrefs.GetInt("mushroom") + ")";
                ingr5.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bloodBerry") + ")";
                ingr6.text = (1 * amount) + " (" + PlayerPrefs.GetInt("horn") + ")";
                ingr7.text = (1 * amount) + " (" + PlayerPrefs.GetInt("bone") + ")";
                break;
            default:
                PlayerPrefs.SetInt("recipeNum", 1);
                recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1") + "\nRequired Ingredients:"; 
                //ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
        }

    }

    public void ToSelection()
    {
        SceneManager.LoadScene("SelectionMenu");
    }
}
