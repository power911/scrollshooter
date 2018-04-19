using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using LapinerTools.Steam.UI;

public class CharacterController : MonoBehaviour {
    public static CharacterController Instance { get; private set; }
    [SerializeField] private float _speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reloadTime;
    [SerializeField] private Slider sliderHp;
    [SerializeField] private float _hp;
    [SerializeField] private GameObject _exp;
    AudioSource audio;
    [SerializeField] private AudioClip _fireClip;
    // rb is Kinematic
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
        sliderHp = GameManager.Instance.SliderHp;
         audio = GetComponent<AudioSource>();
        _hp = ValueManager.Instance.HP;
        sliderHp.maxValue = _hp;
        sliderHp.value = _hp;
        audio.volume = MusicManager.Instance.MusicVolume;
       
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && reloadTime > 10f)
        {
            Instantiate(bullet, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
            reloadTime = 0;
            audio.clip = _fireClip;
            audio.Play();
        }
        reloadTime++;
        transform.Translate(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10, 10), Mathf.Clamp(transform.position.y, -4.75f, 4.75f), 0);

        
       

        if (_hp < 0)
        {
            Destroy(gameObject);
            Spawner.Instance.BossLive = true;
            Instantiate(_exp, transform.position, Quaternion.identity);
            GameManager.Instance.BgGameOver.gameObject.SetActive(true);
            GameManager.Instance._canvas.planeDistance = 1f;
            SteamLeaderboardsUI.UploadScore("HIGHSCORE", GameManager.Instance.Score);
        }

    }

    public void TakeDamage(float damage)
    {
        sliderHp.value -= damage;
        _hp -= damage;
    }

}
