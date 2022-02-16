using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary> This script is responsible for aiming of the player <summary>
/// <summary> Main Camera <summary>

public class AimScript : MonoBehaviour
{
    #region Fields
    // Game starting time constant
    const float gameStartingTime = 4f;
    // This is the speed of the rotation when the user aims. It may be thought as mouse sensitivity
    const float rotationSpeed = 0.1f;
    // This is for determining whether the user is sliding his/her finger on the screen
    private bool previousFrameIsPressed = false;
    private bool currentFrameIsPressed;
    // This is for determining the locations of the users finger on the screen
    private Vector3 previousFrameMousePositionInPixels;
    private Vector3 currentFrameMousePositionInPixels;
    // This is for saving the main camera rotation for aiming in that particular frame
    private Vector3 displacementVectorInPixels;
    // This is for the direction of the pistol. Pistol's direction should be same as main camera direction
    private GameObject PistolGripGameObject;
    // This will become true after 3 seconds, when the game actually starts.
    private bool isStarted = false;
    public bool IsStarted {set {isStarted = value;}}
    #endregion

    #region Main Methods
    private void Awake() {
        // Assign PistolGripGameObject
        PistolGripGameObject = GameObject.Find("PistolGrip");
    }

    private void Start() {
        // Activate DOTween package
        DOTween.Init();

        // After 4 seconds, the game will start and the CompetitorSpawner script will be active
        Invoke("StartGame", gameStartingTime);
        Invoke("SetSpawnerActive", gameStartingTime);
    }

    private void FixedUpdate()
    {
        /* AIM ACTIONS */
        // The user can move only when camera is looking at front, game has started and camera is not turning
        if ((isFront == true) && (isStarted == true) && (isTurning == false)) {
            // Check whether the user is touching to the screen in the current frame
            currentFrameIsPressed = Input.GetMouseButton(0);
    
            // if the user has been touching to the screen
            if ((previousFrameIsPressed == true) && (currentFrameIsPressed == true)) {
                // Save the screen location of touching in the current frame
                currentFrameMousePositionInPixels = Input.mousePosition;
    
                // Save the displacement vector for this frame
                displacementVectorInPixels = currentFrameMousePositionInPixels - previousFrameMousePositionInPixels;
                // You need to make an arrangement for the displacementVector about its rotational axes
                displacementVectorInPixels = new Vector3(-displacementVectorInPixels.y, displacementVectorInPixels.x, 0);
                // You need to arrange its magnitude
                displacementVectorInPixels = rotationSpeed * displacementVectorInPixels;
    
                // Rotate the Main Camera properly
                gameObject.GetComponent<Transform>().Rotate(displacementVectorInPixels);
    
                // Rotate the Flare Gun properly
                PistolGripGameObject.GetComponent<Transform>().Rotate(displacementVectorInPixels);
    
                /* INFORMATION FOR THE NEXT FRAME */
                // Save that the user has just touch the screen for the next frame
                previousFrameIsPressed = true;
                // Assign the currentFrameMousePositionInPixels to previousFrameMousePositionInPixels for the next frame
                previousFrameMousePositionInPixels = currentFrameMousePositionInPixels;
            }
    
            // If the user has been just started to touch to the screen
            else if ((previousFrameIsPressed == false) && (currentFrameIsPressed == true)) {
                // Save the screen location of touching in the current frame
                currentFrameMousePositionInPixels = Input.mousePosition;
    
                /* INFORMATION FOR THE NEXT FRAME */
                // Save that the user has just touch the screen for the next frame
                previousFrameIsPressed = true;
                // Assign the currentFrameMousePositionInPixels to previousFrameMousePositionInPixels for the next frame
                previousFrameMousePositionInPixels = currentFrameMousePositionInPixels;
            }
    
            // If the user has just finished to touch to the screen
            else if ((previousFrameIsPressed == true) && (currentFrameIsPressed == false)) {
                /* INFORMATION FOR THE NEXT FRAME */
                previousFrameIsPressed = false;
                // Then, shot
                PistolGripGameObject.GetComponent<ShotScript>().ClickedShotButton();
            }
    
            // If the user is not touching to the screen at all
            else {
                /* INFORMATION FOR THE NEXT FRAME */
                previousFrameIsPressed = false;
            }
        }

        /* TURN BACK AND FORTH ACTIONS ACCORDING TO THE LIGHT COLOR */
        // If the camera is turning at the moment
        if (isTurning) {
            // Pistol should also turn
            PistolGripGameObject.transform.rotation = transform.rotation;
            
            // If the turning process has just finished
            if (transform.eulerAngles == endRot) {
                // Then, the user may move the aim anymore
                isTurning = false;
            }
        }
    }
    #endregion

    /* TURN BACK AND FRONT ACTIONS ACCORDING TO THE LIGHT COLOR */
    #region RotationBackAndForthFields
    // End direction should be determined by RedGreenLightScript at the beginning of the turning process
    private Vector3 endRot;
    public Vector3 EndRot {set {endRot = value;}}
    // True if camera is looking at front, false if camera looking at back
    private bool isFront = true;
    public bool IsFront {
        get {return isFront;}
        set {isFront = value;}
    }
    // True if the camera is still turning, false if camera is not turning right now
    private bool isTurning = false;
    public bool IsTurning {set {isTurning = value;}}
    // Turning speed
    private const float turningSpeed = 1f;
    #endregion

    #region GameStarterMethods
    // This method will be called by Start method after 3 seconds from game starting
    // It will start the game
    public void StartGame() {
        isStarted = true;
    }
    // This method will be called by Start method after 3 seconds from game starting
    // This function sets the CompetitorSpawner script active
    public void SetSpawnerActive() {
        gameObject.GetComponent<CompetitorSpawner>().enabled = true;
    }
    #endregion

    // This method rotates the main camera back and forth
    public void RotateBack() {
        gameObject.GetComponent<Transform>().DORotate(endRot, turningSpeed);
    }
}
