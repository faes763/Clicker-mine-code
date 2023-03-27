using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enhancement : MonoBehaviour
{
    public Pickaxe pickaxe;
    public GameObject storage;
    private Button button;
    private TextMeshProUGUI textItem;
    public InformatePlayer infoPlayer;


    void Start() {
        button = gameObject.GetComponent<Button>();
        textItem = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(enchant);
    }



    public void enchant() {
        int price = int.Parse(textItem.text);
        string name = textItem.gameObject.name;
        enchantPickaxe(gameObject.name,name,price,1);
    }
    



    public void enchantPickaxe(string _enchant,string _name,int _price,int _enchantParam) {
        
        Transform searchResource = storage.transform.Find(_name);
        if(searchResource != null) {
            GameObject Resource = searchResource.gameObject;
            TextItem foundResource = Resource.GetComponentInChildren<TextItem>();
            if(_price <= int.Parse(foundResource.myTextMeshPro.text)) {
                Debug.Log(_price);
                Debug.Log(foundResource.myTextMeshPro.text);
                bool activeDouble = true;
                bool activeHit = true;
                switch (_enchant)
                {
                    case "damage":
                        pickaxe.damage+=_enchantParam;
                        foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";
                        pickaxe.PickaxeDamage.text = pickaxe.damage + "";
                        Debug.Log("Урон кирки увеличен!");
                        break;
                    case "stability":
                        if(pickaxe.currentIndex != 0) {
                            pickaxe.currentStability+=_enchantParam;
                            pickaxe.PickaxeStability.text = pickaxe.currentStability + "";
                            foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";
                        }
                        break;
                    case "luck":
                        pickaxe.luck+=_enchantParam;
                        foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";
                        break;
                    case "double":
                        pickaxe._double = 1;
                        if(pickaxe._double == 1) activeDouble = false;
                        gameObject.SetActive(activeDouble);
                        foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";

                        break;
                    case "hit":
                        pickaxe.hitStability = 1;
                        if(pickaxe.hitStability == 1) activeHit = false;
                        gameObject.SetActive(activeHit);
                        foundResource.myTextMeshPro.text=int.Parse(foundResource.myTextMeshPro.text) - _price + "";

                        break;   
                }
            } else {
                infoPlayer.infoPlayer("Недостаточно ресурса");
            }
        }else {
            infoPlayer.infoPlayer("У вас нет этого ресурса");
        }
        
    }
}
