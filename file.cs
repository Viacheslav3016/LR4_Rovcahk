using System;
using System.Collections.Generic;

class SBoxPBoxTransformation
{
    // Таблиця підстановок для S-блоку (наприклад, для 4-бітових блоків)
    static readonly byte[] SBox = new byte[16] { 0xE, 0x4, 0xD, 0x1, 0x2, 0xF, 0xB, 0x8, 0x3, 0xA, 0x6, 0xC, 0x5, 0x9, 0x0, 0x7 };

    // Таблиця зворотних підстановок для S-блоку
    static readonly byte[] InvSBox = GenerateInverseSBox(SBox);

    // Таблиця перестановок для P-блоку (наприклад, для 4-бітових блоків)
    static readonly int[] PBox = new int[4] { 2, 0, 3, 1 };

    // Таблиця зворотних перестановок для P-блоку
    static readonly int[] InvPBox = GenerateInversePBox(PBox);

    static void Main()
    {
        byte data = 0x3; // Приклад 4-бітового блоку (0011)

        byte sBoxOutput = ApplySBox(data);
        byte invSBoxOutput = ApplyInvSBox(sBoxOutput);

        byte pBoxOutput = ApplyPBox(data);
        byte invPBoxOutput = ApplyInvPBox(pBoxOutput);

        Console.WriteLine($"Initial data: {Convert.ToString(data, 2).PadLeft(4, '0')}");
        Console.WriteLine($"SBox output: {Convert.ToString(sBoxOutput, 2).PadLeft(4, '0')}");
        Console.WriteLine($"InvSBox output: {Convert.ToString(invSBoxOutput, 2).PadLeft(4, '0')}");
        Console.WriteLine($"PBox output: {Convert.ToString(pBoxOutput, 2).PadLeft(4, '0')}");
        Console.WriteLine($"InvPBox output: {Convert.ToString(invPBoxOutput, 2).PadLeft(4, '0')}");

        // Перевірка правильності реалізації
        Console.WriteLine("SBox Test: " + (data == invSBoxOutput ? "Passed" : "Failed"));
        Console.WriteLine("PBox Test: " + (data == invPBoxOutput ? "Passed" : "Failed"));
    }

    static byte ApplySBox(byte input)
    {
        return SBox[input & 0x0F];
    }

    static byte ApplyInvSBox(byte input)
    {
        return InvSBox[input & 0x0F];
    }

    static byte ApplyPBox(byte input)
    {
        byte output = 0;
        for (int i = 0; i < PBox.Length; i++)
        {
            int bit = (input >> i) & 0x01;
            output |= (byte)(bit << PBox[i]);
        }
        return output;
    }

    static byte ApplyInvPBox(byte input)
    {
        byte output = 0;
        for (int i = 0; i < InvPBox.Length; i++)
        {
            int bit = (input >> i) & 0x01;
            output |= (byte)(bit << InvPBox[i]);
        }
        return output;
    }

    static byte[] GenerateInverseSBox(byte[] sbox)
    {
        byte[] invSBox = new byte[sbox.Length];
        for (int i = 0; i < sbox.Length; i++)
        {
            invSBox[sbox[i]] = (byte)i;
        }
        return invSBox;
    }

    static int[] GenerateInversePBox(int[] pbox)
    {
        int[] invPBox = new int[pbox.Length];
        for (int i = 0; i < pbox.Length; i++)
        {
            invPBox[pbox[i]] = i;
        }
        return invPBox;
    }
}
