using System.Collections;
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
    public TextMeshProUGUI Output; // Reference to the TextMeshProUGUI component for displaying the output message
    [SerializeField] TextMeshProUGUI hydrogenText;
    [SerializeField] TextMeshProUGUI oxygenText;
    [SerializeField] TextMeshProUGUI sodiumText;
    [SerializeField] TextMeshProUGUI chlorineText;
    [SerializeField] TextMeshProUGUI carbonText;

    [SerializeField] UnityEngine.UI.Image potionDisplay;
    [SerializeField] UnityEngine.UI.Image cookMeter;
    [SerializeField] UnityEngine.UI.Image burnMeter;

    public string potionType;
    bool potionCooked = false;

    [SerializeField] private CustomerManager customerManager;

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
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    public void AddHydrogen()
    {
        CurrentMix[hydrogenIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    public void AddCarbon()
    {
        CurrentMix[carbonIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
        
    }

    public void AddSodium()
    {
        CurrentMix[sodiumIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    public void AddChlorine()
    {
        CurrentMix[chlorineIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    private void UpdateElementCountText()
    {
        oxygenText.text = $"Q Oxygen = {CurrentMix[oxygenIndex]}";
        hydrogenText.text = $"W Hydrogen = {CurrentMix[hydrogenIndex]}";
        carbonText.text = $"E Carbon = {CurrentMix[carbonIndex]}";
        sodiumText.text = $"R Sodium = {CurrentMix[sodiumIndex]}";
        chlorineText.text = $"T Chlorine = {CurrentMix[chlorineIndex]}";
    }

    private void PotionController()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Q))
        {
            AddOxygen();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.W))
        {
            AddHydrogen();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.E))
        {
            AddCarbon();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.R))
        {
            AddSodium();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.T))
        {
            AddChlorine();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyDown(KeyCode.Y))
        {
            CookPotion();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.U))
        {
            ResetElements();
        }
    }

    private void CookPotion()
    {
        if (potionCooked == false)
        {
            StartCoroutine(PotionCooking(5f, cookMeter));
        }
        else
        {
            for (int i = 0; i < customerManager.customerControllers.Count; i++)
            {
                if (potionType == customerManager.customerControllers[i].potionOrder)
                {
                    customerManager.customerControllers[i].ServeCustomer();
                    potionType = "Empty";
                    ResetElements();
                }
            }
        }
    }

    private void CheckPotion()
    {
        int oxygenCount = CurrentMix[oxygenIndex];
        int hydrogenCount = CurrentMix[hydrogenIndex];
        int carbonCount = CurrentMix[carbonIndex];
        int sodiumCount = CurrentMix[sodiumIndex];
        int chlorineCount = CurrentMix[chlorineIndex];

        Debug.Log("Display potion");

        if (oxygenCount >= 1 && hydrogenCount >= 2 && carbonCount == 0 && sodiumCount == 0 && chlorineCount == 0) // Water
        {
            Output.text = "Water in the pot";
            potionType = "Water in the pot";
        }
        else if (sodiumCount >= 1 && chlorineCount >= 1 && oxygenCount == 0 && hydrogenCount == 0 && carbonCount == 0) // Salt
        {
            Output.text = "Salt in the pot";
            potionType = "Salt in the pot";
        }
        else if (carbonCount >= 6 && hydrogenCount >= 12 && oxygenCount >= 6 && sodiumCount == 0 && chlorineCount == 0) // Glucose
        {
            Output.text = "Glucose in the pot";
            potionType = "Glucose in the pot";
        }   
        else if (carbonCount >= 1 && hydrogenCount >= 1 && chlorineCount >= 3 && oxygenCount == 0 && sodiumCount == 0) // Chloroform
        {
            Output.text = "Chloroform in the pot";
            potionType = "Chloroform in the pot";
        }
        else if (carbonCount >= 7 && hydrogenCount >= 14 && oxygenCount == 0 && sodiumCount == 0 && chlorineCount == 0) // Jet Fuel
        {
            Output.text = "Jet Fuel in the pot";
            potionType = "Jet Fuel in the pot";
        }
        else if (carbonCount >= 0 && hydrogenCount == 0 && oxygenCount == 1 && sodiumCount == 1 && chlorineCount == 1) // Jet Fuel
        {
            Output.text = "Liquid Bleach in the pot";
            potionType = "Liquid Bleach in the pot";
        }
        else
        {
            Output.text = "Unknown mixture in the pot";
            potionType = "Unknown mixture in the pot";
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



    private IEnumerator PotionCooking(float timer, UnityEngine.UI.Image fillDisplay)
    {
        float timeRemaining = timer;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            fillDisplay.fillAmount += 1.0f / timeRemaining * Time.deltaTime;
        }

        if (potionCooked == false)
        {
            CheckPotion();
            Debug.Log("Checking potion");
            potionCooked = true;
            yield return new WaitForSeconds(1f);

            //StartCoroutine(PotionCooking(10f, burnMeter));
        }
        else
        {
            Debug.Log("Your potion exploded!");
        }     
        yield return null;
    }
}


