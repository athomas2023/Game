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
        if (Time.timeScale == 1)
        {
            SelectNewPotion();
            HighlightSelectedPotion();
            remainingPotions.RemoveAll(s => s == null);
        }  
    }

    private void SelectNewPotion()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") == 1)
        {
            if (axisInUse == false)
            {
                selectedPotion += 1;
                if (selectedPotion > remainingPotions.Count)
                {
                    selectedPotion = 1;
                }
                axisInUse = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") == -1)
        {
            if (axisInUse == false)
            {
                selectedPotion -= 1;
                if (selectedPotion < 1)
                {
                    selectedPotion = remainingPotions.Count;
                }
                axisInUse = true;
            }
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            axisInUse = false;
        }
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
