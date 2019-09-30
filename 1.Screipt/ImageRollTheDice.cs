using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRollTheDice : MonoBehaviour
{
    private Sprite[] diceSides;

    private SpriteRenderer rend;

    public int rollValue = 20;

    public int finalValue = 0;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    public void StartDiceRoll()
    {
        StartCoroutine("RolltheDice");
    }

    //private void OnMouseDown()
    //{
    //    StartCoroutine("RolltheDice");
    //}

    private IEnumerator RolltheDice()
    {
        int randomDiceSide = 0;

        int finalSide = 0;

        for (int i = 0; i <= rollValue; i++)
        {
            randomDiceSide = Random.Range(0, 5);

            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;

        finalValue = finalSide;

        Debug.Log(finalSide);

        transform.parent.parent.GetComponent<CustomCardDB>().totalePower += finalValue;
    }
}