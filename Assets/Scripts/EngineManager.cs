using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineManager : MonoBehaviour {
    [SerializeField] private Button _saveButton;

    private void Start()
    {
        _saveButton.onClick.AddListener(()=> { MusicManager.Instance.SaveValue(); });
        StartCoroutine(One());
    }
    
    IEnumerator One()
    {
        yield return new WaitForSeconds(0.5f);
        MusicManager.Instance.SelectButton();
        yield return null;
    }
}
