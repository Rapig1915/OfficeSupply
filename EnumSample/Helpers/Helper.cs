using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficeSupply.Helpers
{
    public class Helper
    {
        public static string RandomString(int length = 12)
        {
            try
            {
                const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random rnd = new Random();
                var chars = Enumerable.Range(0, length)
                    .Select(x => pool[rnd.Next(0, pool.Length)]);

                return new string(chars.ToArray());
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}