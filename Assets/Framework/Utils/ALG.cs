using System;
using System.Text;
public class ALG  {
    public static string EncodeHexString(string s)
    {
        char[] bs = s.ToCharArray();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bs.Length; i++)
        {
            sb.Append(String.Format("{0:x2}", Convert.ToInt32(bs[i])));
        }
        return sb.ToString();
    }

    public static string DecodeHexString(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i += 2)
        {
            string g = s.Substring(i, 2);
            int k = int.Parse(g, System.Globalization.NumberStyles.HexNumber);
            sb.Append((char)k);
        }
        return sb.ToString();
    }

    public static string EncodeBundleName(string name)
    {
        if (name.EndsWith(".ab"))
            return name;
        return ALG.EncodeHexString(name) + ".ab";
    }
    public static string DecodeBundleName(string name) 
    {
        if (name.EndsWith(".ab"))
            return ALG.DecodeHexString(name.Substring(0, name.Length - 3));
        return name;
    }
}
