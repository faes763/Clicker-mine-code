using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickaxeItem : MonoBehaviour
{
    public int damage;
    public int currentStability;
    public int luck;
    public int _double;
    public int hit;

    public int index;

    public GameObject pickaxeObj;
    public Pickaxe pickaxe;
    private Button button;
    public Texture2D texture;

    public GameObject storage;

    void Start() {
        
        button = gameObject.GetComponentInChildren<Button>();
        pickaxe = pickaxeObj.GetComponent<Pickaxe>();
        Debug.Log(pickaxe);
        button.onClick.AddListener(changeParamPick);
        Debug.Log(pickaxe.name);

    }
    void changeParamPick() {
        
        pickaxeItem[] items=storage.GetComponentsInChildren<pickaxeItem>();
        foreach (pickaxeItem item in items)
        {
            if(item.index == pickaxe.currentIndex) {
                item.currentStability = pickaxe.currentStability;
                item.damage = pickaxe.damage;
                item.luck = pickaxe.luck;
                item._double = pickaxe._double;
                item.hit = pickaxe.hitStability;
                break;
            }
        }

            pickaxe.currentIndex = index;
            pickaxe.changeParam(damage,currentStability,texture,luck,_double,hit);
    }
}
