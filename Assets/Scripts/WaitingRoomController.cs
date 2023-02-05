using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public AudioClip[] timeoutSounds;
    public GameObject mainAudioSource;

    private WaitingState state = WaitingState.INVALID;
    private float currentTime = 6;
    private bool[] countSoundsPlayed = new bool[4];

    private static readonly string waitingBaseLabel = "PRESS ANY BUTTON TO JOIN";

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
                OnStateEnd();
                break;
        }
    }

    public void OnGameEnd(int winnerIndex) {
        state = WaitingState.END;
        waitingText.fontSize = 50;
        waitingText.text = winnerIndex >= 0 ? "Player " + (winnerIndex + 1) + " wins!" : "Tie!";
        waitingText.gameObject.SetActive(true);
        countdownTimerText.gameObject.SetActive(false);
        currentTime = 4.0f;
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

        var intCurrentTime = (int)currentTime;
        if (intCurrentTime <= 3 && intCurrentTime > 0 && !countSoundsPlayed[intCurrentTime]) {
            countSoundsPlayed[intCurrentTime] = true;
            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.PlayOneShot(timeoutSounds[0]);
        } else if (intCurrentTime == 0 && !countSoundsPlayed[0]) {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.PlayOneShot(timeoutSounds[1]);
            countSoundsPlayed[0] = true;
            mainAudioSource.SetActive(true);
        }
    }

    private void OnStateShouldStart() {
        //state = WaitingState.COUNTDOWN;
        //currentTime = 3.99f;
        StartGame();
    }

    private void OnStateCountdown() {
        waitingText.fontSize = 200;
        currentTime -= Time.deltaTime;
        if (currentTime < 1) {
            StartGame();
            return;
        }
        waitingText.text = "" + (int)currentTime;
    }

    private void OnStateEnd() {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void StartGame() {
        waitingText.gameObject.SetActive(false);
        countdownTimerText.gameObject.SetActive(true);
        state = WaitingState.GAME;
        MatchMaker.current.OnGameStart();
    }
}
