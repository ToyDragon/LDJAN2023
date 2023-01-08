using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Mod mod = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!GameState.selectingUpgrade) Highlight(false);
    }

    void OnEnable(){
        if(transform.parent.childCount > 1) transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void Highlight(bool value){
        if(transform.parent.childCount <= 1) return;
        GameObject outline = transform.parent.GetChild(1).gameObject;
        outline.SetActive(value);
    }
}
