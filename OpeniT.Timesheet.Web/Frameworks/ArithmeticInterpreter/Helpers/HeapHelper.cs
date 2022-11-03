using System;
using System.Collections.Generic;
using System.Linq;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;
using static OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers.ContractHelper;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers
{
    public class HeapHelper
    {
        public Dictionary<string, Heap> Heap { get; set; } = new Dictionary<string, Heap>();
        public Heap this[string key]
        {
            get => Get(key);
            set
            {
                if (Heap.ContainsKey(key))
                {
                    Heap[key] = value;
                }
                else
                {
                    Heap.Add(key, value);
                }
            }
        }

        public Heap Add(string key, double value)
        {
            var heap = new Heap();
            heap.Key = key;
            heap.Value = value;
            this[key] = heap;

            return heap;
        }

        public Heap Add(SystemVariable value)
        {
            var heap = new Heap();
            heap.Key = "System";

            var requiredHours = new Heap() { Key = "RequiredHours", Value = value.RequiredHours };
            var recordedHours = new Heap() { Key = "RecordedHours", Value = value.RecordedHours };
            var previousMonth = new Heap() { Key = "PreviousMonth" };
            heap.Heaps.Add(requiredHours);
            heap.Heaps.Add(recordedHours);
            heap.Heaps.Add(previousMonth);
            foreach (var val in value.PreviousMonth)
            {
                var heap_ = new Heap() { Key = val.Name, Value = Convert.ToDouble(val.Value) };
                previousMonth.Heaps.Add(heap_);
            }
            this["System"] = heap;

            return heap;
        }

        public Heap Get(string str)
        {
            var s = str.Split(".");
            var len = s.Length;

            if (len == 1)
            {
                return Heap[str];
            }
            else
            {
                var currentHeap = Heap[s[0]];
                Heap value = null;
                for (int i = 1; i < s.Length; i++)
                {
                    var s_ = s[i];
                    var isLastItem = s.Length - 1 == i;
                    if (isLastItem)
                    {
                        value = currentHeap.Heaps.Where(x => x.Key == s_).FirstOrDefault();
                    }
                    else
                    {
                        currentHeap = currentHeap.Heaps.Where(x => x.Key == s_).FirstOrDefault();
                    }
                }

                return value;
            }

        }

        public bool Contains(string str)
        {
            var s = str.Split(".");
            var len = s.Length;

            if (len == 1)
            {
                return Heap.ContainsKey(str);
            }
            else
            {
                var currentHeap = Heap[s[0]];
                var exist = false;
                for (int i = 1; i < s.Length; i++)
                {
                    var s_ = s[i];
                    var isLastItem = s.Length - 1 == i;
                    if (isLastItem)
                    {
                        exist = currentHeap.Heaps.Where(x => x.Key == s_).Any();
                    }
                    else
                    {
                        currentHeap = currentHeap.Heaps.Where(x => x.Key == s_).FirstOrDefault();
                    }
                }

                return exist;
            }
        }

    }
}
