using System.Data;
using System.Dynamic;

namespace TattoAPI.Repository.Lib
{
    public class ObjectConvert
    {
        public IEnumerable<dynamic> AsDynamicEnumerable(DataTable table)
        {
            if (table == null)
            {
                yield break;
            }

            foreach (DataRow row in table.Rows)
            {
                IDictionary<string, object> dRow = new ExpandoObject();

                foreach (DataColumn column in table.Columns)
                {
                    var value = row[column.ColumnName];
                    dRow[column.ColumnName] = Convert.IsDBNull(value) ? null : value;
                }

                yield return dRow;
            }
        }

    }
}
