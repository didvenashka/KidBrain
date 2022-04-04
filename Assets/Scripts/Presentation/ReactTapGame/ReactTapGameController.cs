using System.Collections;
using UnityEngine;

public class ReactTapGameController : MonoBehaviour
{
    [SerializeField] CircleScript prefab;
    [SerializeField] ScoreScript score;
    [SerializeField] RectTransform spawnBox;
    ReactTapGame game;

    Vector2 spawnSize;
    bool active;
    void Start()
    {
        game = new ReactTapGameManager().CreateNewGame();
        active = true;
        StartCoroutine(Move());
        
    }

    IEnumerator Move()
    {
        yield return new WaitForEndOfFrame();
        spawnSize = spawnBox.rect.size;
        foreach (Circle c in game.Circles)
        {
            if (!active) yield break;
            CircleScript circle = Instantiate(prefab, spawnBox);
            circle.Init(c, spawnSize);
            yield return new WaitUntil(circle.IsDone);
            Destroy(circle.gameObject);
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(game.DurationInSeconds);
        active = false;
    }
}
