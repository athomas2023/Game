using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    [Header("Customer Info")]
    [SerializeField, Tooltip("Determines how long until the customer gets upset and leaves")] private float patienceTimer;
    [SerializeField, Tooltip("How many points the base potion is worth")] private int potionScoreValue;


    [Header("Object References")]
    [SerializeField] private UnityEngine.UI.Image patienceMeter;
    [SerializeField] private List<Sprite> customerImages;
    [SerializeField] private List<Sprite> orderImages;
    [SerializeField] private SpriteRenderer customerImage;
    [SerializeField] private UnityEngine.UI.Image orderImage;
    [SerializeField] private TextMeshPro orderText;
    private float patienceTimerRemaining;
    private CustomerManager customerManager;
    protected bool orderCompleted;  //Keeps track of if an order was completed
    public int customerNumber;  //Keeps track of the order of customers, customer 1 is the active customer
    public string potionOrder;


    private void Start()
    {
        customerImage.sprite = customerImages[Random.Range(0, customerImages.Count)];
        int order = Random.Range(0, orderImages.Count);
        orderImage.sprite = orderImages[order];
        potionOrder = orderImages[order].name;
        orderText.text = orderImages[order].name;
        customerManager = GameObject.FindGameObjectWithTag("CustomerManager").GetComponent<CustomerManager>();
        customerNumber = customerManager.totalCustomers;
        StartCoroutine(CustomerPatienceCountdown());
    }

    private void Update()
    {
        //Updates customer position based on their customer number
        if (customerNumber == 1)
        {
            transform.position = new Vector3(-4, 0, 0);
        }
        else
        {
            transform.position = new Vector3(-6 + (2 * customerNumber), 0, 0);
        }
    }

    private IEnumerator CustomerPatienceCountdown()
    {
        //Slowly reduces patience timer and adjusts fill based on remaining time
        //Sets color of patience timer at certain thresholds
        patienceTimerRemaining = patienceTimer;
        while (patienceTimerRemaining > 0)
        {
            patienceTimerRemaining -= Time.deltaTime;
            patienceMeter.fillAmount -= 1.0f / patienceTimer * Time.deltaTime;
            if (patienceMeter.fillAmount > 0.66)
            {
                patienceMeter.color = Color.green;
            }
            else if (patienceMeter.fillAmount < 0.33)
            {
                patienceMeter.color = Color.red;
            }
            else
            {
                patienceMeter.color = Color.yellow;
            }
            yield return null;
        }
        CustomerPatienceExpires();
        yield return null;
    }


    //These two methods could probably be combined into one because they have so much overlap
    private void CustomerPatienceExpires()
    {
        //When customer patience expires, removes them from the list of customers, reduces total customer count, lowers all customer numbers by 1
        //Decreases customer satisfaction by a value
        //Then destroys the customer
        customerManager.totalCustomers--;
        customerManager.customerControllers.Remove(this);
        foreach (CustomerController c in customerManager.customerControllers)
        {
            c.customerNumber = Mathf.Clamp(customerManager.customerControllers.IndexOf(c) + 1, 1, 5);
        }
        CustomerSatisfactionManager.Instance.customerSatisfaction = Mathf.Clamp(CustomerSatisfactionManager.Instance.customerSatisfaction - 0.25f, 0, 1);
        Destroy(gameObject);
    }

    public void ServeCustomer()
    {
        //Reduces total customer count and removes customer from list
        //Gives points based on customer potionScoreValue and patience timer remaining
        //Increasing the score over time would be cool
        //Increases customer satisfaction by a value
        customerManager.totalCustomers--;
        customerManager.customerControllers.Remove(this);
        customerManager.score += Mathf.FloorToInt(patienceTimerRemaining) + potionScoreValue;
        foreach (CustomerController c in customerManager.customerControllers)
        {
            c.customerNumber = Mathf.Clamp(customerManager.customerControllers.IndexOf(c) + 1, 1, 5);
        }
        CustomerSatisfactionManager.Instance.customerSatisfaction = Mathf.Clamp(CustomerSatisfactionManager.Instance.customerSatisfaction + 0.1f, 0, 1);
        Destroy(gameObject);
    }

}
