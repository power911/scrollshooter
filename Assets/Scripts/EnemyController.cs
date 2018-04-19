using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private int _bonusScore;
    [SerializeField] private float _hp;
    [SerializeField] private GameObject _blood;
    private void Start()
    {
       _speed = Random.Range(-4, -8);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.right + transform.position, Time.deltaTime * _speed);
        if (_hp <= 0)
        {
            GameManager.Instance.ScoreTextUpdate(_bonusScore);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Instantiate(_blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            int random = Random.Range(0, 3);
            if (random == 2)
            {
                Instantiate(GameManager.Instance.Coins[Random.Range(0, GameManager.Instance.Coins.Length)], transform.position, Quaternion.identity);
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.ScoreTextUpdate(_bonusScore);
            CharacterController.Instance.TakeDamage(_damage);
            Destroy(gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Instantiate(_blood, transform.position, Quaternion.identity);
        }
        if(collision.tag == "Finish") { Destroy(gameObject); }        
        if(collision.tag == "Bullet") {
            _hp -=ValueManager.Instance.Damage;
            Destroy(collision.gameObject);
            Debug.Log(ValueManager.Instance.Damage);}
    }
}

