using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuMaker : MonoBehaviour {

    public static MenuMaker current;
    public GameObject corner;
    public int playersCount = 0;
    public PlayerInputManager inputManager;

    GameObject currentPlayer;
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
        currentPlayer = player;
        var reference = corner.transform;
        var rotation = new Vector3(0, 0, 0);
        player.transform.position = reference.position;
        player.transform.eulerAngles = rotation;
        player.GetComponent<CarController>().ChangeBody(currentPlayerIndex);
        player.transform.GetComponent<PlayerInputController>().canMove = true;
    }
}
