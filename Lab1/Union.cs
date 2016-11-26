using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Union
    {
        {
            private int number;
            private String string;

    public Union(int number)
            {
                this.number = number;
                this.string = "-1";
            }

            public Union(String string)
            {
                this.string = string;
                this.number = -1;
            }

            public int getNumber()
            {
                return number;
            }

            public void setNumber(int number)
            {
                this.number = number;
            }

            public String getString()
            {
                return string;
            }

            public void setString(String string)
            {
                this.string = string;
            }

            @Override
    public String toString()
            {
                if (string.equals("-1"))
                    return "Union{" +
                            "number=" + number + '}';
                else return "Union{" +
                        "string=" + string + '}';
            }
        }

    }
}
