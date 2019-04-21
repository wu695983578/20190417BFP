using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class GoodsInfoForms : BaseUIForms
    {
        public GoodsInfoForms()
        {
            abName = "GoodsInfoForms";
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.ReverseChange;
            this.CurrentUIType.UIForms_Type = UIFormsType.PopUp;

        }

        public override void InitUI()
        {
            Button bt = GObj.transform.Find("EnterButton").GetComponent<Button>();
            bt.onClick.Add(() => {
                Debug.Log("HotFix的Button按钮事件触发");
                //  UI.Open(typeof());
            });

            Button bt1 = GObj.transform.Find("backButton").GetComponent<Button>();
            bt1.onClick.Add(() => {
                Debug.Log("HotFix的Button按钮事件触发");
                UI.Close<GoodsInfoForms>();
            });
        }
    }
}


