using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsProblemFixer
{
    public static class Extension
    {
        public static string ToSqlString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.Replace("'", "''");
        }
    }
}
