using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enchantHUD : MonoBehaviour
{
    
    public GameObject enchant;
    public GameObject shop;
    public GameObject inventory;
    public Button button;
    public bool active;
    void Start()
    {
        button.onClick.AddListener(changeActive);
    }
    private void OnClick()
    {
        changeActive();
    }
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyUp(KeyCode.Z)) {
            changeActive();
        }
    }
    void changeBool() {
        active=!active;
    }
    private void changeActive() {
        changeBool();
        enchant.SetActive(active);
        if(inventory.activeSelf && active ) {
            // inv.active = !inventory.activeSelf;
            inventory.SetActive(false);
        }
        if(shop.activeSelf && active ) {
            shop.SetActive(false);
        }
    }
}
