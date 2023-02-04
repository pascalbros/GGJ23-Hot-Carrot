using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float initialTime;

    private float remainingTime;
    private bool isValid = true;


    void Start() {
        remainingTime = initialTime;
    }

    void Update() {
        if (!isValid) { return; }
        remainingTime -= Time.deltaTime;
        textMesh.text = GetFormattedTime(Mathf.Max(0, remainingTime));
        if (remainingTime < 0) {
            isValid = false;
            GetComponent<MatchMaker>().OnTimeout();
            DestroyImmediate(this);
        }
    }

    string GetFormattedTime(float time) {
        var minutes = "" + (int)(time / 60);
        var seconds = "" + (int)(time % 60);
        seconds = seconds.Length > 1 ? seconds : "0" + seconds;
        return minutes + ":" + seconds;
    }
}
