using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private PotionController potion1;
    [SerializeField] private PotionController potion2;
    [SerializeField] private PotionController potion3;
    public List<GameObject> remainingPotions;
    public int selectedPotion = 1;
    private bool axisInUse = false;
    private void Update()
    {
        SelectNewPotion();
        HighlightSelectedPotion();
        remainingPotions.RemoveAll(s => s == null);
    }

    private void SelectNewPotion()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") == 1) && axisInUse == false)
        {

            selectedPotion += 1;
            if (selectedPotion > remainingPotions.Count)
            {
                selectedPotion = 1;
            }
            axisInUse = true;
            Invoke("SelectionCooldown", 0.5f);
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") == -1) && axisInUse == false)
        {
            selectedPotion -= 1;
            if (selectedPotion < 1)
            {
                selectedPotion = remainingPotions.Count;
            }
            axisInUse = true;
            Invoke("SelectionCooldown", 0.25f);
        }
    }

    private void SelectionCooldown()
    {
        axisInUse = false;
    }

    private void HighlightSelectedPotion()
    {
        for (int i = 0; i < remainingPotions.Count; i++)
        {
            if (remainingPotions[i] != null)
            {
                remainingPotions[i].GetComponent<PotionController>().potionSelected = false;
                if (i == selectedPotion - 1)
                {
                    remainingPotions[i].GetComponent<PotionController>().potionSelected = true;
                }
            }

        }
    }
}
