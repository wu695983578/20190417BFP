/**
 *
 */

using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace ETModel
{
    public static class UI
    {
        public static BaseUIForms Open(Type type)
        {
            return UIManagerComponent.Instance.ShowUIForms(type);
        }


        public static void Close(Type type)
        {
            UIManagerComponent.Instance.CloseUIForms(type);
        }

        /// <summary>
        /// 清空ui
        /// </summary>
        public static void ClearUI()
        {
            Game.Scene.RemoveComponent<UIManagerComponent>();
            Game.Scene.AddComponent<UIManagerComponent>();
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="content"></param>
        private static GComponent tip;
        public static void Tip(string content)
        {
            if (tip == null)
            {
#if UNITY_EDITOR
                UIPackage.AddPackage("UI/Common");

#else
                if (UIPackage.GetByName("Common") == null)
                {
                    Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle("common");

                    AssetBundle bundle = Game.Scene.GetComponent<ResourcesComponent>().bundles["common"].AssetBundle;
                    //赋值给FairyGUi。
                    UIPackage.AddPackage(bundle);
                }

              
#endif
                //UIPackage uiPackage = UIPackage.GetByName("Common");
                //if (uiPackage == null)
                //{
                //    Log.Error("这个包common是空的");
                //}

                //List<PackageItem> packageItems = uiPackage.GetItems();
                //foreach (PackageItem item in packageItems)
                //{
                //    Log.Error("这个包里有:"+item.name);
                //}
                tip = UIPackage.CreateObject("Common", "Tip").asCom;
            }
            tip.GetChild("textContent").asTextField.text = content;
            GRoot.inst.ShowPopup(tip);
            tip.Center();
        }


    }//class_end
}