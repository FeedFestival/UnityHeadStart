using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotSeatView : MonoBehaviour, IUiView
{
    public GsTable HotSeatTable;
    public GameObject NewChallengerDialog;
    public List<Text> PredefinedNames;
    public InputFieldCustom NewChallengerInput;
    private List<User> _users;
    public GameObject Gobject { get { return this.gameObject; } }
    public void OnShow()
    {
        UIController._.MainMenu.SetHeaderView(HEADVIEW.Back);
        NewChallengerDialog.SetActive(false);

        List<GsTableData> tableData = new List<GsTableData>();

        List<HighScore> highscores = Game._.DataService.GetHotSeatScores();

        foreach (HighScore hs in highscores)
        {
            tableData.Add(new GsTableData()
            {
                Values = new List<string>() {
                    hs.UserName.ToString(),
                    hs.Points.ToString()
                }
            });
        }

        HotSeatTable.SetData(tableData);
    }

    public void Challenge()
    {
        NewChallengerDialog.SetActive(true);

        if (_users == null || _users.Count == 0)
        {
            _users = Game._.DataService.GetUsers();
        }

        int i = 0;
        foreach (Text predefName in PredefinedNames)
        {
            if (i >= _users.Count)
            {
                predefName.text = "";
                continue;
            }
            predefName.text = _users[i].Name;
            i++;
        }
    }

    public void OnSelectedPredefined(int index)
    {
        if (index >= _users.Count)
        {
            return;
        }
        NewChallengerInput.InputField.text = _users[index].Name;
        NewChallengerInput.OnBlur();
    }

    public void PlayChallenge()
    {
        Game._.HighScoreType = HighScoreType.HOTSEAT;
        User playingUser = Game._.DataService.GetUserByName(NewChallengerInput.InputField.text);
        if (playingUser == null)
        {
            playingUser = new User()
            {
                Name = NewChallengerInput.InputField.text
            };
            Game._.DataService.CreateUser(playingUser);
        }
        Game._.PlayingUser = playingUser;
        Game._.LoadFirstLevel();
    }

    public void CancelChallenge()
    {
        NewChallengerDialog.SetActive(false);
    }
}
