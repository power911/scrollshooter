using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Steamworks;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    [SerializeField] private Text _scoreText;
    public int Score;
    public Slider SliderHp;
    [SerializeField] private int _money;
    [SerializeField] private Text _moneyText;
    public GameObject[] Coins;
    public Transform[] WarpsForBoss;
    public Slider BossHPSllider;
    [SerializeField] private Text _bossText;
    [SerializeField] private Image _bGBossName;
    [SerializeField] private GameObject _youWin;
    public Image BgGameOver;
    public GameObject PauseObj;
    public Canvas _canvas;
    [SerializeField] private GameObject _mobile;
    private void Awake()
    {
        if (!Instance){Instance = this;}
        else{DestroyImmediate(this.gameObject);}
    }

    private void Start()
    {
        MusicManager.Instance.StartCoroutine(MusicManager.Instance.PlayMusic());
#if UNITY_ANDROID && !UNITY_EDITOR
       _mobile.gameObject.SetActive(true);
#else
        _mobile.gameObject.SetActive(false);
#endif
    }

    public void ScoreTextUpdate(int scoreBonus)
    {
        Score += scoreBonus;
        _scoreText.text = Score.ToString();
        if (Score >= 100000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_6");
        }
        if (Score >= 200000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_7");
        }
        if (Score >= 300000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_8");
        }
        if (Score >= 400000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_9");
        }
        if (Score >= 500000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_10");
        }
        if (Score >= 600000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_11");
        }
        if (Score >= 700000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_12");
        }
        if (Score >= 800000)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_13");
        }
        if (Score >= 999999)
        {
            SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_14");
        }
        SteamUserStats.StoreStats(); 
    }

    public void GetMoney(int moneyBonus)
    {
        _money += moneyBonus;
        _moneyText.text = _money.ToString() + "x";
        MoneyManager.Instance.Coin += moneyBonus;
    }
	
    public void SpawnBoss(string bossName)
    {
        _bossText.gameObject.SetActive(true);
        _bGBossName.gameObject.SetActive(true);
        _bossText.text = bossName;
        StartCoroutine(BossNameColor());
    }

    IEnumerator BossNameColor()
    {
        for (int i = 0; i < 5; i++)
        {
            _bossText.color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(0.15f);
            _bossText.color = new Color32(200, 0, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _bossText.color = new Color32(50, 0, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _bossText.color = new Color32(0, 255, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _bossText.color = new Color32(0, 255, 0, 255);
            yield return new WaitForSeconds(0.15f);
            _bossText.color = new Color32(255, 255, 0, 255);
            yield return new WaitForSeconds(0.15f);
        }
        _bossText.gameObject.SetActive(false);
        _bGBossName.gameObject.SetActive(false);
        yield return null;
    }

    public IEnumerator BossDied()
    {
        StartCoroutine(BackGroundColor());
        _youWin.SetActive(true);
        yield return new WaitForSeconds(3f);
        _youWin.SetActive(false);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    public void Pause(int index)
    {
        switch (index)
        {
            case 0:
                Time.timeScale = 0;
                PauseObj.SetActive(true);
                break;
            case 1:
                Time.timeScale = 1;
                PauseObj.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause(0);
        }
    }
    private IEnumerator BackGroundColor()
    {
        Spawner.Instance.Bg.color = new Color32(255, 0, 0, 255);
        float g = Spawner.Instance.Bg.color.g;
        float b = Spawner.Instance.Bg.color.b;
        for (int i = 0; i < 255; i++)
        {
            Spawner.Instance.Bg.color = new Color32(255, (byte)g++, (byte)b++, 255);
            yield return new WaitForSeconds(0.01f);
        }
        Spawner.Instance.Bg.color = new Color32(255, 255, 255, 255);
        yield return null;
    }
}
