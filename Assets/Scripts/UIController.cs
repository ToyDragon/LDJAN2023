using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    CardController hoveredCard;
    public GameObject vampire;

    public List<Material> cardMaterials = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        ShowCards(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)){
            GameState.selectingUpgrade = !GameState.selectingUpgrade;
            SetCardModsAndMaterials(1);
            ShowCards(GameState.selectingUpgrade);
        }


        if(GameState.selectingUpgrade){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                CardController card = hit.transform.GetComponent<CardController>();
                if(card != null){
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
            if(Input.GetMouseButtonDown(0) && hoveredCard != null){
                Debug.Log("Selecting card - " + hoveredCard.gameObject.name);
                SelectCard(hoveredCard);
            }
        }
    }

    public void SetCardModsAndMaterials(int level){
        Mod[] mods = ModPool.GetModsForLevel(level);
        for(int i = 0; i < 3; i++){
            GameObject card = transform.GetChild(i).gameObject;
            Debug.Log(card.gameObject.name);
            CardController controller = card.GetComponentInChildren<CardController>();
            controller.mod = mods[i];
            card.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[mods[i].materialIndex];
        }
    }

    public void ShowCards(bool value){
        for(int i = 0; i < 3; i++){
            GameObject card = transform.GetChild(i).gameObject;
            card.SetActive(value);
        }
    }

    public void SelectCard(CardController card){
        AttackMods mods = vampire.GetComponent<AttackMods>();
        mods.ApplyMod(card.mod);
        ShowCards(false);
        GameState.selectingUpgrade = false;
    }
}
