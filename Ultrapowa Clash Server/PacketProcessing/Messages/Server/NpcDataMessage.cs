﻿using System;
using System.Collections.Generic;
using System.Text;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using Ionic.Zlib;

namespace UCS.PacketProcessing
{
    //Packet 24133
    internal class NpcDataMessage : Message
    {
        public NpcDataMessage(Client client, Level level, AttackNpcMessage cnam) : base(client)
        {
            SetMessageType(24133);
            Player = level;
            JsonBase = ObjectManager.NpcLevels[cnam.LevelId - 0x01700000];
            LevelId = cnam.LevelId;
            Console.WriteLine("[24133] Level ID = " + (LevelId - 0x01700000));
        }

        public string JsonBase { get; set; }

        public int LevelId { get; set; }

        public Level Player { get; set; }

        public override void Encode()
        {
            var data = new List<byte>();

            data.AddInt32(0);
            data.Add(0);
            data.AddString("true");
            data.AddInt32(JsonBase.Length);
            data.AddRange(Encoding.ASCII.GetBytes(JsonBase));
            data.AddRange(Player.GetPlayerAvatar().Encode());
            data.AddString("true");
            data.AddInt32(0);
            data.AddInt32(LevelId);

            Encrypt8(data.ToArray());
        }
    }
}