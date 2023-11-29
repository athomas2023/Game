using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionControllerLV1 : MonoBehaviour
{
    public bool potionSelected;

    [SerializeField] private float cookTime = 5f;
    [SerializeField] private float burnTime = 10f;
    private int[] CurrentMix = new int[5]; // Initialize an array of size 5 (Oxygen, Hydrogen, Fluorine, Sulfur, Nitrogen)
    private int oxygenIndex = 0; // Index for Oxygen
    private int hydrogenIndex = 1; // Index for Hydrogen
    private int FluorineIndex = 2; // Index for Fluorine
    private int SulfurIndex = 3; // Index for Sulfur
    private int NitrogenIndex = 4; // Index for Nitrogen
    public TextMeshProUGUI Output; // Reference to the TextMeshProUGUI component for displaying the output message
    [SerializeField] TextMeshProUGUI hydrogenText;
    [SerializeField] TextMeshProUGUI oxygenText;
    [SerializeField] TextMeshProUGUI SulfurText;
    [SerializeField] TextMeshProUGUI NitrogenText;
    [SerializeField] TextMeshProUGUI FluorineText;

    [SerializeField] Image potionDisplay;
    [SerializeField] Image cookMeter;
    [SerializeField] Image burnMeter;
    [SerializeField] Image trashProgress;

    [SerializeField] SelectionManager selectionManager;

    public string potionType;
    bool potionCooked = false;
    bool potionCooking = false;
    [SerializeField] private float trashTimer = 3f;
    private float trashDelay = 0.25f;
    private float time = 0f;

    [SerializeField] private CustomerManager customerManager;

    void Start()
    {
        UpdateElementCountText();
        Output.text = "Empty";
    }
    private void Update()
    {
        if (Time.timeScale == 1)
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
            if (!potionSelected)
            {
                trashProgress.fillAmount = 0f;
                time = 0f;
            }
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

    public void AddFluorine()
    {
        CurrentMix[FluorineIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();

    }

    public void AddSulfur()
    {
        CurrentMix[SulfurIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    public void AddNitrogen()
    {
        CurrentMix[NitrogenIndex]++;
        potionDisplay.fillAmount += 0.1f;
        UpdateElementCountText();
    }

    private void UpdateElementCountText()
    {
        oxygenText.text = $"O = {CurrentMix[oxygenIndex]}";
        hydrogenText.text = $"H = {CurrentMix[hydrogenIndex]}";
        FluorineText.text = $"F = {CurrentMix[FluorineIndex]}";
        SulfurText.text = $"S = {CurrentMix[SulfurIndex]}";
        NitrogenText.text = $"N = {CurrentMix[NitrogenIndex]}";
    }

    private void AddIngredients()
    {
        if (potionCooking == false && potionCooked == false)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Q))
            {
                AddHydrogen();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.W))
            {
                AddOxygen();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.E))
            {
                AddSulfur();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.R))
            {
                AddNitrogen();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.T))
            {
                AddFluorine();
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space))
        {
            trashDelay = 0.25f;
            if (potionCooked == true || potionCooking == false)
            {
                InteractWithPotion();
            }
        }
        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Space))
        {
            trashDelay -= Time.deltaTime;
            if ((potionCooked == true || potionCooking == true) && trashDelay < 0)
            {
                time += Time.deltaTime;
                trashProgress.fillAmount = time / Mathf.Max(trashTimer, float.Epsilon);
                if (time > trashTimer)
                {
                    trashProgress.fillAmount = 0f;
                    ResetElements();
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Space))
        {
            trashProgress.fillAmount = 0f;
            time = 0f;
        }
    }

    private void CheckPotion()
    {
        int oxygenCount = CurrentMix[oxygenIndex];
        int hydrogenCount = CurrentMix[hydrogenIndex];
        int FluorineCount = CurrentMix[FluorineIndex];
        int SulfurCount = CurrentMix[SulfurIndex];
        int NitrogenCount = CurrentMix[NitrogenIndex];

        if (oxygenCount == 2 && hydrogenCount == 0 && FluorineCount == 2 && SulfurCount == 0 && NitrogenCount == 0) // Dioxygen Difluoride
        {
            Output.text = "Dioxygen Difluoride";
            potionType = "Dioxygen Difluoride";
        }
        else if (SulfurCount == 1 && NitrogenCount ==  && oxygenCount == 4 && hydrogenCount == 0 && FluorineCount == 0) // Sulfate
        {
            Output.text = "Sulfate";
            potionType = "Sulfate";
        }
        else if (FluorineCount == 0 && hydrogenCount == 3 && oxygenCount == 6 && SulfurCount == 0 && NitrogenCount == 1) // Ammonia
        {
            Output.text = "Ammonia";
            potionType = "Ammonia";
        }
        else if (FluorineCount == 0 && hydrogenCount == 0 && NitrogenCount == 0 && oxygenCount == 2 && SulfurCount == 1) //Preservative
        {
            Output.text = "Preservative";
            potionType = "Preservative";
        }
        else if (FluorineCount ==  0 && hydrogenCount == 2 && oxygenCount == 4 && SulfurCount == 1 && NitrogenCount == 0) // Sulfuric Acid
        {
            Output.text = "Sulfuric Acid";
            potionType = "Sulfuric Acid";
        }
       
        else
        {
            Output.text = "Unknown";
            potionType = "Unknown";
        }
    }

    private void InteractWithPotion()
    {
        if (potionCooked == false && potionCooking == false)
        {
            if (CurrentMix[oxygenIndex] + CurrentMix[hydrogenIndex] + CurrentMix[FluorineIndex] + CurrentMix[SulfurIndex] + CurrentMix[NitrogenIndex] > 0)
            {
                potionCooking = true;
                StartCoroutine(PotionCooking(cookTime, cookMeter));
            }
        }
        else
        {
            for (int i = 0; i < customerManager.customerControllers.Count; i++)
            {
                if (potionType == customerManager.customerControllers[i].potionOrder)
                {
                    customerManager.customerControllers[i].ServeCustomer();
                    potionCooked = false;
                    potionType = "Empty";
                    StopAllCoroutines();
                    potionDisplay.fillAmount = 0;
                    burnMeter.fillAmount = 0;
                    ResetElements();
                    break;
                }
            }
        }

        if (customerManager.bossControllers.Count > 0)
        {
            for (int i = 0; i < customerManager.bossControllers[0].ordersRemaining.Count; i++)
            {
                if (customerManager.bossControllers[0].ordersRemaining[i].name == potionType)
                {
                    customerManager.bossControllers[0].ServeCustomer(potionType);
                    potionCooked = false;
                    potionType = "Empty";
                    StopAllCoroutines();
                    potionDisplay.fillAmount = 0;
                    burnMeter.fillAmount = 0;
                    ResetElements();
                    break;
                }
            }
        }
    }

    private void ExplodePotion()
    {
        AudioManager.Instance.PlaySFX("PotionExploding");
        selectionManager.remainingPotions.Remove(this.gameObject);
        Destroy(gameObject, 0.1f);
    }

    private void ResetElements()
    {
        for (int i = 0; i < CurrentMix.Length; i++)
        {
            CurrentMix[i] = 0;
        }
        potionCooked = false;
        potionCooking = false;
        potionType = "Empty";
        StopAllCoroutines();
        potionDisplay.fillAmount = 0;
        burnMeter.fillAmount = 0;
        cookMeter.fillAmount = 0;
        Output.text = "Empty";
    }

    private IEnumerator PotionCooking(float duration, Image fillDisplay)
    {
        AudioManager.Instance.PlaySFX("PotionBrewing");
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
            AudioManager.Instance.PlaySFX("PotionExploding");
            Destroy(gameObject, 0.2f);
        }
        yield return null;
    }
}
