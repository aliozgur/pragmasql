using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PragmaSQL.Core
{
  /// <summary>
  /// Summary description for Security.
  /// </summary>
  public sealed class EncryiptionHelper
  {
    private const string PASSWORD = "{A1830210-2F11-481c-8243-5722CC0C910C}";
    public static string Encrypt( string clearText )
    {
      string Password = PASSWORD;
      byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
      PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

      MemoryStream ms = new MemoryStream();
      Rijndael alg = Rijndael.Create();

      alg.Key = pdb.GetBytes(32);
      alg.IV = pdb.GetBytes(16);

      CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

      cs.Write(clearBytes, 0, clearBytes.Length);

      cs.Close();

      byte[] encryptedData = ms.ToArray();

      return Convert.ToBase64String(encryptedData);
    }

    public static string Decrypt( string cipherText )
    {
      string Password = PASSWORD;
      byte[] cipherBytes = Convert.FromBase64String(cipherText);

      PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

      MemoryStream ms = new MemoryStream();

      Rijndael alg = Rijndael.Create();
      alg.Key = pdb.GetBytes(32);
      alg.IV = pdb.GetBytes(16);

      CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

      cs.Write(cipherBytes, 0, cipherBytes.Length);
      cs.Close();
      byte[] decryptedData = ms.ToArray();

      return System.Text.Encoding.Unicode.GetString(decryptedData);
    }

    public static string CalculateMD5Hash(string input)
    {
      // step 1, calculate MD5 hash from input
      MD5 md5 = System.Security.Cryptography.MD5.Create();
      byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
      byte[] hash = md5.ComputeHash(inputBytes);

      return BitConverter.ToString(hash);

      //// step 2, convert byte array to hex string
      //StringBuilder sb = new StringBuilder();
      //for (int i = 0; i < hash.Length; i++)
      //{
      //  sb.Append(hash[i].ToString("x2"));
      //}
      //return sb.ToString();
    }

    public static bool CalculateAndCompareMD5Hash(string input1, string input2)
    {
      return CalculateMD5Hash(input1) == CalculateMD5Hash(input2); 

    }
  }
}