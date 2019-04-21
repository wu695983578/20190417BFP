/***
 *	Tittle: "SUIFW" UI框架项目
 *		主题:UI窗体基础类
 *	Description:
 *		功能: 定义所有UI窗体共有方法。
 *		定义四个生命周期
 *		1：Display 显示状态。
 *		2：Hiding  隐藏状态。
 *		3：ReDisney 再显示状态。
 *		4：Freeze 冻结状态。
 *		
 *
 *	Date:   2017
 *	version:    0.1版本
 *	Modify Record:
 *
 */

using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{

    public  class BaseUIForms
    {

        #region 属性

        /*字段*/

        private UIType _CurrentUIType = new UIType();

        /*属性*/
        public UIType CurrentUIType
        {
            get
            {
                return _CurrentUIType;
            }
            set
            {
                _CurrentUIType = value;
            }
        }
        public GameObject gameObject
        {
            get
            {
                return GObj;
            }
            
        }
        public GameObject GObj;
        public string abName;
        public BaseUIForms thisForms;
        #endregion

        #region 脚本生命周期

        public void Awake(BaseUIForms forms)
        {
            if (string.IsNullOrEmpty(abName))
            {
                Log.Error(this.GetType() + "/Awake() 初始化Ui界面失败，Gobj为空！");
                return;
            }
            this.thisForms= forms;
            //string bundleName = this.abName.ToLower();
            string bundleName = abName;
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{bundleName }.unity3d");
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset($"{bundleName}.unity3d", $"{bundleName}");
            GObj = UnityEngine.Object.Instantiate(bundleGameObject);
           // forms.gameObject = GObj;
            InitUI();
        }
        #region 子类重写方法
        /// <summary>
        /// UI初始化方法。_必须
        /// </summary>
        public virtual void InitUI()
        {

        }
       
        #endregion


        #endregion

        #region 窗体生命周期

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            GObj.SetActive(true);
            Log.Debug("执行了Display(),打开了：" + this.GetType());
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                //添加UI遮罩处理
                UIManagerComponent.Instance.SetMaskWindow(GObj);
            }
        }

        /// <summary>
        /// 隐藏状态(窗口关闭了)
        /// </summary>
        public virtual void Hiding()
        {
            GObj.SetActive(false);
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                UIManagerComponent.Instance.CancleMaskWindow();
            }
        }

        /// <summary>
        /// 再显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            GObj.SetActive(true);
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                UIManagerComponent.Instance.SetMaskWindow(GObj);
            }
        }

        /// <summary>
        /// 冻结状态（在栈集合中）
        /// </summary>
        public virtual void Freeze()
        {
            GObj.SetActive(true);
        }

        #endregion
    }

}

