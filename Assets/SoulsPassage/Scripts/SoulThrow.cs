using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SoulThrow : MonoBehaviour
{
    public XRGrabInteractable grabInteractable; // Reference to the Soul
    public Rigidbody soulRigidbody;

    //Reference for the scoreboard texts
    public TMP_Text scoreText;
    public TMP_Text correctText;
    public TMP_Text wrongText;
    public TMP_Text timerText;

    private int score = 0;
    private int correctAnswers = 0;
    private int wrongAnswers = 0;
    public float timer = 60f;

    private Vector3 initialPosition = new Vector3(-0.3337124f, 1.596273f, -6.39f);
    private bool isBeingHeld = false;
    private bool isGoodSoul;
    private bool hasMissed = false;

    public SoulManager soulManager;
    public AudioSource correctPlacementAudio;
    public AudioSource badSoulInHeavenAudio;
    public AudioSource goodSoulInHellAudio;
    public AudioSource missedThrowAudio;
    public AudioSource gameOverAudio;
    public AudioSource winAudio;
    public AudioSource backgroundAudio;

    public GameObject gameOverPanel;
    public GameObject winPanel;
    public TMP_Text gameOverText;
    public TMP_Text winText;

    public GameObject gameOverRestartButton;
    public GameObject gameOverQuitButton;
    public GameObject winRestartButton;
    public GameObject winQuitButton;

    public GameObject bookCanvas; // Reference to the book canvas to deactivate

    private bool gameOverTriggered = false;
    private bool winTriggered = false;
    private bool gameActive = true;

    public GrimGuideButtons grimGuideButtons; // Reference to GrimGuideButtons script


    private void Start()
    {
        UpdateScoreText();
        UpdateCorrectText();
        UpdateWrongText();
        UpdateTimerText();
        ResetSoulPosition();
        soulRigidbody.isKinematic = true;

        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        if (backgroundAudio != null && !backgroundAudio.isPlaying)
        {
            backgroundAudio.loop = true; // Ensure looping
            backgroundAudio.Play(); // Start background music
        }
    }

    private void Update()
    {
        if (gameOverTriggered || winTriggered) return;

        HandleTimer();
        CheckGameOverConditions();
        CheckWinConditions();

        if (grabInteractable.isSelected)
        {
            isBeingHeld = true;
            soulRigidbody.isKinematic = true;
            transform.position = grabInteractable.transform.position;
            transform.rotation = grabInteractable.transform.rotation;
        }
        else if (isBeingHeld)
        {
            soulRigidbody.isKinematic = false;
            Vector3 throwDirection = grabInteractable.transform.forward;
            soulRigidbody.AddForce(throwDirection * 10f, ForceMode.Impulse);
            isBeingHeld = false;
        }
    }

    private void HandleTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }

        if (timer <= 0)
        {
            timer = 0;
            Debug.Log("Timer expired!");
        }
    }

    private void UpdateScoreText()
    {
        score = correctAnswers - wrongAnswers;
        scoreText.text = "Score: " + score;
    }

    private void UpdateCorrectText()
    {
        correctText.text = "Correct: " + correctAnswers;
    }

    private void UpdateWrongText()
    {
        wrongText.text = "Wrong: " + wrongAnswers;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
    }

    public void ResetSoulPosition()
    {
        transform.position = initialPosition;
        soulRigidbody.isKinematic = true;
        soulRigidbody.velocity = Vector3.zero;
        soulRigidbody.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void SetSoulStatus(bool isGood)
    {
        isGoodSoul = isGood;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MissedCollider") && !hasMissed)
        {
            hasMissed = true;
            timer -= 6f;
            if (timer < 0) timer = 0;
            UpdateTimerText();
            PlaySound(missedThrowAudio);
            Debug.Log("Missed throw -6 seconds deducted.");

            StartCoroutine(ResetMissedFlag());
        }

        if (other.CompareTag("HellDoor") || other.CompareTag("HeavenDoor"))
        {
            bool correctDoor = (other.CompareTag("HellDoor") && !isGoodSoul) || 
                               (other.CompareTag("HeavenDoor") && isGoodSoul);

            if (correctDoor)
            {
                Debug.Log("Correct placement.");
                correctAnswers++;
                UpdateCorrectText();
                PlaySound(correctPlacementAudio);
            }
            else
            {
                Debug.Log("Incorrect placement.");
                wrongAnswers++;
                UpdateWrongText();

                if (isGoodSoul && other.CompareTag("HellDoor"))
                {
                    PlaySound(goodSoulInHellAudio);
                }
                else if (!isGoodSoul && other.CompareTag("HeavenDoor"))
                {
                    PlaySound(badSoulInHeavenAudio);
                }
            }

            gameObject.SetActive(false);
        }
    }

   public void OnButtonPress()
    {
        if (!gameActive)
        {
            Debug.Log("Cannot summon souls: The game is not active.");
            return;
        }

        ResetSoulPosition();
        soulManager.ShowRiddleAndSoul();

        // When the first soul is summoned, set isFirstSoulSummoned to true
        if (!grimGuideButtons.isFirstSoulSummoned)
        {
            grimGuideButtons.FirstSoulSummoned(); // Set isFirstSoulSummoned to true
        }

        Debug.Log("New soul summoned!");
    }

    private void CheckGameOverConditions()
    {
        if (wrongAnswers > 2 || (timer <= 0 && correctAnswers < 15))
        {
            if (!gameOverTriggered)
            {
                TriggerGameOver();
                gameOverTriggered = true;
            }
        }
    }

    private void CheckWinConditions()
    {
        if (correctAnswers >= 15 && wrongAnswers <= 2)
        {
            if (!winTriggered)
            {
                TriggerWin();
                winTriggered = true;
            }
        }
    }

    private void TriggerGameOver()
    {
        StopAllSounds();
        PlaySound(gameOverAudio);
        gameOverPanel.SetActive(true);
        grabInteractable.enabled = false;
        soulRigidbody.isKinematic = true;

        if (bookCanvas != null)
        {
            bookCanvas.SetActive(false); // Disable the book canvas
        }
    }

    private void TriggerWin()
    {
        StopAllSounds();
        PlaySound(winAudio);
        winPanel.SetActive(true);
        grabInteractable.enabled = false;
        soulRigidbody.isKinematic = true;

        if (bookCanvas != null)
        {
            bookCanvas.SetActive(false); // Disable the book canvas
        }
    }

    public void StopAllSounds()
    {
        StopSound(correctPlacementAudio);
        StopSound(badSoulInHeavenAudio);
        StopSound(goodSoulInHellAudio);
        StopSound(missedThrowAudio);
        StopSound(gameOverAudio);
        StopSound(winAudio);
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            StopSound(audioSource);
            audioSource.Play();
        }
    }

    private void StopSound(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void RestartGame()
    {
        wrongAnswers = 0;
        correctAnswers = 0;
        score = 0;
        timer = 60f;
        UpdateScoreText();
        UpdateCorrectText();
        UpdateWrongText();
        UpdateTimerText();

        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        ResetSoulPosition();

        gameOverTriggered = false;
        winTriggered = false;

        if (bookCanvas != null)
        {
            bookCanvas.SetActive(true); // Re-enable the book canvas
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private IEnumerator ResetMissedFlag()
    {
        yield return new WaitForSeconds(1f);
        hasMissed = false;
    }
}
