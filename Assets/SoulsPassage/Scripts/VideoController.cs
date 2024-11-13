using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;               // Reference to the Video Player component
    public Button playButton;                     // Reference to the Play Button UI component
    public Renderer videoScreenRenderer;          // Reference to the screen's Renderer
    public Material placeholderMaterial;          // Placeholder material to display initially
    public Material videoMaterial;                // Material for when the video is playing

    void Start()
    {
        // Set the initial material to the placeholder
        videoScreenRenderer.material = placeholderMaterial;
        
        // Show the play button at the start
        playButton.gameObject.SetActive(true);

        // Add listener to start video on button click
        playButton.onClick.AddListener(PlayVideo);

        // Add event listeners to show/hide button based on video state
        videoPlayer.started += OnVideoStarted;           // Change material and hide button when video starts
        videoPlayer.loopPointReached += OnVideoEnded;    // Show button and reset material when video finishes
    }

    // Method to play the video
    private void PlayVideo()
    {
        videoPlayer.Play();
    }

    // Method to handle video start
    private void OnVideoStarted(VideoPlayer vp)
    {
        // Switch the screen material to the video material
        videoScreenRenderer.material = videoMaterial;

        // Hide the play button
        playButton.gameObject.SetActive(false);
    }

    // Method to handle video end
    private void OnVideoEnded(VideoPlayer vp)
    {
        // Switch back to the placeholder material
        videoScreenRenderer.material = placeholderMaterial;

        // Show the play button again
        playButton.gameObject.SetActive(true);
    }
}
