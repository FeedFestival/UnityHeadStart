using System.Linq;
using Assets.GameTester;
using Assets.HeadStart.Core;
using UnityEngine;

public enum MAIN_MENU_PHASE
{
    FIRST_TIME,
    PLAY_GAME,
    PLAY_CHALLENGE_GAME,
    PLAY_WITH_BACK_BUTTON
}

public class MainMenuGameTest : GameTestBase
{
    [Header("MainMenuGameTest")]
    public float TimeBetweenActions = 0.5f;
    [Header("MainMenuGameTest")]
    public bool TestSpecificPhase;
    [SerializeField]
    public MAIN_MENU_PHASE SpecificPhase;

    public MainMenu MainMenuRef;
    public ChallengeView ChallengeRef;
    public InputNameView InputNameRef;

    private readonly string MAIN_MENU_SCENE = "MainMenu";

    void Start()
    {
        bool isInitialized = GameTester.S.TestedScenes.Contains(MAIN_MENU_SCENE);
        if (!isInitialized)
        {
            GameTester.S.TestedScenes.Add(MAIN_MENU_SCENE);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.FIRST_TIME, false);
            MainMenuPhase_FirstTime firstTime = new MainMenuPhase_FirstTime(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.FIRST_TIME, firstTime.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME, false);
            MainMenuPhase_PlayGame playGame = new MainMenuPhase_PlayGame(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME, playGame.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME, false);
            MainMenuPhase_PlayChallenge playChallenge = new MainMenuPhase_PlayChallenge(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME, playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_WITH_BACK_BUTTON, false);
            MainMenuPhase_PlayWithBackButton playWithBackButton =
                new MainMenuPhase_PlayWithBackButton(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_WITH_BACK_BUTTON, playWithBackButton.Test);
        }

        Time.timeScale = TimeScale;
    }

    public override void Test()
    {
        __.Time.RxWait(() =>
        {
            if (TestSpecificPhase)
            {
                GameTester.S.TestPhases[(int)SpecificPhase]();
                GameTester.S.Phases[(int)SpecificPhase] = true;
                return;
            }

            var phase = GameTester.S.Phases.Where(p => p.Value == false);
            if (phase == null || phase.Count() == 0)
            {
                Debug.Log("Testing Completed!");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
            int key = phase.First().Key;

            GameTester.S.TestPhases[key]();
            GameTester.S.Phases[key] = true;

        }, TimeBetweenActions);
    }

    public void LoadMainMenuRef()
    {
        if (MainMenuRef == null)
        {
            var go = GetGo(MAIN_MENU_SCENE);
            MainMenuRef = go.GetComponent<MainMenu>();
        }
    }

    public void LoadChallengeRef()
    {
        if (ChallengeRef == null)
        {
            var go = GetGo("Challenge");
            ChallengeRef = go.GetComponent<ChallengeView>();
        }
    }

    public void LoadInputNameRef()
    {
        if (InputNameRef == null)
        {
            var go = GetGo("InputName");
            InputNameRef = go.GetComponent<InputNameView>();
        }
    }
}
