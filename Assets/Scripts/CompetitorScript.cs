using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> This script manages the behaviors of the competitor <summary>
/// <summary> All competitor prefabs <summary>

public class CompetitorScript : MonoBehaviour
{
    #region Constants
    const float runningVelocity = 0.04f;
    #endregion

    #region Properties
    /* Light color support */
    private GameObject redGreenLightGameObject;

    /* Required Components */
    // Animation support
    private Animator animatorComponent;
    // Movement support
    private Transform transformComponent;

    /* Competitor States */
    private int competitorState = 0;
    public int CompetitorState { set { competitorState = value; } }

    /* Light actions support */
    private Color previousFrameLightColor;
    private Color currentFrameLightColor;

    /* After finish line support */
    private Transform pistolGripTransformComponent;
    #endregion

    #region Methods

    private void Awake() {
        // Assign the transform component of the pistol grip
        pistolGripTransformComponent = GameObject.Find("PistolGrip").GetComponent<Transform>();
    }

    void Start()
    {
        // Assign the game objects to the properties
        redGreenLightGameObject = GameObject.FindWithTag("LightGameObject");

        // Assign the components to the properties
        animatorComponent = gameObject.GetComponent<Animator>();
        transformComponent = gameObject.GetComponent<Transform>();

        // Assign light color of the first frame initially
        previousFrameLightColor = redGreenLightGameObject.GetComponent<Image>().color;

        // If the light color is green when the competitor was born
        if (previousFrameLightColor == Color.green) {
            // Then, the competitor will start running
            competitorState = 1;
            animatorComponent.SetInteger("StateNumber", 1);
        }
    }

    void FixedUpdate()
    {
        /* DO THE NECESSARY THINGS ACCORDING TO COMPETITOR STATE */
        // If the competitor is in the idle state
        if (competitorState == 0) {/* Do nothing */}

        // If the competitor state is fast run
        else if (competitorState == 1) {
            // Then, make it move forward
            transformComponent.Translate(-0.04f * transformComponent.forward);
        }
        // If the competitor state is dead
        else if (competitorState == 2) {/* Do nothing*/}

        /* LIGHT ACTIONS */
        // Save the light color in this frame
        currentFrameLightColor = redGreenLightGameObject.GetComponent<Image>().color;
        // If the light has changed from green to red
        if ((previousFrameLightColor == Color.green) && (currentFrameLightColor == Color.red)) {
            if (competitorState != 2) {
                int mistakePercentage = Random.Range(0, 10);
                // If the competitor will make a mistake
                if (mistakePercentage < 3) {
                    // Then, he will keep running
                    competitorState = 1;
                    animatorComponent.SetInteger("StateNumber", 1);
                }
                // If the competitor will not make a mistake
                else {
                    // Then, he will pass to idle state
                    competitorState = 0;
                    animatorComponent.SetInteger("StateNumber", 0);
                }
            }
        }
        // If the light has changed from red to green
        else if ((previousFrameLightColor == Color.red) && (currentFrameLightColor == Color.green)) {
            // If the competitor is not in the process of dying at the moment
            if (competitorState != 2) {
                // Then, the competitor will absolutely start running
                competitorState = 1;
                animatorComponent.SetInteger("StateNumber", 1);
            }
        }
        // At the end of this frame, current frame light color will become previous frame for the next frame
        previousFrameLightColor = currentFrameLightColor;


        /* AFTER FINISH LINE ACTIONS */
        // If the competitor has crossed the finish line
        if (transform.position.z - pistolGripTransformComponent.position.z <= 7f) {
            // Destroy it
            Destroy(gameObject, 0.2f);
        }
    }
    #endregion




}
