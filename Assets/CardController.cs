using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameState.selectingUpgrade) Highlight(false);
    }

    public void Highlight(bool value){
        GameObject outline = transform.parent.GetChild(1).gameObject;
        outline.SetActive(value);
    }
}
