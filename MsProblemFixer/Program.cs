﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MsProblemFixer
{
    class Program
    {
        private static List<string> _commans = new List<string> { "quote" };

        public const string NEW_LINE = "\r\n";
        public const string COLUMN_SEPARATOR = "\t";
        public const string SEPARATOR = ", ";
        public const string VALUES_SEPARATOR = "', '";

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("You must at least provide one argument - table name");
            var arg1 = args[0];
            var text = Clipboard.GetText();
            string[] rows;
            if (_commans.Contains(arg1.ToLower()))
            {
                const string quoteSeparator = "',\r\n'";
                rows = text.Split(new string[1] { NEW_LINE }, StringSplitOptions.None);
                var result = string.Format("in{0}({0}'{1}'{0})", NEW_LINE, string.Join<string>(quoteSeparator, rows));
                Clipboard.SetText(result);
                return;
            }
            else
            {
                var tableName = arg1;
                //IDataObject data = Clipboard.GetDataObject();
                //var formats = data.GetFormats(true);
                var sql = text.ToString();
                var header = sql.Substring(0, sql.IndexOf(NEW_LINE));
                var headerSplit = header.Split(new string[1] { COLUMN_SEPARATOR }, StringSplitOptions.None);
                //var columnNames = new List<string>();
                //foreach (var colmun in headerSplit)
                //{
                //    columnNames.Add(colmun); 
                //}
                var columnNames = string.Join<string>(SEPARATOR, headerSplit);
                rows = sql.Substring(sql.IndexOf(NEW_LINE) + 2).Split(new string[1] { NEW_LINE }, StringSplitOptions.None);
                var result = new List<string>();
                foreach (var row in rows)
                {
                    var rowSplitted = row.Split(new string[1] { COLUMN_SEPARATOR }, StringSplitOptions.None);
                    //if (rowSplitted.Length > 1)

                    var rowValues = string.Join<string>(VALUES_SEPARATOR, rowSplitted);
                    var fullRow = string.Format("INSERT INTO {0} ({1}) VALUES ('{2}')", tableName, columnNames, rowValues);
                    fullRow = fullRow.Replace("'NULL'", "NULL");
                    result.Add(fullRow);
                }

                Clipboard.SetText(string.Join<string>(NEW_LINE, result));
            }
        }
    }
}