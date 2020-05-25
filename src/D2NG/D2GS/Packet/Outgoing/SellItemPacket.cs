﻿using D2NG.D2GS.Items;
using System;

namespace D2NG.D2GS.Packet
{
    internal class SellItemPacket : D2gsPacket
    {
        public SellItemPacket(Entity entity, Item item) :
            base(
                BuildPacket(
                    (byte)OutGoingPacket.SellItem,
                    BitConverter.GetBytes((uint)entity.Id),
                    BitConverter.GetBytes((uint)item.Id),
                    BitConverter.GetBytes((uint)0x00),
                    BitConverter.GetBytes((uint)0x00)
                )
            )
        {
        }
        public SellItemPacket(byte[] packet) : base(packet)
        {
        }

        public uint GetItemId()
        {
            return BitConverter.ToUInt32(Raw, 5);
        }
    }
}