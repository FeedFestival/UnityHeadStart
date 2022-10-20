using System.Linq;
using Assets.GameTester;
using Assets.HeadStart.Core;
using UnityEngine;

public enum MAIN_MENU_PHASE
{
    FIRST_TIME,
    PLAY_GAME,
    PLAY_CHALLENGE_GAME,
    PLAY_CHALLENGE_GAME_WITH_DIFF,
    PLAY_CHALLENGE_GAME_CHANGING_SETTINGS,
    PLAY_GAME_4,
    PLAY_GAME_5,
    PLAY_GAME_6,
    PLAY_GAME_7,
    PLAY_GAME_8,
    PLAY_GAME_9,
    PLAY_GAME_10,
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
            var phase1_firstTime = new MainMenuPhase_FirstTime(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.FIRST_TIME, phase1_firstTime.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME, false);
            var phase2_playGame = new MainMenuPhase_PlayGame(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME, phase2_playGame.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME, false);
            var phase3_playChallenge = new MainMenuPhase_PlayChallenge(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME, phase3_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME_WITH_DIFF, false);
            var phase4_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Second Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME_WITH_DIFF, phase4_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME_CHANGING_SETTINGS, false);
            var phase5_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Third Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_CHALLENGE_GAME_CHANGING_SETTINGS, phase5_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_4, false);
            var phase6_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Forth Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_4, phase6_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_5, false);
            var phase7_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Fifth Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_5, phase7_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_6, false);
            var phase8_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Sixth Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_6, phase8_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_7, false);
            var phase9_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Seventh Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_7, phase9_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_8, false);
            var phase10_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Eight Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_8, phase10_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_9, false);
            var phase11_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Ninth Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_9, phase11_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_10, false);
            var phase12_playChallenge = new MainMenuPhase_PlayChallengeWithDiff(this, "Tenth Game Tester");
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_GAME_10, phase12_playChallenge.Test);

            GameTester.S.Phases.Add((int)MAIN_MENU_PHASE.PLAY_WITH_BACK_BUTTON, false);
            var phase13_playWithBackButton = new MainMenuPhase_PlayWithBackButton(this);
            GameTester.S.TestPhases.Add((int)MAIN_MENU_PHASE.PLAY_WITH_BACK_BUTTON, phase13_playWithBackButton.Test);
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
                Debug.Log("Testing Completed on MAIN MENU!");
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
