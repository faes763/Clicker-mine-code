using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject inventory;
    public GameObject storage;
    public InformatePlayer infoPlayer;

    public Inventory inv;
    public GameObject shop;
    public GameObject enchant;
    public bool active = false;

    public Button button;

    private void Start()
    {
        // inv = inventory.GetComponent<Inventory>();
        // Debug.Log(inv);
        button.onClick.AddListener(changeActive);
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.C)) {
           changeActive();
        }
        
    }
    private void OnClick()
    {
        changeActive();
    }

    public void changeBool() {
        active = !active;
    }
    private void changeActive() {
        changeBool();
        shop.SetActive(active);
        if(inventory.activeSelf && active) {
            // inv.active = !inventory.activeSelf;
            inventory.SetActive(false);
        }
         if(enchant.activeSelf && active ) {
            enchant.SetActive(false);
        }
    }

    public bool buyItem(int _price,string _path, string _name,string _nameItem,int _damage, int _stability) {
        Transform searchResource = storage.transform.Find(_name);
        
        Debug.Log(_price);
        Debug.Log(_name);
        if(searchResource != null) {
            Debug.Log("Объект найден");
            
            GameObject Resource = searchResource.gameObject;
            TextItem foundResource = Resource.GetComponentInChildren<TextItem>();
            if(_price <= int.Parse(foundResource.myTextMeshPro.text)) {
                Debug.Log("Объект куплен");
                foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";
                inv.AddItem("pickaxe", _path,_nameItem, 0,_damage,_stability,0,0,0);
                return true;
            }else {
                infoPlayer.infoPlayer("Недостаточно ресурса");
                return false;
            }
        } else {
            infoPlayer.infoPlayer("У вас нет этого ресурса");
            return false;
        }
    }
    
}
