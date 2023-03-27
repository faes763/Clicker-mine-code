using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextItem : MonoBehaviour
{
    public TextMeshProUGUI myTextMeshPro;
    public void ChangeText(string newText)
    {
        myTextMeshPro.text = newText; 
    }

}
