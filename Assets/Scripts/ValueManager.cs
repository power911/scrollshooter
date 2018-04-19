using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public static ValueManager Instance;
    public float Damage;
    public float HP;
    public int IndexPlayer;
    public float MusicVulume;
    private void Awake()
    {
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

    public void NewValue(int index)
    {
        Damage = MainController.Instance.Dmg[index];
        HP = MainController.Instance.Hp[index];
        IndexPlayer = index;
    }
}
