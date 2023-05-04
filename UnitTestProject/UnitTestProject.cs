using System;
using NUnit.Framework;
using WinFormsAppLabTest;

namespace UnitTestProject
{
    public class Tests
    {
        /// <summary>
        /// Правильные данные для СheckDoctorData.
        /// В функцию переданы данные логина и паролей, соответствующие требованиям.
        /// </summary>
        [TestCase("DoctorSuperBest123!")]
        [TestCase("DoctorSuperBest123$")]
        [TestCase("DoctorSuperBest13@")]
        [TestCase("DoctorSuperBest13#")]
        [TestCase("DoctorSuper$Best13")]
        [TestCase("DoctorSuperbest13%")]
        [TestCase("DoctorSuperBest13^")]
        [TestCase("DoctorSuperBes&t13")]
        [TestCase("Doctorsu*perBest13")]
        public void T_001_СheckDoctorData_BaseFlow(string value)
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = value;
            String repPassword = value;

            //ожидаемое значение            
            bool expectedReturnValue = true;

            //подготовка переменной для полученного значения
            bool actualReturnValue = false;

            //Assert для получения исключения
            Assert.DoesNotThrow(() =>
            {
                actualReturnValue = RegisterForm.checkDoctorData(login, password, repPassword);
            });

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedReturnValue, actualReturnValue);
        }

        /// <summary>
        /// Пустой логин.
        /// Пользователь оставил поле ввода логина пустым.
        /// </summary>
        [Test]
        public void T_002_СheckDoctorData_EmptyLogin()
        {
            //подготовка данных
            String login = "";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSuperBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.EmptyLogin;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пустой первый пароль.
        /// Пользователь оставил поле ввода первого варианта пароля пустым.
        /// </summary>
        [Test]
        public void T_003_СheckDoctorData_EmptyPassword1()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "";
            String repPassword = "DoctorSuperBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.EmptyPassword1;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пустой второй пароль.
        /// Пользователь оставил поле ввода второго варианта пароля пустым.
        /// </summary>
        [Test]
        public void T_004_СheckDoctorData_EmptyPassword2()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123!";
            String repPassword = "";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.EmptyPassword2;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пароли не совпадают.
        /// Пользователь ввел в поля пароля разные строки.
        /// </summary>
        [Test]
        public void T_005_СheckDoctorData_DifferentPasswords()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSupeBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.DifferentPasswords;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Логин и пароль совпадают.
        /// Пользователь ввел в поля пароля и логина одну и ту же строку.
        /// </summary>
        [Test]
        public void T_006_СheckDoctorData_SameLoginPassword()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "myname_doctor";
            String repPassword = "myname_doctor";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.SameLoginPassword;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пароль менее 10 символов.
        /// Пользователь ввел пароль менее 10 символов.
        /// </summary>
        [Test]
        public void T_007_СheckDoctorData_PasswordLess10Chars()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "Doc123!";
            String repPassword = "Doc123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.PasswordLess10Chars;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пароль не содержит цифр.
        /// Пользователь ввел пароль без цифр.
        /// </summary>
        [Test]
        public void T_008_СheckDoctorData_PasswordNoNumber()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest!";
            String repPassword = "DoctorSuperBest!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.PasswordNoNumber;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пароль не содержит спецсимволов.
        /// Пользователь ввел пароль без символов «@#$%^&*!».
        /// </summary>
        [Test]
        public void T_009_СheckDoctorData_PasswordNoExtraChar()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123";
            String repPassword = "DoctorSuperBest123";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.PasswordNoExtraChar;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Пароль не содержит букву в верхнем регистре.
        /// Пользователь ввел пароль без букв в верхнем регистре.
        /// </summary>
        [Test]
        public void T_010_СheckDoctorData_PasswordNoUpperChar()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "doctorsuperbest123!";
            String repPassword = "doctorsuperbest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.PasswordNoUpperChar;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Запрещенный формат логина.
        /// Логин содержит запрещенные символы.
        /// </summary>
        [Test]
        public void T_011_СheckDoctorData_LoginForbidden()
        {
            //подготовка данных
            String login = "myname_doctor!";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSuperBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.LoginForbidden;

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                RegisterForm.checkDoctorData(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

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
            public IDoctorEntry getNewDoctorEntry() { return new MockDoctorEntry() { ID = "1", Login = "myname_doctor", Password = "DoctorSuperBest123!" }; }

            public bool login(string login, string password) { return true; }

            public bool tryConnectDB() { return true; }

            public bool tryCreateAccount(string login, string password) { return true; }
        }
        /// <summary>
        /// Регистрация успешна.
        /// Процесс регистрации успешный.
        /// </summary>
        [Test]
        public void T_012_onRegisterClick_BasicFlow()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSuperBest123!";


            //подготовка данных
            RegisterForm registerForm = new RegisterForm();
            registerForm.controller = new MockDatabaseController_OK();
            IDoctorEntry doctorEntry = null;

            //Assert для получения исключения
            Assert.DoesNotThrow(() =>
            {
                doctorEntry = registerForm.onRegisterClick(login, password, repPassword);
            });

            //Assert для проверки ожидаемого и полученного значения
            Assert.IsNotNull(doctorEntry);
            Assert.AreEqual(doctorEntry.Login, login);
            Assert.AreEqual(doctorEntry.Password, password);
        }
        /// <summary>
        /// Невозможно подключится к БД.
        /// Нет доступа к базе данных, поэтому не можем зарегистрироваться.
        /// </summary>
        [Test]
        public void T_013_onRegisterClick_NoConnectionDB()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSuperBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.NoConnectionDB;

            RegisterForm registerForm = new RegisterForm();

            registerForm.controller = new MockDatabaseController_NoConnection();

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                registerForm.onRegisterClick(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        /// <summary>
        /// Невозможно подключится к БД.
        /// Нет доступа к базе данных, поэтому не можем зарегистрироваться.
        /// </summary>
        [Test]
        public void T_014_onRegisterClick_LoginAlreadyExists()
        {
            //подготовка данных
            String login = "myname_doctor";
            String password = "DoctorSuperBest123!";
            String repPassword = "DoctorSuperBest123!";

            //ожидаемое значение            
            String expectedExceptionMessage = RegisterForm.ExceptionStrings.LoginAlreadyExists;

            RegisterForm registerForm = new RegisterForm();

            registerForm.controller = new MockDatabaseController_LoginExists();

            //Assert для получения исключения
            Exception? exception = Assert.Throws<Exception>(() =>
            {
                registerForm.onRegisterClick(login, password, repPassword);
            });

            Assert.IsNotNull(exception);

            //Assert для проверки ожидаемого и полученного значения
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }
    }
}
