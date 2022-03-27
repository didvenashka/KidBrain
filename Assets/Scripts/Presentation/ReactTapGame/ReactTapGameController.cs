using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactTapGameController : MonoBehaviour
{
    [SerializeField] CircleScript prefab;
    ReactTapGame game;
    bool active;
    void Start()
    {
        game = new ReactTapGameManager().CreateNewGame();
        active = true;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        foreach (Circle c in game.Circles)
        {
            if (!active) yield break;
            CircleScript circle = Instantiate(prefab);
            circle.Init(c);
            yield return new WaitForSeconds(c.DurationInSeconds);
            Destroy(circle.gameObject);
        }

    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(game.DurationInSeconds);
        active = false;
    }
}
