using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : SingletonComponent<BoardController>
{

    [SerializeField] Card[] lsCard;
    [SerializeField] Vector3 deckPosition;

    [SerializeField] CanvasGroup blurPanelCanvasGroup;

    [SerializeField] public Button btConfirm;

    private Card currentCard;

    private bool isDealing;


    private void Start()
    {
        isDealing = false;

        for (int i = 0; i < lsCard.Length; i++)
        {
            RectTransform rectTransform = lsCard[i].GetComponent<RectTransform>();
            lsCard[i]._posDefault = rectTransform.position;
            rectTransform.localScale = new Vector3(0.0f, 0.0f, 1);
            rectTransform.position = deckPosition;
        }
        StartCoroutine(DealCard());
    }


    private void MoveCard(int stt)
    {
        lsCard[stt].Move(lsCard[stt]._posDefault);
    }


 
    public void SelectCard(Card selectCard)
    {
        if (isDealing) return;
        if(currentCard != null && currentCard != selectCard)
        {
            currentCard.UpDown(-1);
        }
        if(currentCard == selectCard)
        {
            return;
        }
        selectCard.UpDown();
        currentCard = selectCard;

        // set xem so tien co du de bat confirm khong moi bat  
        SetInteractableButtonConfirm(SingletonComponent<GameController>.Instance.GetPlayer().CheckMoney(currentCard.Price));
    }



    private IEnumerator DealCard()
    {
        isDealing = true;
        for (int i = 0; i<lsCard.Length; i++)
        {
            MoveCard(i);
            yield return new WaitForSeconds(0.5f);
        }
        isDealing = false;
        SetActiveButtonConfirm(true);
        Utility();

    }

    private void Utility()
    {
        SingletonComponent<TimeUp>.Instance.gameObject.SetActive(true);
    }


    private void SetInteractableButtonConfirm(bool _isActive)
    {
        btConfirm.interactable =  _isActive ;
    }


    private void SetActiveButtonConfirm(bool _isActive)
    {
        btConfirm.gameObject.SetActive( _isActive );
    }

    public void ConfirmCard()
    {
        if (currentCard == null) return;

        ActiveCardLeft(false);

        // panel =0
        blurPanelCanvasGroup.DOFade(0, 1f);


        SingletonComponent<GameController>.Instance.SetPlayerWhenOnClick(currentCard);
        SingletonComponent<TimeUp>.Instance.gameObject.SetActive(false);


    }

    public void ActiveCardLeft(bool _is)
    {
        foreach (var item in lsCard)
        {
            if (item != currentCard)
            {
                item.gameObject.SetActive(_is);
            }
        }

        SetInteractableButtonConfirm(false);
        SetActiveButtonConfirm(_is);

    }



    #region CONTINUE GAME
    public void GameContinue()
    {
        ActiveCardLeft(true);
        blurPanelCanvasGroup.DOFade(1, 1f);
        SingletonComponent<GameController>.Instance.SetPlayerFirstStart();

        currentCard.transform.localScale = Vector3.zero;
        currentCard.gameObject.SetActive(true);
        SingletonComponent<Meme>.Instance.SetDataMeme(currentCard);
        currentCard.canvasGroup.DOFade(1, 0);
        currentCard.transform.position = Vector3.zero;
        currentCard.Move(currentCard._posDefault);
        currentCard = null;
        Utility();
    }
    #endregion
}
