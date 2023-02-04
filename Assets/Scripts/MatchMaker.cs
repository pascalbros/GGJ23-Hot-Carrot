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

    public void OnTimeout() {
        var playerWithTrophy = GetTrophyHolder();
        if (playerWithTrophy) {
            for (int i = 0; i < currentPlayers.Length; i++) {
                if (currentPlayers[i] == playerWithTrophy) {
                    Debug.Log("Player " + (i+1) + " wins!");
                }
            }
        } else {
            // Tie unlikely
            Debug.Log("Tie!");
        }
    }

    private GameObject GetTrophyHolder() {
        for (int i = 0; i < currentPlayers.Length; i++) {
            var player = currentPlayers[i];
            if (!player) { return null; }
            var trophy = player.transform.GetComponentInChildren<TrophyController>().trophy;
            if (trophy) {
                return player;
            }
        }
        return null;
    }
}
