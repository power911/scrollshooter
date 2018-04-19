using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour {
    public static MusicManager Instance;
    [SerializeField] private Slider _musicSlider;
    private string _path;

    OtherValue otherValue = new OtherValue();
    [SerializeField] AudioClip[] _music;
    AudioSource audio;
    public float MusicVolume;
    [SerializeField] private AudioClip _menuClip;
    
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
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
        _musicSlider = MainController.Instance.MusicSlider;
        MusicVolume = otherValue.MusicValue;
        _path = Path.Combine(Application.dataPath, "OtherValue.json");
        PlayMusicMenu();
        if (File.Exists(_path))
        {
            otherValue = JsonUtility.FromJson<OtherValue>(File.ReadAllText(_path));
            _musicSlider.value = otherValue.MusicValue;
            GetComponent<AudioSource>().volume = otherValue.MusicValue;
        }
        else
        {
            _musicSlider.value = 1f;
            audio.volume = 1f;
            otherValue.MusicValue = 1f;
            File.WriteAllText(_path, JsonUtility.ToJson(otherValue));
        }
    }

    public void SelectButton()
    {
        _musicSlider = MainController.Instance.MusicSlider;
        _musicSlider.onValueChanged.AddListener(arg => { gameObject.GetComponent<AudioSource>().volume = arg; });
        _musicSlider.value = otherValue.MusicValue;
        MusicVolume = otherValue.MusicValue;
    }

    public void SaveValue()
    {
        otherValue.MusicValue = _musicSlider.value;
        File.WriteAllText(_path, JsonUtility.ToJson(otherValue));
    }

    public IEnumerator PlayMusic()
    {
        audio.loop = false;
        var randomclip = Random.Range(0, _music.Length);
        audio.clip = _music[randomclip];
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        StartCoroutine(PlayMusic());
    }

    public void PlayMusicMenu()
    {
        StopAllCoroutines();
        audio.clip = _menuClip;
        audio.loop = true;
        audio.Play();
    }
}
public class OtherValue
{
    public float MusicValue;
}
