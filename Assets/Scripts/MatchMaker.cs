using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchMaker : MonoBehaviour {

    public static MatchMaker current;
    public GameObject[] corners;
    public WaitingRoomController waitingRoomController;
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
        Debug.Log(player.transform.parent.name);
        currentPlayerIndex += 1;
        playersCount = currentPlayerIndex + 1;
        currentPlayers[currentPlayerIndex] = player;
        var reference = corners[currentPlayerIndex].transform;
        reference.LookAt(Vector3.zero);
        var rotation = new Vector3(0, reference.eulerAngles.y, 0);
        player.transform.position = reference.position;
        player.transform.eulerAngles = rotation;
        player.GetComponent<CarController>().ChangeBody(currentPlayerIndex);
        player.transform.GetComponent<PlayerInputController>().canMove = false;
    }

    public void OnGameStart() {
        ChangeInputForPlayers(true);
    }

    public void OnTimeout() {
        ChangeInputForPlayers(false);
        var winner = -1;
        var playerWithTrophy = GetTrophyHolder();
        if (playerWithTrophy) {
            for (int i = 0; i < currentPlayers.Length; i++) {
                if (currentPlayers[i] == playerWithTrophy) {
                    winner = i;
                    Camera.main.GetComponent<CameraController>().OnTimeout(currentPlayers[i]);
                }
            }
        }
        waitingRoomController.OnGameEnd(winner);
    }

    public void ChangeInputForPlayers(bool enabled) {
        foreach (var player in currentPlayers) {
            if (!player) { return; }
            player.transform.GetComponent<PlayerInputController>().canMove = enabled;
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
