using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {
    public static MoneyManager Instance;
    public int Coin;
    [SerializeField] private Text _coinText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        _coinText = MainController.Instance.CoinText;
        Coin = PlayerPrefs.GetInt("Coin");
        NewStart();
    }

    private void Start()
    {
        NewStart();
    }

    public void NewStart()
    {
        _coinText = MainController.Instance.CoinText;
    }


    public void UpdateText()
    {
        _coinText.text = Coin.ToString() + " x";
        PlayerPrefs.SetInt("Coin", Coin);
    }
}
