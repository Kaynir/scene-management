namespace Kaynir.Scenes
{
    public interface ISceneLoader
    {
        bool IsLoading { get; }

        void Load(int sceneIndex);
        void LoadAdditive(int sceneIndex);
        void Unload(int sceneIndex);
    }
}