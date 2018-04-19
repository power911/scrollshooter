using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
    [SerializeField] private int _coincost;
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    AudioSource audio = new AudioSource();

    private void Start()
    {
        _speed = Random.Range(-0.01f, -0.025f);
        audio = GetComponent<AudioSource>();
    }

    void Update () {
        transform.Translate(_speed, _speed, transform.position.z);
        _time+=0.01f;
        if (_time > 5)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.GetMoney(_coincost);
            Destroy(gameObject);
            audio.Play();
        }
    }
    
       
    
}
