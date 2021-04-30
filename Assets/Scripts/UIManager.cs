using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SingleTon

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
        }
    }

    #endregion

    private int _amountCoins;
    private int _amountEat;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _eatText;
    [SerializeField] private GameObject _pause;

    public bool fever;
    public event Action Init;
    public int crystalInRow;

    private void Start()
    {
        _coinsText.text = "Crystals " + _amountCoins;
        _eatText.text = "Eaten " + _amountEat;
    }

    public void PlusEat()
    {
        _amountEat++;
        _eatText.text = "Eaten " + _amountEat;
        crystalInRow = 0;
    }

    public void PlusCoins()
    {
        _amountCoins++;
        if(!fever)
            crystalInRow++;
        
        if (crystalInRow >= 3)
        {
            StartCoroutine(StartFever());
        }
        _coinsText.text = "Crystals " + _amountCoins;
    }

    private IEnumerator StartFever()
    {
        fever = true;
        crystalInRow = 0;
        Init.Invoke();

        yield return new WaitForSeconds(3);
        fever = false;
        StopCoroutine(StartFever());
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
