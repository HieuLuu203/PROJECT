using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private int _health;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
        }
    }
     

    private int _speed;

    public int Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private int _money;

    public int Money
    {
        get => _money;
        set => _money = value;
    }




    private void Start()
    {
        animator = GetComponent<Animator>();

        SetAvatarPlayer();

        SettingDefault();
        SetTextData();
    }



    // oanh nhau

    #region DANH NHAU -> 
    public void BattlePhase(Card card)
    {

        SetAddDataPlayer(card);
        SetTextData();

        // OnTEST

        StartCoroutine(IETestDelayBattle());
        IEnumerator IETestDelayBattle()
        {
            print(" delay battle "); // SET -> Animation, Video, VFX
            yield return new WaitForSeconds(5f);
            print(" battle complate");

            BattleComplete(card.Attack);
        }

    }
    #endregion



    private void BattleComplete(int _attack)
    {
        // kiem tra xem mau doi thu het chua
        SingletonComponent<GameController>.Instance.SetHeathPlayerOpponent(_attack);

    }


    public bool CheckMoney(int _price)
    {
        if (Money >= _price)
        {
            return true;
        }
        return false;
    }



    public TextMeshProUGUI txtHealth, txtSpeed, txtMoney;



    private void SetAddDataPlayer(Card card)
    {
        Speed += card.Speed;
        Money -= card.Price;
        Health += card.Heal;

        ShowAnimation(card.GetID());
        AudioManager.instance.PlayMemeAudio(card.GetMemeSound());
    }

    private void SetTextData()
    {
        txtHealth.text = Health.ToString();
        txtMoney.text = Money.ToString();
        txtSpeed.text = Speed.ToString();
    }


    public Animator animator;

    
    private void ShowAnimation(int ID)
    {
        animator.enabled=true;

        animator.SetTrigger("TIG");
        animator.SetFloat("Blend", ID);

        StartCoroutine(SetDisableAnimatorIE(4.5f));
    }


    [SerializeField] SpriteRenderer avatarPlayer;

    [SerializeField] Sprite spDefaultAvatar;
    private void SetAvatarPlayer()
    {
        avatarPlayer.sprite = spDefaultAvatar;

    }
   IEnumerator SetDisableAnimatorIE(float _time)
    {
        yield return new WaitForSeconds(_time);
        animator.enabled = false;

        /// tat tieng animation
        /// Pause
        /// 

        AudioManager.instance.PauseMemeAudio();

        SetAvatarPlayer();
    }
    private void SettingDefault()
    {
                               
        Health = 10;
        Money = 5;
        Speed = 0;
    }

   
}

