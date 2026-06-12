using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class HashUtility
{
    private const string SECRET_KEY = "1234";

    public static string GenerateHash(string data)
    {
        string finalData = data + SECRET_KEY;

        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes =
                sha.ComputeHash(
                    Encoding.UTF8.GetBytes(finalData));

            return Convert.ToBase64String(bytes);
        }
    }
}