using System;
using System.Collections.Generic;

namespace Clones
{
    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> PreviousItem { get; set; }
    }

    public class SmartStack<T>
    {
        StackItem<T> top;

        public SmartStack()
        {
            top = null;
        }

        public SmartStack(StackItem<T> topItem)
        {
            top = topItem;
        }

        public bool IsEmpty { get { return top == null; } }

        public void Push(T value)
        {
            if (value == null)
                return;
            if (IsEmpty)
            {
                top = new StackItem<T> { Value = value, PreviousItem = null };
            }
            else
            {
                top = new StackItem<T> { Value = value, PreviousItem = top };
            }
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                return default(T);
            }
            else
            {
                var tmp = top.Value;
                top = top.PreviousItem;
                return tmp;
            }
        }

        public T GetInsight()
        {
            return this.IsEmpty ? default(T) : top.Value;
        }

        public StackItem<T> GetCopiedTop()
        {
            return IsEmpty ? null : new StackItem<T> { Value = top.Value, PreviousItem = top.PreviousItem };
        }
    }

    public class Clone
    {
        SmartStack<string> programs;
        SmartStack<string> rollbacks;

        public Clone()
        {
            programs = new SmartStack<string>();
            rollbacks = new SmartStack<string>();
        }

        public string Check()
        {
            if (programs.IsEmpty)
                return "basic";
            else
                return programs.GetInsight();
        }

        public void Learn(string program)
        {
            programs.Push(program);
        }

        public void Rollback()
        {
            rollbacks.Push(programs.Pop());
        }

        public void Relearn()
        {
            programs.Push(rollbacks.Pop());
        }

        public Clone Replicate()
        {
            var replicant = new Clone();
            replicant.programs = new SmartStack<string>(this.programs.GetCopiedTop());
            replicant.rollbacks = new SmartStack<string>(this.rollbacks.GetCopiedTop());
            return replicant;
        }
    }

    public class CloneVersionSystem : ICloneVersionSystem
    {
        List<Clone> history;

        public CloneVersionSystem()
        {
            history = new List<Clone>();
            history.Add(new Clone());
        }

        public string Execute(string query)
        {
            var splitQuery = query.Split(' ');
            int index = 0;
            Int32.TryParse(splitQuery[1], out index);
            index--;
            switch (splitQuery[0])
            {
                case "learn":
                    this.history[index].Learn(splitQuery[2]);
                    break;
                case "rollback":
                    this.history[index].Rollback();
                    break;
                case "relearn":
                    this.history[index].Relearn();
                    break;
                case "clone":
                    this.history.Add(this.history[index].Replicate());
                    break;
                case "check":
                    return this.history[index].Check();
            }
            return null;
        }
    }
}