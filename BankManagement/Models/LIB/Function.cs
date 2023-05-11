using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManagement.Models.LIB
{
    public class Function
    {
        // Chuyển long -> string : VD 1234567 -> 1,234,567 VND
        public static string ConvertLongToMoney(long a)
        {
            if (a == 0)
            {
                return "0 VND";
            }
            else
            {
                string s = "";
                int i = 0;
                while (a != 0)
                {
                    if (i % 3 == 0 && i != 0)
                    {
                        s = "," + s;
                    }
                    s = (a % 10).ToString() + s;
                    a /= 10;
                    i++;
                }
                s += " VND";
                return s;
            }
        }
        // Tạo chuỗi số ngẫu nhiên có độ dài là 6
        public static string OTPForgetPass()
        {
            Random rd = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s += rd.Next(10).ToString();
            }
            return s;
        }
        // Tạo chuỗi cấp lại mật khẩu
        public static long ConvertMoneyToLong(string money)
        {
            string s = "0";
            for (int i = 0; i < money.Length; i++)
            {
                if (money[i] != ',')
                    s += money[i];
            }
            return Convert.ToInt64(s);
        }
        public static string NewPass()
        {
            Random rd = new Random();
            string Word = "abcdefghijklmnopqrstuvwxyz";
            string Word1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string Number = "0123456789";
            string SpecialCharacters = "!@#$%^&*()";
            string Password = "";
            Password += Word[rd.Next(Word.Length)];
            Password += Word1[rd.Next(Word.Length)];
            Password += Number[rd.Next(Number.Length)];
            Password += Word[rd.Next(Word.Length)];
            Password += Word1[rd.Next(Word.Length)];
            Password += Number[rd.Next(Number.Length)];
            Password += Number[rd.Next(Number.Length)];
            Password += Word[rd.Next(Word.Length)];
            Password += SpecialCharacters[rd.Next(SpecialCharacters.Length)];
            return Password;
        }
        private static string StringNumber(long x)
        {
            switch (x)
            {
                case 0:
                    return "không ";
                case 1:
                    return "một ";
                case 2:
                    return "hai ";
                case 3:
                    return "ba ";
                case 4:
                    return "bốn ";
                case 5:
                    return "năm ";
                case 6:
                    return "sáu ";
                case 7:
                    return "bảy ";
                case 8:
                    return "tám ";
                case 9:
                    return "chín ";
            }
            return "";
        }
        private static string ConvertLongToStringMoney(long x)
        {
            string res = "";
            string[] arr = { "đồng ", "nghìn ", "triệu ", "tỷ " };
            string[] a = { "", "mươi ", "trăm " };
            int i = 0;
            long xx = 0;
            while (x != 0)
            {
                if (i % 3 == 0)
                {
                    res = arr[i / 3] + res;
                }
                if (x % 10 != 0 && x % 10 != 1)
                    res = a[i % 3] + res;

                if (x % 10 == 1 && i % 3 == 1)
                {
                    res = "mười " + res;
                }
                else if (x % 10 == 1)
                {
                    res = a[i % 3] + res;
                    res = StringNumber(x % 10) + res;
                }
                else if (x % 10 == 0)
                {
                    if (i % 3 == 1 && xx != 0)
                    {
                        res = "linh " + res;
                    }
                }
                else
                {
                    res = StringNumber(x % 10) + res;
                }
                xx = x % 10;
                x /= 10;
                i++;
            }
            return res;
        }
        // 
        private static bool checkstring(string a, string b)
        {
            string[] arr = { "nghìn", "triệu", "tỷ" };
            int a1 = 0;
            int a2 = 0;
            foreach (string i in arr)
            {
                if (a == i)
                    a1++;
                if (b == i)
                    a2++;
            }
            if (a1 == 1 && a2 == 1)
                return false;
            return true;
        }
        //
        private static string deletestring(string res)
        {
            int s = 1;
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == ' ')
                    s++;
            }
            string[] arr = new string[s];
            string a = "";
            int v = 0;
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] == ' ')
                {
                    a = "";
                    v++;
                }
                else
                {
                    a += res[i];
                    arr[v] = a;
                }
            }
            List<string> data = new List<string>(arr);
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (!checkstring(data[i], data[i + 1]))
                {
                    data.Remove(data[i + 1]);
                    i--;
                }
            }
            for (int i = 0; i < data.Count; i++)
            {

                if (i > 0)
                {
                    if (data[i] == "năm" && data[i - 1] == "mươi")
                        a += "lăm ";
                    else
                    {
                        a += data[i] + " ";
                    }
                }
                else
                {
                    a += data[i] + " ";
                }

            }
            return a;
        }
        public static string StringMoney(string Money)
        {
            return deletestring(ConvertLongToStringMoney(ConvertMoneyToLong(Money)));
        }
        public static string ConvertMoneyToMoney(string a)
        {
            return money2(ConvertMoneyToLong(a));
        }
        public static string money2(long a)
        {
            if (a == 0)
            {
                return "0";
            }
            string s = "";
            int i = 0;
            while (a != 0)
            {
                if (i % 3 == 0 && i != 0)
                {
                    s = "," + s;
                }
                s = (a % 10).ToString() + s;
                a /= 10;
                i++;
            }
            return s;
        }
        public static string CalculateInterestRate(string money, double InterestRate, int Term)
        {
            double a = (ConvertMoneyToLong(money) * (InterestRate / 100) / 12) * Term;
            return ConvertLongToMoney(Convert.ToInt64(a));
        }
    }
}