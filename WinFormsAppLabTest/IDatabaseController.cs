using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppLabTest
{
    public interface IDatabaseController
    {
        bool tryConnectDB();
        public bool tryCreateAccount(string login, string password);
        public IDoctorEntry getNewDoctorEntry();
        public bool login(string login, string password);
    }
}

