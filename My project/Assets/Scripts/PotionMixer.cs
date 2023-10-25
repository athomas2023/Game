using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionMixer : MonoBehaviour
{
    private int[] CurrentMix = new int[5]; // Initialize an array of size 5 (Oxygen, Hydrogen, Carbon, Sodium, Chlorine)
    private int oxygenIndex = 0; // Index for Oxygen
    private int hydrogenIndex = 1; // Index for Hydrogen
    private int carbonIndex = 2; // Index for Carbon
    private int sodiumIndex = 3; // Index for Sodium
    private int chlorineIndex = 4; // Index for Chlorine
    public TextMeshProUGUI ElementCountText; // Reference to the TextMeshProUGUI component for displaying element counts
    public TextMeshProUGUI Output; // Reference to the TextMeshProUGUI component for displaying the output message

    void Start()
    {
        // Initialize the ElementCountText with the initial counts
        UpdateElementCountText();
    }

    void Update()
    {
        PotionController();
    }

    public void AddOxygen()
    {
        CurrentMix[oxygenIndex]++;
        UpdateElementCountText();
    }

    public void AddHydrogen()
    {
        CurrentMix[hydrogenIndex]++;
        UpdateElementCountText();
    }

    public void AddCarbon()
    {
        CurrentMix[carbonIndex]++;
        UpdateElementCountText();
    }

    public void AddSodium()
    {
        CurrentMix[sodiumIndex]++;
        UpdateElementCountText();
    }

    public void AddChlorine()
    {
        CurrentMix[chlorineIndex]++;
        UpdateElementCountText();
    }

    private void UpdateElementCountText()
    {
        ElementCountText.text = $"Oxygen = {CurrentMix[oxygenIndex]}, Hydrogen = {CurrentMix[hydrogenIndex]}, Carbon = {CurrentMix[carbonIndex]}, Sodium = {CurrentMix[sodiumIndex]}, Chlorine = {CurrentMix[chlorineIndex]}";
    }

    private void PotionController()
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
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            AddSodium();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            AddChlorine();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            CheckPotion();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            ResetElements();
        }
    }

    private void CheckPotion()
    {
        int oxygenCount = CurrentMix[oxygenIndex];
        int hydrogenCount = CurrentMix[hydrogenIndex];
        int carbonCount = CurrentMix[carbonIndex];
        int sodiumCount = CurrentMix[sodiumIndex];
        int chlorineCount = CurrentMix[chlorineIndex];

        if (oxygenCount >= 1 && hydrogenCount >= 2 && carbonCount == 0 && sodiumCount == 0 && chlorineCount == 0) // Water
        {
            Output.text = "Water in the pot";
        }
        else if (sodiumCount >= 1 && chlorineCount >= 1 && oxygenCount == 0 && hydrogenCount == 0 && carbonCount == 0) // Salt
        {
            Output.text = "Salt in the pot";
        }
        else if (carbonCount >= 6 && hydrogenCount >= 12 && oxygenCount >= 6 && sodiumCount == 0 && chlorineCount == 0) // Glucose
        {
            Output.text = "Glucose in the pot";
        }
        else if (carbonCount >= 1 && hydrogenCount >= 1 && chlorineCount >= 3 && oxygenCount == 0 && sodiumCount == 0) // Chloroform
        {
            Output.text = "Chloroform in the pot";
        }
        else if (carbonCount >= 7 && hydrogenCount >= 14 && oxygenCount == 0 && sodiumCount == 0 && chlorineCount == 0) // Jet Fuel
        {
            Output.text = "Jet Fuel in the pot";
        }

        else if (carbonCount >= 0 && hydrogenCount == 0 && oxygenCount == 1 && sodiumCount == 1 && chlorineCount == 1) // Jet Fuel
        {
            Output.text = "Liquid Bleach in the pot";
        }
        else
        {
            Output.text = "Unknown mixture in the pot";
        }
    }

    private void ResetElements()
    {
        for (int i = 0; i < CurrentMix.Length; i++)
        {
            CurrentMix[i] = 0;
        }
        UpdateElementCountText();
        Output.text = "All elements reset";
    }
}


