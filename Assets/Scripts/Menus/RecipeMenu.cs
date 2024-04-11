using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        PlayerPrefs.SetInt("dish1", 0);
        PlayerPrefs.SetInt("dish2", 0);
        PlayerPrefs.SetInt("dish3", 0);
        PlayerPrefs.SetInt("dish4", 0);
        PlayerPrefs.SetInt("dish5", 0);
        //initialize everything :)
        PlayerPrefs.SetInt("recipeNum", 1);
        amount = 1;
        myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("ingredientA") + " ingredientA\t" + PlayerPrefs.GetInt("ingredientB") + " ingredientB"; 
        recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1"); 
        ingredientsNeeded.text = "Required Ingredients:\n" + 1 + " ingredientA\t" + 1 + " ingredientB"; 
        amountText.text = "" + amount;
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
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 2:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
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
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 2:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 3:
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
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
    }

    //try to craft a dish
    public void Craft()
    {
        int aNeeded = 0;
        int bNeeded = 0;
        
        switch (PlayerPrefs.GetInt("recipeNum"))
        {
            case 1:
                aNeeded = 1 * amount;
                bNeeded = 1 * amount;
                break;
            case 2:
                aNeeded = 3 * amount;
                bNeeded = 1 * amount;
                break;
            case 3:
                aNeeded = 3 * amount;
                bNeeded = 2 * amount;
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

        if (PlayerPrefs.GetInt("ingredientA") < aNeeded || PlayerPrefs.GetInt("ingredientB") < bNeeded)
        {
            craftResult.text = "You do not have enough ingredients";
        }
        else
        {
            switch (PlayerPrefs.GetInt("recipeNum"))
            {
                case 1:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish1", PlayerPrefs.GetInt("dish1") + 1);
                    recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1");
                    break;
                case 2:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish2", PlayerPrefs.GetInt("dish2") + 1);
                    recipeDescription.text = "Recipe 2: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish2");
                    break;
                case 3:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish3", PlayerPrefs.GetInt("dish3") + 1);
                    recipeDescription.text = "Recipe 3: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish3");
                    break;
                case 4:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish4", PlayerPrefs.GetInt("dish4") + 1);
                    recipeDescription.text = "Recipe 4: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish4");
                    break;
                case 5:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish5", PlayerPrefs.GetInt("dish5") + 1);
                    recipeDescription.text = "Recipe 5: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish5");
                    break;
                default:
                    PlayerPrefs.SetInt("ingredientA", PlayerPrefs.GetInt("ingredientA") - aNeeded);
                    PlayerPrefs.SetInt("ingredientB", PlayerPrefs.GetInt("ingredientB") - bNeeded);
                    PlayerPrefs.SetInt("dish1", PlayerPrefs.GetInt("dish1") + 1);
                    recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1");
                    break;
            }

            myIngredients.text = "Ingredients Owned:\n" + PlayerPrefs.GetInt("ingredientA") + " ingredientA\t" + PlayerPrefs.GetInt("ingredientB") + " ingredientB";
            craftResult.text = "Successfully crafted";
        }

    }

    //update descriptions for a recipe
    public void SelectRecipe()
    {
        amount = 1;
        amountText.text = "" + amount;
        craftResult.text = "";

        switch (currentRecipe)
        {
            case 1:
                PlayerPrefs.SetInt("recipeNum", 1);
                recipeDescription.text = "Recipe 1: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish1"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (1 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 2:
                PlayerPrefs.SetInt("recipeNum", 2);
                recipeDescription.text = "Recipe 2: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish2"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (1 * amount) + " ingredientB"; 
                break;
            case 3:
                PlayerPrefs.SetInt("recipeNum", 3);
                recipeDescription.text = "Recipe 3: describe the effects provided by the dish\nOwned: " + PlayerPrefs.GetInt("dish3"); 
                ingredientsNeeded.text = "Required Ingredients:\n" + (3 * amount) + " ingredientA\t" + (2 * amount) + " ingredientB"; 
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
}
