using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using EllipticCurves.Model;

namespace EllipticCurves.Services
{
    public static class TaskService
    {
        public static string[] Solve(string filename)
        {
            var solutions = new List<string>();

            using (var file = new StreamReader(filename))
            {
                var curveType = new CurveType(file.ReadLine());
                IEllepticCurve curve;
                string a;
                string b;
                if (curveType.Type == CurveType.Nonsupersingular || curveType.Type == CurveType.Supersingular)
                {
                    var polynome = file.ReadLine();
                    a = file.ReadLine();
                    b = file.ReadLine();
                    var c = file.ReadLine();

                    curve = curveType.Type == CurveType.Supersingular
                        ? (IEllepticCurve) new SupersingularEllepticCurve(BitString.BinaryToBigInteger(polynome),
                            BitString.BinaryToBigInteger(a),
                            BitString.BinaryToBigInteger(b),
                            BitString.BinaryToBigInteger(c))
                        : (IEllepticCurve) new NonsupersingularEllepticCurve(BitString.BinaryToBigInteger(polynome),
                            BitString.BinaryToBigInteger(a),
                            BitString.BinaryToBigInteger(b),
                            BitString.BinaryToBigInteger(c));
                }
                else
                {

                    var n = file.ReadLine();
                    a = file.ReadLine();
                    b = file.ReadLine();
                    curve = new PrimeEllepticCurve(BigInteger.Parse(n), BigInteger.Parse(a), BigInteger.Parse(b),
                        BigInteger.Parse(curveType.Type));
                }

                var operation = file.ReadLine();
                while (!string.IsNullOrWhiteSpace(operation))
                {
                    if (operation == "M")
                    {
                        var firstPoint = GetPoint(file, curveType);
                        var number = BigInteger.Parse(file.ReadLine());

                        var result = curve.MultiplyPointByNumber(firstPoint, number);

                        solutions.Add($"{firstPoint.ToString(curveType)} * {number} = {result.ToString(curveType)}");
                    }

                    if (operation == "A")
                    {
                        var firstPoint = GetPoint(file, curveType);
                        var secondPoint = GetPoint(file, curveType);
                        var result = curve.SummarizePoints(firstPoint, secondPoint);

                        solutions.Add($"{firstPoint.ToString(curveType)} + {secondPoint.ToString(curveType)} = {result.ToString(curveType)}");
                    }

                    operation = file.ReadLine();
                }
            }

            return solutions.ToArray();
        }

        private static BigIntegerPoint GetPoint(StreamReader file, CurveType curveType)
        {
            var xRaw = file.ReadLine();
            var yRaw = file.ReadLine();

            BigInteger x;
            BigInteger y;

            if (curveType.Type == CurveType.Nonsupersingular || curveType.Type == CurveType.Supersingular)
            {
                x = BitString.BinaryToBigInteger(xRaw);
                y = BitString.BinaryToBigInteger(yRaw);
            }
            else
            {
                x = BigInteger.Parse(xRaw);
                y = BigInteger.Parse(yRaw);
            }

            return new BigIntegerPoint(x, y);
        }
    }
}
