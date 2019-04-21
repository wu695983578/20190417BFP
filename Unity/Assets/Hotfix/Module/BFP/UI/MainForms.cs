using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public class MainForms : BaseUIForms
    {
        public MainForms()
        {
            abName = "MainForms";
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.HideOther;
            this.CurrentUIType.UIForms_Type = UIFormsType.Normal;

        }

        public override void InitUI()
        {
            EventCenterComponent testEvent = Game.Scene.GetComponent<EventCenterComponent>();
            //Debug.Log(testEvent+"KKKKKKKKKKKKKKKKKK");
            // var main= thisForms as MainForms;
            testEvent.Test.AddListener(() =>
            {
                 Text bt22 = GObj.transform.Find("EnterButton/Text").GetComponent<Text>();
                 bt22.text = "2222222222";
            });
            Button bt = GObj.transform.Find("EnterButton").GetComponent<Button>();
            bt.onClick.Add(() =>
            {
                Debug.Log("HotFix的Button按钮事件触发");
              
                UI.Open<MarketForms>();
            });


            Button bt1 = GObj.transform.Find("backButton").GetComponent<Button>();
            bt1.onClick.Add(() =>
            {
                Debug.Log("HotFix的Button按钮事件触发");
                UI.Close<MainForms>();
            });
        }
    }
}

