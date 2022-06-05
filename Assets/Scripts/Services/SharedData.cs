using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Configurations;

namespace Fabros.EcsLite.Services
{
    public class SharedData
    {
        #region Public
        public RuntimeData RuntimeData { get; private set; }
        public Configs Configurations { get; private set; }
        public SceneData SceneData { get; private set; }
        #endregion

        public SharedData(RuntimeData runtimeData, Configs configurations, SceneData sceneData)
        {
            RuntimeData = runtimeData;
            Configurations = configurations;
            SceneData = sceneData;
        }
    }
}
