using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameChooserBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void SetText(string text) {
        this.text.text = text;
    }

    public void NamePressed() {
        GameObject parent = transform.parent.gameObject;
        Debug.LogWarning(parent.name);
        parent.GetComponent<PlayerNamesHolderBehaviour>().SetPlayerChose(text.text);
    }

    
}
