using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [SerializeField] private float patienceTimer;
    private float patienceTimerRemaining;
    [SerializeField] private UnityEngine.UI.Image patienceMeter;
    public List<GameObject> ordersRemaining;
    protected bool orderCompleted;

    void Start()
    {
        StartCoroutine(CustomerPatienceCountdown());
    }

    void Update()
    {
        if (ordersRemaining.Count <= 0)
        {
            SceneManager.LoadScene("Win Screen");
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

    private void CustomerPatienceExpires()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void ServeCustomer(string orderName)
    {
        for (int i = 0; i < ordersRemaining.Count; i++)
        {
            if (ordersRemaining[i].name == orderName)
            {
                Destroy(ordersRemaining[i].gameObject);
                ordersRemaining.Remove(ordersRemaining[i].gameObject);
                break;
            }
        }
    }
}
