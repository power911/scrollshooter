﻿using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimChenIn : MonoBehaviour {

    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _hp;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private int _rnd;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _exp;
    [SerializeField] private string _bossName;
    [SerializeField] private int _bossScoreBonus;
    [SerializeField] private bool _specSkill;

    private void Start()
    {
        GameManager.Instance.BossHPSllider.maxValue = _hp;
        GameManager.Instance.BossHPSllider.value = _hp;
        GameManager.Instance.BossHPSllider.gameObject.SetActive(true);
        StartCoroutine(Fire());
        StartCoroutine(Move());
        GameManager.Instance.SpawnBoss(_bossName);
        Spawner.Instance.BossLive = true;
        
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(GameManager.Instance.WarpsForBoss[_rnd].position.x, GameManager.Instance.WarpsForBoss[_rnd].position.y, transform.position.z), _speed + Time.deltaTime);
        }

        if (_hp <= 0)
        {
            Instantiate(_exp, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.BossHPSllider.gameObject.SetActive(false);
            GameManager.Instance.ScoreTextUpdate(_bossScoreBonus);
            Spawner.Instance.BossLive = false;
            GameManager.Instance.StartCoroutine(GameManager.Instance.BossDied());
            SteamUserStats.SetAchievement(_bossName);
            SteamUserStats.StoreStats();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            _hp-=ValueManager.Instance.Damage;
            GameManager.Instance.BossHPSllider.value -= ValueManager.Instance.Damage;
            StartCoroutine(Damage());

        }
    }

    IEnumerator Damage()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return null;
    }

    IEnumerator Fire()
    {
        while (_hp >= 0)
        {
            if (!_specSkill)
            {
                Vector3 dir = transform.position - CharacterController.Instance.transform.position;
                dir.Normalize();
                float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, rot));
                yield return new WaitForSeconds(0.5f);
                if (Random.Range(0, 10) == 3)
                {
                    StartCoroutine(SpecSkill());
                    yield return null;
                }
            }
            yield return null;
        }
        yield return null;
    }


    IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        _rnd = Random.Range(0, 3);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Move());
    }

    IEnumerator SpecSkill()
    {
        if (!_specSkill)
        {
            Debug.Log("SpecSkill");
            _specSkill = true;
            for (int i = -45; i <= 45; i += 15)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(1f);
            for (int i = -45; i <= 45; i += 15)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(1f);
            for (int i = -45; i <= 45; i += 15)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(1f);
            for (int i = -45; i <= 45; i += 10)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(1f);
            for (float i = -45; i <= 45; i +=7.5f)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(2.5f);
            for (float i = -45; i <= 45; i += 5.5f)
            {
                Instantiate(_bullet, transform.position, Quaternion.Euler(0, 0, i));
            }
            yield return new WaitForSeconds(2.5f);
            _specSkill = false;
            yield return new WaitForSeconds(10f);
        }
        yield return null;
    }
}
