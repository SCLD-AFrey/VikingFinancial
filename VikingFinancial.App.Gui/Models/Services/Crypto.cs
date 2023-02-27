﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Services;

public class Crypto
{
    private readonly ILogger<Crypto> m_logger;
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
    
    public Crypto(ILogger<Crypto> p_logger)
    {
        m_logger = p_logger;
    }

    public string GeneratePasswordHash(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithm,
            KeySize);
        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}