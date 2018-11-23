﻿using UnityEngine;
using System.Collections;

public class CircularMotion : MonoBehaviour
{
    public float RotateSpeed;
    public float Radius;

    private Vector2 _centre;
    private float _angle;

    private void Start()
    {

        _centre = transform.position;
    }
    private void Update()
    {
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }
}
