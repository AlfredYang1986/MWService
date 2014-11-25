using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MWStylePalette
{
    public class MWStylePaletteSharer
    {
        private string _id;
        public MWStylePaletteSharer(string uid) 
        {
            UserID = uid;
            using(MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(UserID));
                _id = new Guid(hash).ToString();
            }
            
        }
        public string ID
        {
            get
            {
                return _id;
            }
        }
        public string UserID { get; set; }
        public List<string> Friends = new List<string>();
        public List<string> FriendPending = new List<string>();
    }
}
