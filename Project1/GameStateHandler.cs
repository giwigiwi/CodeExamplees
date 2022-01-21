using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using UnityEngine.Events;



namespace RunawayBride
{
    public enum FailReason
    {
        obstacle,
        flower,
        bridge
    }
    public class GameStateHandler : Singleton<GameStateHandler>
    {

        public UnityAction<int> onLevelStart;
        public UnityAction<int> onLevelRestart;
        public UnityAction onPlayerFinish;
        public UnityAction<FailReason> onPlayerFail;
        public UnityAction<PortalData> OnPortalEnter;

    }
}