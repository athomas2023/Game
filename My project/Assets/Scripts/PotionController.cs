using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionController : MonoBehaviour
{
    public bool potionSelected;

    [SerializeField] private float cookTime = 5f;
    [SerializeField] private float burnTime = 10f;
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

    [SerializeField] Image potionDisplay;
    [SerializeField] Image cookMeter;
    [SerializeField] Image burnMeter;

    [SerializeField] SelectionManager selectionManager;

    public int potionType;
    bool potionCooked = false;
    bool potionCooking = false;

    [SerializeField] private CustomerManager customerManager;

    void Start()
    {
        UpdateElementCountText();
        Output.text = "Empty";
    }
    private void Update()
    {
        if (burnMeter.fillAmount == 1)
        {
            ExplodePotion();
        }

        if (potionSelected)
        {
            GetComponent<Outline>().enabled = true;
            UpdateElementCountText();
            AddIngredients();
        }
        else
        {
            GetComponent<Outline>().enabled = false;
        }
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
        oxygenText.text = $"O = {CurrentMix[oxygenIndex]}";
        hydrogenText.text = $"H = {CurrentMix[hydrogenIndex]}";
        carbonText.text = $"C = {CurrentMix[carbonIndex]}";
        sodiumText.text = $"Na = {CurrentMix[sodiumIndex]}";
        chlorineText.text = $"Cl = {CurrentMix[chlorineIndex]}";
    }

    private void AddIngredients()
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
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Y))
        {
            InteractWithPotion();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.U))
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
            Output.text = "Water";
            potionType = 1;
        }
        else if (sodiumCount >= 1 && chlorineCount >= 1 && oxygenCount == 0 && hydrogenCount == 0 && carbonCount == 0) // Salt
        {
            Output.text = "Salt";
            potionType = 2;
        }
        else if (carbonCount >= 6 && hydrogenCount >= 12 && oxygenCount >= 6 && sodiumCount == 0 && chlorineCount == 0) // Glucose
        {
            Output.text = "Glucose";
            potionType = 3;
        }   
        else if (carbonCount >= 1 && hydrogenCount >= 1 && chlorineCount >= 3 && oxygenCount == 0 && sodiumCount == 0) // Chloroform
        {
            Output.text = "Chloroform";
            potionType = 4;
        }
        else if (carbonCount >= 7 && hydrogenCount >= 14 && oxygenCount == 0 && sodiumCount == 0 && chlorineCount == 0) // Jet Fuel
        {
            Output.text = "Jet Fuel";
            potionType = 5;
        }
        else if (carbonCount >= 0 && hydrogenCount == 0 && oxygenCount == 1 && sodiumCount == 1 && chlorineCount == 1) // Jet Fuel
        {
            Output.text = "Liquid Bleach";
            potionType = 6;
        }
        else
        {
            Output.text = "Unknown";
            potionType = 0;
        }
    }

    private void InteractWithPotion()
    {
        if (potionCooked == false && potionCooking == false)
        {
            potionCooking = true;
            StartCoroutine(PotionCooking(cookTime, cookMeter));
        }
        else
        {
            for (int i = 0; i < customerManager.customerControllers.Count; i++)
            {
                if (potionType == customerManager.customerControllers[i].potionOrder)
                {
                    customerManager.customerControllers[i].ServeCustomer();
                    potionCooked = false;
                    potionType = 0;
                    StopAllCoroutines();
                    potionDisplay.fillAmount = 0;
                    burnMeter.fillAmount = 0;
                    ResetElements();
                }
            }
        }
    }

    private void ExplodePotion()
    {
        selectionManager.remainingPotions.Remove(this.gameObject);
        Destroy(gameObject);
    }

    private void ResetElements()
    {
        for (int i = 0; i < CurrentMix.Length; i++)
        {
            CurrentMix[i] = 0;
        }
        potionCooked = false;
        potionType = 0;
        StopAllCoroutines();
        potionDisplay.fillAmount = 0;
        burnMeter.fillAmount = 0;
        cookMeter.fillAmount = 0;
        Output.text = "Empty";
    }

    private IEnumerator PotionCooking(float duration, Image fillDisplay)
    {
        float time = 0.0f;
        while (time < duration)
        {
            fillDisplay.fillAmount = time / Mathf.Max(duration, float.Epsilon);
            time += Time.deltaTime;

            yield return null;
        }

        if (potionCooked == false)
        {
            potionCooked = true;
            potionCooking = false;
            CheckPotion();
            StartCoroutine(PotionCooking(burnTime, burnMeter));
            yield return null;
        }
        else
        {
            Debug.Log("Your potion exploded!");
            Destroy(gameObject);    
        }     
        yield return null;
    }
}
