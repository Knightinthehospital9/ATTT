using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTT_N5_PACKED
{
    internal class CaeserCipher
    {
        public string MaHoa(string vanBan, int k)
        {
            StringBuilder chuoi = new StringBuilder();
            for (int i = 0; i < vanBan.Length; i++)
            {
                char c = vanBan[i];
                if (char.IsUpper(c))
                {
                    char ch = (char)(((c - 'A' + k) % 26 + 26) % 26 + 'A');
                    chuoi.Append(ch);
                }
                else if (char.IsLower(c))
                {
                    char ch = (char)(((c - 'a' + k) % 26 + 26) % 26 + 'a');
                    chuoi.Append(ch);
                }
                else
                {
                    chuoi.Append(c);
                }
            }
            return chuoi.ToString();
        }

        public string GiaiMa(string vanBan, int k)
        {
            StringBuilder chuoi = new StringBuilder();
            for (int i = 0; i < vanBan.Length; i++)
            {
                char c = vanBan[i];
                if (char.IsUpper(c))
                {
                    char ch = (char)(((c - 'A' - k) % 26 + 26) % 26 + 'A');
                    chuoi.Append(ch);
                }
                else if (char.IsLower(c))
                {
                    char ch = (char)(((c - 'a' - k) % 26 + 26) % 26 + 'a');
                    chuoi.Append(ch);
                }
                else
                {
                    chuoi.Append(c);
                }
            }
            return chuoi.ToString();
        }
    }
}
