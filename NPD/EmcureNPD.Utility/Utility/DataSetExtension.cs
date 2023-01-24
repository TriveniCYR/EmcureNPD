using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace EmcureNPD.Utility.Utility
{
    public static class DataSetExtension
    {
        public static string DataSetToJSON(this DataSet ds)
        {
            ArrayList root = new();
            List<Dictionary<string, object>> table;
            Dictionary<string, object> data;

            foreach (DataTable dt in ds.Tables)
            {
                table = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    data = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        data.Add(col.ColumnName, dr[col]);
                    }
                    table.Add(data);
                }
                root.Add(table);
            }
            return JsonConvert.SerializeObject(root);
        }
    }
}
