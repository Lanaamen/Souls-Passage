# SoulsPassage
 Souls Passage is a VR game where the player takes on the role of the Grim Reaper, tasked with guiding souls to their proper destinations—Heaven or Hell. Through solving riddles, managing time, and precision throwing mechanics, players aim to earn their way back to the land of the living.

Features
Unique Gameplay Mechanics: Solve riddles to determine whether souls are "good" or "bad" and throw them into the correct portals.
Timer and Scoring: A 60-second timer challenges players to score at least 15 points while avoiding more than 2 mistakes.
Interactive VR Elements: Grab, interact with objects, and navigate a fully immersive VR environment.
Dynamic Audio Feedback: Sounds for correct, incorrect, and missed actions enhance the immersive experience.
Game States: Win and Game Over screens dynamically activate based on performance.

Installation
Clone or Download this repository.
Open the project in Unity 2022.3.X (or the version you used during development).
Ensure you have the XR Interaction Toolkit and OpenXR Plugin installed via Unity Package Manager.
Build the project for your target VR platform (e.g., Oculus, SteamVR).

How to Play
Objective: Earn at least 15 points within 60 seconds with no more than 2 wrong answers to win.
Controls:
Grab Souls: Use VR controllers to grab and throw souls.
Throwing: Aim at either the Heaven or Hell portal and release to throw.
Solve Riddles: Read the riddles on the book to determine a soul's moral alignment.
Scoring:
Correct Answer: +1 point
Wrong Answer: -1 point
Missed Throw: Lose 6 seconds on the timer.
Game End:
Win by reaching 15 points with no more than 2 wrong answers.
Lose if time runs out or you exceed 2 mistakes.

/Assets
    /Scripts
        GameManager.cs         // Manages timer, score, and game states
        GrimGuideButtons.cs    // Manages 
        SoulThrow.cs           // Handles soul interactions and throwing mechanics
        SoulManager.cs         // Handles soul spawning and riddles
    /Prefabs
        SoulPrefab             // Prefab for soul objects
        Portals                // Heaven and Hell portals
    /Audio
        CorrectPlacement.wav   // Sound for correct answers
        WrongPlacement.wav     // Sound for wrong answers
        BackgroundMusic.wav    // Ambient background music
    /UI
        WinScreen              // Win screen UI
        GameOverScreen         // Game over screen UI
    /Scenes
        MainScene              // Primary game scene


Certainly! Here's a sample README.md file tailored for your Unity VR game project "Souls Passage". You can customize this further based on your specific needs.

Souls Passage
Souls Passage is a VR game where the player takes on the role of the Grim Reaper, tasked with guiding souls to their proper destinations—Heaven or Hell. Through solving riddles, managing time, and precision throwing mechanics, players aim to earn their way back to the land of the living.

Features
Unique Gameplay Mechanics: Solve riddles to determine whether souls are "good" or "bad" and throw them into the correct portals.
Timer and Scoring: A 60-second timer challenges players to score at least 15 points while avoiding more than 2 mistakes.
Interactive VR Elements: Grab, interact with objects, and navigate a fully immersive VR environment.
Dynamic Audio Feedback: Sounds for correct, incorrect, and missed actions enhance the immersive experience.
Game States: Win and Game Over screens dynamically activate based on performance.
Installation
Clone or Download this repository.
Open the project in Unity 2022.3.X (or the version you used during development).
Ensure you have the XR Interaction Toolkit and OpenXR Plugin installed via Unity Package Manager.
Build the project for your target VR platform (e.g., Oculus, SteamVR).
How to Play
Objective: Earn at least 15 points within 60 seconds with no more than 2 wrong answers to win.
Controls:
Grab Souls: Use VR controllers to grab and throw souls.
Throwing: Aim at either the Heaven or Hell portal and release to throw.
Solve Riddles: Read the riddles on the book to determine a soul's moral alignment.
Scoring:
Correct Answer: +1 point
Wrong Answer: -1 point
Missed Throw: Lose 6 seconds on the timer.
Game End:
Win by reaching 15 points with no more than 2 wrong answers.
Lose if time runs out or you exceed 2 mistakes.

Project Structure

/Assets
    /Scripts
        GameManager.cs         // Manages timer, score, and game states
        SoulThrow.cs           // Handles soul interactions and throwing mechanics
        UIManager.cs           // Controls UI elements for win and game over screens
        SoulManager.cs         // Handles soul spawning and riddles
        LockGrabInteractable.cs // Ensures the scythe remains grabbable but not droppable
        HandAnimationController.cs // Handles hand animations for VR interactions
        ScytheButtonPress.cs   // Enables pressing buttons with the scythe object
        VideoController.cs     // Plays videos on user interaction
        Riddle.cs              // Manages riddles associated with souls
    /Prefabs
        (Various)              // Includes soul objects, portals, and other reusable objects
    /Audio and video
        (Various)              // The lastMemory video and Sound effects for interactions, background music, etc.
    /UI
        WinScreen              // Win screen UI
        GameOverScreen         // Game over screen UI
        BookCanvas             // UI for displaying riddles and sending souls
    /Scenes
        MainScene              // Primary game scene
    /Animation
        HandAnimations         // Animation files for VR hand movements
    /Fonts
        (Various)              // Fonts used for in-game text and UI
    /Materials and images
        (Various)              // Materials applied to game objects and environments. Images for UI and textures
    /Models
        (Various)              // 3D models used in the game, such as the scythe
    /Skybox
        (Various)              // Skybox materials for the game environment
    /Textures
        (Various)              // Textures used for materials and objects


Known Issues
- Player cannot switch what hand holds the scythe

Future Improvements
- Add additional riddles for more replayability.
- Implement difficulty levels (e.g., shorter timers, trickier riddles).
- Include a tutorial mode for first-time players.

Credits
Developer: Lana Amen
Audio Assets:

portal sound
https://pixabay.com/sound-effects/transitional-swipe-3-211685/

AI Voice Samuel
https://play.ht/studio

Ambience audio
https://pixabay.com/sound-effects/horror-rumble-winds-253834/

BookPageAudio
https://pixabay.com/sound-effects/page-turn02-103822/

New soul audio
https://pixabay.com/sound-effects/whoosh-fire-movement-243099/

UI-click sound
https://pixabay.com/sound-effects/ui-click-43196/

Other Contributions:
Scythe
https://assetstore.unity.com/packages/3d/props/tools/tool-set-polypack-207678

Cemetary
https://assetstore.unity.com/packages/3d/environments/stylized-cemetery-pack-56402

Book
https://www.cgtrader.com/items/2794075/download-page

Magic fire (SoulOrb)
https://assetstore.unity.com/packages/vfx/particles/fire-explosions/procedural-fire-141496#publisher

frame
https://assetstore.unity.com/packages/3d/props/interior/picture-frames-with-photos-106907

video background
https://picjumbo.com/

play button
https://www.flaticon.com/free-icon/play-button_6388835

Font
https://ggbot.itch.io/unutterable-font/download/eyJpZCI6MTcwODg1OSwiZXhwaXJlcyI6MTczMTc0NzMxMX0%3d.crSRf6nLeC8kpsJb%2biZV9Bu%2bcMs%3d

Contact
For feedback or inquiries, reach out at: 
LinkedIn: (https://www.linkedin.com/in/lana-amen/)