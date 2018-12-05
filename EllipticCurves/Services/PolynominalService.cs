using System.Numerics;
using EllipticCurves.Extensions;

namespace EllipticCurves.Services
{
    public static class PolynominalService
    {
        public static BigInteger InversePolynominal(BigInteger denominator, BigInteger polynome)
        {
            var v1 = polynome;
            var v2 = denominator;
            BigInteger v3 = 0;

            BigInteger x1 = 0;
            BigInteger x2 = 1;
            BigInteger x3;

            while (v3 != 1)
            {
                var v1len = v1.ToBinaryString().Length;
                var v2len = v2.ToBinaryString().Length;
                if (v1len == v2len)
                {
                    v3 = v1 ^ v2;
                    x3 = x1 ^ x2;
                }
                else if (v1len < v2len)
                {
                    v3 = v1 << v2len - v1len;
                    x3 = x1 << v2len - v1len;
                    v3 ^= v2;
                    x3 ^= x2;
                }
                else
                {
                    v3 = v2 << v1len - v2len;
                    x3 = x2 << v1len - v2len;
                    v3 ^= v1;
                    x3 ^= x1;
                }

                v2 = v1;
                x2 = x1;
                v1 = v3;
                x1 = x3;
            }

            denominator = x1;
            return ModPolynominal(denominator, polynome);
        }

        public static BigInteger ModPolynominal(BigInteger denominator, BigInteger polynome)
        {
            var denominatorLength = denominator.ToBinaryString().Length;
            var polynomeLength = polynome.ToBinaryString().Length;
            while (denominatorLength >= polynomeLength)
            {
                var t = polynome << denominatorLength - polynomeLength;
                denominator ^= t;
                denominatorLength = denominator.ToBinaryString().Length;
            }

            return denominator;
        }

        public static BigInteger MultiplyPolynominal(BigInteger firstX, BigInteger secondX)
        {
            if (firstX == 0 || secondX == 0)
                return 0;

            var result = firstX;
            var t = secondX.ToBinaryString().Substring(2);
            foreach (var c in t)
            {
                result <<= 1;
                if (c == '1')
                {
                    result ^= firstX;
                }
            }

            return result;
        }
    }
}