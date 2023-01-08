using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    CardController hoveredCard;

    public List<Material> cardMaterials = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        ShowCards(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.selectingUpgrade){
            Debug.Log("selecting upgrade");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                Debug.Log("hit an object");
                CardController card = hit.transform.gameObject.GetComponent<CardController>();
                if(card != null){
                    Debug.Log("object was a card");
                    if(hoveredCard != null && hoveredCard != card){
                        //unhighlight previous card, highlight new
                        hoveredCard.Highlight(false);
                        card.Highlight(true);
                        hoveredCard = card;
                    } else if(hoveredCard == null){
                        //highlight new
                        card.Highlight(true);
                        hoveredCard = card;
                    }
                } else if(hoveredCard != null){
                    //unhover hovered card
                    hoveredCard.Highlight(false);
                    hoveredCard = null;
                }
            }
        }
    }

    public void ShowCards(bool value){
        for(int i = 0; i < 3; i++){
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}
