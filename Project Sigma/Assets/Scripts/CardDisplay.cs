using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{

    public Card card;

    public Image artworkImage;
    public Image affinityImage;

    public TextMeshProUGUI mergeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI resText;
    public TextMeshProUGUI levelText;


    void Update()
    {
        artworkImage.sprite = card.portrait;
        switch (card.affinity)
        {
            case Card.Affinity.Club:
                affinityImage.sprite = card.ruleset.club;
                break;
            case Card.Affinity.Spade:
                affinityImage.sprite = card.ruleset.spade;
                break;
            case Card.Affinity.Heart:
                affinityImage.sprite = card.ruleset.heart;
                break;
            case Card.Affinity.Diamond:
                affinityImage.sprite = card.ruleset.diamond;
                break;
        }

        mergeText.text = card.merge.ToString();
        nameText.text = card.name;
        hpText.text = card.HP.ToString() + "/" + card.HP.ToString();
        atkText.text = card.Atk.ToString();
        spdText.text = card.Spd.ToString();
        defText.text = card.Def.ToString();
        resText.text = card.Res.ToString();
        levelText.text = card.level.ToString();


    }

}
