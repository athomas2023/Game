using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    [Header("Customer Info")]
    [SerializeField, Tooltip ("Determines how long until the customer gets upset and leaves")] private float patienceTimer;

    [Header("Object References")]
    [SerializeField] private UnityEngine.UI.Image patienceMeter;
    private CustomerManager customerManager;
    protected bool orderCompleted;  //Keeps track of if an order was completed
    public int customerNumber;  //Keeps track of the order of customers, customer 1 is the active customer

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
            transform.position = Vector3.zero;
        }
        else
        {
            transform.position = new Vector3((2 * customerNumber) - 2, 2, 0);
        }
    }

    private IEnumerator CustomerPatienceCountdown()
    {
        //Slowly reduces patience timer and adjusts fill based on remaining time
        //Sets color of patience timer at certain thresholds
        float t = patienceTimer;
        while (t > 0)
        {
            t -= Time.deltaTime;
            patienceMeter.fillAmount -= 1.0f / patienceTimer * Time.deltaTime;
            if (t > 12)
            {
                patienceMeter.color = Color.green;
            }
            else if (t < 5)
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


}
