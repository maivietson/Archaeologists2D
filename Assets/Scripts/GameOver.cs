using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject panelMenu;
    [SerializeField] Text currentLevel;
    [SerializeField] Text currentCoin;

    // Start is called before the first frame update
    void Start()
    {
        //panelMenu.SetActive(true);
        //currentLevel.text = GameSession.instance.currentLevel.ToString();
        //currentCoin.text = GameSession.instance.GetScore().ToString();
    }

    public void Ads()
    {

    }

    public void MainMenu()
    {
        GameSession.instance.ResetGameSession();
    }
}
