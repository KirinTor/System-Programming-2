using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    static class Coincidence
    {
        public static string ReplaceAll(this string str)
        {
            str = str.Replace('і', 'i');
            str = str.Replace('І', 'I');
            str = str.Replace('е', 'e');
            str = str.Replace('Е', 'E');
            str = str.Replace('м', 'M');
            str = str.Replace('Н', 'H');
            str = str.Replace('о', 'o');
            str = str.Replace('О', 'O');
            str = str.Replace('Р', 'P');
            str = str.Replace('х', 'x');
            str = str.Replace('Х', 'X');
            str = str.Replace('Т', 'T');
            str = str.Replace('у', 'y');
            str = str.Replace('р', 'p');
            str = str.Replace('а', 'a');
            str = str.Replace('А', 'A');
            str = str.Replace('к', 'k');
            str = str.Replace('К', 'K');
            str = str.Replace('с', 'c');
            str = str.Replace('С', 'C');
            str = str.Replace('В', 'B');
            return str;
        }

    }
    class Table
    {
        private List<TableRow> table = new List<TableRow>();
        private int point = 0;
        private int linPoint = 0;
        private int binPoint = 0;
        private TableRow buffer = null;

        public Table(List<TableRow> table)
        {
            this.table = table;
        }
        public Table(params TableRow[] table)
        {
            foreach(TableRow tableRow in table)
            {
                this.table.Add(tableRow);
            }
        }
        public TableRow SelectByDirectAddress(int address)
        {
            return table[address];
        }
        public Table SelectByDirectKey(ushort key)
        {
            Table result = new Table();
            if (point >= table.Count) point = 0;
            for (int i = point+1; i < table.Count; i++)
            {
                if (table[i].Key.UshortKey == key)
                {
                    result.Insert(table[i]);
                    point=i;
                    break;
                }
            }
            if (!result.table.Any())
            {
                point = 0;
                for (int i = point+1; i < table.Count; i++)
                {
                    if (table[i].Key.UshortKey == key)
                    {
                        result.Insert(table[i]);
                        point = i;
                        break;
                    }
                }
            }
            return result;
        }
        public void Insert(TableRow tableRow)
        {
            this.table.Add(tableRow);
        }
        public void Delete(Key key)
        {
            foreach(TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                {
                    table.Remove(tableRow);
                    break;
                }
            }
        }
        public void Clear()
        {
            table.Clear();
        }
        public void Update(Key key, Union value)
        {
            foreach (TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                    tableRow.Value=value;
            }
        }
        public Table SelectBySimilarSearch(Key key)
        {
            string searchKey=key.StringKey.ReplaceAll().ToLower();

            Table result = new Table();
            int maxSimilarity = 0;
            while (searchKey.Length > 0)
            {
                foreach (TableRow tableRow in table)
                {
                    string thisKey = tableRow.Key.StringKey.ReplaceAll().ToLower();
                    int tempSimilarity = 0;

                    for (int i = 0; i < Math.Min(thisKey.Length, searchKey.Length);)
                    {
                        if (thisKey[i] == searchKey[i]) 
                        {
                            tempSimilarity++;
                            i++;
                            result.Delete(tableRow.Key);
                            result.Insert(tableRow);
                        }
                        else
                        {
                            if (tempSimilarity > maxSimilarity)
                            {
                                maxSimilarity = tempSimilarity;
                                result.Clear();
                            }
                            break;
                        }
                    }
                }
                if (result.table.Any()) break;
                else searchKey.Substring(0, searchKey.Length - 1);
            }
            return result;
        }
        private bool Сontains(Key key)
        {
            foreach(TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key)) return true;
            }
            return false;
        }
        
        public TableRow SelectByLinearSearch(Key key)
        {
            TableRow result = null;
                if (linPoint >= table.Count) linPoint = 0;
                for (int i = linPoint; i < table.Count; i++)
                {
                    if (table[i].Key.StringKey.Length > key.StringKey.Length)
                    {
                        if (table[i].Key.StringKey.Contains(key.StringKey))
                        {
                            result = table[i];
                            linPoint = i+1;
                            break;
                        }
                    }
                    else
                    {
                        if (table[i].Key.StringKey == key.StringKey)
                        {
                            result = table[i];
                            linPoint = i+1;
                            break;
                        }
                    }
                }
                if (result==null)
                {
                    linPoint = 0;
                    for (int i = linPoint; i < table.Count; i++)
                    {
                        if (table[i].Key.StringKey.Length > key.StringKey.Length)
                        {
                            if (table[i].Key.StringKey.Contains(key.StringKey))
                            {
                                result = table[i];
                                linPoint = i+1;
                                break;
                            }
                        }
                        else
                        {
                            if (table[i].Key.StringKey == key.StringKey)
                            {
                                result = table[i];
                                linPoint = i+1;
                                break;
                            }
                        }
                    }
                }
            return result;
        }
        /*public TableRow SelectByLinearSearch(Key key)
        {
            TableRow result = null;
            if (point >= table.Count) point = 0;
            for (int i = point + 1; i < table.Count; i++)
            {
                if (CompareKey(table[i].Key, key) == 0)
                {
                    result = table[i];
                    point = i;
                    break;
                }

            }
            if (result == null)
            {
                point = 0;
                for (int i = point + 1; i < table.Count; i++)
                {

                    if (CompareKey(table[i].Key, key) == 0)
                    {
                        result = table[i];
                        point = i;
                        break;
                    }
                }
            }
            return result;
        }*/
        /*public TableRow SelectByBinarySearch(Key key)
        {
            TableRow result = null;
            List<TableRow> copy = SortAndCopy();
            TableRow[] array = new TableRow[copy.Count];
            array = copy.ToArray();

            int index = binarySearch(array, point, array.Length - 1, key);
            if (index >= 0)
            {
                result = array[index];
                point = result.Key.UshortKey;
            }
            else
            {
                point = 0;
            }
            return result;
        }*/
        /*private List<TableRow> SortAndCopy()
        {
            List<TableRow> result = new List<TableRow>(table.GetRange(point, table.Count - point - 1));
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (CompareKey(result[j].Key, result[j + 1].Key) > 0)
                    {
                        TableRow buf = result[j];
                        result.Insert(j, result[j + 1]);
                        result.Insert(j + 1, buf);
                    }
                }
            }
            return result;
        }*/
        private int CompareString(string s1, string s2)
        {
            int n = Math.Min(s1.Length, s2.Length);

            for (int i = 0; i < n; i++)
            {
                if (s1[i] != s2[i])
                {
                    return s1[i] - s2[i];
                }
            }
            return 0;
        }
        private int CompareKey(Key key1, Key key2)
        {
            int result = 0;
            if (CompareString(key1.StringKey, key2.StringKey) > 0)
            {
                result = 1;
            }
            else if (CompareString(key1.StringKey, key2.StringKey) < 0)
            {
                result = -1;
            }
            else if (key1.UshortKey - key2.UshortKey > 0)
            {
                result = 1;
            }
            else if (key1.UshortKey - key2.UshortKey < 0)
            {
                result = -1;
            }
            return result;
        }


        public int BinarySearch(List<string> keys, string key, int low, int high) //from Wikipedia
        {
            //keys.Sort();
            int i = -1;
            if (keys != null)
            {
                int mid;
                while (low < high)
                {
                    mid = (low + high) / 2; 
                    if (keys[mid].Contains(key) || (keys[mid].Length >= key.Length && keys[mid].Substring(0, key.Length)==key))
                    {
                        i = mid;
                        break;
                    }
                    else
                    {
                        if (CompareString(keys[mid], key)>0)
                        {
                            high = mid;
                        }
                        else
                        {
                            low = mid + 1;
                        }
                    }
                }
            }
            return i;
        }
        public TableRow SelectByBinarySearch(Key key)
        {
            TableRow result = null;         
            List<TableRow> copy = new List<TableRow>();

            if (binPoint >= table.Count) binPoint = 0;
            for (int i=0; i<table.Count; i++)
            {
                copy.Add(table[i]);
            }

            List<string> keys = new List<string>();            
            for (int i = 0; i < copy.Count; i++)
                keys.Add(copy[i].Key.StringKey);

            //int index = keys.BinarySearch(key.StringKey);
            int index = BinarySearch(keys, key.StringKey, binPoint+1, keys.Count);
            if (index >= 0)
            {
                binPoint=index;
                foreach (TableRow tableRow in copy)
                {
                    if (tableRow.Key.StringKey == keys[binPoint])
                    {
                        result = tableRow;
                        break;
                    }
                }
            }
            if (result==null)
            {
                index = BinarySearch(keys, key.StringKey, 0, binPoint);
                if (index >= 0)
                {
                    binPoint=index;
                    foreach (TableRow tableRow in copy)
                    {
                        if (tableRow.Key.StringKey == keys[binPoint])
                        {
                            result = tableRow;
                            break;
                        }
                    }
                }
            }
            return result;
        }
       
        /*
                private int getRowIndexByForeignAddressKey(String foreignKey)
                {
                    int resultIndex = -1;
                    int maxNumberOfIdenticalChars = -1;
                    int currentNumberOfIdenticalChars;

                    if ((foreignKey != null) || (foreignKey.length() > 0))
                    {

                        for (int i = 0; i < mainTable.size(); i++)
                        {
                            String currentForeignKey = mainTable.get(i).getForeignAddressKey();

                            currentNumberOfIdenticalChars = 0;

                            int comparisonLength = (currentForeignKey.length() >= foreignKey.length()) ? foreignKey.length() : currentForeignKey.length();

                            for (int j = 0; j < comparisonLength; j++)
                            {

                                if (isCharInUnicodeCoincidence(currentForeignKey.charAt(j)) && isCharInUnicodeCoincidence(foreignKey.charAt(j)))
                                {
                                    // check row numbers inside unicodeTable
                                    if (getCharRowInUnicodeCoincidence(currentForeignKey.charAt(j)) == getCharRowInUnicodeCoincidence(foreignKey.charAt(j)))
                                    {
                                        currentNumberOfIdenticalChars++;
                                    }
                                    else break;

                                }
                                else
                                {
                                    if ((currentForeignKey.substring(j, j + 1).compareToIgnoreCase(foreignKey.substring(j, j + 1))) == 0)
                                    {
                                        currentNumberOfIdenticalChars++;
                                    }
                                    else break;
                                }
                            }

                            if ((currentNumberOfIdenticalChars != 0) && (currentNumberOfIdenticalChars >= maxNumberOfIdenticalChars))
                            {
                                maxNumberOfIdenticalChars = currentNumberOfIdenticalChars;
                                resultIndex = i;
                            }
                        }
                    }
                    return resultIndex;
                }

                public void addRow(TableRow tableRow)
                {
                    mainTable.add(tableRow);
                }

                public void showTable(ArrayList<TableRow> mainTable)
                {
                    for (TableRow row : mainTable)
                    {
                        System.out.println(row.toString());
                    }
                }

                public void showTable()
                {
                    for (TableRow row : mainTable)
                    {
                        System.out.println(row.toString());
                    }
                }

                // --- Linear search ---

                // - direct address -
                public void createRowDirectAdr(TableRow newTableRow)
                {
                    if (isAlreadyInTable(newTableRow.getDirectAddressKey(), newTableRow.getForeignAddressKey())) mainTable.add(newTableRow);
                    else System.out.println("Already exist with identical key");
                }

                public void readRowDirectAdr(short directKey)
                {
                    int currentIndex = getRowIndexByDirectAdrKey(directKey);
                    if (currentIndex != -1) showRow(currentIndex);
                }

                public void updateRowDirectAdr(short directKey, TableRow updatedTableRow)
                {
                    int currentIndex = getRowIndexByDirectAdrKey(directKey);
                    if (currentIndex != -1) mainTable.get(currentIndex).update(updatedTableRow);
                    else System.out.println("No such field");
                }

                public void deleteRowDirectAdr(short directKey)
                {
                    int currentIndex = getRowIndexByDirectAdrKey(directKey);
                    if (currentIndex != -1) mainTable.remove(currentIndex);
                    else System.out.println("No such field");
                }

                // - foreign address -
                public void createRowForeignAdr(TableRow newTableRow)
                {
                    if (isAlreadyInTable(newTableRow.getDirectAddressKey(), newTableRow.getForeignAddressKey())) mainTable.add(newTableRow);
                    else System.out.println("Already exist with identical key");
                }

                public void readRowForeignAdr(String foreignKey)
                {
                    int currentIndex = getRowIndexByForeignAddressKey(foreignKey);
                    if (currentIndex != -1) showRow(currentIndex);
                    else System.out.println("No such field");
                }

                public void updateRowForeignAdr(String foreignKey, TableRow updatedTableRow)
                {
                    int currentIndex = getRowIndexByForeignAddressKey(foreignKey);
                    if (currentIndex != -1) mainTable.get(currentIndex).update(updatedTableRow);
                    else System.out.println("No such field");
                }

                public void deleteRowForeignAdr(String foreignKey)
                {
                    int currentIndex = getRowIndexByForeignAddressKey(foreignKey);
                    if (currentIndex != -1) mainTable.remove(currentIndex);
                    else System.out.println("No such field");
                }

                // --- Binary search ---

                private int getNumberOfIdenticalChars(String currentForeignKey, String foreignKey)
                {
                    int currentNumberOfIdenticalChars = 0;
                    int comparisonLength = (currentForeignKey.length() > foreignKey.length()) ? foreignKey.length() : currentForeignKey.length();

                    for (int j = 0; j < comparisonLength; j++)
                    {

                        if (isCharInUnicodeCoincidence(currentForeignKey.charAt(j)) && isCharInUnicodeCoincidence(foreignKey.charAt(j)))
                        {
                            // check row numbers inside unicodeTable
                            if (getCharRowInUnicodeCoincidence(currentForeignKey.charAt(j)) == getCharRowInUnicodeCoincidence(foreignKey.charAt(j)))
                            {
                                currentNumberOfIdenticalChars++;
                            }
                            else break;

                        }
                        else
                        {
                            if ((currentForeignKey.substring(j, j + 1).compareToIgnoreCase(foreignKey.substring(j, j + 1))) == 0)
                            {
                                currentNumberOfIdenticalChars++;
                            }
                            else break;
                        }
                    }

                    return currentNumberOfIdenticalChars;
                }

                public void getRowWithBinarySearch(String foreignKey)
                {

                    int currentIndex = -1;

                    ArrayList<TableRow> tempTableList = (ArrayList<TableRow>)mainTable.clone();
                    ComparatorByKey comparator = new ComparatorByKey();
                    Collections.sort(tempTableList, comparator);

                    System.out.println("Sorted table : ");
                    showTable(tempTableList);

                    int start = 0;
                    int end = tempTableList.size() - 1;
                    int middle;

                    while (start < end)
                    {
                        middle = (start + end) / 2;

                        if ((end - start) == 1)
                        {
                            int startValue = getNumberOfIdenticalChars(tempTableList.get(start).getForeignAddressKey(), foreignKey);
                            int endValue = getNumberOfIdenticalChars(tempTableList.get(end).getForeignAddressKey(), foreignKey);

                            if ((startValue == endValue) && (startValue == 0))
                            {
                                currentIndex = -1;
                                break;
                            }
                            else if ((startValue == endValue) && (startValue != 0))
                            {
                                currentIndex = end;
                                break;
                            }
                            else if (startValue > endValue)
                            {
                                currentIndex = start;
                                break;
                            }
                            else
                            {
                                currentIndex = end;
                                break;
                            }
                        }

                        if ((middle == 0) || (middle == (tempTableList.size() - 1)))
                        {
                            currentIndex = middle;
                            break;
                        }

                        int numberSameCharsCurrent = getNumberOfIdenticalChars(tempTableList.get(middle).getForeignAddressKey(), foreignKey);
                        int numberSameCharsPrevious = getNumberOfIdenticalChars(tempTableList.get(middle - 1).getForeignAddressKey(), foreignKey);
                        int numberSameCharsNext = getNumberOfIdenticalChars(tempTableList.get(middle + 1).getForeignAddressKey(), foreignKey);

                        if ((numberSameCharsCurrent > numberSameCharsPrevious) && (numberSameCharsCurrent > numberSameCharsNext))
                        {
                            currentIndex = middle;
                            break;
                        }
                        else
                        {
                            int min = numberSameCharsCurrent, max = numberSameCharsCurrent;

                            for (int i = (middle - 1); i >= start; i--)
                            {
                                if (getNumberOfIdenticalChars(tempTableList.get(i).getForeignAddressKey(), foreignKey) != numberSameCharsCurrent)
                                {
                                    min = getNumberOfIdenticalChars(tempTableList.get(i).getForeignAddressKey(), foreignKey);
                                    break;
                                }
                            }

                            for (int i = (middle + 1); i <= end; i++)
                            {
                                if (getNumberOfIdenticalChars(tempTableList.get(i).getForeignAddressKey(), foreignKey) != numberSameCharsCurrent)
                                {
                                    max = getNumberOfIdenticalChars(tempTableList.get(i).getForeignAddressKey(), foreignKey);
                                    break;
                                }
                            }

                            if ((min == max) && (min == numberSameCharsCurrent))
                            {
                                if (min <= 0)
                                {
                                    currentIndex = -1;
                                    break;
                                }
                                else start = middle + 1;
                            }
                            else if (numberSameCharsCurrent < max) start = middle + 1;
                            else if (numberSameCharsCurrent < min) end = middle - 1;
                            else if (min <= numberSameCharsCurrent) start = middle;
                        }
                    }

                    if (start == end) currentIndex = start;

                    if (currentIndex != -1) System.out.println(tempTableList.get(currentIndex).getForeignAddressKey());
                else System.out.println("No such field");

                }

                private class ComparatorByKey implements Comparator<TableRow> {
                    @Override
                public int compare(TableRow o1, TableRow o2)
                {
                    if (o1 == null || o2 == null) return 0;
                    return o1.getForeignAddressKey().compareToIgnoreCase(o2.getForeignAddressKey());
                }*/
        public override string ToString()
        {
            if (table.Any())
            {
                string str = "";
                foreach (TableRow tableRow in table)
                    str += tableRow +"\n";
                return str.Substring(0, str.Length-1);
            }
            else
            {
                return "No results";
            }
        }
    }
}
