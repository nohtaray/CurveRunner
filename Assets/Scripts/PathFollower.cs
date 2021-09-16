using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;

    private Quaternion _initialRotation;
    private Vector3 _appliedAdditionalVector = Vector3.zero;

    private float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        var inv = Quaternion.Inverse(pathCreator.path.GetRotationAtDistance(0f));
        _initialRotation = inv * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        t += .005f;
        while (t > 1.0f) t -= 1;
        var position = pathCreator.path.GetPointAtTime(t);
        transform.position = position + pathCreator.path.up * .1f;

        var rotation = pathCreator.path.GetRotation(t);
        transform.rotation = rotation * _initialRotation;


        var additionalX = Mathf.Sin(t * 30) * 2f;
        var forward = pathCreator.path.GetDirection(t);
        // 上と前を考慮して横のベクトルにする
        var additionalVector = Vector3.Cross(forward, pathCreator.path.up).normalized * additionalX;
        transform.position -= _appliedAdditionalVector;
        transform.position += additionalVector;
        _appliedAdditionalVector = additionalVector;
        // Debug.Log(additionalVector);
    }
}