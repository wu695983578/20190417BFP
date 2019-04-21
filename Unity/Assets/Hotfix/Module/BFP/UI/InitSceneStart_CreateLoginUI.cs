﻿using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.InitSceneStart)]
    public class InitSceneStart_CreateLoginUI : AEvent
    {
        public override void Run()
        {
           UI.Open<LobbyBgUI>();
        }
    }
}
