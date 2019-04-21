/***
 *	Tittle: "SUIFW" UI框架项目
 *		主题:全局配置类
 *	Description:
 *		功能:1.定义系统常量。
 *		     2.定义系统全局方法。
 *		     3.定义系统的枚举类型。
 *		     4.系统的委托定义。
 *
 *	Date:   2017
 *	version:    0.1版本
 *	Modify Record:
 *
 */
using UnityEngine;

namespace ETModel
{
    #region 系统枚举
    /// <summary>
    /// UI窗体类型
    /// </summary>
    public enum UIFormsType
    {
        Normal,             // 普通全屏界面(例如主城UI界面)
        PopUp,              // 弹出模式(小窗口)窗口 (例如：商场、背包、确认窗口等)
    }

    /// <summary>
    /// UI窗体显示类型
    /// </summary>
    public enum UIFormsShowMode
    {
        Normal,             //普通显示
        ReverseChange,      //反向切换      
        HideOther,          //隐藏其他界面 
    }

    #endregion


}

