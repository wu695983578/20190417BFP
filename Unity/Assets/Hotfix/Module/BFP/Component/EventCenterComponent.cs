using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ETHotfix
{
    public class EventCenterComponent : Component
    {
        //测试
        public readonly Events<string> Test = new Events<string>(); //发送测试事件
    }
}


