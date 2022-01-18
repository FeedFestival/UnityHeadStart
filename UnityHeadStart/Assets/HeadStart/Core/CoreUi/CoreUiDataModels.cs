namespace Assets.HeadStart.CoreUi
{
    public enum UiDependency
    {
        ScreenData
    }

    public interface IUiDependency
    {
        void Register(CoreUiObservedValue obj);
    }

    public class CoreUiObservedValue
    {
        public CoreUiObservedValue() { }
    }
}
