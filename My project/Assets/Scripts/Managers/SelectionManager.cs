using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private PotionController potion1;
    [SerializeField] private PotionController potion2;
    [SerializeField] private PotionController potion3;
    private int selectedPotion = 1;

    private void Update()
    {
        SelectNewPotion();
        HighlightSelectedPotion();
    }

    private void SelectNewPotion()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedPotion += 1;
            if (selectedPotion > 3)
            {
                selectedPotion = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedPotion -= 1;
            if (selectedPotion < 1)
            {
                selectedPotion = 3;
            }
        }
    }

    private void HighlightSelectedPotion()
    {
        if (selectedPotion == 1)
        {
            potion1.potionSelected = true;
            potion2.potionSelected = false;
            potion3.potionSelected = false;
        }
        else if (selectedPotion == 2)
        {
            potion1.potionSelected = false;
            potion2.potionSelected = true;
            potion3.potionSelected = false;
        }
        else if (selectedPotion == 3)
        {
            potion1.potionSelected = false;
            potion2.potionSelected = false;
            potion3.potionSelected = true;
        }
    }
}
