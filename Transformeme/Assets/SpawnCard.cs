using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    [SerializeField] private Card[] lsCard;
    private int[] _attribute;
    private bool hasFreeCard;
    private int numberOfCardIsNotFree;
    private int _attributeType1, _attributeType2, _price;
[SerializeField]    private DataCard dataCard;

    public int getAttributeType1() { return _attributeType1; }
    public int getAttributeType2() { return _attributeType2; }

    public void SetNotHasFreeCard()
    {
        this.hasFreeCard = false;
        numberOfCardIsNotFree = 0;
    }

    private void Start()
    {
        numberOfCardIsNotFree = 0;
        hasFreeCard = false;
        _attribute = new int[5];
        _attribute[0] = 2; //Speed
        _attribute[1] = 4; //Money
        _attribute[2] = 4; //Healing
        _attribute[3] = 3; //Atk
        for (int i =0; i<5; i++)
        {
            SpawnPower();
            dataCard.Price = _price;
            lsCard[i].SetDataCard(dataCard);
            dataCard.DataCardDefault();
        }
    }
    private int CardValue()
    {

        switch (SpawnPrice())
        {
            case 0:
                return Random.Range(16, 20);
                break;
            case 1:
                return Random.Range(20, 25);
                break;
            case 2:
                return Random.Range(25, 30);
                break;
            case 3:
                return Random.Range(30, 36);
            default:
                return 36;

        }
        
    }

    private int SpawnPrice()
    {
        if (numberOfCardIsNotFree == 4)
        {
            hasFreeCard = true;
            numberOfCardIsNotFree = 0;
            _price = 0;
            return _price;
        }
        int x = Random.Range(1, 101);
        if ( x <= 60)
        {
            hasFreeCard = true;
            numberOfCardIsNotFree = 0;
            _price = 0;
            return _price;
        }
        else
        {
            numberOfCardIsNotFree++;
            if (61 <= x && x <= 75)
            {
                _price = 1;
                return _price;
            }
            else if (76 <= x && x <= 90)
            {
                _price = 2;

                return _price;
            }
            else
            {
                _price = 3;

                return _price;
            }
        }
    }

    private int SpawnCombination()
    {
        int x = Random.Range(1, 91);
        if (1 <= x && x <= 25)
        {
            return 0;// ATK + Money
        }
        else if (26 <= x && x <= 50)
        {
            return 1;// ATK + Healing
        }
        else if (51 <= x && x <= 60)
        {
            return 2;// ATK + SPD
        }
        else if (61 <= x && x <= 75)
        {
            return 3;// Money + Healing
        }
        else if (76 <= x && x <= 80)
        {
            return 4;// Money + SPD
        }
        else
        {
            return 5;// HP + SPD
        }
        //else return 6; // special case
    }

    private void SpawnPower()
    {
        int sum = CardValue(); ;
        switch (SpawnCombination())
        {
            case 0:
                _attributeType1 = 1;
                _attributeType2 = 3;
                dataCard.Money = Random.Range(1, _price / 3 +2);
                dataCard.Attack = (sum - dataCard.Money * _attribute[_attributeType1])/_attribute[_attributeType2];
                break;
            case 1:
                _attributeType1 = 2;
                _attributeType2 = 3;
                dataCard.Heal = Random.Range(1, 4);
                dataCard.Attack = (sum - dataCard.Heal * _attribute[_attributeType1]) / _attribute[_attributeType2];
                break;
            case 2:
                _attributeType1 = 0;
                _attributeType2 = 3;
                dataCard.Speed = Random.Range(1, 4);
                dataCard.Attack = (sum - dataCard.Speed * _attribute[_attributeType1]) / _attribute[_attributeType2];
                break;
            case 3:
                _attributeType1 = 1;
                _attributeType2 = 2;
                dataCard.Money = Random.Range(1, _price / 3 + 2);
                dataCard.Heal = (sum - dataCard.Money * _attribute[_attributeType1]) / _attribute[_attributeType2];
                break;
            case 4:
                _attributeType1 = 1;
                _attributeType2 = 0;
                dataCard.Money = Random.Range(1, _price / 3 + 2);
                dataCard.Speed = (sum - dataCard.Money * _attribute[_attributeType1]) / _attribute[_attributeType2];
                break;
            case 5:
                _attributeType1 = 0;
                _attributeType2 = 2;
                dataCard.Speed = Random.Range(1, 4);
                dataCard.Heal = (sum - dataCard.Speed * _attribute[_attributeType1]) / _attribute[_attributeType2];
                break;
            default:
                _attributeType1 = 999;
                _attributeType2 = 999;
                break;
        }
    }

}
