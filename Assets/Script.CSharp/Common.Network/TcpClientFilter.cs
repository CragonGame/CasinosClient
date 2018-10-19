using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.ProtoBase;

public class TcpClientReceiveFilter : FixedHeaderReceiveFilter<BufferedPackageInfo<ushort>>
{
    //---------------------------------------------------------------------
    public TcpClientReceiveFilter()
        : base(sizeof(ushort))
    {
    }

    //---------------------------------------------------------------------
    protected override int GetBodyLengthFromHeader(IList<ArraySegment<byte>> package_data, int length)
    {
        using (var reader = this.GetBufferReader<BufferedPackageInfo<ushort>>(package_data))
        {
            return reader.ReadUInt16(true);
        }
    }

    //---------------------------------------------------------------------
    public override BufferedPackageInfo<ushort> ResolvePackage(IList<ArraySegment<byte>> package_data)
    {
        using (var reader = this.GetBufferReader<BufferedPackageInfo<ushort>>(package_data))
        {
            ushort body_len = reader.ReadUInt16(true);
            return new BufferedPackageInfo<ushort>(body_len, package_data);
        }
    }
}
