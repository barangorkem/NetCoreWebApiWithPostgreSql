﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCorePostgreSql.Core.Class
{
    public class Hash
    {
        public static string Create(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 32);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool Validate(string value, string salt, string hash)
            => Create(value, salt) == hash;
    }
}
