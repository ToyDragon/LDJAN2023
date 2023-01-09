using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTextOnActivate : MonoBehaviour
{
    void OnEnable(){
        GetComponent<TMPro.TMP_Text>().text = StatManager.GenerateStatText();
    }
}
