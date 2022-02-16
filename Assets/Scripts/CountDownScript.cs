using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary> This script counts down at the beginning of the game <summary>
/// <summary> CountDown <summary>

public class CountDownScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Countdown(3));
    }
   
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
       
        while (count > -1) {
           
            // Reduce the number on the screen
            gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();

            yield return new WaitForSeconds(1);
            count --;
        }

        // After 4 seconds destroy the CountDown game object
        Destroy(gameObject);
    }
}
