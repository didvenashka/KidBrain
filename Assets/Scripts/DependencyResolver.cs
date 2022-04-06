using Zenject;

public class DependencyResolver : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMemorySquaresGameManager>()
            .To<MemorySquaresGameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IQuickCalculationsGameManager>()
            .To<QuickCalculationsGameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IQuickEyeGameManager>()
            .To<QuickEyeGameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IReactTapGameManager>()
            .To<ReactTapGameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IScoreManager>()
            .To<ScoreManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IScoreRepository>()
            .To<ScoreRepository>()
            .AsSingle()
            .NonLazy();
    }
}
