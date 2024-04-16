using Infra.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LojaServices_Application.PasswordHasher
{
    public class PasswordProtectedRepository : IPasswordProtected
    {
        const int saltSize = 256 / 8;
         char Delimiter =';';
        const int iterations = 10000;
        const int keySize = 64;
        HashAlgorithmName Hash = HashAlgorithmName.SHA512;

        //Create Cryptographic Password
        public byte[] PasswordHashed(string password)
        {
            var Salt = RandomNumberGenerator.GetBytes(saltSize);
            var hashPassword = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),Salt,iterations,Hash,keySize);
            var senhaString = String.Join(Delimiter,Convert.ToBase64String(hashPassword), Convert.ToBase64String(Salt));
            return Encoding.UTF8.GetBytes(senhaString);
        }

        //Verify IF the Password Input is Equal the Password On Database
        public bool VerifyPassword(string Inputpassword, byte[] PasswordFromUser)
        {
            var Senha = Encoding.UTF8.GetString(PasswordFromUser);
            var Elements = Senha.Split(Delimiter);
            var hash = Convert.FromBase64String(Elements[0]);
            var Salt = Convert.FromBase64String(Elements[1]);
            

            var HashToVerify = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(Inputpassword), Salt,iterations, Hash, keySize);

            return CryptographicOperations.FixedTimeEquals(hash, HashToVerify);
        }
    }
}
