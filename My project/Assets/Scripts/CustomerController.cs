using System.Collections;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [Header("Customer Info")]
    [SerializeField, Tooltip ("Determines how long until the customer gets upset and leaves")] private float patienceTimer;
    [SerializeField, Tooltip ("How many points the base potion is worth")] private int potionScoreValue;

    [Header("Object References")]
    [SerializeField] private UnityEngine.UI.Image patienceMeter;
    private float patienceTimerRemaining;
    private CustomerManager customerManager;
    protected bool orderCompleted;  //Keeps track of if an order was completed
    public int customerNumber;  //Keeps track of the order of customers, customer 1 is the active customer
    public int potionOrder;
    

    private void Start()
    {
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
        //Then destroys the customer
        customerManager.totalCustomers--;
        customerManager.customerControllers.Remove(this);
        foreach (CustomerController c in customerManager.customerControllers)
        {
            c.customerNumber--;
        }
        
        Destroy(gameObject);
    }

    public void ServeCustomer()
    {
        //Reduces total customer count and removes customer from list
        //Gives points based on customer potionScoreValue and patience timer remaining
        //Increasing the score over time would be cool
        customerManager.totalCustomers--;
        customerManager.customerControllers.Remove(this);
        customerManager.score += Mathf.FloorToInt(patienceTimerRemaining) + potionScoreValue;
        //Debug.Log("You had " + Mathf.FloorToInt(patienceTimerRemaining) + " seconds remaining");
        foreach (CustomerController c in customerManager.customerControllers)
        {
            c.customerNumber = Mathf.Clamp(customerManager.customerControllers.IndexOf(c) + 1, 1, 5);
        }

        Destroy(gameObject);
    }

    //Create a method that will serve the selected potion to the customer that wants it with the lowest patience timer
    //Will need to have a way to accept input of the selected potion
    //Every customer will need to keep track of what potion they want
}
