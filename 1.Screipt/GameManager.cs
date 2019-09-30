using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int HeroCount = 0;
    public int _heroCount = 0;

    public GameObject _startPanel;
    public GameObject _battalePanel;

    public GameObject _rectDicePrefab;

    public List<GameObject> lostDiceList;

    public TextMeshProUGUI msage;

    //private delegate GameObject LostDices(GameObject);

    public GameObject Push(GameObject s)
    {
        lostDiceList.Add(s);

        return null;
    }

    public GameObject ALLDiceDestroy()
    {
        //lostDiceList.Clear();
        foreach (var item in lostDiceList)
        {
            Destroy(item);
        }

        lostDiceList.Clear();
        //for (int i = 0; i < lostDiceList.Count; i++)
        //{
        //    Destroy();
        //}
        return null;
    }

    private void Update()
    {
        //_heroCount = GameManager.HeroCount;

        if (_heroCount >= 2)
        {
            _startPanel.SetActive(false);
            _battalePanel.SetActive(true);
        }
        else
        {
            _startPanel.SetActive(true);
            _battalePanel.SetActive(false);
        }
    }

    public void BattleStart()
    {
        GameObject[] battleCards = GameObject.FindGameObjectsWithTag("BattleCard");

        battleCards = DBActiveCheck(battleCards);

        if (battleCards.Length > 0)
        {
            _heroCount = battleCards.Length;
        }

        if (battleCards == null || battleCards.Length < 2)
        {
            Debug.Log("배틀카드가 부족합니다.");
            return;
        }

        CustomCardDB[] powers = new CustomCardDB[2];

        if (lostDiceList.Count != 2)
            ALLDiceDestroy();

        if (battleCards.Length == 2)
        {
            for (int i = 0; i < battleCards.Length; i++)
            {
                powers[i] = battleCards[i].GetComponent<CustomCardDB>();

                DiceProcessor(powers[i]);
            }
        }

        GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");
        Items = DBActiveCheck(Items);

        if (Items == null)
        {
            return;
        }

        if (lostDiceList.Count != 2)
            ALLDiceDestroy();

        foreach (var item in battleCards)
        {
            //var tmpCamera = Camera.main.transform;

            Debug.Log(item.transform.localRotation.eulerAngles);
            //var itemY = UnityEditor.TransformUtils.GetInspectorRotation(item.transform).y;
            //item.transform.rotation = Quaternion.identity;
            var itemY = WrapAngle(item.transform.localEulerAngles.y);
            //item.transform.rotation.eulerAngles.y;

            //Debug.LogFormat("{0} {1}", itemY, item.name);

            foreach (var item2 in Items)
            {
                if (item2 == null)
                {
                    break;
                }

                //item2.transform.rotation = Quaternion.identity;
                var item2Y = WrapAngle(item2.transform.localEulerAngles.y);

                //var item2Y = UnityEditor.TransformUtils.GetInspectorRotation(item2.transform).y;
                //transform.localRotation.eulerAngles.y;

                //Debug.LogFormat("{0} {1}", item2Y, item2.name);

                //var plus = itemY + item2Y;

                //Debug.LogFormat("{0} {1}", plus, item2.name);

                if (itemY > 0 && item2Y > 0)
                {
                    item.GetComponent<CustomCardDB>().cardPower += item2.GetComponent<CustomCardDB>().cardPower;
                }
                else if (itemY < 0 && item2Y < 0)
                {
                    item.GetComponent<CustomCardDB>().cardPower += item2.GetComponent<CustomCardDB>().cardPower;
                }
            }
        }

        for (int i = 0; i < battleCards.Length; i++)
        {
            powers[i] = battleCards[i].GetComponent<CustomCardDB>();

            DiceProcessor(powers[i]);
        }
    }

    //public float EulerAnglesTo(float eulerangValue)
    //{
    //    if (eulerAngY > 180f)
    //    {
    //        if (eulerAngX > 256f)
    //            xAngle = (eulerAngX * -1f) + 360f;
    //        else
    //            xAngle = -eulerAngX;
    //    }
    //    else
    //    {
    //        if (eulerAngX > 256f)
    //            xAngle = eulerAngX - 180f;
    //        else
    //            xAngle = ((eulerAngX * -1f) + 180f) * -1f;
    //    }
    //}

    public GameObject[] DBActiveCheck(GameObject[] Cards)
    {
        GameObject[] tmpCards = new GameObject[2];
        int count = 0;

        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].GetComponent<CustomCardDB>().enabled)
            {
                tmpCards[count] = Cards[i].gameObject;
                ++count;
            }
        }
        if (count == 0)
        {
            return null;
        }

        return tmpCards;
    }

    public int PowerDice(CustomCardDB powerDB)
    {
        // 주사위 갯수
        int power = powerDB.cardPower;
        int totalePower = 0;

        for (int i = 0; i < power; i++)
        {
            totalePower += Random.Range(1, 7);
        }

        powerDB.gameObject.GetComponentInChildren<TextMeshPro>().text = powerDB.gameObject.name + totalePower;

        return totalePower;
    }

    public string PowerCheck(CustomCardDB[] powers)
    {
        if (powers[0].totalePower > powers[1].totalePower)
        {
            return powers[0].gameObject.name;
        }
        else if (powers[0].totalePower < powers[1].totalePower)
        {
            return powers[1].gameObject.name;
        }

        return "비김";
    }

    private void DiceProcessor(CustomCardDB db)
    {
        SetDice(db.cardPower, db);
        db.gameObject.GetComponentInChildren<MouseEventCheck>().dices = db.gameObject.GetComponentsInChildren<ImageRollTheDice>();
        db.gameObject.GetComponentInChildren<MouseEventCheck>().mouseTrigger = true;
    }

    public void SetDice(int num, CustomCardDB db)
    {
        var tmpTr = db.gameObject.transform.GetComponentInChildren<HorizontalLayoutGroup>().transform;

        for (int i = 0; i < num; i++)
        {
            Push(Instantiate(_rectDicePrefab, tmpTr));
        }
    }

    private float WrapAngle(float angle)
    {
        Debug.LogFormat("디버그 시작 {0}", angle);
        angle %= 360;
        if (angle > 180)
        {
            Debug.LogFormat("디버그 if 걸림 {0}", angle);
            return angle - 360;
        }

        Debug.LogFormat("디버그 if 안걸림 {0}", angle);
        return angle;
    }

    private float UnwrapAngle(float angle)
    {
        if (angle >= 0)
        {
            Debug.Log(angle);
            return angle;
        }

        angle = -angle % 360;

        Debug.Log(angle);

        return 360 - angle;
    }

    //private IEnumerator RollTheDice()
    //{
    //    return
    //}
}