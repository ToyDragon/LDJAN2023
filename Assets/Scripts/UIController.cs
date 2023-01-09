using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    CardController hoveredCard;
    private GameObject vampire;

    public List<Material> cardMaterials = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        vampire = GameObject.Find("Vampire");
        XPLevelController.instance.onLevelUp += OnLevelUp;
        ShowCards(false);
    }

    void OnLevelUp(){
        int level = XPLevelController.instance.level;
        if(level == 5){
            GameState.selectingUpgrade = true;
            SetCardModsAndMaterials(1);
            ShowCards(GameState.selectingUpgrade);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.SuppressUpdates() && !GameState.paused){
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
            //Debug.Log(card.gameObject.name);
            CardController controller = card.GetComponentInChildren<CardController>();
            controller.mod = mods[i];
            card.transform.GetChild(0).GetComponent<MeshRenderer>().material = cardMaterials[mods[i].materialIndex];
        }
    }

    public void ShowCards(bool value){
        var cards = GetComponentsInChildren<CardController>(true);
        foreach(var card in cards){
            Debug.Log(card.transform.parent.gameObject.name);
            card.transform.parent.gameObject.SetActive(value);
        }
    }

    public void SelectCard(CardController card){
        AttackMods mods = vampire.GetComponent<AttackMods>();
        mods.ApplyMod(card.mod);
        ShowCards(false);
        GameState.selectingUpgrade = false;
    }
}
