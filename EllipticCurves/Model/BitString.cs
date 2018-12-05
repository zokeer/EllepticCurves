using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EllipticCurves.Extensions;

namespace EllipticCurves.Model
{
    class BitString
    {
        public string Bits { get; set; }

        public int Length => Bits.Length;

        public char this[int key] => Bits[key];


        public BitString(string s)
        {
            Bits = s;
        }

        public static BitString operator+ (BitString first, BitString second)
        {
            return new BitString(
                (BinaryToBigInteger(first.Bits) ^ BinaryToBigInteger(second.Bits))
                .ToBinaryString()
                );
        }

        public static BitString operator* (BitString first, BitString second)
        {
            var result = new BitString("0");
            for (int i = 0; i < first.Length; i++)
            {
                var cur = MultiplyByChar(second.Bits + new string('0', i), first[i]);
                result = new BitString(cur) + result;
            }

            return result;
        }

        public static string MultiplyByChar(string number, char bit)
        {
            return bit == '0' ? new string('0', number.Length) : number;
        }
        
        public override string ToString()
        {
            return Bits;
        }

        /// <summary>
        ///     Creates a new BigInteger from a binary (Base2) string
        /// </summary>
        public static BigInteger BinaryToBigInteger(string binaryValue)
        {
            BigInteger res = 0;
            if (binaryValue.Count(b => b == '1') + binaryValue.Count(b => b == '0') != binaryValue.Length) return res;
            foreach (var c in binaryValue)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res;
        }
    }
}
