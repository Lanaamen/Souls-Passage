using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the Video Player component
    public Button playButton; // Reference to the Play Button UI component

    void Start()
    {
        // Ensure the button is visible at the start
        playButton.gameObject.SetActive(true);

        // Add listener to start video on button click
        playButton.onClick.AddListener(PlayVideo);

        // Add event listeners to show/hide button based on video state
        videoPlayer.started += HideButton;           // Hide when video starts
        videoPlayer.loopPointReached += ShowButton;  // Show again when video finishes
    }

    // Method to play the video and hide the button
    private void PlayVideo()
    {
        videoPlayer.Play();
    }

    // Hide the play button
    private void HideButton(VideoPlayer vp)
    {
        playButton.gameObject.SetActive(false);
    }

    // Show the play button when video finishes
    private void ShowButton(VideoPlayer vp)
    {
        playButton.gameObject.SetActive(true);
    }
}
