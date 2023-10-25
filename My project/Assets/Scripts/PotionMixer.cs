using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionMixer : MonoBehaviour
{
    private int[] CurrentMix = new int[3]; // Initialize an array of size 3 (Oxygen, Hydrogen, Carbon)
    public TextMeshProUGUI ElementCountText; // Reference to the TextMeshProUGUI component for displaying element counts
    public TextMeshProUGUI Output; // Reference to the TextMeshProUGUI component for displaying the output message

    void Start()
    {
        // Initialize the ElementCountText with the initial counts
        UpdateElementCountText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            AddOxygen();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            AddHydrogen();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            AddCarbon();
        }
        // Check if all values in the CurrentMix array are 0
        if (CurrentMix[0] == 0 && CurrentMix[1] == 0 && CurrentMix[2] == 0)
        {
            Output.text = "No elements in the pot";
        }
        // Check if only one element is present
        else if (CurrentMix[0] == 0 && CurrentMix[1] == 1 && CurrentMix[2] == 0)
        {
            Output.text = "Hydrogen only in the pot\n1 Hydrogen";
        }
        // Check if there is water (1 Oxygen and 2 Hydrogen)
        else if (CurrentMix[0] == 1 && CurrentMix[1] == 2 && CurrentMix[2] == 0)
        {
            Output.text = "Water in the pot";
        }
    }

    // Function to add 1 to Oxygen
    public void AddOxygen()
    {
        CurrentMix[0]++;
        UpdateElementCountText();
    }

    // Function to add 1 to Hydrogen
    public void AddHydrogen()
    {
        CurrentMix[1]++;
        UpdateElementCountText();
    }

    // Function to add 1 to Carbon
    public void AddCarbon()
    {
        CurrentMix[2]++;
        UpdateElementCountText();
    }

    // Function to update the TextMeshProUGUI with element counts
    private void UpdateElementCountText()
    {
        ElementCountText.text = $"Oxygen = {CurrentMix[0]}, Hydrogen = {CurrentMix[1]}, Carbon = {CurrentMix[2]}";
    }
}
