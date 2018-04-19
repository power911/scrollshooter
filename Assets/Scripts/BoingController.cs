using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingController : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _time;

    private void Start()
    {
        StartCoroutine(SpawnBomb());
    }

    private void Update()
    {
        transform.Translate(-_speed * Time.deltaTime, 0, transform.position.z);
        _time += 0.01f;
        if (_time > 5)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(1f);
        while (_time < 5)
        {
            Instantiate(_bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
}
