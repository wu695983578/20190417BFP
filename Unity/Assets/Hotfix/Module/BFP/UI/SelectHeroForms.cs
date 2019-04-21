using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class SelectHeroForms : BaseUIForms
    {
        public SelectHeroForms()
        {
            abName = "SelectHeroForms";
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.HideOther;
            this.CurrentUIType.UIForms_Type = UIFormsType.Normal;

        }

        public override void InitUI()
        {
            Button bt = GObj.transform.Find("BtnEnter").GetComponent<Button>();
            bt.onClick.Add(() => {
                Debug.Log("HotFix的Button按钮事件触发");
                UI.Open<MainForms>();
            });
          
            Button bt1 = GObj.transform.Find("BtnBack").GetComponent<Button>();
            bt1.onClick.Add(() => {
                Debug.Log("HotFix的Button按钮事件触发");
                UI.Close<SelectHeroForms>();
            });
        }
    }
}

