using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYMiniProgram
{
    public static class Extenssions
    {
        public static string GetTableName(this object obj)
        {
            var arr = (TableAttribute[])obj.GetType().GetCustomAttributes(typeof(TableAttribute), false);
            if (arr != null) return arr[0].Name;

            return obj.GetType().Name;
        }

        public static string GetTableName(this Type type)
        {
            var arr = (TableAttribute[])type.GetCustomAttributes(typeof(TableAttribute), false);
            if (arr != null) return arr[0].Name;

            return type.Name;
        }

        public static Dictionary<string, object> ToDictionary(this object source)
        {
            var type = source.GetType();

            return type.GetProperties().ToDictionary(
                  p => p.Name,
                  p =>
                  {
                      var v = p.GetValue(source, null);
                      if (v == null)
                          return string.Empty;
                      return v;
                  });
        }
    }
}
