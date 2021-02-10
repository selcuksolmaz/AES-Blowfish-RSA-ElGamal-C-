using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Encryption_Algorithms
{
    class ElGamalAlgorithm
    {
        public int _keySize { get; set; }
        public BigInteger pValue { get; set; }
        public BigInteger kValue { get; set; }
        public BigInteger gValue { get; set; }
        public BigInteger xValue { get; set; }
        public BigInteger yValue { get; set; }
        private List<BigInteger> smallPrimeNumList { get; set; }
        public ElGamalAlgorithm(int keySize)
        {
            _keySize = keySize;
            var smallPrimeNum = new SmallPrimeNum();
            var _smallPrimeNumList = smallPrimeNum.SmallPrimeNumbers();
            smallPrimeNumList = _smallPrimeNumList;
            var keysArr = GenerateKeys();
            pValue = keysArr[0];
            kValue = keysArr[1];
            gValue = keysArr[2];
            xValue = keysArr[3];
            yValue = keysArr[4];
        }
        public string Encryption(string msg)
        {
            var cipher = "";
            var c1 = CalculatePowAndMod(gValue, kValue, pValue);
            var temp = CalculatePowAndMod(yValue, kValue, pValue);
            foreach (char c in msg)
            {
                BigInteger m = c;


                var c2 = CalculatePowAndMod(m * temp, 1, pValue);
                cipher += c1.ToString() + "," + c2.ToString() + " ";

            }
            return cipher;
        }
        public string Decryption(string cipher)
        {
            var msg = "";
            var parts = cipher.Split(" ");
            foreach (var part in parts)
            {
                if (part != "")
                {

                    var computeArr = part.Split(',');
                    var c1 = CalculatePowAndMod2(Convert.ToInt32(computeArr[0]), (int)xValue, (int)pValue);
                    var charNumber = Mod(Convert.ToInt32(computeArr[1]), c1, (int)pValue);
                    msg += Convert.ToChar(charNumber);
                }
            }

            return msg;
        }

        private int CalculatePowAndMod2(int taban, int kuvvet, int mod)
        {
            int temp = 1;
            for (int i = 0; i < kuvvet; i++)
            {
                temp = (temp * taban) % mod;
            }
            return temp;
        }
        public int Mod(int pay, int payda, int mod)
        {

            if (pay % payda != 0)
            {
                pay += mod;
                if (pay % payda != 0)
                {
                    return Mod(pay, payda, mod);
                }
                return (pay / payda) % mod;
            }
            return (pay / payda) % mod;
        }

        private BigInteger[] GenerateKeys()
        {
            var p = GenerateLargePrime(_keySize);
            var k = KValue(p);
            var gAndXVal = gAndxValues((int)p);
            var g = gAndXVal[0];
            var x = gAndXVal[1];
            var y = YValue(p, g, x);
            var keysArr = new BigInteger[] { p, k, g, x, y };
            return keysArr;

        }
        private BigInteger KValue(BigInteger p)
        {

            var rnd = new Random();
            var check = 0;
            var coPrime = p - 1;
            BigInteger k = 0;
            if (_keySize <= 8 && _keySize > 0)
            {

                k = rnd.Next(1, (int)coPrime);
            }
            else
            {
                k = RandomBigInteger(1, coPrime);
            }
            for (BigInteger i = 2; i <= k; i++)
            {
                if (coPrime % i == 0 && k % i == 0)
                {
                    check++;
                }
            }

            if (check == 0)
                return k;
            else
            {
                return KValue(p);
            }

        }

        private BigInteger[] gAndxValues(int p)
        {
            var rnd = new Random();
            var g = rnd.Next(1, p);
            var x = rnd.Next(1, p);
            var values = new BigInteger[2];
            if (g != x)
            {
                values[0] = g;
                values[1] = x;
                return values;
            }
            return gAndxValues(p);
        }

        private BigInteger YValue(BigInteger p, BigInteger g, BigInteger x)
        {
            var y = CalculatePowAndMod(g, x, p);
            return y;
        }

        private BigInteger CalculatePowAndMod(BigInteger taban, BigInteger kuvvet, BigInteger mod)
        {
            BigInteger temp = 1;
            for (int i = 0; i < kuvvet; i++)
            {
                temp = (temp * taban) % mod;
            }
            return temp;
        }
        private BigInteger GenerateLargePrime(int keySize)
        {
            var rnd = new Random();
            while (true)
            {
                if (keySize <= 8 && keySize > 0)
                {
                    int num2 = rnd.Next(Convert.ToInt32((Math.Pow(2, keySize - 1))), Convert.ToInt32((Math.Pow(2, keySize) - 1)));
                    if (IsPrime(num2))
                    {
                        return num2;
                    }
                }
                else if (keySize < 0)
                {
                    throw new Exception("Key size can't be negatif value!");
                }
                else
                {
                    var num = RandomBigInteger(BigInteger.Pow(2, keySize - 1), (BigInteger.Pow(2, keySize) - 1));
                    if (IsPrime(num))
                    {
                        return num;
                    }
                }
            }
        }

        private BigInteger RandomBigInteger(BigInteger start, BigInteger end)
        {

            var rand = new Random();
            BigInteger result = 0;
            do
            {
                int length = (int)Math.Ceiling(BigInteger.Log(end, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rand.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= end || result <= start);
            return result;
        }

        private bool IsPrime(BigInteger num)
        {
            ;
            if (num < 2)
            {
                return false;
            }

            if (smallPrimeNumList.Contains(num))
            {
                return true;
            }

            foreach (var prime in smallPrimeNumList)
            {
                if (num % prime == 0)
                {
                    return false;
                }
            }

            var c = num - 1;
            while (c % 2 == 0)
            {
                c /= 2;
            }

            for (int i = 0; i < 128; i++)
            {
                if (!RabinMiller(num, c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool RabinMiller(BigInteger num, BigInteger d)
        {
            var rnd = new Random();
            var a = 2 + (int)(rnd.Next() % (num - 4));
            var x = Power(a, d, num);
            if (x == 1 || x == num - 1)
            {
                return true;
            }

            while (d != num - 1)
            {
                x = Power(x, 2, num);
                d *= 2;

                if (x == 1)
                {
                    return false;
                }
                else if (x == num - 1)
                {
                    return true;
                }

            }

            return false;
        }

        private BigInteger Power(BigInteger x, BigInteger y, BigInteger p)
        {

            BigInteger res = 1;
            x = x % p;

            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = (res * x) % p;

                y = y >> 1;
                x = (x * x) % p;
            }

            return res;
        }
    }
}
