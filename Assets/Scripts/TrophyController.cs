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
        if (other.transform.parent && other.transform.parent.CompareTag("Trophy")) {
            GetTheTrophy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player")) {
            var trophy = collision.gameObject.GetComponent<TrophyController>().GimmeTheTrophy(this);
            if (trophy != null) {
                GetTheTrophy(trophy);
            }
        }
    }

    private void GetTheTrophy(GameObject trophy) {
        trophy.transform.parent = transform;
        trophy.transform.localPosition = positionOffset;
        trophy.GetComponent<Collider>().enabled = false;
        this.trophy = trophy;
        graceTimer = 1;
    }
}
