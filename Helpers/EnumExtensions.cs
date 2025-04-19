using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LigaTabajara.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType().GetMember(enumValue.ToString());

            if (member.Length > 0)
            {
                var attr = member[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                    return attr.Name;
            }

            return enumValue.ToString();
        }
    }
}
