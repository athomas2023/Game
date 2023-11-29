using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI targetOverwriteText;
    public string[] textEntries;
    public Sprite[] imageEntries; // Array to hold images corresponding to text entries
    public int maxEntries = 5;

    private int currentEntryIndex = 0;

    [Header("Choices")]
    public GameObject ButtonManager;

    [Header("Next Section")]
    private GameObject TextManager;

    public GameObject GreatPath;
    public GameObject GoodPath;
    public GameObject BadPath;

    public Image imageDisplay; // Reference to the Image component

    private bool InDialog;
    #endregion

    void Start()
    {
        DisplayCurrentEntry();
        ButtonManager.SetActive(false);
        InDialog = true;

        if (TextManager == null)
        {
            Debug.Log("Please set the 'TextManager' GameObject field in the inspector.");
        }
    }

    void Awake()
    {
        InDialog = true;
    }

    void Update()
    {
        // Check for spacebar input to advance to the next entry
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (InDialog == true) // Dialogue Entries
            {
                currentEntryIndex++;

                // Check if there are more entries to display
                if (currentEntryIndex < textEntries.Length)
                {
                    DisplayCurrentEntry();
                }
                else
                {
                    Debug.Log("No more entries");
                    ButtonManager.SetActive(true);
                    InDialog = false;
                }
            }
            else
            {
                if (TextManager != null)
                {
                    gameObject.SetActive(false);
                    TextManager.SetActive(true);
                }
                else
                {
                    Debug.Log("Please set the 'TextManager' GameObject field in the inspector.");
                }
            }
        }
    }

    void DisplayCurrentEntry()
    {
        if (currentEntryIndex < textEntries.Length)
        {
            // Set the TextMeshProUGUI text to the current entry
            if (targetOverwriteText != null)
            {
                targetOverwriteText.text = textEntries[currentEntryIndex];

                // Display the associated image
                if (imageDisplay != null && currentEntryIndex < imageEntries.Length)
                {
                    imageDisplay.sprite = imageEntries[currentEntryIndex];
                }
            }
        }
    }

    public void setGreat()
    {
        TextManager = GreatPath;
    }

    public void setGood()
    {
        TextManager = GoodPath;
    }

    public void setBad()
    {
        TextManager = BadPath;
    }
}
