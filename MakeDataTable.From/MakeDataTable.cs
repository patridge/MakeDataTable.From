namespace MakeDataTable.From {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;

    public static class MakeDataTable {
        private static Type GetFinalType(Type type) {
            var nullUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullUnderlyingType != null) {
                type = nullUnderlyingType;
            }
            if (type.IsEnum) {
                type = Enum.GetUnderlyingType(type);
            }
            return type;
        }
        public static DataTable From(IEnumerable<object> objs) {
            DataTable table = new DataTable();
            if (objs == null) {
                return new DataTable();
            }

            bool firstRun = true;
            Dictionary<string, Type> columnData = new Dictionary<string, Type>();
            List<Dictionary<string, object>> rowData = new List<Dictionary<string, object>>();
            foreach (object rowObject in objs) {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(rowObject)) {
                    object value = descriptor.GetValue(rowObject);
                    string name = descriptor.Name;
                    Type type = descriptor.PropertyType;
                    row.Add(name, value ?? DBNull.Value);
                    if (firstRun) {
                        columnData.Add(name, type);
                    }
                }
                rowData.Add(row);
                firstRun = false;
            }

            foreach (KeyValuePair<string, Type> column in columnData) {
                table.Columns.Add(column.Key, GetFinalType(column.Value));
            }
            // Populate table
            foreach (Dictionary<string, object> rowValues in rowData) {
                DataRow row = table.NewRow();
                foreach (KeyValuePair<string, object> kvp in rowValues) {
                    row[kvp.Key] = kvp.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable From(object obj) {
            return From(new object[] { obj });
        }
    }
}
