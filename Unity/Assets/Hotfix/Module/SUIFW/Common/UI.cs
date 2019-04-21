/**
 *
 */

using System;
using ETModel;

namespace ETHotfix
{
    public static class UI
    {
        public static BaseUIForms Open<T>() where T:BaseUIForms
        {
            return  UIManagerComponent.Instance.ShowUIForms(typeof(T));
        }


        public static void Close<T>() where T :BaseUIForms
        {
            UIManagerComponent.Instance.CloseUIForms(typeof(T));
        }

        /// <summary>
        /// 清空ui
        /// </summary>
        public static void ClearUI()
        {
            Game.Scene.RemoveComponent<UIManagerComponent>();
            Game.Scene.AddComponent<UIManagerComponent>();
        }

//        /// <summary>
//        /// 提示
//        /// </summary>
//        /// <param name="content"></param>
//        private static GComponent tip;
//        public static void Tip(string content)
//        {
//            if (tip == null)
//            {
//#if Editor
//                UIPackage.AddPackage("UI/Common");
//#endif
//                tip = UIPackage.CreateObject("Common", "Tip").asCom;
//            }
//            //SoundComponent.Instance?.PlayClip(SoundName.window_tips);
//            tip.GetChild("textContent").asTextField.text = content;
//            GRoot.inst.ShowPopup(tip);
//            tip.Center();
//        }

    }//class_end
}