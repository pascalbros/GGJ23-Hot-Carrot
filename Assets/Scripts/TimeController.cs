using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float initialTime;

    private float remainingTime;


    void Start() {
        remainingTime = initialTime;
    }

    void Update() {
        remainingTime -= Time.deltaTime;
        textMesh.text = GetFormattedTime(Mathf.Max(0, remainingTime));
    }

    string GetFormattedTime(float time) {
        var minutes = "" + (int)(time / 60);
        var seconds = "" + (int)(time % 60);
        seconds = seconds.Length > 1 ? seconds : "0" + seconds;
        return minutes + ":" + seconds;
    }
}
