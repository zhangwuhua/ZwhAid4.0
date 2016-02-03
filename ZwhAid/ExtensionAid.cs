using System.Data;

namespace ZwhAid
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionAid
    {
        public static bool ToLowerEqual(this string original, string values)
        {
            bool bl = false;

            bl = original.ToLower().Equals(values);

            return bl;
        }

        public static string ToJson(this DataTable s)
        {
            string json = string.Empty;

            return json;
        }
    }
}
