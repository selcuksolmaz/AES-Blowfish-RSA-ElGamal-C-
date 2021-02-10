using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Encryption_Algorithms
{

    public class RSA
    {
        public int _keySize { get; set; }
        private BigInteger nValue { get; set; }
        private BigInteger eValue { get; set; }
        private BigInteger dValue { get; set; }
        private List<BigInteger> smallPrimeNumList { get; set; }
        public RSA(int keySize)
        {
            _keySize = keySize;
            var smallPrimeNum = new SmallPrimeNum();
            var _smallPrimeNumList = smallPrimeNum.SmallPrimeNumbers();
            smallPrimeNumList = _smallPrimeNumList;
            var keys = GenerateKeys(_keySize);
            eValue = keys[0];
            dValue = keys[1];
            nValue = keys[2];

        }


        public string Encryption(string msg)
        {
            var cipher = "";
            foreach (char c in msg)
            {
                BigInteger m = c;
                cipher += CalculatePowAndMod(m, eValue, nValue).ToString(CultureInfo.InvariantCulture) + " ";
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
                    int c = Convert.ToInt32(part);
                    msg += Convert.ToChar(Convert.ToInt32(CalculatePowAndMod(c, dValue, nValue)));
                }
            }

            return msg;
        }

        private BigInteger Totient(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        private BigInteger NValue(BigInteger p, BigInteger q)
        {
            return p * q;
        }

        private BigInteger EValue(int keySize, BigInteger totient)
        {

            var check = 0;
            var e = RandomBigInteger(1, totient);
            for (BigInteger i = 2; i <= e; i++)
            {
                if (totient % i == 0 && e % i == 0)
                {
                    check++;
                }
            }

            if (check == 0)
                return e;
            else
            {
                return EValue(keySize, totient);
            }
        }

        private BigInteger ModularInv(BigInteger e, BigInteger totient)
        {
            var egdcdArr = Egcd(e, totient);
            var gcd = egdcdArr[0];
            var x = egdcdArr[1];
            var y = egdcdArr[2];
            if (x < 0)
            {
                x += totient;
            }

            return x;
        }

        private BigInteger[] Egcd(BigInteger a, BigInteger b)
        {
            BigInteger s = 0; BigInteger old_s = 1;
            BigInteger t = 1; BigInteger old_t = 0;
            BigInteger r = b; BigInteger old_r = a;

            while (r != 0)
            {
                BigInteger tempR = old_r;
                BigInteger tempS = old_s;
                BigInteger tempT = old_t;
                BigInteger quentient = (old_r / r);
                old_r = r;
                r = tempR - quentient * r;
                old_s = s;
                s = tempS - quentient * s;
                old_t = t;
                t = tempT - quentient * t;
            }
            var arr = new BigInteger[3];
            arr[0] = old_r;
            arr[1] = old_s;
            arr[2] = old_t;
            return arr;
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

        private bool IsPrime(BigInteger num)
        {

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

        private BigInteger[] GenerateKeys(int keySize)
        {
            BigInteger e = 0;
            BigInteger d = 0;
            BigInteger N = 0;

            var p = GenerateLargePrime(keySize);
            var q = GenerateLargePrime(keySize);

            N = NValue(p, q);
            var totient = Totient(p, q);
            e = EValue(keySize, totient);
            d = ModularInv(e, totient);
            var arr = new BigInteger[3] { e, d, N };
            return arr;
        }


        private int CalculatePowAndMod(BigInteger taban, BigInteger kuvvet, BigInteger mod)
        {
            BigInteger temp = 1;
            for (int i = 0; i < kuvvet; i++)
            {
                temp = (temp * taban) % mod;
            }
            return (int)temp;
        }
    }

}
