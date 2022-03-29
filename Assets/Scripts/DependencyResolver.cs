using Zenject;

public class DependencyResolver : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMemorySquaresGameManager>()
            .To<MemorySquaresGameManager>()
            .AsSingle();

        Container.Bind<IQuickCalculationsGameManager>()
            .To<QuickCalculationsGameManager>()
            .AsSingle();

        Container.Bind<IQuickEyeGameManager>()
            .To<QuickEyeGameManager>()
            .AsSingle();

        Container.Bind<IReactTapGameManager>()
            .To<ReactTapGameManager>()
            .AsSingle();

        Container.Bind<IScoreManager>()
            .To<ScoreManager>()
            .AsSingle();

        Container.Bind<IScoreRepository>()
            .To<ScoreRepository>()
            .AsSingle();
    }
}
