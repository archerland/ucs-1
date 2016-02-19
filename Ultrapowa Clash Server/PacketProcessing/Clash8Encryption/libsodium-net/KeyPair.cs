﻿using Sodiumc.Exceptions;
using System;
using System.Security.Cryptography;

namespace Sodiumc
{
    /// <summary>A public / private key pair.</summary>
    public class KeyPair : IDisposable
    {
        private readonly byte[] _privateKey;

        /// <summary>Initializes a new instance of the <see cref="KeyPair" /> class.</summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        /// <exception cref="KeyOutOfRangeException"></exception>
        public KeyPair(byte[] publicKey, byte[] privateKey)
        {
            //verify that the private key length is a multiple of 16
            if (privateKey.Length%16 != 0)
                throw new KeyOutOfRangeException("Private Key length must be a multiple of 16 bytes.");

            PublicKey = publicKey;

            _privateKey = privateKey;
            _ProtectKey();
        }

        /// <summary>Gets or sets the Private Key.</summary>
        public byte[] PrivateKey
        {
            get
            {
                _UnprotectKey();
                var tmp = new byte[_privateKey.Length];
                Array.Copy(_privateKey, tmp, tmp.Length);
                _ProtectKey();

                return tmp;
            }
        }

        /// <summary>Gets or sets the Public Key.</summary>
        public byte[] PublicKey { get; }

        /// <summary>Dispose of private key in memory.</summary>
        public void Dispose()
        {
            if (_privateKey != null && _privateKey.Length > 0)
                Array.Clear(_privateKey, 0, _privateKey.Length);
        }

        ~KeyPair()
        {
            Dispose();
        }

        private void _ProtectKey()
        {
            if (!SodiumLibrary.IsRunningOnMono)
                ProtectedMemory.Protect(_privateKey, MemoryProtectionScope.SameProcess);
        }

        private void _UnprotectKey()
        {
            if (!SodiumLibrary.IsRunningOnMono)
                ProtectedMemory.Unprotect(_privateKey, MemoryProtectionScope.SameProcess);
        }
    }
}