using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuickCalculationsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI equationText;
    [SerializeField] Answer[] answers;
    QuickCalculationsGame game;

    void Start()
    {
        game = new QuickCalculationsGameManager().CreateNewGame();
        StartCoroutine(Game());
    }

    IEnumerator Game()
    {
        foreach (Equation equation in game.Equations)
        {
            equationText.text = equation.StringRepresentation();
            int[] components = new int[3]
            {
                equation.FirstNumber,
                equation.SecondNumber,
                equation.Answer
            };
            int questioned = components[(int)equation.HiddenPosition];
            int[] variants = equation.Variants.ToArray();
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].Set(variants[i].ToString(), questioned == variants[i]);
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
}
