using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Mod mod = null;
    // Start is called before the first frame update
    private Outlineify outlineify;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!GameState.selectingUpgrade) Highlight(false);
    }

    void OnEnable(){
        outlineify = GetComponentInParent<Outlineify>();
        outlineify.enabled = false;
    }

    public void Highlight(bool value){
        outlineify.enabled = value;
    }
}
