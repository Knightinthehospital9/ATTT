using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTT_N5_PACKED
{
    internal class DonBangCipher
    {
        private Dictionary<char, char> bangThayThe = new Dictionary<char, char>();

        public void KhoiTaoBang(string chuoiThayThe)
        {
            string bangChuCai = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (chuoiThayThe.Length != bangChuCai.Length)
                throw new ArgumentException("Chuỗi thay thế phải có độ dài bằng bảng chữ cái.");

            HashSet<char> kyTuDocNhat = new HashSet<char>();
            foreach (char c in chuoiThayThe)
            {
                if (!kyTuDocNhat.Add(c))
                    throw new ArgumentException("Chuỗi thay thế không được chứa ký tự trùng lặp.");
            }

            for (int i = 0; i < bangChuCai.Length; i++)
            {
                bangThayThe[bangChuCai[i]] = chuoiThayThe[i];
            }
        }

        public string MaHoa(string vanBan)
        {
            StringBuilder vanBanMaHoa = new StringBuilder();

            foreach (char c in vanBan.ToUpper())
            {
                if (bangThayThe.ContainsKey(c))
                    vanBanMaHoa.Append(bangThayThe[c]);
                else
                    vanBanMaHoa.Append(c); // Giữ nguyên ký tự không có trong bảng
            }

            return vanBanMaHoa.ToString();
        }

        public string GiaiMa(string vanBanMaHoa)
        {
            StringBuilder vanBanGiaiMa = new StringBuilder();

            foreach (char c in vanBanMaHoa.ToUpper())
            {
                bool timThay = false;
                foreach (var entry in bangThayThe)
                {
                    if (entry.Value == c)
                    {
                        vanBanGiaiMa.Append(entry.Key);
                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                    vanBanGiaiMa.Append(c); // Giữ nguyên ký tự không có trong bảng
            }

            return vanBanGiaiMa.ToString();
        }
    }
}
