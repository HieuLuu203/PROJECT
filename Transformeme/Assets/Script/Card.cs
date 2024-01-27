using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Card : MonoBehaviour
{
    private int _heal;

    public int Heal
    {
        get => _heal;
        set => _heal = value;
    }

    private int _attack;

    public int Attack
    {
        get => _attack;
        set => _attack = value;
    }

    private int _money;

    public int Money
    {
        get => _money;
        set => _money = value;
    }

    private int _price;

    public int Price
    {
        get => _price;
        set => _price = value;
    }


    private int _speed;

    public int Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public Vector3 _posDefault;


    [SerializeField] TextMeshProUGUI txtHeal, txtPrice, txtMoney, txtAttack, txtSpeed;
    
    public CanvasGroup canvasGroup;
    public GameObject iconHeal, iconMoney, iconAttack, iconSpeed;


    public void SetText()
    {
        txtHeal.text = Heal.ToString();
        txtAttack.text = Attack.ToString();
        txtMoney.text = Money.ToString();
        txtPrice.text = Price.ToString();
        txtSpeed.text = Speed.ToString();
    }

    #region SETTING GAME ->  
    public void SettingCard()
    {
        int type = Random.Range(1, 100);
        Price = 0;
        Attack = 0;
        Money = 0;
        Heal = 0;
        Speed = Random.Range(1, 10);
    }

    #endregion


    public void OnClick()
    {
        SingletonComponent<BoardController>.Instance.SelectCard(this);
    }


    private float timeMove = 0.7f;

    public float timeUp = 0.3f;

    
    /// <summary>
    /// di chuyen cua Card
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="_ease"></param>
    /// <param name="_scale"></param>
    /// <param name="_action"></param>
    public void Move(Vector3 destination, Ease _ease = Ease.OutBack,float _scale= 1, Action _action = null)
    {
        transform.DOMove(destination, timeMove).SetEase(_ease);
        transform.DOScale(Vector3.one * _scale, timeMove).OnComplete(() =>
        {
            _action?.Invoke();
        });
    }


    public void UpDown(int _isUp = 1)// _isUp = 1 -> up // _isUp = -1
    {
        Move(transform.position + Vector3.up * _isUp);
    }

    public void FlyToPlayerSelect(Vector3 destination)
    {
        Move(destination, Ease.OutBack, 2, () =>
        {
            canvasGroup.DOFade(0f, timeMove).OnComplete(() => { gameObject.SetActive(false); });
        }
        );
    }

    public void SetDataCard(DataCard dataCard)
    {
        Attack = dataCard.Attack;
        Speed = dataCard.Speed;
        Heal = dataCard.Heal;
        Price = dataCard.Price;
        Money = dataCard.Money;


        SetText();
        SetActiveAllIcon();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActiveAllIcon()
    {
        SetActiveOneIcon(Attack, iconAttack);
        SetActiveOneIcon(Heal, iconHeal);
        SetActiveOneIcon(Money, iconMoney);
        SetActiveOneIcon(Speed, iconSpeed);

    }
    public void SetActiveOneIcon(int power, GameObject icon)
    {
        if (power == 0) icon.SetActive(false);
        else icon.SetActive(true);
    }

    // xu ly anh meme
    private DataMeme dataMeme;
    public Image memeImage;
    public void SetDataMemeToCard(DataMeme dataMeme)
    {
        this.dataMeme = dataMeme;
        memeImage.sprite = dataMeme.sprite;
    }

    public int GetID() { return dataMeme.ID; }
    public AudioClip GetMemeSound() { return dataMeme.audioClip; }

}

[Serializable] public class DataCard
{
    public int Attack = 0;
    public int Price = 0;
    public int Heal = 0;
    public int Speed = 0;
    public int Money = 0;

    public void DataCardDefault ()
    {
        Attack = 0;
        Price = 0;
        Heal = 0;
        Speed = 0;
        Money = 0;
    }


}
