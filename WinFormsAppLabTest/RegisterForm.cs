using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppLabTest
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            controller = ManageClass.GetDatabaseController();
        }

        public static class ExceptionStrings
        {
            public const string EmptyLogin = "Логин не может быть пустым.";
            public const string EmptyPassword1 = "Пропущено поле первого ввода пароля.";
            public const string EmptyPassword2 = "Пропущено поле второго ввода пароля.";
            public const string DifferentPasswords = "Пароли не совпадают!";
            public const string SameLoginPassword = "Логин и пароль не могут совпадать.";
            public const string PasswordLess10Chars = "Пароль не может быть менее 10 символов.";
            public const string PasswordNoNumber = "Пароль должен содержать хотя бы один символ цифры.";
            public const string PasswordNoExtraChar = "Пароль должен содержать хотя бы один символ из @#$%^&&*! .";
            public const string PasswordNoUpperChar = "Пароль должен содержать хотя бы один символ в верхнем регистре.";
            public const string LoginForbidden = "Логин должен состоять только из цифр, букв и символа _.";

            public const string NoConnectionDB = "Нет доступа к базе данных, проверьте подключение.";
            public const string LoginAlreadyExists = "Уже существует пользователь с данным логином.";
        }

        public IDatabaseController controller = null;

        public IDoctorEntry onRegisterClick(string login, string password, string repPassword)
        {
            if (checkDoctorData(login, password, repPassword))
            {
                if (controller.tryConnectDB())
                {
                    if (controller.tryCreateAccount(login, password))
                    {
                        IDoctorEntry doctor = controller.getNewDoctorEntry();

                        controller.login(doctor.Login, doctor.Password);

                        return doctor;
                    }
                    else
                    {
                        throw new Exception(ExceptionStrings.LoginAlreadyExists);
                    }
                }
                else
                {
                    throw new Exception(ExceptionStrings.NoConnectionDB);
                }
            }

            return null;
        }

        public static bool checkDoctorData(string login,
                                           string password,
                                           string repPassword)
        {
            if (login == null || login.Length == 0)
            {
                throw new Exception(ExceptionStrings.EmptyLogin);
            }

            if (password == null || password.Length == 0)
            {
                throw new Exception(ExceptionStrings.EmptyPassword1);
            }

            if (repPassword == null || repPassword.Length == 0)
            {
                throw new Exception(ExceptionStrings.EmptyPassword2);
            }

            if (password != repPassword)
            {
                throw new Exception(ExceptionStrings.DifferentPasswords);
            }

            if (login == password)
            {
                throw new Exception(ExceptionStrings.SameLoginPassword);
            }

            if (password.Length < 10)
            {
                throw new Exception(ExceptionStrings.PasswordLess10Chars);
            }

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[0-9]");

            if (!regex.IsMatch(password))
            {
                throw new Exception(ExceptionStrings.PasswordNoNumber);
            }

            regex = new System.Text.RegularExpressions.Regex(@"[@#$%^&*!]");
            if (!regex.IsMatch(password))
            {
                throw new Exception(ExceptionStrings.PasswordNoExtraChar);
            }

            regex = new System.Text.RegularExpressions.Regex(@"[A-ZА-Я]");

            if (!regex.IsMatch(password))
            {
                throw new Exception(ExceptionStrings.PasswordNoUpperChar);
            }

            regex = new System.Text.RegularExpressions.Regex(@"^[0-9A-ZА-Яa-zа-я_]+$");

            if (!regex.IsMatch(login))
            {
                throw new Exception(ExceptionStrings.LoginForbidden);
            }

            return true;
        }


        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";

            try
            {
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;
                string repPassword = RepPasswordTextBox.Text;
                IDoctorEntry doctor = onRegisterClick(login, password, repPassword);

                ErrorLabel.Text = "Пользователь " + doctor.Login + " зарегистрирован!";
                if (MessageBox.Show("Пользователь " + doctor.Login + " зарегистрирован!", "Внимание!") == DialogResult.OK)
                {
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                ErrorLabel.Text = exception.Message;
            }
        }
    }
}


