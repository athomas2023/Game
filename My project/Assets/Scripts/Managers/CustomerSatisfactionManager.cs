using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerSatisfactionManager : MonoBehaviour
{
    public static CustomerSatisfactionManager Instance;
    public float customerSatisfaction = 1;
    [SerializeField] private Slider customerSatisfactionMeter;
    [SerializeField] private Image customerSatisfactionMeterFill;
    [SerializeField] private string loseScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        customerSatisfactionMeter.value = customerSatisfaction;
        if (customerSatisfaction >= 0.75)
        {
            customerSatisfactionMeterFill.color = Color.green;
        }
        else if (customerSatisfaction < 0.75 && customerSatisfaction > 0.25f)
        {
            customerSatisfactionMeterFill.color = Color.yellow;
        }
        else if (customerSatisfaction <= 0.25f)
        {
            customerSatisfactionMeterFill.color = Color.red;
        }
        if (customerSatisfaction <= 0)
        {
            SceneManager.LoadScene(loseScene);
        }
    }
}
