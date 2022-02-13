namespace Assets.HeadStart.Features.ScreenData
{
    public interface IPoolObject
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        int Id { get; }
        bool IsUsed { get; }
        void Show();
        void Hide();
    }
}