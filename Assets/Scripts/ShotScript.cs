using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary> This script manages the shot when shot button is clicked <summary>
/// <summary> PistolGrip <summary>

public class ShotScript : MonoBehaviour
{
    #region BloodParticleSupport
    // Blood support
    private GameObject bloodParticle;

    private void Awake() {
        // Pull the blood particle from resources folder inside assets
        bloodParticle = Resources.Load<GameObject>("Prefabs/BloodExplosion");

        // Save the animator component of PistolGrip
        animatorComponent = gameObject.GetComponent<Animator>();
    }
    #endregion


    #region ShotSupport
    // Bullet support
    const float destroyDelay = 6f;
    const float rayDistance = 800f;
    // collidedObject will keep the information about the object which is shot
    private RaycastHit collidedObject;

    /* When shot button is clicked, we will send a ray from screen to crosshair */
    public void ClickedShotButton() {
        // Print the system message
        Debug.Log("Strike");
        // Flareback the pistol
        FlareBack();
        // Shake the camera
        ShakeCamera();
        // Define ray
        Ray bulletRay = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // If the ray collided with a game object, it returns true. Otherwise, false.
        if (Physics.Raycast(bulletRay, out collidedObject, rayDistance)) {
            // If the ray specifically collided with a competitor
            if (collidedObject.collider.gameObject.tag == "CompetitorTag") {
                // Then, type this info
                Debug.Log("Competitor has been killed");

                // Save the competitor game object
                GameObject killedCompetitorGameObject = collidedObject.collider.gameObject;

                /* Make the killed competitor bloody */
                // Save the hit position
                Vector3 hitPoint = collidedObject.point;
                // Explode the blood
                GameObject currentBlood = Object.Instantiate(bloodParticle, hitPoint, Quaternion.identity);
                
                // Change the competitors state to dead (2)
                killedCompetitorGameObject.GetComponent<CompetitorScript>().CompetitorState = 2;
                // Change the competitors animation clip to dying animation clip (2)
                killedCompetitorGameObject.GetComponent<Animator>().SetInteger("StateNumber", 2);
                // Destroy the competitor from the scene
                Destroy(killedCompetitorGameObject, destroyDelay);
            }
        }
    }
    #endregion


    #region CameraShakeSupport
    private void Start() {
        // Activate DOTween package
        DOTween.Init();
    }
    const float shakeTime = 0.4f;
    const float shakePower = 1f;
    private void ShakeCamera() {
        // Shake the camera by both rotation and positioning
        Camera.main.DOShakeRotation(shakeTime, shakePower, fadeOut: true);
        Camera.main.DOShakePosition(shakeTime, shakePower, fadeOut: true);
    }
    #endregion



    /* FLAREBACK ACTIONS */
    #region Properties
    // Animation support (This component will be assigned in Awake method)
    private Animator animatorComponent;
    #endregion
    #region Methods
    // This method runs the shoot animation when pistol is shot
    private void FlareBack() {
        animatorComponent.SetTrigger("ShootTrigger");
    }
    #endregion
}
