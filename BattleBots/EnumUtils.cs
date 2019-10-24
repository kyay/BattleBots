using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBots
{
    //A class that has various methods for dealing with enums generally and our specific enums.
    public static class EnumUtils
    {
        //Returns either the DescriptionAttribute value of this enum or its string representation if it doesn't have one
        public static string GetDescription(this Enum en)
        {
            try
            {
                //Get the MemberInfo object for that specific enum value from its enum Type and then get the DescriptionAttribute from it and retrieve the description from that
                return ((DescriptionAttribute)(en.GetType().GetMember(en.ToString()).FirstOrDefault()
                    .GetCustomAttributes(typeof(DescriptionAttribute), true)[0])).Description;
            }
            catch (Exception)
            {
                //Fail-safe: just return the string representation of that enum value
                return en.ToString();
            }
        }
    }
}
