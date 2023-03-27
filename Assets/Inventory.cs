using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour
{
    public GameObject prefabItem;
    public GameObject ResourceInventoryPrefabItem;
    public GameObject pickaxeObj;

   
    public GameObject inventory;
    
    public GameObject shop;
    public GameObject enchant;
    
    private Shop activeShop;

    public GameObject storage;

    public bool active = false;

    public Button button;

    private void Start()
    {
        activeShop = shop.GetComponent<Shop>();
        button.onClick.AddListener(changeActive);
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.B)) {
           changeActive();
        }
        
    }
    private void OnClick()
    {
        changeActive();
        lengthStorage();
    }
    private void changeActive() {
        changeBool();

        inventory.SetActive(active);
        if(shop.activeSelf && active) {
            // activeShop.active = !shop.activeSelf;
            // Debug.Log(activeShop.active);
            shop.SetActive(false);
        };
        if(enchant.activeSelf && active ) {
            enchant.SetActive(false);
        }
    }

    public void changeBool() {
        active = !active;
    }

    public void lengthStorage() {
        Transform[] lenth = storage.GetComponentsInChildren<Transform>();
        Debug.Log((lenth.Length -3)/3 + 1);
    }

    public void AddItem(string typeItem, string _path,string _name, int _countItem,int damage, int stability,int luck, int _double, int hit) {
        switch (typeItem)
        {
            case "resource": 
                addResource(_path,_name,_countItem);
                break;
            case "pickaxe":
                addPickaxe(_path,_name,damage,stability,luck,_double,hit);
                Debug.Log(1000);
                Debug.Log(_name);
                break;
        }
    }
    int _index = 0;
    private void addPickaxe(string _path,string _name,int _damage,int _stability,int _luck,int _double, int _hit) {
        GameObject newPickaxe = Instantiate(prefabItem) as GameObject;
        newPickaxe.transform.parent = storage.transform;
        newPickaxe.name = _name;

        // Image image =  newPickaxe.GetComponentsInChildren   <Image>();
        Image[] images = newPickaxe.GetComponentsInChildren<Image>();
        pickaxeItem item = newPickaxe.GetComponent<pickaxeItem>();
        
        item.pickaxe = pickaxeObj.GetComponent<Pickaxe>();
        item.pickaxeObj = pickaxeObj;
        
        item.name = _name;
        item.storage = storage;

        item.luck = _luck;
        item._double = _double;
        item.hit = _hit;

        item.damage = _damage;
        item.currentStability = _stability;

        _index++;
        item.index = _index;

        byte[] fileData = System.IO.File.ReadAllBytes(_path);
        Texture2D newTexture = new Texture2D(2, 2);
        newTexture.LoadImage(fileData);
        images[1].sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
        item.texture = newTexture;
    }
    private void addResource(string _path,string _name, int _countItem) {
        Transform searchResource = storage.transform.Find(_name);
        if (searchResource != null) {
            GameObject Resource = searchResource.gameObject;
            TextItem foundResource = Resource.GetComponentInChildren<TextItem>();
            int currentCount = int.Parse(foundResource.myTextMeshPro.text) + _countItem;
            foundResource.ChangeText("" + currentCount);
            // Объект найден, делайте что-то с ним здесь.
        } else {
            byte[] fileData = System.IO.File.ReadAllBytes(_path);
            GameObject InstantResources = Instantiate(ResourceInventoryPrefabItem) as GameObject;
            InstantResources.transform.parent = storage.transform;
            InstantResources.name = _name;
            
            // InstantResources
            Image image =  InstantResources.GetComponentInChildren<Image>();
            TextMeshPro textMashPro = InstantResources.GetComponentInChildren<TextMeshPro>();
            TextItem itemText = InstantResources.GetComponentInChildren<TextItem>();
            // TextMeshProUGUI text = textMashPro;
            Texture2D newTexture = new Texture2D(2, 2);
            newTexture.LoadImage(fileData);
            image.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
            itemText.ChangeText(""+_countItem);
        }
    }
}
