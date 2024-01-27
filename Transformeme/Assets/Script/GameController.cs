using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonComponent<GameController>
{
    [SerializeField] Player[] players;
    private Vector3[] playerPositions;

    private int _turn;

    public void Start()
    {

        SetPlayerFirstStart();

        playerPositions = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerPositions[i] = players[i].transform.position;
        }
    }

    int checknextTurn=0;
    public void SetPlayerFirstStart()
    {
        _turn = players[0].Speed >= players[1].Speed ? 0 : 1;
    }


    public void SetPlayerWhenOnClick(Card card)
    {
        card.FlyToPlayerSelect(playerPositions[_turn % players.Length]);

        players[_turn % players.Length].BattlePhase(card);

        _turn++;
        checknextTurn++;
        if (checknextTurn == 2)
    }

    public Player GetPlayer()
    {
        return players[_turn % players.Length];
    }


    #region WIN LOSE

    /// <summary>
    /// tru mau doi thu
    /// </summary>
    /// <param name="_healthSub"></param>
    public void SetHeathPlayerOpponent(int _healthSub)
    {
        Player player = GetPlayer();
        player.Health -= _healthSub;

        if(CheckIfHealthOpponentIsOver(player))
        {
            print("LOSE");
            return;
        }
        else
        {
            SingletonComponent<BoardController>.Instance.GameContinue();
        }
    }

    #endregion




    /// <summary>
    /// Kiem tra xem nguoi choi con lai het mau chua
    /// </summary>
    public bool CheckIfHealthOpponentIsOver(Player player)
    {
        if (player.Health <= 0)
            return true;// het mau
        return false;
    }

#if UNITY_EDITOR

   private  void OnApplicationQuit()
    {
        transform.DOKill();
    }

#endif


}
