using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsWinFormsClientEngine
{
    public class StateSystem
    {
        Dictionary<EnumState, IGameObject> _stateStore = new Dictionary<EnumState, IGameObject>();
        IGameObject _currentState = null;

        public void Update(double elapsedTime)
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

        public void AddState(EnumState stateId, IGameObject state)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId) == false);
            _stateStore.Add(stateId, state);
        }

        public void ChangeState(EnumState stateId)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId));
            _currentState = _stateStore[stateId];
        }

        public bool Exists(EnumState stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }
    }
}
