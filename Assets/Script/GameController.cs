using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


    public enum GameState
    {
        Menu,
        Tutorial,
        Settings,
        Playing,                           // gör så att den inte ser ut som att vora gömd
        Paused,
        LifeLost,
        CountDownToStart,
        CountDownToEnd,
        GameOver
    }
    public SoundController soundController;
    public GameObject player;
    public GameObject hud;
    public GameObject scoreBoard;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public Text menuBestScoreText;
    public Text countdownText;
    public Text speedText;
    public Text scoreText;
    public Text DistanceText;
    public Text TutorialText;

    public Text[] EndScoreText;
    public Text TotalScoreText;

    //UI
    public GameObject TutorialContainer;
    public GameObject TiltPicContainer;
    public GameObject countdownTextContainer;
    public GameObject CreditPage;
    public PlatformGenerator Generator;
    public PlayerController PlayerInformation;
    List<GameObject> CollectionOfGameObject;
    private GameObject Warp;

    [HideInInspector]
    public static GameState gameState = GameState.Menu;
    // Score And Numbers//
    public int score;
    private int highScore;
    private float enemySpawnTimer;
    private float countdownToStartTimer;
    private float countdownToEndTimer;
    private float respawnTimerStep = 1f;
    private float countdown;
    private int _TotalScore;

    // other sub classes

    public AudioSource ExplosionMusic;
    public AudioSource GoldCollected;
    public AudioSource ButtonSoundEffect;
    PlatformDestroyer[] platformdestroyer;

    bool showads = false;
    // adsManager
    bool hasShownAdOneTime;
    AdsManager ads;
    public int WhenToShowAds;
    GoogleplayController googleplayservice;
    
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        ads = FindObjectOfType<AdsManager>();
        googleplayservice = FindObjectOfType<GoogleplayController>();
        Warp = GameObject.Find("WarpSpeedz");
        Warp.SetActive(false);
        CollectionOfGameObject = new List<GameObject>();
        // Set application settings
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Set high score and show on main menu
        if (!PlayerPrefs.HasKey(Strings.HighScore))
        {
            PlayerPrefs.SetInt(Strings.HighScore, 0);
        }
        highScore = PlayerPrefs.GetInt(Strings.HighScore);
        menuBestScoreText.text = PlayerPrefs.GetInt(Strings.HighScore).ToString();
        hud.SetActive(false);
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                break;
            case GameState.Playing:
               
                if(PlayerInformation == null)
                {
                    PlayerInformation = FindObjectOfType<PlayerController>();
                }
                UpdateSpeed();
                UpdateMiles();
                 _TotalScore = TotalScoreCalculation();

                TotalScoreText.text = _TotalScore.ToString("0");
                //if (PlayerInformation.walkingspeed >= 3)
                //{
                //    Warp.SetActive(true);
                //}

                
                break;
            case GameState.Paused:
               
                if (Input.GetKeyDown("space"))
                {
                    ToggleGame();
                }
                break;

            case GameState.CountDownToStart:

                countdownToStartTimer += Time.deltaTime * Constants.CountDownMultiplier;
                TutorialText.text = "Press Right or Left to Control.";

                if (countdownToStartTimer >= respawnTimerStep)
                {
                    countdownToStartTimer = 0;
                    countdown--;
                    countdownText.text = (countdown > 0) ? countdown.ToString() : "GO!";

                    if (countdown == -1)
                    {
                        TutorialContainer.SetActive(false);
                        TiltPicContainer.SetActive(false);
                        countdownTextContainer.SetActive(false);
                        countdownText.text = "3";
                        gameState = GameState.Playing;
                        CreatePlayer();
                    }
                }
                break;
            case GameState.CountDownToEnd:
                countdownToEndTimer += Time.deltaTime;
                if (countdownToEndTimer >= Constants.CountDownToEndTimeout)
                {
                    FinalizeGame();
                }
                break;
        }
    }

    public void CreatePlayer()
    {
        GameObject po =Instantiate(player, new Vector3(0, -5f, 0), Quaternion.identity) as GameObject;
        CollectionOfGameObject.Add(po);
    }

    public void DestroyAllObject()
    {
        for (int i = 0; i < CollectionOfGameObject.Count; i++)
        {
            GameObject.Destroy(CollectionOfGameObject[i]);
        }
    }
    public void RespawnPlayer()
    {
        gameState = GameState.Playing;
        UpdateMiles();
        UpdateScore();
        UpdateSpeed();
       
    }
    public void IncrementScore()
    {
        GoldCollected.Play();
        score+=10;


        switch (score)
        {
            case 10:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_this_is_my_first_time_she_said);
                break;
            case 100:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_my_first_100_points);
                break;

            case 151:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_collect_em_all);
                break;

            case 200:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_im_one_of_two_hundred);
                break;

            case 350:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_im_just_trying_to_survive_as_a_millenial);
                break;

            case 500:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_geezus_i_think_i_stop_giving_you_more_rewards_wait_until_next_patch);
                break;

            case 650:
                googleplayservice.UnlockAchievement(GooglePlayAchievement.achievement_plus_ultra);
                break;

            default:
                break;
        }
        UpdateScore();
    }

    public void ConfirmPlayerKill()
    {
        ExplosionMusic.Play();
        gameState = GameState.LifeLost;
        Warp.SetActive(false);
        EndGame();

        
    }
    public void ToggleGame()
    {
        if (gameState == GameState.Playing)
        {
            soundController.DoMusic(SoundController.SoundAction.Pause);
            gameState = GameState.Paused;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

        }
        else if (gameState == GameState.Paused)
        {
            soundController.DoMusic(SoundController.SoundAction.Play);
            gameState = GameState.Playing;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void StartGame()
    {
        //if (!showads)
        //{
        //    showads = true;
        //    ads.RequestBanner();
        //}
        Generator.everytime = 80;
        Generator.transform.position = Generator.PlatformStartPosition;
        ButtonSoundEffect.Play();
        Generator.enabled = true;
        PlayerInformation = FindObjectOfType<PlayerController>();
        gameState = GameState.CountDownToStart;
        score = Constants.ZeroDefault;
        countdown = Constants.CountDownDefault;
        countdownToStartTimer = Constants.ZeroDefault;
        countdownToEndTimer = Constants.ZeroDefault;
        countdownTextContainer.SetActive(true);
        TutorialContainer.SetActive(true);
        TiltPicContainer.SetActive(true);
        hud.SetActive(true);
        mainMenu.SetActive(false);
        scoreBoard.SetActive(false);
        pauseMenu.SetActive(false);
        DistanceText.text = "0";
        scoreText.text = "0";
        Camera.main.transform.position = new Vector3(0, 0, -10);  // Origin Position so i repeat the step back
    }
    private void EndGame()
    {
        gameState = GameState.CountDownToEnd;
        if (_TotalScore > highScore)
        {
            highScore = _TotalScore;
         
            PlayerPrefs.SetInt(Strings.HighScore, highScore);
            PlayerPrefs.SetInt("Score", _TotalScore);
            googleplayservice.Reportscore(highScore);
        }
        hud.SetActive(false);
        //TutorialContainer.SetActive(true);
    }

    private int TotalScoreCalculation()
    {
        int total = (int)PlayerInformation.transform.position.y + score;
        total *= (int)PlayerInformation.walkingspeed;
       return total;
    }
    private void FinalizeGame()
    {
        countdownToEndTimer = Constants.ZeroDefault;
        EndScoreText[0].text = score.ToString();
        EndScoreText[1].text = PlayerInformation.transform.position.y.ToString("0.0");
        EndScoreText[2].text = speedText.text;
        
        //WhenToShowAds++;

        //if(WhenToShowAds>3)
        //{
        //    gameState = GameState.GameOver;
        //    WhenToShowAds = 0;
        //}
        //ads.hasShownAdOneTime = false;
        /*ads.RequestInterstitialAds();*/ // ask for new interstitialads
        scoreBoard.SetActive(true);
    }
    public void RestartGame()
    {
        platformdestroyer = FindObjectsOfType<PlatformDestroyer>();
        for(int i = 0; i< platformdestroyer.Length; i++)
        {
            platformdestroyer[i].gameObject.SetActive(false);
        }
        //player.SetActive(true);
        ButtonSoundEffect.Play();
        EndGame();
        Generator.transform.position = Generator.PlatformStartPosition;
        Generator.everytime = 80;
        
        StartGame();
        //soundController.DoMusic(SoundController.SoundAction.Play); // i dont want to repeat music when im restarting

    }
    public void QuitGame()
    {

        ButtonSoundEffect.Play();
        platformdestroyer = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformdestroyer.Length; i++)
        {
            platformdestroyer[i].gameObject.SetActive(false);
        }
        mainMenu.SetActive(true);
        scoreBoard.SetActive(false);
        hud.SetActive(false);
        gameState = GameState.Menu;
        soundController.DoMusic(SoundController.SoundAction.Play);

    }

    public void CreditPageOpen()
    {
        if (!CreditPage.activeInHierarchy)
        {
            CreditPage.SetActive(true);
        }
        else
        {
            CreditPage.SetActive(false);
        }
    }
    public void MuteGame()
    {
        if(AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {

            AudioListener.volume = 0;
        }
    }
    
    public void RateButton()
    {
        Application.OpenURL("market://details?id=com.OneArmy113.NeonDash#details-reviews");
    }
    private Vector2 ScreenToWorld(Vector3 position)
    {
        Vector3 worldCoordinate = new Vector3(position.x, position.y, 10);
        return Camera.main.ScreenToWorldPoint(worldCoordinate);
    }
    private void UpdateSpeed()
    {
        speedText.text = PlayerInformation.walkingspeed.ToString("0");
    }
    private void UpdateMiles()
    {
        DistanceText.text = PlayerInformation.transform.position.y.ToString("0.0");
    }
    private void UpdateScore()
    {
            scoreText.text = score.ToString("0");
    }
}


