using System.Numerics;

namespace EllipticCurves.Model
{
    interface IEllepticCurve
    {
        BigIntegerPoint MultiplyPointByNumber(BigIntegerPoint point, BigInteger number);
        BigIntegerPoint SummarizePoints(BigIntegerPoint first, BigIntegerPoint second);
    }
}