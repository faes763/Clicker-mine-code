using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickaxe : MonoBehaviour
{
    public int damage = 1;

    public int maxStability = 10;
    public int currentStability = 0;

    public int luck =0;
    public int _double = 0;
    public int hitStability = 0;

    public int currentIndex = 0;
    
    public Text PickaxeDamage;
    public Text PickaxeStability;
    
    private Image image;
    private Sprite defImage;

    public GameObject enchantPick;
    public GameObject storage;
    

    void Start() {
        image = GetComponent<Image>();
        defImage = image.sprite;
        currentStability = maxStability;
        checkStability();
    }


    public void checkStability() {
        PickaxeDamage.text = "" + damage;
        PickaxeStability.text = "" + currentStability;
        if(currentStability<=0) {
            // Destroy(gameObject);
            destroyPickaxe(currentIndex);
        }
    }
    public void changeParam(int _damage,int _currentStability,Texture2D texture,int _luck,int _two, int _hit) {
        damage = _damage;
        maxStability = _currentStability;
        currentStability = maxStability;
        luck = _luck;
        _double = _two;
        hitStability = _hit;
        PickaxeDamage.text = "" + damage;
        PickaxeStability.text = "" + currentStability;
        bool activeDouble = true;
        bool activeHit = true;
        if(_hit == 1) activeHit = false;
        if(_double == 1) activeDouble = false;
        
        
        Transform hit = enchantPick.transform.Find("hit");
        Transform two = enchantPick.transform.Find("double");
        hit.gameObject.SetActive(activeHit);
        two.gameObject.SetActive(activeDouble);
        if(texture == null) {
            image.sprite = defImage;
        }else {
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }   
    }
    public void destroyPickaxe(int _index) {
        pickaxeItem[] items=storage.GetComponentsInChildren<pickaxeItem>();
        foreach (pickaxeItem item in items)
        {
            if(item.index == 0) {
                changeParam(item.damage,item.currentStability,null,item.luck,item._double,item.hit);
                Debug.Log(item.currentStability);
            }
            if(item.index == _index) {
                Destroy(item.gameObject);
                break;
            }
        }
    }
}
