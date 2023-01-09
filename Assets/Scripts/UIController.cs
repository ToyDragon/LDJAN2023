using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    CardController hoveredCard;
    public List<Sprite> sprites = new List<Sprite>();
    private GameObject vampire;
    public GameObject cardObjLeft;
    public GameObject cardObjMiddle;
    public GameObject cardObjRight;
    private CardController cardControllerLeft;
    private CardController cardControllerMiddle;
    private CardController cardControllerRight;
    private TMPro.TMP_Text cardTextLeft;
    private TMPro.TMP_Text cardTextMiddle;
    private TMPro.TMP_Text cardTextRight;
    public List<Material> cardMaterials = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        vampire = GameObject.Find("Vampire");
        XPLevelController.instance.onLevelUp += OnLevelUp;
        cardControllerLeft = cardObjLeft.GetComponentInChildren<CardController>();
        cardControllerMiddle = cardObjMiddle.GetComponentInChildren<CardController>();
        cardControllerRight = cardObjRight.GetComponentInChildren<CardController>();
        cardTextLeft = GameObject.Find("CardText-Left").GetComponent<TMPro.TMP_Text>();
        cardTextMiddle = GameObject.Find("CardText-Middle").GetComponent<TMPro.TMP_Text>();
        cardTextRight = GameObject.Find("CardText-Right").GetComponent<TMPro.TMP_Text>();
        Debug.Log("Init texts " + (cardTextLeft) + " " + (cardTextMiddle) + " " + (cardTextRight));
        ShowCards(false);
    }

    void OnLevelUp(){
        int level = XPLevelController.instance.level;
        GameState.selectingUpgrade = true;
        SetCardModsAndMaterials(level);
        ShowCards(GameState.selectingUpgrade);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.selectingUpgrade) {
            float offsetForImg = -.4f;
            cardTextLeft.transform.position = Camera.main.WorldToScreenPoint(cardObjLeft.transform.position + (((cardControllerLeft.mod.imageIndex > 0) ? offsetForImg : 0) * cardObjLeft.transform.up));
            cardTextMiddle.transform.position = Camera.main.WorldToScreenPoint(cardObjMiddle.transform.position + (((cardControllerMiddle.mod.imageIndex > 0) ? offsetForImg : 0) * cardObjMiddle.transform.up));
            cardTextRight.transform.position = Camera.main.WorldToScreenPoint(cardObjRight.transform.position + (((cardControllerRight.mod.imageIndex > 0) ? offsetForImg : 0) * cardObjRight.transform.up));

            float scale = (Camera.main.WorldToScreenPoint(cardObjLeft.transform.position) - Camera.main.WorldToScreenPoint(cardObjMiddle.transform.position)).magnitude / 10f;
            cardTextLeft.fontSize = cardTextMiddle.fontSize = cardTextRight.fontSize = scale;
            cardTextLeft.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scale * 7f);
            cardTextMiddle.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scale * 7f);
            cardTextRight.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scale * 7f);

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
                //Debug.Log("Selecting card - " + hoveredCard.gameObject.name);
                SelectCard(hoveredCard);
            }
        }
    }

    public void SetCardModsAndMaterials(int level){
        Mod[] mods = ModPool.GetModsForLevel(level);
        cardControllerLeft.mod = mods[0];
        cardTextLeft.SetText(mods[0].disp);
        var leftImg = cardTextLeft.GetComponentInChildren<UnityEngine.UI.Image>();
        leftImg.enabled = mods[0].imageIndex > 0;
        if (mods[0].imageIndex > 0) { leftImg.sprite = sprites[mods[0].imageIndex - 1]; }

        cardControllerMiddle.mod = mods[1];
        cardTextMiddle.SetText(mods[1].disp);
        var middleImg = cardTextMiddle.GetComponentInChildren<UnityEngine.UI.Image>();
        middleImg.enabled = mods[1].imageIndex > 0;
        if (mods[1].imageIndex > 0) { middleImg.sprite = sprites[mods[1].imageIndex - 1]; }

        cardControllerRight.mod = mods[2];
        cardTextRight.SetText(mods[2].disp);
        var rightImg = cardTextRight.GetComponentInChildren<UnityEngine.UI.Image>();
        rightImg.enabled = mods[2].imageIndex > 0;
        if (mods[2].imageIndex > 0) { rightImg.sprite = sprites[mods[2].imageIndex - 1]; }
    }

    public void ShowCards(bool value){
        foreach (var text in new []{cardTextLeft, cardTextMiddle, cardTextRight}) {
            text.enabled = value;
            if (!value) {
                text.GetComponentInChildren<UnityEngine.UI.Image>().enabled = false;
            }
        }
        foreach (var obj in new []{cardObjLeft, cardObjMiddle, cardObjRight}) {
            obj.SetActive(value);
        }
    }

    public void SelectCard(CardController card){
        AttackMods mods = vampire.GetComponent<AttackMods>();
        mods.ApplyMod(card.mod);
        ShowCards(false);
        GameState.selectingUpgrade = false;
    }
}
