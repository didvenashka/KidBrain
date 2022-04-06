using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    [SerializeField] PlayerRepository playerRepository;

    void Start()
    {
        StartCoroutine(Splash());
    }


    IEnumerator Splash()
    {
        yield return new WaitForSeconds(1f);
        if (playerRepository.Name.Length == 0 || playerRepository.Avatar == -1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
