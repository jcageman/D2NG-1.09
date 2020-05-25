﻿using System;

namespace D2NG.D2GS.Packet
{
    internal class RightSkillRepeatOnLocationPacket : D2gsPacket
    {
        public RightSkillRepeatOnLocationPacket(Point point) :
            base(
                BuildPacket(
                    (byte)OutGoingPacket.RightSkillRepeatOnLocation,
                    BitConverter.GetBytes(point.X),
                    BitConverter.GetBytes(point.Y)
                )
            )
        {
        }
    }
}