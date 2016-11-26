using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<TableRow> rowList = new ArrayList<>(5);
            rowList.add(new TableRow((short)0, "aabbc", new Union(1)));
            rowList.add(new TableRow((short)1, "abbde", new Union(3)));
            rowList.add(new TableRow((short)2, "abjko", new Union(2)));
            rowList.add(new TableRow((short)3, "detrue", new Union("32")));
            rowList.add(new TableRow((short)4, "kiout", new Union("5")));
            rowList.add(new TableRow((short)5, "mioadfz", new Union("5")));
            rowList.add(new TableRow((short)6, "tzposw", new Union("5")));

            Table table = new Table(rowList);

            table.addRow(new TableRow((short)7, "stirel", new Union("44")));
            table.addRow(new TableRow((short)8, "stream", new Union("12")));

            System.out.println("Before");
            table.showTable();
            System.out.println("  ");

            //table.createRowDirectAdr(new TableRow((short) 9, "zooyork", new Union("54")));
            //table.readRowDirectAdr((short) 2);
            //table.updateRowDirectAdr((short) 3, new TableRow((short) 7, "LG", new Union("54")));
            //table.deleteRowDirectAdr((short) 4);

            //table.createRowForeignAdr(new TableRow((short) 3, "stirela", new Union("54")));
            //table.readRowForeignAdr("АВ");
            //table.updateRowForeignAdr("AA", new TableRow((short) 7, "LG", new Union("54")));
            //table.deleteRowForeignAdr("AВ");

            System.out.println("  ");
            System.out.println("After");

            //table.showTable();
        }


        //Delay
        Console.ReadKey();
        }
    }
}