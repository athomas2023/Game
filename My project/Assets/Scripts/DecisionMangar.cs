using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DecisionMangar : MonoBehaviour
{
    [Header("Text Manager")]
    public bool LastScript = false;
    public GameObject NextSceneButton;
    [Header("Varibales")]
     #region Fields
    public TextMeshProUGUI ChoiceMeter;

    private float choiceVar = 0f;
    private string choiceDisplay;

    public TextMeshProUGUI OverWriteText;
    #region  Choices
      [Header("Choices")]
    public string GreatEnding = "";
    public float GreatPoints = 1.0f;
    public string GoodEnding = "";
    public float GoodPoints = .5f;
    public string BadEnding = "";
    public float Badpoints = -1.0f;
    #endregion

    public GameObject Buttonrow;
    #region Health
    [Header("Health Points")]
    public int IncreaseHealth = 1;
    public int Decreasehealth =-1;
    public int NaturalHealth = 0; 
    #endregion

     private PlayerHealth playerHealthScript; // Reference to the PlayerHealth script
     private DialogManager dialogManager;

    
    #endregion 
    
    private void Start()
    {

      // Load choiceVar from PlayerPrefs if it exists, otherwise, initialize it to 0.
        if (PlayerPrefs.HasKey("ChoiceVar"))
        {
            choiceVar = PlayerPrefs.GetFloat("ChoiceVar");
        }
        else
        {
            choiceVar = 0.0f;
        }

        if(LastScript== false)
        {
          NextSceneButton.SetActive(false);
        }

        choiceDisplay = choiceVar.ToString();
        ChoiceMeter.text = choiceDisplay;

        // Find the PlayerHealth script globally
        playerHealthScript = FindObjectOfType<PlayerHealth>();
        dialogManager = FindObjectOfType<DialogManager>();

    }


  private void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPlayerPrefs();
            playerHealthScript.ResetPlayerPrefs();

            // Reset the choiceVar and update the display
            choiceVar = 0.0f;
            // Quit the application or stop play mode in the editor
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Close play mode in the editor
            #else
            Application.Quit(); // Quit the application
            #endif

           
        }
    }

   public void Great()
   {
        choiceVar += GreatPoints;
        choiceDisplay = choiceVar.ToString();
        ChoiceMeter.text = choiceDisplay;

        OverWriteText.text = GreatEnding;

         // Save choiceVar
    PlayerPrefs.SetFloat("ChoiceVar", choiceVar);
    PlayerPrefs.Save();
    playerHealthScript.AdjustPlayerHealth(IncreaseHealth);

    dialogManager.setGreat();

     if(LastScript == true)
      {
        NextSceneButton.SetActive(true);
      }  
       
      Buttonrow.SetActive(false);

     

   }

   public void Good()
   {

        choiceVar += GoodPoints;
        choiceDisplay = choiceVar.ToString();
        ChoiceMeter.text = choiceDisplay;

        OverWriteText.text = GoodEnding;

         // Save choiceVar
    PlayerPrefs.SetFloat("ChoiceVar", choiceVar);
    PlayerPrefs.Save();

    playerHealthScript.AdjustPlayerHealth(NaturalHealth);
    dialogManager.setGood();
    if(LastScript == true)
      {
        NextSceneButton.SetActive(true);
      } 

    Buttonrow.SetActive(false);
   }

   public void Bad()
   {
        choiceVar -= Badpoints;
        choiceDisplay = choiceVar.ToString();
        ChoiceMeter.text = choiceDisplay;

        OverWriteText.text = BadEnding;

         // Save choiceVar
    PlayerPrefs.SetFloat("ChoiceVar", choiceVar);
    PlayerPrefs.Save();

    playerHealthScript.AdjustPlayerHealth(Decreasehealth);
    dialogManager.setBad();
    if(LastScript == true)
      {
        NextSceneButton.SetActive(true);
      } 

    Buttonrow.SetActive(false);

   }


    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("ChoiceVar");
        PlayerPrefs.Save();

        choiceVar = 0.0f;
    }
}
