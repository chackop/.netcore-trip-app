using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Servces
{
    public interface IMailService
    {
        void SendMail(string to, string from, string subject, string body);
    }
}
