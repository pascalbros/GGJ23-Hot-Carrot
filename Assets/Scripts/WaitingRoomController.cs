using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingRoomController : MonoBehaviour
{
    private enum WaitingState {
        INVALID,
        WAITING,
        SHOULD_START,
        COUNTDOWN,
        GAME,
        END
    }

    public TextMeshProUGUI waitingText;
    public TextMeshProUGUI countdownTimerText;

    private WaitingState state = WaitingState.INVALID;
    private float currentTime = 6;

    private static readonly string waitingBaseLabel = "WAITING FOR OTHER\nPLAYERS TO JOIN";

    void Update() {
        switch (state) {
            case WaitingState.INVALID:
                OnStateInvalid();
                break;
            case WaitingState.WAITING:
                OnStateWaiting();
                break;
            case WaitingState.SHOULD_START:
                OnStateShouldStart();
                break;
            case WaitingState.COUNTDOWN:
                OnStateCountdown();
                break;
            case WaitingState.GAME:
                break;
            case WaitingState.END:
                break;
        }
    }

    public void OnGameEnd(int winnerIndex) {
        state = WaitingState.END;
        waitingText.text = winnerIndex >= 0 ? "Player " + (winnerIndex + 1) + " wins!" : "Tie!";
        waitingText.gameObject.SetActive(true);
        countdownTimerText.gameObject.SetActive(false);

    }

    private void OnStateInvalid() {
        var waitingLabel = waitingBaseLabel;
        waitingText.text = waitingLabel;

        if (MatchMaker.current.playersCount > 1) {
            state = WaitingState.WAITING;
        }
    }

    private void OnStateWaiting() {
        currentTime -= Time.deltaTime;
        var waitingLabel = waitingBaseLabel + "\n" + (int)currentTime;
        waitingText.text = waitingLabel;

        if (MatchMaker.current.playersCount == 4 || currentTime < 1) {
            state = WaitingState.SHOULD_START;
        }
    }

    private void OnStateShouldStart() {
        state = WaitingState.COUNTDOWN;
        currentTime = 3.99f;
        Debug.Log("Should start");
    }

    private void OnStateCountdown() {
        currentTime -= Time.deltaTime;
        if (currentTime < 1) {
            StartGame();
            return;
        }
        waitingText.text = "" + (int)currentTime;
    }

    private void StartGame() {
        waitingText.gameObject.SetActive(false);
        countdownTimerText.gameObject.SetActive(true);
        state = WaitingState.GAME;
        MatchMaker.current.OnGameStart();
    }
}
