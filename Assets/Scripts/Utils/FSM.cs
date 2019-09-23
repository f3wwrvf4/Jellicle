using System;
using System.Collections;
using System.Reflection;

namespace Utils
{
    public class FSM<T, ID> where ID : IComparable, IConvertible, IFormattable
    {
        T obj;
        public FSM(T ob)
        {
            obj = ob;
            Setup();
        }

        System.Action[] initFuncs;
        System.Action[] procFuncs;
        System.Action[] termFuncs;

        int curr = -1;
        int next = 0;
        void Setup()
        {
            Type tp = typeof(ID);
            int max = Enum.GetValues(tp).Length;
            initFuncs = new System.Action[max];
            procFuncs = new System.Action[max];
            termFuncs = new System.Action[max];

            foreach (Object ob in Enum.GetValues(tp))
            {
                int id = (int)ob;
                string stateName = ob.ToString();
                initFuncs[id] = (System.Action)Delegate.CreateDelegate(typeof(System.Action), obj, stateName + "_Init", false, false);
                procFuncs[id] = (System.Action)Delegate.CreateDelegate(typeof(System.Action), obj, stateName + "_Proc", false, false);
                termFuncs[id] = (System.Action)Delegate.CreateDelegate(typeof(System.Action), obj, stateName + "_Term", false, false);
            }
        }

        public ID Get()
        {
            return (ID)Enum.ToObject(typeof(ID), curr);
        }

        public void Set(ID id)
        {
            next = Convert.ToInt32(id);
        }

        public void Init(ID id)
        {
            Set(id);
            while (curr != next)
            {
                if (curr != -1)
                    termFuncs[curr]();
                curr = next;
                initFuncs[next]();
            }
        }

        public void Proc()
        {
            while (curr != next)
            {
                if (curr != -1)
                    if (termFuncs[curr] != null) termFuncs[curr]();
                curr = next;
                if (initFuncs[curr] != null) initFuncs[curr]();
            }
            if (procFuncs[curr] != null) procFuncs[curr]();
        }

        public void Term()
        {
            if (termFuncs[curr] != null) termFuncs[curr]();
            curr = -1;
        }
    }
}