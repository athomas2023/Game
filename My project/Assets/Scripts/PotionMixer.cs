using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class Elements
{
    public ElementData element;
}

[Serializable]
public class Recipe
{
    public List<RecipeData.RecipeElement> requiredElements;
    public string potionName;
}

public class PotionMixer : MonoBehaviour
{
    public List<Elements> elements;
    public List<RecipeData> recipeDataList; // Drag your RecipeData objects here in the Inspector
    public TextMeshProUGUI elementCountText;
    public TextMeshProUGUI outputText;
    public string potionType;

    private void Start()
    {
        UpdateElementCountText();
    }

    private void Update()
    {
        PotionController();
        CheckPotion();
    }

    private void AddElement(ElementData element)
    {
        element.currentAmount++;
        UpdateElementCountText();
    }

    private void UpdateElementCountText()
    {
        string text = "Element Count: ";
        for (int i = 0; i < elements.Count; i++)
        {
            text += $"{elements[i].element.name} = {elements[i].element.currentAmount}, ";
        }
        elementCountText.text = text.TrimEnd(',', ' ');
    }

    private void PotionController()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Space))
        {
            ResetElements();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Q))
        {
            AddElement(elements[0].element);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.W))
        {
            AddElement(elements[1].element);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.E))
        {
            AddElement(elements[2].element);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.R))
        {
            AddElement(elements[3].element);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.T))
        {
            AddElement(elements[4].element);
        }

        CheckPotion();
    }

    private void ResetElements()
    {
        foreach (Elements element in elements)
        {
            element.element.currentAmount = 0;
        }
        UpdateElementCountText();
    }

    private void CheckPotion()
    {
        foreach (RecipeData recipeData in recipeDataList)
        {
            bool isRecipeMatch = true;
            foreach (RecipeData.RecipeElement recipeElement in recipeData.elements)
            {
                if (!IsRecipeMatch(recipeElement))
                {
                    isRecipeMatch = false;
                    break;
                }
            }

            if (isRecipeMatch)
            {
                // If all required elements are found in the current mix, set the output text to the recipe name
                outputText.text = recipeData.name;
                potionType = recipeData.name;
                return;
            }
        }

        // If no match is found, output "Undefined"
        outputText.text = "Undefined mixture in the pot";
    }

    private bool IsRecipeMatch(RecipeData.RecipeElement recipeElement)
    {
        // Check if the current mix has the required elements and amounts for a given recipe
        foreach (Elements currentElement in elements)
        {
            if (currentElement.element == recipeElement.element &&
                currentElement.element.currentAmount >= recipeElement.requiredAmount)
            {
                return true;
            }
        }

        return false;
    }
}
