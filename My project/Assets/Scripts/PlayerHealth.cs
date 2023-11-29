using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int healthPoints; // Initial health points
    public int StartHealthPoints =100;
    public GameObject displayHealthObject; // Reference to the TextMeshPro object
    
    private DecisionMangar decisionManager;
    private Load load;
    private int HealthPoints
    {
        get { return healthPoints; }
    }

    private void Start()
    {
        Debug.Log("Health Adjusted: " + healthPoints);
        // Load player's health from PlayerPrefs if it exists, or set to the initial value
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            healthPoints = PlayerPrefs.GetInt("PlayerHealth");
        }
        else
        {
            healthPoints = StartHealthPoints; // Set the initial health value
        }

        // Initialize the display text with the initial health value
        UpdateHealthDisplay();

         // Find the DecisionManager script globally
        decisionManager = FindObjectOfType<DecisionMangar>();
        load = FindObjectOfType<Load>();

    }

    private void Update()
    {
        // Autosave the player's health to PlayerPrefs in the Update function
        PlayerPrefs.SetInt("PlayerHealth", healthPoints);
        PlayerPrefs.Save();

        if (healthPoints == 0)
        {
            // Call the reset function in DecisionManager
            if (decisionManager != null)
            {
                decisionManager.ResetPlayerPrefs();
            }

        
                // Load the GameOverScene
              ResetPlayerPrefs();
               SceneManager.LoadScene("Scene4");
            
        }
    }

    public void AdjustPlayerHealth(int adjustment)
    {
        healthPoints += adjustment;
        // Ensure health doesn't go below 0 or exceed a maximum value (if needed)
        healthPoints = Mathf.Clamp(healthPoints, 0, 100); // Adjust the maximum value as necessary

        // Debug the player's health when an adjustment is made
        Debug.Log("Health Adjusted: " + healthPoints);

        // Update the health display
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (displayHealthObject != null)
        {
            TextMeshProUGUI healthText = displayHealthObject.GetComponent<TextMeshProUGUI>();
            if (healthText != null)
            {
                // Convert the health value to a string and update the TMP text
                healthText.text = "Health: " + healthPoints.ToString();
            }
        }
    }

    public void ResetPlayerPrefs()
    {
        // Reset the PlayerPrefs for health and save it
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.Save();

        healthPoints = 100;
        // Set health back to the initial value
        
        // Update the health display after resetting
        UpdateHealthDisplay();


         PlayerPrefs.SetInt("PlayerHealth", healthPoints);
        PlayerPrefs.Save();
    }
}

