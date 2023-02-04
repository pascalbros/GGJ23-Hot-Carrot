using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour {

    public GameObject trophy;
    public Vector3 positionOffset;
    public float graceTimer;

    void Start()
    {
        
    }

    private void Update() {
        if (trophy) {
            trophy.transform.position = transform.position + positionOffset;
        }
    }

    private void FixedUpdate() {
        if (graceTimer > 0) {
            graceTimer -= Time.fixedDeltaTime;
        }
    }

    public GameObject GimmeTheTrophy(TrophyController other) {
        if (graceTimer > 0) { return null; }
        var currentTrophy = trophy;
        trophy = null;
        return currentTrophy;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Trophy")) {
            GetTheTrophy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var trophy = collision.gameObject.GetComponent<TrophyController>().GimmeTheTrophy(this);
            if (trophy != null) {
                GetTheTrophy(trophy);
            }
        }
    }

    private void GetTheTrophy(GameObject trophy) {
        trophy.GetComponent<Collider>().enabled = false;
        this.trophy = trophy;
        graceTimer = 1;
    }
}
