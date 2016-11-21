using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public enum Type
    {
        Binary = 0,
        Hash = 1,
        Linear = 2
    }
    public class Table<TKey, TValue>
    {
        private Dictionary<TKey, TValue> table = new Dictionary<TKey, TValue>();
        private Type type;

        public void Select(TKey key)
        {
            Table<TKey, TValue> buffer = new Table<TKey, TValue>();
            if (SearchType == Type.Hash && table.ContainsKey(key))
            {
                buffer.Insert(key, table[key]);
            }
            if (SearchType == Type.Binary)
            {
                string thatKey = (string)(object)key;
                do
                {
                    foreach (KeyValuePair<TKey, TValue> kvp in table)
                    {
                        string thisKey = (string)(object)kvp.Key;
                        if (thatKey.Length <= thisKey.Length)
                        {
                            thisKey = ((string)(object)kvp.Key).Substring(0, thatKey.Length);
                        }
                        if (thisKey == thatKey && !buffer.table.ContainsKey(kvp.Key))
                        {
                            buffer.Insert(kvp.Key, kvp.Value);
                        }
                    }
                    if (!buffer.table.Any()) { thatKey = thatKey.Substring(0, thatKey.Length - 1); }
                } while (!buffer.table.Any() || thatKey.Length == 0);
            }
            if (SearchType == Type.Linear)
            {
                string thatKey = key.ToString();
                foreach (KeyValuePair<TKey, TValue> kvp in table)
                {
                    string thisKey = kvp.Key.ToString();
                    if (thisKey.Contains(thatKey) && !buffer.table.ContainsKey(kvp.Key))
                    {
                        buffer.Insert(kvp.Key, kvp.Value);
                    }
                }
            }
            if (buffer.table.Any())
            {
                buffer.Print();
            }
            else
            {
                Console.WriteLine(new string('-', 30) + "/nThis key is empty!");
            }
        }
        public void Insert(TKey key, TValue value)
        {
            this.table.Add(key, value);
        }
        public void Update(TKey oldKey, TValue newValue)
        {
            this.table[oldKey] = newValue;
        }
        public void Delete(TKey key)
        {
            this.table.Remove(key);
        }
        public void Print()
        {
            Console.WriteLine(new string('-', 30));
            foreach (KeyValuePair<TKey, TValue> kvp in table)
            {
                Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }
        }
        public Type SearchType
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Table<int, string> tableHash = new Table<int, string>();
            tableHash.SearchType = Type.Hash;
            tableHash.Insert(11, "val");
            tableHash.Insert(21, "val2");
            tableHash.Insert(211, "val2");
            tableHash.Insert(13, "val3");
            tableHash.Print();
            tableHash.Select(21);

            Table<string, string> tableBinary = new Table<string, string>();
            tableBinary.SearchType = Type.Binary;
            tableBinary.Insert("121212", "val");
            tableBinary.Insert("1212", "val2");
            tableBinary.Insert("12", "val3");
            tableBinary.Print();
            tableBinary.Select("12133");

            Table<int, string> tableLinear = new Table<int, string>();
            tableLinear.SearchType = Type.Linear;
            tableLinear.Insert(123456789, "val");
            tableLinear.Insert(12345678, "val2");
            tableLinear.Insert(1234567890, "val3");
            tableLinear.Print();
            tableLinear.Select(89);

            //Delay
            Console.ReadKey();
        }
    }
}