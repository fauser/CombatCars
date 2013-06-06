using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CombatCarsWinformsClient.State;

namespace CombatCarsWinformsClient
{
    class StateSystem
    {
        Dictionary<EnumState, IGameObject> _stateStore = new Dictionary<EnumState, IGameObject>();
        IGameObject _currentState = null;

        internal void Update(double elapsedTime)
        {
            if (_currentState == null)
                return;

            _currentState.Update(elapsedTime);
        }

        public void Render()
        {
            if (_currentState == null)
                return;

            _currentState.Render();
        }

        internal void AddState(EnumState stateId, IGameObject state)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId) == false);
            _stateStore.Add(stateId, state);
        }

        internal void ChangeState(EnumState stateId)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId));
            _currentState = _stateStore[stateId];
        }

        internal bool Exists(EnumState stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }
    }
}
