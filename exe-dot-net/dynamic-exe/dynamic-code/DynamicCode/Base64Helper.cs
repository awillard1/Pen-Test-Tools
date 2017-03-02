using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DynamicCode
{
    public abstract class Base64Encoding
    {
        private static char[] lookupTable = new char[64]
        { 
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','+','/'
        };
        private static char[] unvalidChars = new char[] { '\r', '\n', '\t' };
        private static Dictionary<char, int> lookupTableDictionnary = new Dictionary<char, int>();

        private static void BuidLookupTableDictionnary()
        {
            if (lookupTableDictionnary.Count > 0)
                return;

            for (int i = 0; i < lookupTable.Length; i++)
                lookupTableDictionnary.Add(lookupTable[i], i);
        }
        private static char[] DeleteUnvalidChars(char[] input)
        {
            int numberOfValid = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (Array.IndexOf(unvalidChars, input[i]) < 0)
                    numberOfValid++;
            }
            char[] validChars = new char[numberOfValid];

            for (int i = 0, n = 0; i < input.Length; i++)
            {
                if (Array.IndexOf(unvalidChars, input[i]) < 0)
                {
                    validChars[n] = input[i];
                    n++;
                }
            }
            return validChars;
        }

        private static void DeleteUnvalidChars(FileStream inputStream, FileStream ouputStream)
        {
            byte[] b = new byte[1];
            inputStream.Position = 0;
            ouputStream.Position = 0;

            while (inputStream.Read(b, 0, 1) > 0)
                if (Array.IndexOf(unvalidChars, (char)b[0]) < 0)
                    ouputStream.Write(b, 0, b.Length);
        }

        private static byte CharToSixbit(char c)
        {
            if (c == '=')
                return 0;

            else
            {
                // Use the dictionnary for faster result !
                if (Base64Encoding.lookupTableDictionnary.ContainsKey(c))
                    return (byte)Base64Encoding.lookupTableDictionnary[c];

                //should not reach here
                return 0;
            }
        }
        private static char SixbitToChar(byte b)
        {
            if ((b >= 0) && (b <= 63))
                return lookupTable[(int)b];
            else
                return ' '; //should not happen;
        }

        public abstract class MemoryEncoding
        {
            /// <summary>
            /// Summary description for Base64Encoder.
            /// </summary>
            public class MemoryBase64Encoder
            {
                byte[] source;
                int length, length2;
                int blockCount;
                int paddingCount;

                public MemoryBase64Encoder(string input, Encoding v)
                {
                    byte[] inputBytes = v.GetBytes(Base64Encoding.DeleteUnvalidChars(input.ToCharArray()));
                    this.InitEncoder(inputBytes);
                }

                public MemoryBase64Encoder(byte[] input)
                {
                    this.InitEncoder(input);
                }

                private void InitEncoder(byte[] input)
                {
                    Base64Encoding.BuidLookupTableDictionnary();
                    source = input;
                    length = input.Length;

                    if ((length % 3) == 0)
                    {
                        paddingCount = 0;
                        blockCount = length / 3;
                    }
                    else
                    {
                        paddingCount = 3 - (length % 3);//need to add padding
                        blockCount = (length + paddingCount) / 3;
                    }
                    length2 = length + paddingCount;//or blockCount *3
                }

                public char[] Encode()
                {
                    int i;
                    byte[] sourceBuffer;

                    if (length != length2)
                    {
                        sourceBuffer = new byte[length2];
                        for (i = 0; i < length2; i++)
                        {
                            if (i < length)
                                sourceBuffer[i] = source[i];
                            else
                                sourceBuffer[i] = 0;
                        }
                    }
                    else
                    {
                        sourceBuffer = source;
                    }

                    byte b1, b2, b3;
                    byte temp, temp1, temp2, temp3, temp4;
                    char[] result = new char[blockCount * 4];

                    for (i = 0; i < blockCount; i++)
                    {
                        b1 = sourceBuffer[i * 3];
                        b2 = sourceBuffer[i * 3 + 1];
                        b3 = sourceBuffer[i * 3 + 2];

                        temp1 = (byte)((b1 & 252) >> 2);
                        temp = (byte)((b1 & 3) << 4);
                        temp2 = (byte)((b2 & 240) >> 4);
                        temp2 += temp;
                        temp = (byte)((b2 & 15) << 2);
                        temp3 = (byte)((b3 & 192) >> 6);
                        temp3 += temp;
                        temp4 = (byte)(b3 & 63);

                        result[i * 4] = Base64Encoding.SixbitToChar(temp1);
                        result[i * 4 + 1] = Base64Encoding.SixbitToChar(temp2);
                        result[i * 4 + 2] = Base64Encoding.SixbitToChar(temp3);
                        result[i * 4 + 3] = Base64Encoding.SixbitToChar(temp4);
                    }

                    //covert last "A"s to "=", based on paddingCount
                    switch (paddingCount)
                    {
                        case 0:
                            break;
                        case 1:
                            result[blockCount * 4 - 1] = '=';
                            break;
                        case 2:
                            result[blockCount * 4 - 1] = '=';
                            result[blockCount * 4 - 2] = '=';
                            break;
                        default:
                            break;
                    }
                    return result;
                }
            }

            /// <summary>
            /// Summary description for MemoryBase64Decoder.
            /// </summary>
            public class MemoryBase64Decoder
            {
                char[] source;
                int length, length2, length3;
                int blockCount;
                int paddingCount;

                public MemoryBase64Decoder(string input)
                {
                    char[] inputChars = input.ToCharArray();
                    this.InitDecoder(inputChars);
                }

                public MemoryBase64Decoder(char[] input)
                {
                    this.InitDecoder(input);
                }

                private void InitDecoder(char[] input)
                {
                    Base64Encoding.BuidLookupTableDictionnary();
                    input = Base64Encoding.DeleteUnvalidChars(input);

                    int temp = 0;
                    source = input;
                    length = input.Length;

                    for (int i = 0; i < 2; i++)
                    {
                        if (input[length - i - 1] == '=')
                            temp++;
                    }
                    paddingCount = temp;
                    blockCount = length / 4;
                    length2 = blockCount * 3;
                }

                public byte[] Decode()
                {
                    byte[] buffer2 = new byte[length2];
                    for (int i = 0; i < length; i++)
                        source[i] = (char)Base64Encoding.CharToSixbit(source[i]);

                    byte b, b1, b2, b3;
                    byte temp1, temp2, temp3, temp4;

                    for (int i = 0; i < blockCount; i++)
                    {
                        temp1 = (byte)source[i * 4];
                        temp2 = (byte)source[i * 4 + 1];
                        temp3 = (byte)source[i * 4 + 2];
                        temp4 = (byte)source[i * 4 + 3];

                        b = (byte)(temp1 << 2);
                        b1 = (byte)((temp2 & 48) >> 4);
                        b1 += b;

                        b = (byte)((temp2 & 15) << 4);
                        b2 = (byte)((temp3 & 60) >> 2);
                        b2 += b;

                        b = (byte)((temp3 & 3) << 6);
                        b3 = temp4;
                        b3 += b;

                        buffer2[i * 3] = b1;
                        buffer2[i * 3 + 1] = b2;
                        buffer2[i * 3 + 2] = b3;
                    }

                    byte[] result = null;

                    if (paddingCount > 0)
                    {
                        length3 = length2 - paddingCount;
                        result = new byte[length3];

                        for (int i = 0; i < length3; i++)
                            result[i] = buffer2[i];
                    }
                    else
                    {
                        result = buffer2;
                    }

                    return result;
                }
            }
        }
    }
}
