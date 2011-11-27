using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RecommenderSystem.Core.RS_Core
{
    public class Util
    {
        public static DataRow CloneRow(DataRow source)
        {
            DataRow result = null;
            for (int i = 0; i < source.Table.Rows.Count; i++)
                for (int j = 0; j < source.Table.Columns.Count; j++)
                {
                    result.Table.Rows[i][j] = source.Table.Rows[i][j];
                }
            return result;
        }
    }
}
