namespace VUDK.Generic.Managers.Main.Interfaces
{
    using VUDK.Generic.Managers.Main.Bases;

    public interface ICastGameStats<T> where T : GameStatsBase
    {
        public T GameStats { get; }
    }
}
