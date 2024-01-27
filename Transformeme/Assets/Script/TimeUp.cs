using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUp : SingletonComponent<TimeUp>
{
    public int _timeUp = 5;
    [SerializeField] private TextMeshProUGUI timing;
    private void ShowTime(int time)
    {
        timing.text = time.ToString();
    }

    private void Start()
    {
        transform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(CountTime());
    }
    private IEnumerator CountTime()
    {
        for(int i = _timeUp; i >= 0; i--)
        {
            ShowTime(i);
            yield return new WaitForSeconds(1);
        }
    }

    public void RandomCard()
    {

    }
}
