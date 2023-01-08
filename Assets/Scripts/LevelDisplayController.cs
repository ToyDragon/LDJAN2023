using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayController : MonoBehaviour
{
    private TMPro.TMP_Text textEle;
    void OnEnable() {
        textEle = GetComponent<TMPro.TMP_Text>();
    }
    void Update() {
        textEle.SetText(XPLevelController.instance.level.ToString());
    }
}
