using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary> This script changes the color of the light <summary>
///<summary> RedGreenLight <summary>

public class RedGreenLightScript : MonoBehaviour
{

    #region Constants
    const float gameStartingTime = 4f;
    const float minSwitchTime = 5f;
    const float maxSwitchTime = 10f;
    #endregion

    #region Properties
    private Timer timer;
    private Image RedGreenLightImageComponent;
    private AimScript mainCameraAimScriptComponent;
    #endregion

    #region Methods
    void Start()
    {
        // Add timer as a component of RedGreenLight game object
        timer = gameObject.AddComponent<Timer>();

        // Assign the image component of the game object to RedGreenLightImageComponent
        RedGreenLightImageComponent = gameObject.GetComponent<Image>();

        // Assign AimScript component of the Main Camera
        mainCameraAimScriptComponent = GameObject.Find("Main Camera").GetComponent<AimScript>();

        // Set the duration of the timer to 4 seconds for the first time (Game will start 4 seconds later)
        timer.Duration = gameStartingTime;

        // Start the timer for the first time
        timer.Run();
    }

    void FixedUpdate()
    {
        if (timer.Finished) {
            // Set the duration of the timer to a random time amount
            float randomDuration = Random.Range(minSwitchTime, maxSwitchTime);
            timer.Duration = randomDuration;

            /* CHANGE THE COLOR */
            // If the color is red currently
            if (RedGreenLightImageComponent.color == Color.red) {
                // Make the color green
                RedGreenLightImageComponent.color = Color.green;
                // Give the camera the command to turn back
                mainCameraAimScriptComponent.IsFront = false;
                /* THİS İS THE POİNT THAT THE TURNİNG BACK PROCESS HAS STARTED */
                // Since the camera has just started turning, isTurning should be true;
                mainCameraAimScriptComponent.IsTurning = true;
                // Therefore, endRot should be assigned here
                // End rotation is the rotation of backside game object
                mainCameraAimScriptComponent.EndRot = GameObject.Find("BackSide").transform.eulerAngles;
                // Rotate the main camera
                mainCameraAimScriptComponent.RotateBack();



            }
            // If the color is green currently
            else {
                // Make the color red
                RedGreenLightImageComponent.color = Color.red;
                // Give the camera the command to turn front
                mainCameraAimScriptComponent.IsFront = true;
                /* THİS İS THE POİNT THAT THE TURNİNG FRONT PROCESS HAS STARTED */
                // Since the camera has just started turning, isTurning should be true;
                mainCameraAimScriptComponent.IsTurning = true;
                // Therefore, endRot should be assigned here
                // End rotation is the rotation of frontside game object
                mainCameraAimScriptComponent.EndRot = GameObject.Find("FrontSide").transform.eulerAngles;
                // Rotate the main camera
                mainCameraAimScriptComponent.RotateBack();
            }

            // Run the timer
            timer.Run();
        }
    }
    #endregion
}
