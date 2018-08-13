using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject ingameCanvas;
    public GameObject menugo;
    public GameObject highscorego;
    public GameObject aboutgo;
    public GameObject settingsgo;
    public GameObject exitgo;
    public GameObject deadgo;
    public GameObject tutgo;
    public GameObject touchDestroyEffect;
    public GameObject deadAdgo;

    private Animator menuAnim;
    private Animator highscoreAnim;
    private Animator aboutAnim;
    private Animator settingsAnim;
    private Animator exitAnim;
    private Animator deadAnim;
    private Animator tutAnim;
    private Animator deadAdAnim;

    public Button playButton;
    public Button highScoreButton;
    public Button aboutButton;
    public Button settingsButton;
    public Button backButtonFromHighScore;
    public Button backButtonFromAbout;
    public Button backButtonFromSettings;
    public Button noButtonFromExit;
    public Button yesButtonFromExit;
    public Button exitButton;
    public Button restartButton;
    public Button menuButton;
    public Button tutorialButton;
    public Text gameOverText;
    public Text viewHighScoreText;
    public Slider volumeSlider;
    public Button tutorialDone;
    public Button blast;
    public Button slow;
    public Text blastAmt;
    public Text slowAmt;
    public Button deadAdYesButton;
    public Button deadAdNoButton;



    public void StartGame()
    {
        GameManager.gameStarted = true;
        menuAnim.Play("Popup Out");
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
    }

    public void CheckBlastSlowAmt()
    {
        blastAmt.text = GameManager.blast.ToString();
        slowAmt.text = GameManager.slowMotion.ToString();
    }

    public void HighScoreOpen()
    {
        
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        highscorego.SetActive(true);
        highscoreAnim.Play("Popup In");
        viewHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }

    public void HighScoreClose()
    {
        FindObjectOfType<AudioManager>().Play("swoosh");
        FindObjectOfType<AudioManager>().Play("buttonclick");
        highscoreAnim.Play("Popup Out");
    }

    public void AboutOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        aboutgo.SetActive(true);
        aboutAnim.Play("Popup In");
    }

    public void AboutClose()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        aboutAnim.Play("Popup Out");
    }

    public void SettingsOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        settingsgo.SetActive(true);
        settingsAnim.Play("Popup In");
    }

    public void SettingsClose()
    {
       
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        settingsAnim.Play("Popup Out");
    }

    public void ExitOpen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        exitgo.SetActive(true);
        Debug.Log("Exit clicked");
        exitAnim.Play("Popup In");
    }

    public void ExitNo()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        exitAnim.Play("Popup Out");
    }

    public void ExitYes()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        Application.Quit();
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        //
        deadAnim.Play("Popup Out");
        GameManager.playerDead = false;
        GameManager.Restart(true);
    }

    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        //
        deadAnim.Play("Popup Out");
        menugo.SetActive(true);
        menuAnim.Play("Popup In");
        ingameCanvas.SetActive(false);
        GameManager.playerDead = false;
        GameManager.Restart(false);
        GameManager.giveRewardChance = false;
        GameManager.elligibleForReward = true;

    }

    public void TutDone()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        tutAnim.Play("Popup Out");
    }

    public void TutOpen()
    {
        tutAnim.Play("Popup In");
        tutgo.SetActive(true);
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
    }

    
    void Start () {
        
        //PlayerPrefs.DeleteAll();
        menuAnim = menugo.gameObject.GetComponent<Animator>();
        highscoreAnim = highscorego.gameObject.GetComponent<Animator>();
        aboutAnim = aboutgo.gameObject.GetComponent<Animator>();
        settingsAnim = settingsgo.gameObject.GetComponent<Animator>();
        exitAnim = exitgo.gameObject.GetComponent<Animator>();
        deadAnim = deadgo.gameObject.GetComponent<Animator>();
        tutAnim = tutgo.gameObject.GetComponent<Animator>();
        deadAdAnim = deadAdgo.gameObject.GetComponent<Animator>();

        playButton.onClick.AddListener(StartGame);
        highScoreButton.onClick.AddListener(HighScoreOpen);
        backButtonFromHighScore.onClick.AddListener(HighScoreClose);
        aboutButton.onClick.AddListener(AboutOpen);
        backButtonFromAbout.onClick.AddListener(AboutClose);
        settingsButton.onClick.AddListener(SettingsOpen);
        backButtonFromSettings.onClick.AddListener(SettingsClose);
        exitButton.onClick.AddListener(ExitOpen);
        noButtonFromExit.onClick.AddListener(ExitNo);
        yesButtonFromExit.onClick.AddListener(ExitYes);
        restartButton.onClick.AddListener(Menu);
        menuButton.onClick.AddListener(Menu);
        tutorialDone.onClick.AddListener(TutDone);
        tutorialButton.onClick.AddListener(TutOpen);
        blast.onClick.AddListener(BlastCheck);
        slow.onClick.AddListener(SlowMotionCheck);
        deadAdYesButton.onClick.AddListener(DeadAdYesButton);
        deadAdNoButton.onClick.AddListener(DeadAdNoButton);

    }
	


    void BlastCheck()
    {
        if (GameManager.blast != 0) Blast();
    }
    void SlowMotionCheck()
    {
        if (GameManager.slowMotion != 0) StartCoroutine(SlowMotion());
    }

    void Blast()
    {
        GameManager.blast--;
            //Blast Power Up
            GameObject[] listOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in listOfEnemies)
            {
                FindObjectOfType<AudioManager>().Play("whack");
                Instantiate(touchDestroyEffect, go.transform.position, Quaternion.identity);
                go.SetActive(false);
            }
        
    }

    IEnumerator SlowMotion()
    {
        FindObjectOfType<AudioManager>().Play("timeslow");
        GameManager.slowMotion--;
        Time.timeScale = GameManager.slowMotionFactor;
        yield return new WaitForSecondsRealtime(GameManager.slowMotionDuration);
        Time.timeScale = 1f;
    }

    void DeathScreen()
    {
        deadgo.SetActive(true);
        //ingameCanvas.SetActive(false);
        deadAnim.Play("Popup In");
        if (GameManager.hasNewHighScore)
        {
            gameOverText.text = "New High Score: " + GameManager.score;
        }
        else
        {
            gameOverText.text = "Score: " + GameManager.score;
        }
    }

    void DeadAdScreen()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        GameManager.playerDead = false;
        deadAdgo.SetActive(true);
        deadAdAnim.Play("Popup In");
    }

    void DeadAdYesButton()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        deadAdAnim.Play("Popup Out");
        //Show rewarded ad
        GameManager.playRewardAd = true;
    }

    void DeadAdNoButton()
    {
        FindObjectOfType<AudioManager>().Play("buttonclick");
        FindObjectOfType<AudioManager>().Play("swoosh");
        deadAdAnim.Play("Popup Out");
        GameManager.playerDead = true;
    }

    void Update () {

        if (GameManager.giveRewardChance)
        {
            GameManager.giveRewardChance = false;
            Invoke("DeadAdScreen", 2f);
        }

        if (GameManager.playerDead)
        {
            Invoke("DeathScreen", 2f);
        }

        if (GameManager.restart)
        {
            menugo.SetActive(false);
        }
        CheckBlastSlowAmt();
        GameManager.volume = volumeSlider.value;
        

    }
}
