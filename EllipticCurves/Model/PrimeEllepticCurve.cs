using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EllipticCurves.Extensions;

namespace EllipticCurves.Model
{
    class PrimeEllepticCurve : IEllepticCurve
    {
        public BigInteger P { get; set; }

        public BigInteger A { get; set; }

        public BigInteger B { get; set; }

        public BigInteger N { get; set; }

        public PrimeEllepticCurve(BigInteger n, BigInteger a, BigInteger b, BigInteger p)
        {
            P = p;
            A = a;
            B = b;
            N = n;
        }

        public BigIntegerPoint SummarizePoints(BigIntegerPoint first, BigIntegerPoint second)
        {
            BigInteger k = 0;
            if (first.X != second.X)
                k = (second.Y - first.Y) * MulInv(second.X - first.X, P);
            else
                k = (3 * first.X * first.X + A) * MulInv(2 * first.Y, P);

            var d = first.Y - k * first.X;
            var newX = k * k - first.X - second.X;

            return new BigIntegerPoint(newX.Mod(P), (-(k * newX + d)).Mod(P));
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

        private BigInteger MulInv(BigInteger b, BigInteger p)
        {
            BigInteger x0 = 1;
            BigInteger x1 = 0;
            BigInteger y0 = 0;
            BigInteger y1 = 1;

            while (p != 0)
            {
                var saveb = b;
                var savep = p;
                var q = saveb / savep;
                p = saveb.Mod(savep);
                b = savep;

                var savex = x1;
                x1 = x0 - q * x1;
                x0 = savex;

                var savey = y1;
                y1 = y0 - q * y1;
                y0 = savey;
            }

            return b == 1 ? x0.Mod(P) : 0;
        }
    }
}
