namespace Assets.HeadStart.Features.ScreenData
{
    public interface IPoolObject
    {
        int Id { get; }
        bool IsUsed { get; }
        void Show();
        void Hide();
    }
}