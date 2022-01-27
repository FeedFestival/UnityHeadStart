namespace Assets.HeadStart.CoreUi
{
    public enum UiDependency
    {
        ScreenData, Dialog
    }

    public interface IUiDependency
    {
        void InitDependency(object obj);
        void ListenForChanges(CoreUiObservedValue obj);
        void UnregisterOnDestroy();
    }

    public class CoreUiObservedValue
    {
        public CoreUiObservedValue() { }
    }
}
