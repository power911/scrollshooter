using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Steamworks;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private Text[] _play;
    [SerializeField] private Text[] _upgradeHP;
    [SerializeField] private Text[] _upgradeDMG;
    [SerializeField] private Text[] _save;
    [SerializeField] private Text[] _setting;
    [SerializeField] private Text[] _back;
    [SerializeField] private Text[] _hp;
    [SerializeField] private Text[] _dmg;
    [SerializeField] private Text[] _exit;
    [SerializeField] private Text[] _music;
    [SerializeField] private Text[] _buy;
    private string _path;

    Language lang = new Language();

    private void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            _path = File.ReadAllText(Application.streamingAssetsPath + PlayerPrefs.GetString("Language"));
            lang = JsonUtility.FromJson<Language>(_path);
            LoadLanguage(PlayerPrefs.GetString("Language"));
        }
        else
        {
            _path = File.ReadAllText(Application.streamingAssetsPath + "\\EN.json");
            lang = JsonUtility.FromJson<Language>(_path);
            LoadLanguage("\\EN.json");
        }
    }

    public void LoadLanguage(string language)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, language);
        WWW reader = new WWW(path);
        while (!reader.isDone) {  }
        _path = reader.text;
#else
        _path = File.ReadAllText(Application.streamingAssetsPath + language);
#endif
        lang = JsonUtility.FromJson<Language>(_path);
        for (int i = 0; i < _play.Length; i++)
        {
            _play[i].text = lang.Play;
        }
        for (int i = 0; i < _upgradeHP.Length; i++)
        {
            _upgradeHP[i].text = lang.UpgradeHP;
        }
        for (int i = 0; i < _upgradeDMG.Length; i++)
        {
            _upgradeDMG[i].text = lang.UpgradeDMG;
        }
        for (int i = 0; i < _save.Length; i++)
        {
            _save[i].text = lang.Save;
        }
        for (int i = 0; i < _setting.Length; i++)
        {
            _setting[i].text = lang.Setting;
        }
        for (int i = 0; i < _back.Length; i++)
        {
            _back[i].text = lang.Back;
        }
        for (int i = 0; i < _hp.Length; i++)
        {
            _hp[i].text = lang.HP;
        }
        for (int i = 0; i < _dmg.Length; i++)
        {
            _dmg[i].text = lang.DMG;
        }
        for (int i = 0; i < _exit.Length; i++)
        {
            _exit[i].text = lang.Exit;
        }
        for (int i = 0; i < _music.Length; i++)
        {
            _music[i].text = lang.Music;
        }
        for (int i = 0; i < _buy.Length; i++)
        {
            _buy[i].text = lang.Buy;
        }
       SteamUserStats.SetAchievement(language);
       SteamUserStats.StoreStats();
        MainController.Instance.LoadOtherString(language);
        PlayerPrefs.SetString("Language", language);
    }
}
[SerializeField]
public  class Language
{
    public string Play;
    public string UpgradeHP;
    public string UpgradeDMG;
    public string Save;
    public string Setting;
    public string Back;
    public string HP;
    public string DMG;
    public string Exit;
    public string Music;
    public string Buy;
}
