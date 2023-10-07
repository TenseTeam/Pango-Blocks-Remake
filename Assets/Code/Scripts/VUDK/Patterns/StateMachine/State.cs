namespace VUDK.Patterns.StateMachine
{
    using System;
    using VUDK.Patterns.StateMachine.Interfaces;

    public abstract class State : IEventState
    {
        private StateMachine _relatedStateMachine;

        protected Context Context { get; private set; }

        protected State(StateMachine relatedStateMachine, Context context)
        {
            _relatedStateMachine = relatedStateMachine;
            Context = context;
        }

        /// <summary>
        /// Called when entering the state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public abstract void Exit();

        /// <summary>
        /// Called to process the state's logic each frame.
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// Called to process the state's logic each fixed frame.
        /// </summary>
        public abstract void PhysicsProcess();

        /// <summary>
        /// Changes the state of its related state machine.
        /// </summary>
        /// <param name="key">State key.</param>
        protected void ChangeState(Enum key) 
        {
            _relatedStateMachine.ChangeState(key);
        }

        /// <summary>
        /// Changes the state of its related state machinee to a state in the dictionary by its key after waiting for seconds.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        /// <param name="timeToWait">Time to wait in seconds.</param>
        protected void ChangeState(Enum key, float time)
        {
            _relatedStateMachine.ChangeState(key, time);
        }
    }

    public abstract class State<T> : State, IContextConverter<T> where T : Context
    {
        public new T Context => (T)base.Context;

        protected State(StateMachine relatedStateMachine, Context context) : base(relatedStateMachine, context)
        {
        }
    }
}