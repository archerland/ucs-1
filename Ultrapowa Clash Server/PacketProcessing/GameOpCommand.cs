﻿using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    class GameOpCommand
    {
        private byte m_vRequiredAccountPrivileges;

        public virtual void Execute(Level level)
        {
        }

        public byte GetRequiredAccountPrivileges()
        {
            return m_vRequiredAccountPrivileges;
        }

        public void SetRequiredAccountPrivileges(byte level)
        {
            m_vRequiredAccountPrivileges = level;
        }

        public void SendCommandFailedMessage(Client c)
        {
            //Debugger.WriteLine("GameOp command failed. Insufficient privileges.");
            var p = new GlobalChatLineMessage(c);
            p.SetChatMessage("GameOp command failed. Insufficient privileges.");
            p.SetPlayerId(0);
            p.SetPlayerName("Ultrapowa Clash Server");
            PacketManager.ProcessOutgoingPacket(p);
        }
    }
}