using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Steamworks;
using LapinerTools.uMyGUI;
using LapinerTools.Steam.UI;

public class MainController : MonoBehaviour {
    public static MainController Instance;
    [SerializeField] private GameObject[] _canvas;
    private string _path;
    Upgrade upgrade = new Upgrade();
    [SerializeField] private Text[] _textForUpdate;
    [SerializeField] private Text[] _textCost;
    [SerializeField] private int _costForUpgrade;
    [SerializeField] private int _costForUpgradeDMG;
    [SerializeField] public float[] Hp;
    [SerializeField] public float[] Dmg;
    [SerializeField] private Text[] _hpText;
    [SerializeField] private Text[] _dmgText;
    [SerializeField] private int[] _costHP;
    [SerializeField] private int[] _costDMG;
    [SerializeField] private float _damage;
    [SerializeField] private Button[] _planeButton;
    [SerializeField] private Button[] _planeBuy;
    [SerializeField] private Button[] _playButtonBuyedPlane;
    public Text CoinText;
    public Slider MusicSlider;
    [SerializeField] private int[] _costFlyShip;
    [SerializeField] private Text[] _costFlyText;
    [SerializeField] private Text _mainText;
    [SerializeField] private Image _steamAvatar;
    [SerializeField] private Text _steamName;
    Lang lang = new Lang();
    [SerializeField] Button _hightscore;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        
        
    }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Upgrate.json");
#else
        _path = Path.Combine(Application.dataPath, "Upgrate.json");
#endif
        if (File.Exists(_path))
        {
            upgrade = JsonUtility.FromJson<Upgrade>(File.ReadAllText(_path));
        }
        else
        {
            File.WriteAllText(_path, JsonUtility.ToJson(upgrade));
        }
        for (int i = 0; i < 6; i++)
        {
            LoadUpgrade(i);
        }
        for (int i = 0; i < upgrade.BuyedPlanets.Count; i++)
        {
            LoadingPlane(i);
        }
        MusicManager.Instance.SelectButton();
        MoneyManager.Instance.NewStart();
        MoneyManager.Instance.UpdateText();
        MusicManager.Instance.PlayMusicMenu();
        SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_0");
        SteamUserStats.StoreStats();
        StartCoroutine(TextScale());
        StartCoroutine(TextColor());
        _steamName.text = SteamFriends.GetPersonaName();
        StartCoroutine(SteamAvatar());
#if UNITY_ANDROID && !UNITY_EDITOR
       _hightscore.gameObject.SetActive(false);
#else
        _hightscore.gameObject.SetActive(true);
#endif
    }
    public void ChangeCanvas(int index)
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            _canvas[i].SetActive(false);
        }
        _canvas[index].SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void LoadUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                _costHP[0] = _costForUpgrade * upgrade.UpgradesForFirstHP.Count;
                Hp[0] += upgrade.UpgradesForFirstHP.Count;
                _textCost[index].text = "x " + _costHP[0].ToString();
                _hpText[0].text = lang.HP +": " + Hp[0];
                break;
            case 1:
                _costDMG[0] = _costForUpgradeDMG * upgrade.UpgradesForFirstDMG.Count;
                Dmg[0] += upgrade.UpgradesForFirstDMG.Count* _damage;
                _textCost[index].text = "x " + _costDMG[0].ToString();
                _dmgText[0].text = lang.DMG +": " + Dmg[0];
                break;
            case 2:
                _costHP[1] = _costForUpgrade * upgrade.UpgradesForTwoHP.Count;
                Hp[1] += upgrade.UpgradesForTwoHP.Count;
                _textCost[index].text = "x " + _costHP[1].ToString();
                _hpText[1].text = lang.HP + ": " + Hp[1];
                break;
            case 3:
                _costDMG[1] = _costForUpgradeDMG * upgrade.UpgradesForTwoDMG.Count;
                Dmg[1] += upgrade.UpgradesForTwoDMG.Count * _damage;
                _textCost[index].text = "x " + _costDMG[1].ToString();
                _dmgText[1].text = lang.DMG + ": " + Dmg[1];
                break;
            case 4:
                _costHP[2] = _costForUpgrade * upgrade.UpgradesForThreeHP.Count;
                Hp[2] += upgrade.UpgradesForThreeHP.Count;
                _textCost[index].text = "x " + _costHP[2].ToString();
                _hpText[2].text = lang.HP + ": " + Hp[2];
                break;
            case 5:
                _costDMG[2] = _costForUpgradeDMG * upgrade.UpgradesForThreeDMG.Count;
                Dmg[2] += upgrade.UpgradesForThreeDMG.Count * _damage;
                _textCost[index].text = "x " + _costDMG[2].ToString();
                _dmgText[2].text = lang.DMG + ": " + Dmg[2];
                break;
        }
    }
    public void Upgrade(int index)
    {
        switch (index)
        {
            case 0:
                if(MoneyManager.Instance.Coin < _costHP[0]){return;}
                MoneyManager.Instance.Coin -= _costHP[0];
                upgrade.UpgradesForFirstHP.Add(0);
                Hp[0]++;
                _hpText[0].text = lang.HP + ": " + Hp[0].ToString();
                _costHP[0] += _costForUpgrade;
                _textCost[index].text = "x " + _costHP[0];
                break;
            case 1:
                if (MoneyManager.Instance.Coin < _costDMG[0]) { return; }
                MoneyManager.Instance.Coin -= _costDMG[0];
                upgrade.UpgradesForFirstDMG.Add(0);
                Dmg[0] += _damage;
                Dmg[0] = Mathf.Round(Dmg[0] *10)/10;
                _dmgText[0].text = lang.DMG + ": " + Dmg[0].ToString();
                _costDMG[0] += _costForUpgrade;
                _textCost[index].text = "x " + _costDMG[0];
                break;
            case 2:
                if (MoneyManager.Instance.Coin < _costHP[1]) { return; }
                MoneyManager.Instance.Coin -= _costHP[1];
                upgrade.UpgradesForTwoHP.Add(0);
                Hp[1]++;
                _hpText[1].text = lang.HP + ": " + Hp[1].ToString();
                _costHP[1] += _costForUpgrade;
                _textCost[index].text = "x " + _costHP[1];
                break;
            case 3:
                if (MoneyManager.Instance.Coin < _costDMG[1]) { return; }
                MoneyManager.Instance.Coin -= _costDMG[1];
                upgrade.UpgradesForTwoDMG.Add(0);
                Dmg[1]+= _damage;
                Dmg[1] = Mathf.Round(Dmg[1]*10)/10;
                _dmgText[1].text = lang.DMG + ": " + Dmg[1].ToString();
                _costDMG[1] += _costForUpgrade;
                _textCost[index].text = "x " + _costDMG[1];
                break;
            case 4:
                if (MoneyManager.Instance.Coin < _costHP[2]) { return; }
                MoneyManager.Instance.Coin -= _costHP[2];
                upgrade.UpgradesForThreeHP.Add(0);
                Hp[2]++;
                _hpText[2].text = lang.HP + ": " + Hp[2].ToString();
                _costHP[2] += _costForUpgrade;
                _textCost[index].text = "x " + _costHP[2];
                break;
            case 5:
                if (MoneyManager.Instance.Coin < _costDMG[2]) { return; }
                MoneyManager.Instance.Coin -= _costDMG[2];
                upgrade.UpgradesForThreeDMG.Add(0);
                Dmg[2]+= _damage;
                Dmg[2] = Mathf.Round(Dmg[2]*10)/10;
                _dmgText[2].text = lang.DMG + ": " + Dmg[2].ToString();
                _costDMG[2] += _costForUpgrade;
                _textCost[index].text = "x " + _costDMG[2];
                break;
        }
        File.WriteAllText(_path, JsonUtility.ToJson(upgrade));
        MoneyManager.Instance.UpdateText();
    }
    public void Buy(int index)
    {
        if (MoneyManager.Instance.Coin < _costFlyShip[index]) { return; }
        MoneyManager.Instance.Coin -= _costFlyShip[index];
        LoadingPlane(index);
        upgrade.BuyedPlanets.Add(index);
        File.WriteAllText(_path, JsonUtility.ToJson(upgrade));
        MoneyManager.Instance.NewStart();
    }
    public void LoadingPlane(int index)
    {
        switch (index)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    _planeButton[i].interactable = true;
                }
                _planeBuy[0].gameObject.SetActive(false);
                _costFlyText[0].gameObject.SetActive(false);
                _playButtonBuyedPlane[0].gameObject.SetActive(true);
                break;
            case 1:
                for (int i = 2; i < 4; i++)
                {
                    _planeButton[i].interactable = true;
                }
                _planeBuy[1].gameObject.SetActive(false);
                _costFlyText[1].gameObject.SetActive(false);
                _playButtonBuyedPlane[1].gameObject.SetActive(true);
                break;
        }
    }
    public void PlayGame(int index)
    {
        ValueManager.Instance.NewValue(index);
        SceneManager.LoadScene(1);
    }
    public void LoadLeaderBoard()
    {
        ((SteamLeaderboardsPopup)uMyGUI_PopupManager.Instance.ShowPopup("steam_leaderboard")).LeaderboardUI.DownloadScores("HIGHSCORE");
    }
    public void LoadOtherString(string language)
    {
        string path = File.ReadAllText(Application.streamingAssetsPath + language);
        lang = JsonUtility.FromJson<Lang>(path);
        for (int index = 0; index < 6; index++)
        {
            switch (index)
            {
                case 0:
                    _costHP[0] = _costForUpgrade * upgrade.UpgradesForFirstHP.Count;
                    _textCost[index].text = "x " + _costHP[0].ToString();
                    _hpText[0].text = lang.HP + ": " + Hp[0];
                    break;
                case 1:
                    _costDMG[0] = _costForUpgradeDMG * upgrade.UpgradesForFirstDMG.Count;
                    _textCost[index].text = "x " + _costDMG[0].ToString();
                    _dmgText[0].text = lang.DMG + ": " + Dmg[0];
                    break;
                case 2:
                    _costHP[1] = _costForUpgrade * upgrade.UpgradesForTwoHP.Count;
                    Hp[1] += upgrade.UpgradesForTwoHP.Count;
                    _textCost[index].text = "x " + _costHP[1].ToString();
                    _hpText[1].text = lang.HP + ": " + Hp[1];
                    break;
                case 3:
                    _costDMG[1] = _costForUpgradeDMG * upgrade.UpgradesForTwoDMG.Count;
                    _textCost[index].text = "x " + _costDMG[1].ToString();
                    _dmgText[1].text = lang.DMG + ": " + Dmg[1];
                    break;
                case 4:
                    _costHP[2] = _costForUpgrade * upgrade.UpgradesForThreeHP.Count;
                    _textCost[index].text = "x " + _costHP[2].ToString();
                    _hpText[2].text = lang.HP + ": " + Hp[2];
                    break;
                case 5:
                    _costDMG[2] = _costForUpgradeDMG * upgrade.UpgradesForThreeDMG.Count;
                    _textCost[index].text = "x " + _costDMG[2].ToString();
                    _dmgText[2].text = lang.DMG + ": " + Dmg[2];
                    break;
            }
        }
    }
    IEnumerator TextScale()
    {
        for (int i = 0; i <= 50; i++)
        {
            _mainText.fontSize -= 1;
            yield return new WaitForSeconds(0.015f);
        }
        for (int i = 0; i <= 50; i++)
        {
            _mainText.fontSize += 1;
            yield return new WaitForSeconds(0.015f);
        }
        StartCoroutine(TextScale());
        yield return null;
    }
    IEnumerator TextColor()
    {
        for (int i = 0; i < 5; i++)
        {
            _mainText.color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(0.15f);
            _mainText.color = new Color32(200, 0, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _mainText.color = new Color32(50, 0, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _mainText.color = new Color32(0, 255, 255, 255);
            yield return new WaitForSeconds(0.15f);
            _mainText.color = new Color32(0, 255, 0, 255);
            yield return new WaitForSeconds(0.15f);
            _mainText.color = new Color32(255, 255, 0, 255);
            yield return new WaitForSeconds(0.15f);
        }
        StartCoroutine(TextColor());
        yield return null;
    }
    int _avatarInt;
    uint width, height;
    Texture2D dowloadedAvatar;
    Rect rect = new Rect(0, 0, 184, 184);
    Vector2 pivot = new Vector2(0.5f, 0.5f);
    IEnumerator SteamAvatar()
    {
        _avatarInt =  SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
        while(_avatarInt == -1)
        {
            yield return null;
        }
        if (_avatarInt > 0)
        {
            SteamUtils.GetImageSize(_avatarInt, out width, out height);
            if(width>0 && height > 0)
            {
                byte[] avatarSteam = new byte[4 * (int)width * (int)height];
                SteamUtils.GetImageRGBA(_avatarInt, avatarSteam, 4 * (int)width * (int)height);
                dowloadedAvatar = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                dowloadedAvatar.LoadRawTextureData(avatarSteam);
                dowloadedAvatar.Apply();
                _steamAvatar.sprite = Sprite.Create(dowloadedAvatar, rect, pivot);
            }
        }
       
    }
}
[SerializeField]
public class Upgrade
{
    public List<int> UpgradesForFirstHP = new List<int>();
    public List<int> UpgradesForFirstDMG = new List<int>();
    public List<int> UpgradesForTwoHP = new List<int>();
    public List<int> UpgradesForTwoDMG = new List<int>();
    public List<int> UpgradesForThreeHP = new List<int>();
    public List<int> UpgradesForThreeDMG = new List<int>();
    public List<int> BuyedPlanets = new List<int>();
}
[SerializeField]
public class Lang
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
