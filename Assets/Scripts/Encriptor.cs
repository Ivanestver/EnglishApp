using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Шифрует ответы
public class Encriptor : MonoBehaviour
{
    private static int key = 1;

    public static string Encript(string enc)
    {
        if (enc == null)
            return "";

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < enc.Length; i++)
        {
            if (!IsCorrect(enc[i]))
            {
                builder.Append(enc[i]);
                continue;
            }

            int newS = enc[i] + key;
            if (char.ToLower(enc[i]) == enc[i])
            {
                if ('a' <= enc[i] && enc[i] <= 'z')
                {
                    if (newS > 'z')
                        newS -= 26;
                }

                if ('а' <= enc[i] && enc[i] <= 'я')
                {
                    if (newS > 'я')
                        newS -= 32;
                }
                builder.Append((char)(newS));
            }
            else
            {
                if ('A' <= enc[i] && enc[i] <= 'Z')
                {
                    if (newS > 'Z')
                        newS -= 26;
                }

                if ('А' <= enc[i] && enc[i] <= 'Я')
                {
                    if (newS > 'Я')
                        newS -= 32;
                }
                builder.Append((char)(newS));
            }
        }

        return builder.ToString();
    }

    public static string Decrypt(string dec)
    {
        if (dec == null)
            return "";

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < dec.Length; i++)
        {
            if (!IsCorrect(dec[i]))
            {
                builder.Append(dec[i]);
                continue;
            }

            int newS = dec[i] - key;
            if (char.ToLower(dec[i]) == dec[i])
            {
                if ('a' <= dec[i] && dec[i] <= 'z')
                {
                    if (newS < 'a')
                        newS += 26;
                }

                if ('а' <= dec[i] && dec[i] <= 'я')
                {
                    if (newS < 'а')
                        newS += 32;
                }
                builder.Append((char)(newS));
            }
            else
            {
                if ('A' <= dec[i] && dec[i] <= 'Z')
                {
                    if (newS < 'A')
                        newS += 26;
                }

                if ('А' <= dec[i] && dec[i] <= 'Я')
                {
                    if (newS < 'А')
                        newS += 32;
                }
                builder.Append((char)(newS));
            }
        }

        return builder.ToString();
    }

    private static bool IsCorrect(char c)
    {
        return ('a' <= c && c <= 'z') || 
            ('а' <= c && c <= 'я') ||
            ('A' <= c && c <= 'Z') || 
            ('А' <= c && c <= 'Я');
    }
}
