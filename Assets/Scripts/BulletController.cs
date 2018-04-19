using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _exp;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, _speed * Time.deltaTime);
        _time += 0.01f;

        if (_time >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Spawner")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            CharacterController.Instance.TakeDamage(_damage);
            Instantiate(_exp, transform.position, Quaternion.identity);
        }
    }
}