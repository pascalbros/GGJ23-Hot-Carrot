using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMaker : MonoBehaviour {

    public static MatchMaker current;
    public GameObject[] corners;
    public int playersCount = 0;
    readonly GameObject[] currentPlayers = new GameObject[4];
    int currentPlayerIndex = -1;

    private void Start() {
        current = this;
    }

    private void OnDestroy() {
        current = null;
    }

    public void OnPlayerAdded(GameObject player) {
        currentPlayerIndex += 1;
        playersCount = currentPlayerIndex + 1;
        currentPlayers[currentPlayerIndex] = player;
        var reference = corners[currentPlayerIndex].transform;
        reference.LookAt(Vector3.zero);
        var rotation = new Vector3(0, reference.eulerAngles.y, 0);
        player.transform.position = reference.position;
        player.transform.eulerAngles = rotation;
    }
}
