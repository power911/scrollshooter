using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
    public static Spawner Instance;
    [SerializeField] private GameObject[] _enemyPool;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject[] _bompSpawnPlace;
    [SerializeField] private GameObject[] _warningObj;
    [SerializeField] private GameObject _bodyBuilder;
    [SerializeField] private GameObject _rocket;
    public float TimeForEnemySpawn;
    public bool BossLive;
    [SerializeField] private int _countBoss;
    [SerializeField] private float _scoreBoss;
    [SerializeField] private GameObject[] _boss;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private GameObject _boing;
    [SerializeField] private GameObject _bear;
    public RawImage Bg;
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
        StartCoroutine(SpawnEnemyPool());
        StartCoroutine(SpawnBomb());
        StartCoroutine(SpawnBodyBuilder());
        StartCoroutine(SpawnRockect());
        StartCoroutine(SpawnBoss());
        StartCoroutine(SpawnBoing());
        StartCoroutine(SpawnBodyBear());
        Instantiate(_players[ValueManager.Instance.IndexPlayer], new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    IEnumerator SpawnEnemyPool()
    {
        if (!BossLive)
        {
            var rund = Random.Range(-4f, 4f);
            Instantiate(_enemyPool[Random.Range(0, _enemyPool.Length)], new Vector2(transform.position.x, rund), Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnEnemyPool());
        yield return null;

    }
    
    private IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(15f);
        if (!BossLive)
        {
            for (int i = 0; i < 3; i++)
            {
                _warningObj[i].SetActive(true);
            }
            yield return new WaitForSeconds(1f);
            for (int q = 0; q < _bompSpawnPlace.Length; q++)
            {
                Instantiate(_bomb, _bompSpawnPlace[q].transform.position, Quaternion.Euler(0, 0, 90));
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 3; i++)
            {
                _warningObj[i].SetActive(false);
            }
            yield break;
        }
        yield return new WaitForSeconds(25f);
        StartCoroutine(SpawnBomb());
        yield return null;
    }

    IEnumerator  SpawnBodyBuilder()
    {
        yield return new WaitForSeconds(7.5f);
        if (!BossLive)
        {
            float new_y = -3f;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(_bodyBuilder, new Vector3(11, new_y, 0), Quaternion.identity);
                new_y += 3;
            }
        }
        yield return new WaitForSeconds(20f);
        StartCoroutine(SpawnBodyBuilder());
        yield return null;
    }

    IEnumerator SpawnBodyBear()
    {
        yield return new WaitForSeconds(17.5f);
        if (!BossLive)
        {
            float new_y = -4.5f;
            for (int i = 0; i < 7; i++)
            {
                Instantiate(_bear, new Vector3(11, new_y, 0), Quaternion.identity);
                new_y += 2.25f;
            }
        }
        yield return new WaitForSeconds(20f);
        StartCoroutine(SpawnBodyBuilder());
        yield return null;
    }

    private IEnumerator SpawnRockect()
    {
        yield return new WaitForSeconds(35f);
        if (!BossLive)
        {
            for (int i = 3; i < _warningObj.Length; i++)
            {
                _warningObj[i].SetActive(true);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 3; i < _warningObj.Length; i++)
            {
                Vector3 dir = _warningObj[i].transform.position - CharacterController.Instance.transform.position;
                dir.Normalize();
                float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Instantiate(_rocket, new Vector3(_warningObj[i].transform.position.x, _warningObj[i].transform.position.y, _warningObj[i].transform.position.z - 25f), Quaternion.Euler(0, 0, rot));
                yield return new WaitForSeconds(0.5f);
            }
            for (int i = 3; i < _warningObj.Length; i++)
            {
                _warningObj[i].SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }
        }
        StartCoroutine(SpawnRockect());
        yield return null;
    }
    private IEnumerator SpawnBoss()
    {
        int count = 1;
        yield return new WaitForSeconds(5f);
        if (!BossLive)
        {
            if (GameManager.Instance.Score >= _scoreBoss)
            {
                StartCoroutine(BackGroundColor());
                Instantiate(_boss[_countBoss], new Vector3(), Quaternion.identity);
                _scoreBoss += _scoreBoss * count;
                count++;
                _countBoss++;
            }
        }
        StartCoroutine(SpawnBoss());
        yield return null;
    }
    private IEnumerator SpawnBoing()
    {
        yield return new WaitForSeconds(45f);
        if (!BossLive)
        {
           Instantiate(_boing, new Vector3(14, 4, 0), Quaternion.identity);
        }
        StartCoroutine(SpawnBoing());
        yield return null;
    }
   private IEnumerator BackGroundColor()
    {
        Bg.color = new Color32(255, 255, 255, 255);
        float g = Bg.color.g;
        float b = Bg.color.b;
        for (int i = 0; i < 255; i++)
        {
          Bg.color = new Color32(255, (byte)g--, (byte)b--, 255);
          yield return new WaitForSeconds(0.01f);
        }
        Bg.color = new Color32(255, 0, 0, 255);
        yield return null;
    }
}
