using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUpMenu : MonoBehaviour
{
    [SerializeField] Avatar[] avatars;
    [SerializeField] AvatarsSO avatarSO;
    [SerializeField] TMP_InputField nameInput;
    int _selectedAvatar = -1;
    string _name = "";

    private void Start()
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].Set(avatarSO.avatars[i], i);
            avatars[i].Click += AvatarClicked;
        }
    }

    private void OnDestroy()
    {
        foreach (Avatar avatar in avatars)
            avatar.Click -= AvatarClicked;
    }

    public void AvatarClicked(int index)
    {
        if (_selectedAvatar != -1)
            avatars[_selectedAvatar].DeSelect();
        _selectedAvatar = index;
        avatars[_selectedAvatar].Select();
    }

    public void ContinueClicked()
    {
        _name = nameInput.text;
        if (_selectedAvatar == -1 || _name.Length == 0) return;
        PlayerPrefs.SetString("Name", _name);
        PlayerPrefs.SetInt("Avatar", _selectedAvatar);
        SceneManager.LoadScene(1); //main menu scene
    }
}
