using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    [Header("Customer Spawning Info")]
    [SerializeField, Tooltip("Number of seconds between each customer spawn")] private float customerSpawnRate = 5f;
    [SerializeField, Tooltip("Maximum number of customers allowed at a time")] private int maxCustomers = 5;

    [Header("Object References")]
    [SerializeField] private GameObject boss;
    [SerializeField] private List<GameObject> customers;    //List of all active customers
    [SerializeField] private Transform customerParent;  //Spawns all customers under this parent object
    private float timer;    //Temp timer for spawning customers
    public int totalCustomers = 0;  //Total number of customers active
    public int score = 0;
    private Vector3 customerSpawnPos = Vector3.zero;
    public List<CustomerController> customerControllers;
    public List<BossController> bossControllers;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    public HighScoreSO highScoreSO;
    public HighScoreSO gameScore1;
    public HighScoreSO gameScore2;
    [SerializeField] private TextMeshProUGUI gameTimerUI;
    [SerializeField] private GameObject gameTimerDisplay;
    [SerializeField] private float gameTimer;
    [SerializeField] private float highScoreRequiredToSpawnBoss;
    [SerializeField] private bool spawnBossAtEndOfLevel;
    private bool bossSpawned;

    private void Update()
    {
        if (gameTimer < 0 && bossSpawned == false)
        {
            if (score >= highScoreRequiredToSpawnBoss)
            {
                if (spawnBossAtEndOfLevel == true)
                {
                    SpawnBoss();
                }
                else
                {
                    SceneManager.LoadScene("Win Screen");
                }
            }
            else
            {
                SceneManager.LoadScene("Lose Screen");
            }
        }

        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            gameTimerUI.text = "Time: " + (int)gameTimer;
            //Changes customer spawn location based on the current number of active customers
            if (totalCustomers > 0)
            {
                customerSpawnPos = new Vector3(-6 + (2 * totalCustomers), 0, 0);
            }
            else
            {
                customerSpawnPos = new Vector3(-4, 0, 0);
            }

            //Spawns a new customer every set period of time
            //Adds each customer to the customers list
            timer -= Time.deltaTime;
            if (timer < 0 && totalCustomers < maxCustomers)
            {
                SpawnCustomer();
            }

            scoreText.text = "Score: " + score;
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                gameScore1.highScore = score;
            }
            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                gameScore2.highScore = score;
            }

            if (gameScore1.highScore + gameScore2.highScore > highScoreSO.highScore)
            {
                highScoreSO.highScore = gameScore1.highScore + gameScore2.highScore;
                highScoreText.text = "High Score: " + highScoreSO.highScore;
            }
        }
    }
    private void SpawnBoss()
    {
        gameTimerDisplay.SetActive(false);
        AudioManager.Instance.PlayMusic("BossMusic");
        bossSpawned = true;
        //Spawn boss
        foreach (CustomerController c in customerControllers)
        {
            Destroy(c.gameObject);
        }
        customerControllers.Clear();
        customerSpawnPos = Vector3.zero;
        GameObject b = Instantiate(boss, customerSpawnPos, Quaternion.identity, customerParent);
        bossControllers.Add(b.GetComponent<BossController>());
    }

    private void SpawnCustomer()
    {
        int r = UnityEngine.Random.Range(1, customers.Count + 1);
        GameObject cc = Instantiate(customers[r - 1], customerSpawnPos, Quaternion.identity, customerParent);
        customerControllers.Add(cc.GetComponent<CustomerController>());
        totalCustomers++;
        timer = customerSpawnRate;
    }
}


