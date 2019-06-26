using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{

    public Card card;

    public Image artworkImage;

    public TextMeshProUGUI mergeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI resText;
    public TextMeshProUGUI levelText;


    // Start is called before the first frame update
    void Start()
    {
        artworkImage.sprite = card.artwork;
        mergeText.text = card.merge.ToString();
        nameText.text = card.name;
        hpText.text = card.HP.ToString() + "/" + card.HP.ToString();
        atkText.text = card.Atk.ToString();
        spdText.text = card.Spd.ToString();
        defText.text = card.Def.ToString();
        resText.text = card.Res.ToString();
        levelText.text = card.Level.ToString();


    }

}
