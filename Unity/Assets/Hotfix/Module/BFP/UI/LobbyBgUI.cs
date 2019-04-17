/**
 * 大厅背景
 * **/

using System.Threading.Tasks;
using ETModel;
using FairyGUI;

namespace ETHotfix
{
    public class LobbyBgUI : BaseUIForms
    {
        public static LobbyBgUI Instance;
        public LobbyBgUI()
        {
            this.pakName = "Lobby";
            this.cmpName = "LobbyBg";
            this.CurrentUIType.NeedClearingStack = true;
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.Normal;
            this.CurrentUIType.UIForms_Type = UIFormsType.Normal;

            Instance = this;
        }

        public override void InitUI()
        {
            base.InitUI();

            GComponent gCmp = this.GObject.asCom;

            //注册事件
            //gCmp.GetChild("CreateRoomBtn").asButton.onClick.Add(OpenCreateRoomUI);
            //gCmp.GetChild("DaikaiBtn").asButton.onClick.Add(OpenDaikaiUI);
            //gCmp.GetChild("EnterRoomBtn").asButton.onClick.Add(EnterRoom);
            //gCmp.GetChild("n4").asButton.onClick.Add(() => { UI.Tip("有疑问请联系客服QQ535421993"); });
            //开始声音
           // SoundComponent.Instance?.PlayMusic(SoundName.mainBgm, 1, 1, true, false);

        }

       
    }
}

