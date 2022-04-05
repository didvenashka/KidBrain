using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpMenu : MonoBehaviour
{
    [SerializeField] Avatar[] avatars;
    [SerializeField] AvatarsSO avatarSO;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] SharedSignScene fromScene;

    [SerializeField] PlayerRepository playerRepository;

    int _selectedAvatar = -1;
    string _name = "";

    private void Start()
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].Set(avatarSO.avatars[i], i);
            avatars[i].Click += AvatarClicked;
        }

        _name = playerRepository.Name;
        _selectedAvatar = playerRepository.Avatar;

        if (_selectedAvatar != -1)
            avatars[_selectedAvatar].Select();
        nameInput.text = _name;
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
        playerRepository.SetName(_name);
        playerRepository.SetAvatar(_selectedAvatar);
        if (fromScene.Scene == Scenes.Splash)
            SceneManager.LoadScene(Scenes.Main);
        if (fromScene.Scene == Scenes.Stats)
            SceneManager.LoadScene(Scenes.Stats);
    }
}
