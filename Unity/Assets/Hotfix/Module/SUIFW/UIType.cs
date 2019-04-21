/***
 *	Tittle: "SUIFW" UI框架项目
 *		主题:UI窗体类型
 *	Description:
 *		功能:描述UI窗体的类型
 *
 *	Date:   2017
 *	version:    0.1版本
 *	Modify Record:
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{   
    /// <summary>
    /// UI（窗体）类型
    /// </summary>
    public  class UIType
    {
        //是否需要清空“反向切换”
        public bool NeedClearingStack = false;
        //UI窗体类型
        public UIFormsType UIForms_Type = UIFormsType.Normal;
        //UI窗体显示类型
        public UIFormsShowMode UIForms_ShowMode = UIFormsShowMode.Normal;
    }
}

