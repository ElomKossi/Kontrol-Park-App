using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace APP.Models
{
    public class Crypte
    {
        private const string Cle1 = "KPK۩๑۞♥ஐ•@ღ○₪√™";
        private const string Cle2 = "№╬~ξ€ﺕ≈ॐ♪®♂♀ûâî";
        static SHA512 sha512 = SHA512.Create();
        public static string Crypter(string motdepasse)
        {
            return Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(Cle1 + motdepasse + Cle2)));
        }

        public static string Decrypter(string cryptogramme)
        {
            string texte = "";
            texte = Encoding.Default.GetString(Convert.FromBase64String(cryptogramme));
            texte = texte.Remove(0, Cle1.Length);
            int i = texte.Length - Cle2.Length;
            texte = texte.Remove(i, texte.Length);
            return texte;
        }
    }
}