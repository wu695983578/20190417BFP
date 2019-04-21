using System;
namespace ETModel
{
    public class Events
    {
        private Action _action;
        public string EventName { get; private set; }

        public Events(string name = "")
        {
            EventName = name;
        }

        public void AddListener(Action listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action listener)
        {
            if (_action != null)
                _action -= listener;
        }

        public void Broadcast()
        {
            if (_action != null)
                _action();
        }

        public void Clear()
        {
            _action = null;
        }
    }

    public class Events<T>
    {
        private Action<T> _action;
        public string EventName { get; private set; }

        public Events(string name = "")
        {
            EventName = name;
        }

        public void AddListener(Action<T> listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            if (_action != null)
                _action -= listener;
        }

        public void Broadcast(T p1)
        {
            if (_action != null)
                _action(p1);
        }

        public void Clear()
        {
            _action = null;
        }
    }

    public class Events<T1, T2>
    {
        private Action<T1, T2> _action;
        public string EventName { get; private set; }

        public Events(string name = "")
        {
            EventName = name;
        }

        public void AddListener(Action<T1, T2> listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action<T1, T2> listener)
        {
            if (_action != null)
                _action -= listener;
        }

        public void Broadcast(T1 p1, T2 p2)
        {
            if (_action != null)
                _action(p1, p2);
        }

        public void Clear()
        {
            _action = null;
        }
    }

    public class Events<T1, T2, T3>
    {
        private Action<T1, T2, T3> _action;
        public string EventName { get; private set; }

        public Events(string name = "")
        {
            EventName = name;
        }

        public void AddListener(Action<T1, T2, T3> listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3> listener)
        {
            if (_action != null)
                _action -= listener;
        }

        public void Broadcast(T1 p1, T2 p2, T3 p3)
        {
            if (_action != null)
                _action(p1, p2, p3);
        }

        public void Clear()
        {
            _action = null;
        }
    }

    public class Events<T1, T2, T3, T4>
    {
        private Action<T1, T2, T3, T4> _action;
        public string EventName { get; private set; }

        public Events(string name = "")
        {
            EventName = name;
        }

        public void AddListener(Action<T1, T2, T3, T4> listener)
        {
            _action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3, T4> listener)
        {
            if (_action != null)
                _action -= listener;
        }

        public void Broadcast(T1 p1, T2 p2, T3 p3, T4 p4)
        {
            if (_action != null)
                _action(p1, p2, p3, p4);
        }

        public void Clear()
        {
            _action = null;
        }
    }

}