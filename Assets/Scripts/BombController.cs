using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _exp;
    [SerializeField] private float _damage;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(_exp, new Vector3(transform.position.x,transform.position.y,transform.position.z-25f), Quaternion.identity);
            CharacterController.Instance.TakeDamage(_damage);
            Destroy(gameObject);
        }
        if(collision.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
