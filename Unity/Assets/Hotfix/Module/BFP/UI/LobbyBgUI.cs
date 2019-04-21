/**
 * 大厅背景
 * **/

using System.Threading.Tasks;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class LobbyBgUI : BaseUIForms
    {
        public static LobbyBgUI Instance;
        public LobbyBgUI()
        {
            abName = "LobbyBgUI";
          
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.Normal;
            this.CurrentUIType.UIForms_Type = UIFormsType.Normal;

            Instance = this;
        }

        public override void InitUI()
        {
            //base.InitUI();

            // GComponent gCmp = this.GObject.asCom;
            Transform btn = GObj.transform.Find("Panel/EnterMap");
            Button bt = btn.GetComponent<Button>();
            bt.onClick.Add(() => {
                Debug.Log("HotFix的Button按钮事件触发");
                UI.Open<SelectHeroForms>();
            });
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

