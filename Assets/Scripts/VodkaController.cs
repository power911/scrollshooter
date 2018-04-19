using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VodkaController : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _exp;
    [SerializeField] private int _scoreBonus;
    [SerializeField] private float _hp;
    private void Update()
    {
        transform.Translate(-_speed*Time.deltaTime*1.5f, -_speed*Time.deltaTime, transform.position.z);
        _time += 0.01f;
        if (_time > 25)
        {
            Destroy(gameObject);
        }
        if (_hp < 0)
        {
            Instantiate(_exp, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.ScoreTextUpdate(_scoreBonus);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spawner")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            CharacterController.Instance.TakeDamage(_damage);
            Instantiate(_exp, transform.position, Quaternion.identity);
        }
        if (collision.tag == "Bullet")
        {
            _hp-= ValueManager.Instance.Damage;
        }
    }
}
