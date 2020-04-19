using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestConvert : MonoBehaviour
{
    BoxCollider2D chest;

    [SerializeField] GameObject panelQuestion;
    [SerializeField] Text textTitleMess;
    [SerializeField] Text textContentMess;

    [SerializeField] int coinConvert;


    // Start is called before the first frame update
    void Start()
    {
        chest = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (chest.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textTitleMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_TITLE);
            textContentMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_CONTENT);
            panelQuestion.SetActive(true);
        }
    }

    public void ConvertGold()
    {
        if(GameSession.instance.GetScore() > coinConvert)
        {
            GameSession.instance.AddToScore(-coinConvert);
            GameSession.instance.AddLife(1);
        }
        else
        {
            panelQuestion.SetActive(false);
        }
    }

    public void NoConvert()
    {
        panelQuestion.SetActive(false);
    }

}
