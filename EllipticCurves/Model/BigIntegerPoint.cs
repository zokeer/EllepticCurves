using System.Numerics;
using EllipticCurves.Extensions;

namespace EllipticCurves.Model
{
    public class BigIntegerPoint
    {
        public BigInteger X { get; set; }

        public BigInteger Y { get; set; }

        public BigIntegerPoint(BigInteger x, BigInteger y)
        {
            X = x;
            Y = y;
        }

        public string ToString(CurveType curveType)
        {
            if (curveType.Type == CurveType.Supersingular || curveType.Type == CurveType.Nonsupersingular)
            {
                return $"({X.ToBinaryString().TrimStart('0')}, {Y.ToBinaryString().TrimStart('0')})";
            }

            return $"({X}, {Y})";
        }
    }
}
