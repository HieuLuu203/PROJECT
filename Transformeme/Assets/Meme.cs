using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meme : SingletonComponent<Meme>
{
    private Queue<DataMeme> queueDataMeme = new Queue<DataMeme>();
    [SerializeField] private List<DataMeme> listSpriteMeme;
    [SerializeField] private Card[] card;

    private void Start()
    {
        Shuffle();
        SetUpMemeCardOnStart();
    }

    private void Shuffle()
    {
        while (listSpriteMeme != null && listSpriteMeme.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, listSpriteMeme.Count);
            queueDataMeme.Enqueue(listSpriteMeme[index]);
            listSpriteMeme.RemoveAt(index);

        }
    }

    private void SetUpMemeCardOnStart()
    {
        for (int i = 0; i < card.Length; i++) {
            DataMeme dataMeme = queueDataMeme.Dequeue();
            card[i].SetDataMemeToCard(dataMeme);
            queueDataMeme.Enqueue(dataMeme);
        }
    }

    public void SetDataMeme(Card card)
    {
        DataMeme dataMeme = queueDataMeme.Dequeue();
        card.SetDataMemeToCard(dataMeme);
        queueDataMeme.Enqueue(dataMeme);
    }
}

[Serializable] public class DataMeme
{
    public AudioClip audioClip;
    public Sprite sprite;
    public int ID;
}
