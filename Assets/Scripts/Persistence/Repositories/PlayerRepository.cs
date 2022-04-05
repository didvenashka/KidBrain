using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRepository")]
public class PlayerRepository: ScriptableObject
{
    static string NAME_KEY = "Name";
    static string AVATAR_KEY = "Avatar";

    public string Name { get; private set; }
    public int Avatar { get; private set; }

    public void OnEnable()
    {
        Name = PlayerPrefs.GetString(NAME_KEY, "");
        Avatar = PlayerPrefs.GetInt(AVATAR_KEY, -1);
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString(NAME_KEY, name);
        Name = name;
    }

    public void SetAvatar(int avatar)
{
        PlayerPrefs.SetInt(AVATAR_KEY, avatar);
        Avatar = avatar;
    }
}
