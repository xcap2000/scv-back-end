using System;
using System.Security.Cryptography;
using System.Text;

namespace SCVBackend.Infrastructure
{
    public static class SecurityExtensions
    {
        private const int PASSWORD_SIZE = 32;

        private const int SALT_SIZE = 8;

        private const int ITERATIONS = 1000;

        public static Tuple<string, string> Encrypt(this string password)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SALT_SIZE, ITERATIONS);
            
            return Tuple.Create
            (
                ByteArrayToString(rfc2898DeriveBytes.GetBytes(PASSWORD_SIZE)),
                ByteArrayToString(rfc2898DeriveBytes.Salt)
            );
        }

        public static bool IsValid(this string password, string encryptedPassword, string salt)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, StringToByteArray(salt), ITERATIONS);
            byte[] bytesOfEncryptedPasswordToBeVerified = rfc2898DeriveBytes.GetBytes(PASSWORD_SIZE);
            byte[] bytesOfEncryptedPassword = StringToByteArray(encryptedPassword);
            return SafelyCompare(bytesOfEncryptedPasswordToBeVerified, bytesOfEncryptedPassword);
        }

        private static bool SafelyCompare(byte[] byteSet1, byte[] byteSet2)
        {
            uint diferenca = (uint)byteSet1.Length ^ (uint)byteSet2.Length;
            for (int i = 0; i < byteSet1.Length && i < byteSet2.Length; i++)
                diferenca |= (uint)(byteSet1[i] ^ byteSet2[i]);
            return diferenca == 0;
        }

        public static string ByteArrayToString(byte[] value)
        {
            var builder = new StringBuilder(value.Length * 2);
            foreach (byte b in value)
                builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }

        public static byte[] StringToByteArray(string value)
        {
            var numberOfChars = value.Length;
            byte[] bytes = new byte[numberOfChars / 2];
            for (int i = 0; i < numberOfChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }
    }
}
