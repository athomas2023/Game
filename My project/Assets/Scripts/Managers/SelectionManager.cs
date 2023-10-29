using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private PotionController potion1;
    [SerializeField] private PotionController potion2;
    [SerializeField] private PotionController potion3;
    public List<GameObject> remainingPotions;
    public int selectedPotion = 1;
    private void Update()
    {
        SelectNewPotion();
        HighlightSelectedPotion();
        remainingPotions.RemoveAll(s => s == null);
    }

    private void SelectNewPotion()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedPotion += 1;
            if (selectedPotion > remainingPotions.Count)
            {
                selectedPotion = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedPotion -= 1;
            if (selectedPotion < 1)
            {
                selectedPotion = remainingPotions.Count;
            }
        }
    }

    private void HighlightSelectedPotion()
    {
        for (int i = 0; i < remainingPotions.Count; i++)
            {
                remainingPotions[i].GetComponent<PotionController>().potionSelected = false;
                if (i == selectedPotion-1)
                {
                    remainingPotions[i].GetComponent<PotionController>().potionSelected = true;
                }
            }
    }
}
