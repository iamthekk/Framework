﻿/*
 * This file is part of the CatLib package.
 *
 * (c) Yu Bin <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: http://catlib.io/
 */

#if CATLIB
using CatLib.API.Hashing;
using CatLib.Hashing.Checksum;
using CatLib.Hashing.HashString;
using Murmur;

namespace CatLib.Hashing
{
    /// <summary>
    /// 哈希服务提供者
    /// </summary>
    public sealed class HashingProvider : IServiceProvider
    {
        /// <summary>
        /// 默认的校验类
        /// </summary>
        public string DefaultChecksum { get; set; }

        /// <summary>
        /// 默认的哈希类
        /// </summary>
        public string DefaultHash { get; set; }

        /// <summary>
        /// 哈希服务提供者
        /// </summary>
        public HashingProvider()
        {
            DefaultChecksum = Checksums.Crc32;
            DefaultHash = Hashes.MurmurHash;
        }

        /// <summary>
        /// 服务提供者初始化
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// 当注册服务提供者
        /// </summary>
        public void Register()
        {
            App.Singleton<Hashing>((_, __) => new Hashing(DefaultChecksum, DefaultHash))
                .Alias<IHashing>().OnResolving((_, obj) =>
            {
                var hashing = (Hashing)obj;

                hashing.RegisterChecksum(Checksums.Crc32, () => new Crc32());
                hashing.RegisterChecksum(Checksums.Adler32, () => new Adler32());

                hashing.RegisterHash(Hashes.MurmurHash, () => new Murmur32ManagedX86());
                hashing.RegisterHash(Hashes.Djb, () => new DjbHash());

                return obj;
            });
        }
    }
}
#endif