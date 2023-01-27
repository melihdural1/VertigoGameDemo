using System;
using Inventories;
using Rullet;
using UnityEngine;

namespace Controllers
{
    public enum GameState
    {
        menu,
        game,
        pause,
    }

    public class EventController : MonoBehaviour
    {
        public static EventController instance;
        public GameState state = GameState.pause;
        public delegate void UpdateState(GameState state);
        public static UpdateState UpdateStateEvent;
        
        public static Action OnGameStart;
        

        public static Action<float> OnSpinStart;
        public static Action<SpinState> OnSpinStateChange;
        public static Action<int> OnSpinEnd;

        
        public static Action OnPlayerWinReward;
        public static Action OnPlayerFail;
        public static Action<Slot> OnDataUpdate;
        public static Action<int> OnPlayerReachBonus;

        public static Action<BaseInventory> OnInventoriesSubscribeToList;
        
        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            
        }

        public void UpdateStateEventRun(GameState state)
        {
            this.state = state;
            UpdateStateEvent?.Invoke(state);
        }

    }
}