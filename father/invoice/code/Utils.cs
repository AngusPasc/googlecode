using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice
{
    //================================================================================
    // Utils
    //    �����, ���������� ������������ ��������� �������
    //================================================================================
    static class Utils
    {
        //================================================================================
        // ToStringWithoutZeroes
        //    ��������� Version � ������ �������� Build � Revision ������ ���� ��� �� ����
        //================================================================================
        public static string ToStringWithoutZeroes(Version version)
        {
            StringBuilder builder = new StringBuilder(version.Major.ToString() + "." + version.Minor.ToString());

            if (version.Build != 0)
                builder.Append("." + version.Build.ToString());

            if (version.Revision != 0)
                builder.Append("." + version.Revision.ToString());

            return builder.ToString();
        }

        //================================================================================
        // Capitalize
        //    ������ ������ ����� � ������ ���������   
        //================================================================================
        public static string Capitalize(string s)
        {
            return s.Length > 1 ? (Char.ToUpper(s[0]) + s).Remove(1, 1) : s;
        }

        //================================================================================
        // IsUpper
        //================================================================================
        public static bool IsUpper(Char c)
        {
            return c == Char.ToUpper(c);
        }

        private static string[] rub = { "������", "�����", "�����", "������" };
        private static string[] thousand = { "", "������ ", "������ ", "����� " };
        private static string[] million = { "", "������� ", "�������� ", "��������� " };
        private static string[] _100 = { "", "��� ", "������ ", "������ ", "��������� ", "������� ", "�������� ", "������� ", "��������� ", "��������� " };
        private static string[] _10 = { "", "", "�������� ", "�������� ", "����� ", "��������� ", "���������� ", "��������� ", "����������� ", "��������� " };
        private static string[] _1 = { "��� ", "������ ", "���� ", "����� ", "���� ", "������ ", "������ ", "������ ", "����������� ", "���������� ", "���������� ", "������������ ", "���������� ", "����������� ", "���������� ", "������������ ", "������������ " };
        private static string[] __1 = { "", "���� ", "��� ", "", "���� ", "��� " };
        private static byte[] _suffix = { 3, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

        private static string convert(long number, string[] suffix, bool sex)
        {
            string result = "";

            if (number == 0) return suffix[0];

            if (number > 99) result += _100[ number / 100 ];

            if ((number = number % 100) > 19)
            {
                result += _10[number / 10];
                number %= 10;
            }

            if (number > 2) result += _1[number - 3];
            else result += __1[sex ? number : number + 3];            
            
            return result + suffix[_suffix[number]];
        }
        
        public static string ConvertToString(long number)
        {
            return Capitalize(
                    convert(number / 1000000, million, true) +
                    convert((number / 1000) % 1000, thousand, false) +
                    convert(number % 1000, rub, true));
        }

        public static string ExtractDirectorName(string s)
        {
            for (int i = 0, prevSpace = 0; i < s.Length - 4; ++i)
                if (s[i] == ' ')
                    if (IsUpper(s[i + 1]) && s[i + 2] == '.' && IsUpper(s[i + 3]) && s[i + 4] == '.' )
                        return s.Substring(prevSpace + 1, i - prevSpace + 4);
                    else
                        prevSpace = i;
            return "";
        }
    }
}

