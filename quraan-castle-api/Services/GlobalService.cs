using System.Security.Cryptography;
using System.Drawing;
using System.Text;

namespace quraan_castle_api
{
    public static class GlobalService
    {
        public static string LogToString(this Exception ex)
        {
            string error = "System " + ex.Message;
            if (ex.InnerException != null)
            {
                error += " --> " + ex.InnerException.Message ?? "";

                if (ex.InnerException.InnerException != null)

                    error += " --> " + ex.InnerException.InnerException.Message ?? "";
            }

            return error;
        }

        public static string GenerateRandomPassword( int length)
        {
            const string chars = "8a626c9ee95a45c1836558066595aad";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public static int GenerateOTP4Digits()
        {
            int _min = 1000;
            int _max = 9998;
            Random _rdm = new Random();

            return _rdm.Next(_min, _max);
        }

        public static string ComputeSHA256Hash(this string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }
    }


}
