using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour {

[SerializeField] private float speed = 0.5f;
  private  Rect uv;
  private  RawImage _rawImg;

    private void Awake()
    {
        _rawImg = GetComponent<RawImage>();
    }
    void Update()
    {
         uv = new Rect(speed*Time.time, 0, 1, 1);
        _rawImg.uvRect = uv;
    }    

}

