using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace PatientPortal.Api.Extensions
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Check(string hash, string password);
    }

    public sealed class PasswordHash : IPasswordHash
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private HashOptions HashOptions { get; }
        public PasswordHash(IOptions<HashOptions> options)
        {
            HashOptions = options.Value;
        }

        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SaltSize,
                HashOptions.Iterations, HashAlgorithmName.SHA256);
            var salt = Convert.ToBase64String(algorithm.Salt);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            return $"${HashOptions.Iterations}.{salt}.{key}";
        }

        public bool Check(string hash, string password)
        {
            var passwordParts = password.Split('.', 3, StringSplitOptions.None);

            if (passwordParts.Length != 3)
                throw new FormatException("Invalid hash format");

            var iterations = Convert.ToInt32(passwordParts[0]);
            var salt = Convert.FromBase64String(passwordParts[1]);
            var key = Convert.FromBase64String(passwordParts[2]);

            using var algoritm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var checkKey = algoritm.GetBytes(KeySize);
            return checkKey.SequenceEqual(key);
        }
    }
}
