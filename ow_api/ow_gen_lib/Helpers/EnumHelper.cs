namespace ow_gen_lib.Helpers
{
    public static class EnumHelper
    {
        public static string GetName<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            return value.ToString();
        }
    }
}
