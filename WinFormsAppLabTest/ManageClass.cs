using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppLabTest;

namespace WinFormsAppLabTest
{
    public static class ManageClass
    {


        public static int index = 3;
        public static IDatabaseController GetDatabaseController()
        {
#if DEBUG
            switch (index)
            {
                case 0: throw new NotImplementedException(); break;
                case 1: return new MockDatabaseController_NoConnection(); break;
                case 2: return new MockDatabaseController_LoginExists(); break;
                case 3: return new MockDatabaseController_OK(); break;
            }

            return null;
#else
             throw new NotImplementedException();
#endif
        }

#if DEBUG
        public class MockDoctorEntry : IDoctorEntry
        {
            public string ID { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }
        public class MockDatabaseController_NoConnection : IDatabaseController
        {
            public IDoctorEntry getNewDoctorEntry() { throw new NotImplementedException(); }

            public bool login(string login, string password) { throw new NotImplementedException(); }

            public bool tryConnectDB() { return false; }

            public bool tryCreateAccount(string login, string password) { throw new NotImplementedException(); }
        }

        public class MockDatabaseController_LoginExists : IDatabaseController
        {
            public IDoctorEntry getNewDoctorEntry() { throw new NotImplementedException(); }

            public bool login(string login, string password) { return true; }

            public bool tryConnectDB() { return true; }

            public bool tryCreateAccount(string login, string password) { return false; }
        }

        public class MockDatabaseController_OK : IDatabaseController
        {
            MockDoctorEntry doctorEntry;
            public IDoctorEntry getNewDoctorEntry() { return doctorEntry; }

            public bool login(string login, string password) { return true; }

            public bool tryConnectDB() { return true; }

            public bool tryCreateAccount(string login, string password) { doctorEntry = new MockDoctorEntry() { ID = "1", Login = login, Password = password }; return true; }
        }
#endif
    }
}


