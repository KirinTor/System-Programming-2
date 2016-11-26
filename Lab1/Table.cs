using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    static class StringCast
    {
        public static string Cast(this string str)
        {
            str = str.Replace("і", "i");
            str = str.Replace("І", "I");
            str = str.Replace("е", "e");
            str = str.Replace("Е", "E");
            str = str.Replace("М", "M");
            str = str.Replace("Н", "H");
            str = str.Replace("о", "o");
            str = str.Replace("О", "O");
            str = str.Replace("Р", "P");
            str = str.Replace("х", "x");
            str = str.Replace("Х", "X");
            str = str.Replace("Т", "T");
            str = str.Replace("у", "y");
            str = str.Replace("р", "p");
            str = str.Replace("а", "a");
            str = str.Replace("А", "A");
            str = str.Replace("к", "k");
            str = str.Replace("К", "K");
            str = str.Replace("с", "c");
            str = str.Replace("С", "C");
            str = str.Replace("В", "B");
            str = str.ToLower();
            return str;
        }
    }
    class Table
    {
        private List<TableRow> table;
        private int point = 0;
        public Table(List<TableRow> table)
        {
            this.table = table;
        }
        public Table(params TableRow[] rows)
        {
            foreach(TableRow row in rows)
            {
                this.table.Add(row);
            }
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
                    table.Remove(tableRow);
            }
        }
        public void Update(Key key, TableRow newTableRow)
        {
            foreach (TableRow tableRow in table)
            {
                if (tableRow.Key.IsEquals(key))
                    tableRow.UpdateRow(newTableRow);
            }
        }
        public Table SelectByDirectKey(char charKey)
        {
            Table result = null;
            if (point >= table.Count) point = 0;
            {
                for (int i = point; i < table.Count; i++)
                {
                    if (table[i].Key.CharKey == charKey)
                    {
                        result.Insert(table[i]);
                        point++;
                        break;
                    }
                }
            }
            return result;
        }
        public Table SelectBySimilarSearch(string searchKey)
        {
            searchKey.Cast();

            Table result = null;
            int maxSimilarity = 0;

            foreach (TableRow tableRow in table)
            {
                string thisKey = tableRow.Key.StringKey.Cast();
                int tempSimilarity = 0;

                for (int i = 0; i < Math.Min(thisKey.Length, searchKey.Length);)
                {
                    if (thisKey[i] == searchKey[i])
                    {
                        tempSimilarity++;
                        i++;
                    }
                    else
                    {
                        if (tempSimilarity == maxSimilarity)
                        {
                            result.Insert(tableRow);
                        }
                        else if (tempSimilarity > maxSimilarity)
                        {
                            maxSimilarity = tempSimilarity;
                            result.table.Clear();
                            result.Insert(tableRow);
                        }
                        break;
                    }
                }
            }
            return result;
        }

        private boolean isAlreadyInTable(short directAdrKey, String foreignAdrKey)
        {
            for (TableRow row : mainTable)
            {
                if ((row.getDirectAddressKey() == directAdrKey) || (row.getForeignAddressKey().compareToIgnoreCase(foreignAdrKey) == 0)) return false;
            }
            return true;
        }

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
        }
    }
}
}
