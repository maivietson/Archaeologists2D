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
    [SerializeField] int takeCoin;
    [SerializeField] int takeLive;
    [SerializeField] float timeDelay;

    bool isOpenChest = false;

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
        if (chest.IsTouchingLayers(LayerMask.GetMask("Player")) && !isOpenChest)
        {
            switch(gameObject.name)
            {
                case "GoldToLive":
                    {
                        if (GameSession.instance.GetScore() > coinConvert)
                        {
                            //isOpenChest = true;
                            textTitleMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_TITLE);
                            textContentMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_CONTENT);
                            panelQuestion.SetActive(true);
                            ConvertGold();
                        }
                        else
                        {
                            textContentMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_NOT_ENOUGH_GOLD);
                            panelQuestion.SetActive(true);
                            NotEnoughGold();
                        }
                    }
                    break;
                case "TakeGold":
                    {
                        isOpenChest = true;
                        string content = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_TAKE_GOLD);
                        content = content.Replace("XXX", takeCoin.ToString());
                        textTitleMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_TITLE);
                        textContentMess.text = content;
                        panelQuestion.SetActive(true);
                        TakeGold();
                    }
                    break;
                case "TakeLive":
                    {
                        isOpenChest = true;
                        string content = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_TAKE_LIVE);
                        content = content.Replace("XXX", takeLive.ToString());
                        textTitleMess.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_COLLIDER_CHEST_CONVERT_TITLE);
                        textContentMess.text = content;
                        panelQuestion.SetActive(true);
                        TakeLive();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void ConvertGold()
    {
        while(GameSession.instance.GetScore() >= coinConvert)
        {
            GameSession.instance.AddToScore(-coinConvert);
            GameSession.instance.AddLife(1);
        }
        StartCoroutine("DelayPopup");
    }

    private void NotEnoughGold()
    {
        StartCoroutine("DelayPopup");
    }

    private void TakeGold()
    {
        GameSession.instance.TakeCoin(takeCoin);
        StartCoroutine("DelayPopup");
    }

    private void TakeLive()
    {
        GameSession.instance.AddLife(takeLive);
        StartCoroutine("DelayPopup");
    }

    IEnumerator DelayPopup()
    {
        yield return new WaitForSeconds(timeDelay);
        panelQuestion.SetActive(false);
    }
}
