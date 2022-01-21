using Zenject;

namespace WayOfLove
{
    public class LevelInstaller : MonoInstaller, IInitializable
    {
        public LevelController levelController;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInterfacesTo<LevelInstaller>().FromInstance(this);
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TinySauceAnalytics>().AsSingle();
            BindLevelController();
            BindPatternControllers();
            BindTileRotatedSignal();
        }

        private void BindTileRotatedSignal()
        {
            Container.DeclareSignal<TileRotatedSignal>();
            Container.BindSignal<TileRotatedSignal>()
                .ToMethod<PatternInspector>(p => p.TileRotated)
                .FromResolve();
        }

        private void BindPatternControllers()
        {
            Container
                .Bind<PatternInspector>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<PatternCreator>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLevelController()
        {
            Container
                .Bind<LevelController>()
                .FromInstance(levelController)
                .AsSingle();
        }

        public void Initialize()
        {
            var levelFactory = Container.Resolve<ILevelFactory>();
            levelFactory.Load();
            var tinySauceAnalytics = Container.Resolve<TinySauceAnalytics>();
            tinySauceAnalytics.Init();
        }
    }
}