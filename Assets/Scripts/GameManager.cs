using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int score = 0;
    public static int playerHealth = 3;
    public static int blast = 0;
    public static int slowMotion = 0;
    public static float slowMotionDuration = 3f;
    public static float slowMotionFactor = .1f;
    public static bool gameStarted;
    public static bool playerDead;
    public static int highScore = 0;
    public static bool hasNewHighScore = false;
    public static bool restart;
    public static float volume = 1f;
    public Text scoreText;
    public GameObject inGameCanvas;
    public Image life1, life2, life3;
    public static bool playInterstitialAd = false;
    public static bool elligibleForReward = true;
    public static bool giveRewardChance = false;
    public static bool playRewardAd = false;

    void Start () {
        gameStarted = false;
	}
	
	void Update () {
        
        //score updated OnMouseDown() of EnemyScript

        //health updated OnCollisionEnter() of PlayerScript;
        CountScore();
        CheckPlayerDead();
        HealthUpdater();
        AudioListener.volume = volume;
        

        if (gameStarted)
        {
            inGameCanvas.SetActive(true);
        }
        if (restart)
        {
            gameStarted = true;
        }
    }


    public static void Restart(bool re)
    {
        playInterstitialAd = true;
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        restart = re;
        playerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        blast = 0;
        slowMotion = 0;
        playerHealth = 3;
        Debug.Log("Scene Restarted");
        score = 0;
    }

    public static void Replay()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        playerDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerDead = false;
        playerHealth = 1;
    }

    void CheckPlayerDead()
    {
        if (playerDead)
        {
            if (!PlayerPrefs.HasKey("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                hasNewHighScore = true;
            }
            else if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                hasNewHighScore = true;
            }
            else
            {
                hasNewHighScore = false;
            }
        }
    }

    void CountScore()
    {
        scoreText.text = score.ToString();
       
    }


    

    

    public void HealthUpdater()
    {
        if(playerHealth == 3)
        {
            life1.color = Color.white;
            life2.color = Color.white;
            life3.color = Color.white;
        }
        if (playerHealth == 2)
        {
            life1.color = Color.white;
            life2.color = Color.white;
            life3.color = Color.red;
        }
        if (playerHealth == 1)
        {
            life1.color = Color.white;
            life2.color = Color.red;
            life3.color = Color.red;
        }
        if (playerHealth == 0)
        {
            life1.color = Color.red;
            life2.color = Color.red;
            life3.color = Color.red;
        }
    }
}
