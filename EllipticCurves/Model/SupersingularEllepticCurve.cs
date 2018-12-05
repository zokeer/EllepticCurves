using System.Numerics;
using System.Runtime.InteropServices;
using EllipticCurves.Extensions;
using EllipticCurves.Services;

namespace EllipticCurves.Model
{
    public class SupersingularEllepticCurve : IEllepticCurve
    {
        public BigInteger Polynome { get; set; }

        public BigInteger A { get; set; }
        
        public BigInteger B { get; set; }

        public BigInteger C { get; set; }

        public SupersingularEllepticCurve(BigInteger polynome, BigInteger a, BigInteger b, BigInteger c)
        {
            Polynome = polynome;
            A = a;
            B = b;
            C = c;
        }

        public BigIntegerPoint MultiplyPointByNumber(BigIntegerPoint point, BigInteger number)
        {
            var newPoint = new BigIntegerPoint(point.X, point.Y);
            var binaryNumber = number.ToBinaryString();

            foreach (var bit in binaryNumber.Substring(2))
            {
                newPoint = SummarizePoints(newPoint, newPoint);
                if (bit == '1')
                    newPoint = SummarizePoints(newPoint, point);
            }

            return newPoint;
        }

        public BigIntegerPoint SummarizePoints(BigIntegerPoint first, BigIntegerPoint second)
        {
            if (first.X == second.X && first.Y == second.Y)
                return new BigIntegerPoint(0,0);

            BigInteger numerator;
            BigInteger denominator;
            if (first.X != second.X)
            {
                numerator = second.Y ^ first.Y;
                denominator = second.X ^ first.X;
            }
            else
            {
                numerator = PolynominalService.MultiplyPolynominal(first.X, second.X);
                numerator ^= B;
                denominator = A;
            }

            denominator = PolynominalService.InversePolynominal(denominator, Polynome);
            var k = PolynominalService.MultiplyPolynominal(numerator, denominator);
            k = PolynominalService.ModPolynominal(k, Polynome);

            var x = PolynominalService.MultiplyPolynominal(k, k);
            if (first.X != second.X)
            {
                x ^= first.X;
                x ^= second.X;
            }
            x = PolynominalService.ModPolynominal(x, Polynome);

            var y = first.X ^ x;
            y = PolynominalService.MultiplyPolynominal(y, k);
            y ^= first.Y;
            y ^= A;
            y = PolynominalService.ModPolynominal(y, Polynome);

            return new BigIntegerPoint(x, y);
        }
    }
}