using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Element List/Recipe", order = 52)]
public class RecipeData : ScriptableObject
{
    [System.Serializable]
    public struct RecipeElement
    {
        public ElementData element;
        public int requiredAmount;
    }

    public RecipeElement[] elements;

    // Additional properties or methods related to the recipe can be added here
}

