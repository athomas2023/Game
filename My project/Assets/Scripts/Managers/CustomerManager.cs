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
    public HighScoreSO gameScore;
    [SerializeField] private TextMeshProUGUI gameTimerUI;
    [SerializeField] private GameObject gameTimerDisplay;
    [SerializeField] private float gameTimer;
    [SerializeField] private float highScoreRequiredToSpawnBoss;
    private bool bossSpawned;

    private void Start()
    {

    }
    private void Update()
    {
        if (gameTimer < 0 && bossSpawned == false)
        {
            if (gameScore.highScore >= highScoreRequiredToSpawnBoss)
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
            else
            {
                Debug.Log("You lose");
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
                int r = UnityEngine.Random.Range(1, customers.Count + 1);
                GameObject cc = Instantiate(customers[r - 1], customerSpawnPos, Quaternion.identity, customerParent);
                customerControllers.Add(cc.GetComponent<CustomerController>());
                totalCustomers++;
                timer = customerSpawnRate;
            }

            scoreText.text = "Score: " + score;
            gameScore.highScore = score;

            if (score > highScoreSO.highScore)
            {
                highScoreSO.highScore = score;
                highScoreText.text = "High Score: " + highScoreSO.highScore;
            }
        }
    }
}
