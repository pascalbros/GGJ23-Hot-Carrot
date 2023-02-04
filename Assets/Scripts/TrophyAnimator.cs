using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyAnimator : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public float yAmplitude = 1.0f;
    public float yTranslationSpeed = 3.0f;
    private float initialYTranslation;

    private void Start() {
        initialYTranslation = transform.localPosition.y;
    }

    void Update() {
        transform.eulerAngles = transform.eulerAngles + (rotationSpeed * Time.deltaTime * Vector3.up);
        //Vector3 translation = new(transform.position.x, initialYTranslation + Mathf.Sin(yTranslationSpeed * Time.time) * yAmplitude, transform.position.z);
        //transform.transform.position = translation;
    }
}
