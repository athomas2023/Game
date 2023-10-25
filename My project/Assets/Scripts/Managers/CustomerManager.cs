using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Spawning Info")]
    [SerializeField, Tooltip("Number of seconds between each customer spawn")] private float customerSpawnRate = 5f;
    [SerializeField, Tooltip("Maximum number of customers allowed at a time")] private int maxCustomers = 5;

    [Header("Object References")]
    [SerializeField] private List<GameObject> customers;    //List of all active customers
    [SerializeField] private Transform customerParent;  //Spawns all customers under this parent object
    private float timer;    //Temp timer for spawning customers
    public int totalCustomers = 0;  //Total number of customers active
    private Vector3 customerSpawnPos = Vector3.zero;
    public List<CustomerController> customerControllers;
    
    private void Update()
    {
        //Changes customer spawn location based on the current number of active customers
        if (totalCustomers > 0)
        {
            customerSpawnPos = new Vector3(2 * totalCustomers, 2, 0);
        }
        else
        {
            customerSpawnPos = Vector3.zero;
        }


        //Spawns a new customer every set period of time
        //Adds each customer to the customers list
        timer -= Time.deltaTime;
        if (timer < 0 && totalCustomers < maxCustomers)
        {
            int r = UnityEngine.Random.Range(1, 3);
            GameObject cc = Instantiate(customers[r - 1], customerSpawnPos, Quaternion.identity, customerParent);
            customerControllers.Add(cc.GetComponent<CustomerController>());
            totalCustomers++;
            timer = customerSpawnRate;
        }
    }
}
