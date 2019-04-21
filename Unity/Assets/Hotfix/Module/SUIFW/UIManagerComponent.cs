/***
 *	Tittle: "SUIFW" UI框架项目
 *		主题:UI管理器
 *	Description:
 *		功能:整个UI框架的核心。用户程序通过该脚本完成框架大部分功能。
 *
 *	Date:   2017
 *	version:    0.1版本
 *	Modify Record:
 *
 */

using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
 

namespace ETHotfix
{
    [ObjectSystem]
    public class UIManagerComponentAwakeSystem : AwakeSystem<UIManagerComponent>
    {
        public override void Awake(UIManagerComponent self)
        {
            self.Awake();
        }
    }
    public class UIManagerComponent : Component
    {

        #region        字段 
        //实例引用
        public static UIManagerComponent Instance;
        //缓存所有UI窗体
        private Dictionary<Type, BaseUIForms> _DicAllUIForms;
        //缓存所有显示中的窗体
        private Dictionary<Type, BaseUIForms> _DicCurrentShowUIForms;
        //栈结构缓存当前显示的层级窗体,
        private Stack<BaseUIForms> _StaCurrentUIForms;


        //UI根节点对象
        private GameObject _GoCanvasRoot = null;
        //顶层面板
        private GameObject _GoTopPlane;
        //遮罩面板
        private GameObject _GoMaskPlane;
        //UI摄像机
        private Camera _UICamear;
        //普通全屏界面节点
        private Transform _CanTransformNormal = null;
        //弹出模式节点
        private Transform _CanTransformPopUp = null;
        //原始UI摄像机的层深
        private float _OriginalUICameraDepth;

        #endregion

        #region 公共方法
        /// <summary>
        /// 加载核心字段数据
        /// </summary>
        public void Awake()
        {
            Instance = this;
           
            _DicAllUIForms = new Dictionary<Type, BaseUIForms>();
            _DicCurrentShowUIForms = new Dictionary<Type, BaseUIForms>();
            _StaCurrentUIForms = new Stack<BaseUIForms>();

            #region Mask逻辑
            //得到UI根节点、UI脚本节点                    
            _GoCanvasRoot = GameObject.Find("UIRoot");
            //得到“顶层面板”与“遮罩面板”
            _GoTopPlane = _GoCanvasRoot;
            _GoMaskPlane = _GoCanvasRoot.transform .Find("PopUp/UIMaskPanels").gameObject;

            //得到UI摄像机的原始“层深”
            _UICamear = GameObject.Find("UICamera").GetComponent<Camera>();
            //得到普通全屏界面节点、固定界面节点、弹出模式节点、UI脚本节点
            _CanTransformNormal =_GoCanvasRoot.transform .Find("Normal");
            _CanTransformPopUp = _GoCanvasRoot.transform .Find("PopUp");
            if (_UICamear != null)
            {
                _OriginalUICameraDepth = _UICamear.depth;
            }
            else
            {
                Debug.Log(GetType() + "/Start()/_UICamera is Null ,please Check!");
            }
            #endregion
        }


        /// <summary>
        /// 打开UI
        /// 
        /// 加载指定名称的UI窗体预设到内存中，对不同显示类型的窗体做不同处理
        /// </summary>
        /// <param name="uiType">ui窗体的类型</param>
        /// <returns></returns>
        public BaseUIForms ShowUIForms(Type uiType)
        {
            //根据UI窗体名称,将预设加载到“所有窗体”的缓存集合中
            Debug.Log("HotFix******************************************");
            //初始化UI窗体
          
            BaseUIForms baseUIForms = this.LoadUIFormFromAllUIFormsCatch(uiType);
            if (baseUIForms == null) return null;

            //是否需要清空【栈集合】，如果需要则清空。 
            if (baseUIForms.CurrentUIType.NeedClearingStack)
            {
                ClearingStack();
            }
            if(!baseUIForms.GObj)
            baseUIForms.Awake(baseUIForms);

            //根据UI窗体的显示模式，做不同的处理
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormsShowMode.Normal:                //默认显示
                    LoadFormsToCurrentShowCache(uiType);
                    break;

                case UIFormsShowMode.ReverseChange:         //反向切换
                    PushUiFormsToStack(uiType);
                    break;

                case UIFormsShowMode.HideOther:             //隐藏其他
                    EnterUIFormsAndHideOther(uiType);
                    break;
            }
            //根据弹窗类型，放在对应的父对象下边
            switch (baseUIForms.CurrentUIType.UIForms_Type)
            {
                case UIFormsType.Normal:
                    baseUIForms.GObj.transform.SetParent(_CanTransformNormal, false);
                    break;
                case UIFormsType.PopUp:
                    baseUIForms.GObj.transform.SetParent(_CanTransformPopUp, false);
                    break;
                default:
                    break;
            }
            return baseUIForms;

        }

        /// <summary>
        ///  关闭指定的UI窗体
        ///  对不同显示类型的窗体做不同处理
        /// </summary>
        /// <param name="uiType">ui窗体的类型</param>
        public void CloseUIForms(Type uiType)
        {
            //
            BaseUIForms baseUIForms;
            _DicAllUIForms.TryGetValue(uiType, value: out   baseUIForms);
            if (baseUIForms == null) return;

            //根据不同显示类型做不同处理
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormsShowMode.Normal:                //【普通窗体】
                    ExitNormalForms(uiType);
                    break;
                case UIFormsShowMode.ReverseChange:         //【反向切换窗体】
                    ExitReverseChangeForms();
                    break;
                case UIFormsShowMode.HideOther:             //【隐藏其他窗体】
                    ExitHideotherFormsAndDisplayOther(uiType);
                    break;
            }

        }

        #endregion
        #region 私有方法



        #region 打开ui窗体的逻辑

        /// <summary>
        /// 将窗体加载到【显示中】字典集合中(普通窗体的显示逻辑)
        /// </summary>
        /// <param name="uiType">ui窗体名称</param>
        private void LoadFormsToCurrentShowCache(Type uiType)
        {
            //检查是否已经存在显示中
            BaseUIForms baseUiForms;
            _DicCurrentShowUIForms.TryGetValue(uiType, out  baseUiForms);
            if (baseUiForms != null)
            {
                return;
            }
            //从全部窗体集合字典中取出baseUIForm 
            BaseUIForms allDictionaryForms;
            _DicAllUIForms.TryGetValue(uiType, out allDictionaryForms);
            if (allDictionaryForms != null)
            {
                _DicCurrentShowUIForms.Add(uiType, allDictionaryForms);
                //显示当前的UI窗体。
                allDictionaryForms.Display();
            }
            else
            {
                Log.Error("Error! allDictionary is Null!!! The UIForm :" + uiType + "is not show!");
            }

        }

        /// <summary>
        /// UI窗体入栈 （隐藏其他窗台类型的显示逻辑)
        /// </summary>
        /// <param name="uiType">ui窗体名称</param>
        private void PushUiFormsToStack(Type uiType)
        {
            //检查【栈】内是否有其他窗体，如果有就将栈顶窗体冻结
            if (_StaCurrentUIForms.Count > 0)
            {
                BaseUIForms topUiForms = _StaCurrentUIForms.Peek();
                //冻结栈顶窗体
                topUiForms.Freeze();
            }
            //从【所有窗体集合】中取出指定名称的窗体
            BaseUIForms baseUiForms = null;
            _DicAllUIForms.TryGetValue(uiType, out baseUiForms);
            if (baseUiForms == null)
            {
                Log.Error("PushUIFormsToStack failed! the uiForms named【" + uiType + "】is null!");
            }
            else
            {
                //显示窗体
                baseUiForms.Display();
                //将窗体压栈
                _StaCurrentUIForms.Push(baseUiForms);
            }
        }

        /// <summary>
        /// 显示【隐藏其他】类型窗体，将其他窗体隐藏
        /// </summary>
        /// <param name="uiType">ui窗体名称</param>
        private void EnterUIFormsAndHideOther(Type uiType)
        {
            BaseUIForms baseUIForm = null;                  //校验用窗体基类
            BaseUIForms baseUIFormFormAll = null;           //从【所有窗体集合】中取出的数据。

            _DicCurrentShowUIForms.TryGetValue(uiType, out baseUIForm);
            if (baseUIForm != null) return;

            //将【栈结构】和【当前显示中集合】中的窗体隐藏

            foreach (BaseUIForms forms in _StaCurrentUIForms)
            {
                forms.Hiding();
            }
            foreach (Type key in _DicCurrentShowUIForms.Keys)
            {
                _DicCurrentShowUIForms[key].Hiding();
            }

            //显示当前窗体，并且加入到【当前显示中集合】
            _DicAllUIForms.TryGetValue(uiType, out baseUIFormFormAll);
            if (baseUIFormFormAll != null)
            {
                baseUIFormFormAll.Display();
                _DicCurrentShowUIForms.Add(uiType, baseUIFormFormAll);
            }
        }

        #endregion

        #region 关闭ui窗体的逻辑

        /// <summary>
        /// 退出当前显示的【普通窗体】
        /// </summary>
        /// <param name="uiType">UI窗体名称</param>
        private void ExitNormalForms(Type uiType)
        {
            BaseUIForms baseUIForms = null;                 //UI窗体基类
            //参数校验【当前显示集合】中没有直接返回
            _DicCurrentShowUIForms.TryGetValue(uiType, out baseUIForms);
            if (baseUIForms == null) return;
            //隐藏窗体
            baseUIForms.Hiding();
            //从【当前显示集合】中移除
            _DicCurrentShowUIForms.Remove(uiType);

        }

        /// <summary>
        /// 退出【反向切换】的窗体。从栈顶弹出隐藏，之后重新显示栈顶的窗体
        /// </summary>
        private void ExitReverseChangeForms()
        {
            if (_StaCurrentUIForms.Count >= 2)
            {
                BaseUIForms topUIForm = _StaCurrentUIForms.Pop();
                topUIForm.Hiding();
                //因为Hiding的原因，会导致栈离的count提前减去。在这里判断一下防止emptyStack异常，以后有好的注意再优化！！！！！！！！！
                if (this._StaCurrentUIForms.Count > 0)
                {
                    BaseUIForms nextUIForm = _StaCurrentUIForms.Peek();
                    nextUIForm.ReDisplay();
                }
               
            }
            else if (_StaCurrentUIForms.Count == 1)
            {
                BaseUIForms topUIForm = _StaCurrentUIForms.Pop();
                topUIForm.Hiding();
            }
        }

        /// <summary>
        /// 退出【隐藏其他】 窗体，且显示其他窗体
        /// </summary>
        /// <param name="uityType">打开的指定窗体名称</param>
        private void ExitHideotherFormsAndDisplayOther(Type uityType)
        {
            BaseUIForms baseUIForm;                          //UI窗体基类

            _DicCurrentShowUIForms.TryGetValue(uityType, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(uityType);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (BaseUIForms baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.ReDisplay();
            }
            foreach (BaseUIForms baseUI in _StaCurrentUIForms)
            {
                baseUI.ReDisplay();
            }
        }

        /// <summary>
        /// 清空栈集合。
        /// </summary>
        private void ClearingStack()
        {
            Log.Debug("------------------------开始清理栈结构");
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count > 0)
            {
                while (this._StaCurrentUIForms.Count > 0)
                {
                    BaseUIForms ui = this._StaCurrentUIForms.Pop();
                     
                        ui.Hiding();
                }
            _StaCurrentUIForms.Clear();
           
            }
            Log.Debug("-----------------------栈结构清理结束");
        }


        #endregion


        #region 加载/创建UI窗体

        /// <summary>
        /// 根据UI窗体名称,将预设加载到“所有窗体”的集合中
        ///     检查指定类型的ui是否已经加载到集合缓存中，如果缓存中没有才进行创建。
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private BaseUIForms LoadUIFormFromAllUIFormsCatch(Type uiType)
        {
            //定义返回结果引用变量。
            BaseUIForms baseUIFormsResult = null;
            //检查_DicAllUIForms缓存中是否存在
            _DicAllUIForms.TryGetValue(uiType, out baseUIFormsResult);

            if (baseUIFormsResult == null)
            {
                //根据指定名称加载UI窗体预设  将预设加载到“所有窗体”的集合中  
                baseUIFormsResult = this.CreateUiForms(uiType);
            }

            return baseUIFormsResult;
        }


        /// <summary>
        /// 根据指定名称创建UI实体 将实体加载到“所有窗体”的集合中
        /// 1.根据uiType创建BaseUIforms实例
        /// 2.把UI克隆体加载到“所有UI窗体集合”
        /// 3.初始化UI窗体。
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private BaseUIForms CreateUiForms(Type uiType)
        {
            //根据类型创建
            BaseUIForms baseUIForms = Activator.CreateInstance(uiType) as BaseUIForms;
            if (baseUIForms == null)
            {
                Log.Error("Error!BaseUIForms is null,please check the UIForm：" + uiType.Name);
                return null;
            }
          
            //加载到“所有UI窗体集合”
            this._DicAllUIForms.Add(uiType, baseUIForms);

            return baseUIForms;
        }



        #endregion



        #endregion

        #region mask逻辑
        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayPlane">需要显示的窗体</param>
        public void SetMaskWindow(GameObject goDisplayPlane)
        {
            //顶层窗体下移。
            _GoTopPlane.transform.SetAsLastSibling();
            _GoMaskPlane.SetActive(true);

            //遮罩窗体下移
            _GoMaskPlane.transform.SetAsLastSibling();
            //显示窗体下移
            goDisplayPlane.transform.SetAsLastSibling();
            //增加当前UI摄像机的“层深”
            if (_UICamear != null)
            {
                _UICamear.depth = _UICamear.depth + 100;
            }
        }

        /// <summary>
        /// 取消遮罩窗体
        /// </summary>
        public void CancleMaskWindow()
        {
            //顶层窗体上移
            _GoTopPlane.transform.SetAsFirstSibling();
            //禁用遮罩窗体
            if (_GoMaskPlane.activeInHierarchy)
            {
                _GoMaskPlane.SetActive(false);
            }
            //回复UI摄像机的原来的“层深”
            _UICamear.depth = _OriginalUICameraDepth;
        }
        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            
            Instance = null;
            this._DicAllUIForms.Clear();
            this._DicCurrentShowUIForms.Clear();
            this._StaCurrentUIForms.Clear();

            base.Dispose();
        }



    }//end class

}

