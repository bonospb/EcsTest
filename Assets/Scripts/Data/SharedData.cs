using Fabros.EcsLite.Configurations;
using Fabros.EcsLite.Services;

namespace Fabros.EcsLite.Behaviours
{
    public class SharedData
    {
        #region Public
        public Configs Configurations { get; private set; }
        public TimeService TimeService { get; private set; }
        public SceneData SceneData { get; private set; }
        #endregion

        public SharedData(Configs configurations, TimeService timeService, SceneData sceneData)
        {
            Configurations = configurations;
            TimeService = timeService;
            SceneData = sceneData;
        }
    }
}
