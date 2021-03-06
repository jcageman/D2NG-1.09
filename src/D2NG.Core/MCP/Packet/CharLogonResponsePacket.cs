﻿using D2NG.Core.MCP.Exceptions;
using System.IO;
using System.Text;

namespace D2NG.Core.MCP.Packet
{
    internal class CharLogonResponsePacket : McpPacket
    {
        public CharLogonResponsePacket(McpPacket packet) : base(packet.Raw)
        {
            var reader = new BinaryReader(new MemoryStream(Raw), Encoding.ASCII);
            if (Raw.Length != reader.ReadUInt16())
            {
                throw new McpPacketException("Packet length does not match");
            }
            if (Mcp.CHARLOGON != (Mcp)reader.ReadByte())
            {
                throw new McpPacketException("Expected Packet Type Not Found");
            }
            Result = reader.ReadUInt32();
        }

        public uint Result { get; }
    }
}