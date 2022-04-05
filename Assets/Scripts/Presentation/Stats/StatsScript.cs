using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{
    [SerializeField] PlayerRepository playerRepository;
    [SerializeField] SharedSignScene fromScene;
    [SerializeField] Image avatar;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] UIPolygon poligon;
    [SerializeField] AvatarsSO avatarsSO;

    Type[] games = new Type[4]
    {
        typeof(MemorySquaresGame),
        typeof(QuickEyeGame),
        typeof(ReactTapGame),
        typeof(QuickCalculationsGame)
    };

    private void Start()
    {
        nameText.text = playerRepository.Name;
        avatar.sprite = avatarsSO.avatars[playerRepository.Avatar];
        Dictionary<string, GameStatistic> stats = new ScoreRepository().GetStatistic().GameStatistics;
        float[] verticies = new float[5] { 0.1f, 0.1f, 0.1f, 0.1f, 1f };
        for (int i = 0; i < games.Length; i++)
        {
            int sum = 0;
            int countGames = 0;
            if (stats.ContainsKey(games[i].Name))
            {
                Dictionary<string, DailyStatistic> gameStats = stats[games[i].Name].DailyStatistics;
                foreach (string date in gameStats.Keys)
                {
                    if ((DateTime.Now - DateTime.Parse(date)).TotalDays <= 5)
                    {
                        sum += gameStats[date].Points;
                        countGames += gameStats[date].NumberOfGames;
                    }
                }

                verticies[i] = (float)sum / countGames / 100f;
            }
        }
        poligon.DrawPolygon(4, verticies);
    }

    public void OnEditClick()
    {
        fromScene.Scene = Scenes.Stats;
        SceneManager.LoadScene(Scenes.SignUp);
    }
}
