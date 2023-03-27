using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InformatePlayer : MonoBehaviour
{
    public TextMeshProUGUI pin;
    private bool active = false;

    // public string text;
    // void Start()
    // {   
    //     pin = GetComponent<TextMeshProUGUI>();
    // }

    public void infoPlayer(string text) {
        pin.text = text;
        gameObject.SetActive(true);
        // if(!active) {
            // active = !active;
            // gameObject.SetActive(active);
            // Invoke("changeBool",2);
        // }
        // Debug.Log(active);
        
    }
    private void changeBool() {
        active = !active;
        Debug.Log(active);
        Debug.Log("Функция вызвалась");

        gameObject.SetActive(active);
    }
   
}
