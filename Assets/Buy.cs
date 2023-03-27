using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buy : MonoBehaviour
{
    private Button button;
    public Shop shop;

    private int price;
    private string resource;

    private TextMeshProUGUI textItem;
    private TextMeshProUGUI nameItem;
    private Image image;
    

    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Shop");
        GameObject nameObj = GameObject.Find("Name");
        TextMeshProUGUI[] text = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log(text[1].gameObject.name);

        nameItem = text[0];
        textItem = text[1];

        image = gameObject.GetComponentInChildren<Image>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(buy);
    }
    

    

    public void buy() {
        
        string name = textItem.gameObject.name;
        string[] spritePickaxe = System.IO.Directory.GetFiles("Assets/Image/Pickaxe",nameItem.text + ".png");
        int price = int.Parse(textItem.text);
        int damage = 0;
        int stability = 0;
        Debug.Log(spritePickaxe[0]);
        Debug.Log(nameItem.text);

        if(nameItem.text == "scarlet pickaxe") {
            damage=7;
            stability =35;
        }
        if(nameItem.text == "gold pickaxe") {
            damage=15;
            stability =50;
        }
        if(nameItem.text == "crystal pickaxe") {
            damage=30;
            stability =75;
        }
        if(nameItem.text == "malachite pickaxe") {
            damage=45;
            stability =85;
        }
         if(nameItem.text == "fluorite pickaxe") {
            damage=65;
            stability =105;
        }
        
        shop.buyItem(price,spritePickaxe[0],name,nameItem.text,damage,stability);
    }
}
