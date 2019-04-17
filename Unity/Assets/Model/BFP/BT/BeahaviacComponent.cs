/**
 * 
 * 行为树加载组件
 * 
 * **/

namespace ETModel
{
    [ObjectSystem]
    public class BeahaviacComponentAwakeSystem : AwakeSystem<BeahaviacComponent>
    {
        public override void Awake(BeahaviacComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class BeahaviacComponentUpdateSystem : UpdateSystem<BeahaviacComponent>
    {
        public override void Update(BeahaviacComponent self)
        {
            self.Update();
        }
    }


    public class BeahaviacComponent : Component
    {
        EXFileManager fileManager;
        ETHotfix.ConnectionManager connectionManagerBT;

        public void Awake()
        {
            Log.Debug("开始加载网络管理行为树");

            fileManager = new EXFileManager();
            behaviac.Workspace.Instance.FilePath = "BT";
            behaviac.Workspace.Instance.FileFormat = behaviac.Workspace.EFileFormat.EFF_xml;
            behaviac.Workspace.Instance.IsExecAgents = false;
            //behaviac.Config.IsLogging = true;
            //behaviac.Config.SocketPort = 9931;
            behaviac.Config.IsSocketing = false;

            Log.Debug("开始初始化行为树");
            connectionManagerBT = new ETHotfix.ConnectionManager();

            bool bRet = connectionManagerBT.btload("ConnectionManager");
            if (bRet)
            {
                connectionManagerBT.btsetcurrent("ConnectionManager");
            }
            else
            {
                Log.Debug("加载行为树失败");
            }

        }

        public void Update()
        {
            connectionManagerBT.btexec();
        }




        void Cleanup()
        {

            connectionManagerBT = null;
        }

        void CleanupBehaviac()
        {
            behaviac.Workspace.Instance.Cleanup();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
            Cleanup();
            CleanupBehaviac();

        }

    }
}
