using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using NUnit.Framework;
using System;
using System.Linq;
using WinFormsAppLabTest;

namespace UnitTestProject
{
    internal class UITest
    {
        /// <summary>Путь до исполняемого файла приложения для тестирования</summary>
        string PathTestingApp = @"G:\study\ТП\ProgrammingTechnology\LabWork02\WinFormsAppLabTest\bin\Debug\WinFormsAppLabTest.exe";

        /// <summary>время мс допустимой задержки</summary>
        int M = 5000;

        //текст внутри элементов управления

        const string registrationTitleString = "Регистрация";
        const string registrationButtonString = "Зарегистрироваться";
        const string loginString = "Имя:";
        const string passwordString = "Пароль:";
        const string repPasswordString = "Повтор пароля:";
        const string errorLabelString = "Введите регистрационные данные";

        //automatisation-id для элементов управления

        const string idRegisterButton = "RegisterButton";
        const string idLoginLabel = "LoginLabel";
        const string idPasswordLabel = "PasswordLabel";
        const string idRepPasswordLabel = "RepPasswordLabel";
        const string idErrorLabel = "ErrorLabel";
        const string idLoginTextBox = "LoginTextBox";
        const string idPasswordTextBox = "PasswordTextBox";
        const string idRepPasswordTextB = "RepPasswordTextBox";


        public T WaitForElement<T>(Func<T> getter)
        {
            var retry = Retry.WhileNull<T>(
                getter,
                TimeSpan.FromMilliseconds(M));

            if (!retry.Success)
            {
                Assert.Fail($"Невозможно найти элемент {M} ms");
            }

            return retry.Result;
        }

        /// <summary>проверяем все варианты ввода логина и пароля, а также проверяем внешний вид и событие изменения размера формы</summary>
        [Test]
        public void T_001_RegisterForm()
        {
            //Step #001
            FlaUI.Core.Application app = FlaUI.Core.Application.Launch(PathTestingApp, "3");

            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                //Окно с заголовком «Регистрация»
                string title = window.Title;
                Assert.AreEqual(registrationTitleString, title);


                //получаем ссылки на все элементы управления
                var registerButton = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRegisterButton)).AsButton());

                var loginTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginTextBox)).AsTextBox());
                var passwordTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordTextBox)).AsTextBox());
                var repPasswordTextB = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordTextB)).AsTextBox());

                var loginLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginLabel)).AsLabel());
                var passwordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordLabel)).AsLabel());
                var repPasswordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordLabel)).AsLabel());
                var errorLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idErrorLabel)).AsLabel());

                //Проверяем строки
                Assert.AreEqual(registrationButtonString, registerButton.AsLabel().Text);

                Assert.AreEqual(loginString, loginLabel.Text);
                Assert.AreEqual(passwordString, passwordLabel.Text);
                Assert.AreEqual(repPasswordString, repPasswordLabel.Text);
                Assert.AreEqual(errorLabelString, errorLabel.Text);

                Assert.AreEqual("", loginTextBox.Text);
                Assert.AreEqual("", passwordTextBox.Text);
                Assert.AreEqual("", repPasswordTextB.Text);

                //Step #002
                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("EmptyLogin.png");

                var retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyLogin, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyLogin, errorLabel.Text);
                }

                //Step #003
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("");
                repPasswordTextB.Enter("DoctorSuperBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("EmptyPassword1.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyPassword1, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyLogin, errorLabel.Text);
                }

                //Step #004
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("EmptyPassword2.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyPassword2, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.EmptyPassword2, errorLabel.Text);
                }

                //Step #005
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("DoctorSupeBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("DifferentPasswords.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.DifferentPasswords, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.DifferentPasswords, errorLabel.Text);
                }

                //Step #006
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("myname_doctor");
                repPasswordTextB.Enter("myname_doctor");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("SameLoginPassword.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.SameLoginPassword, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.SameLoginPassword, errorLabel.Text);
                }

                //Step #007
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("Doc123!");
                repPasswordTextB.Enter("Doc123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("PasswordLess10Chars.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordLess10Chars, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordLess10Chars, errorLabel.Text);
                }

                //Step #008
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest!");
                repPasswordTextB.Enter("DoctorSuperBest!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("PasswordNoNumber.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoNumber, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoNumber, errorLabel.Text);
                }

                //Step #009
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123");
                repPasswordTextB.Enter("DoctorSuperBest123");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("PasswordNoExtraChar.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoExtraChar.Replace("&&", "&"), errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoExtraChar.Replace("&&", "&"), errorLabel.Text);
                }

                //Step #010
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("doctorsuperbest123!");
                repPasswordTextB.Enter("doctorsuperbest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("PasswordNoUpperChar.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoUpperChar, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.PasswordNoUpperChar, errorLabel.Text);
                }

                //Step #011
                loginTextBox.Enter("myname_doctor!");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("DoctorSuperBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("LoginForbidden.png");

                retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.LoginForbidden, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.LoginForbidden, errorLabel.Text);
                }

                //Step #012
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("DoctorSuperBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("ok.png");

                retry = Retry.WhileException(() =>
                {
                    var msg = window.ModalWindows.FirstOrDefault().AsWindow();

                    Assert.NotNull(msg);

                    var message = msg.FindFirstChild(cf => cf.ByText("Пользователь " + "myname_doctor" + " зарегистрирован!")).AsLabel();

                    Assert.NotNull(message);

                    //Step #013
                    var yesButton = msg.FindFirstChild(cf => cf.ByName("ОК")).AsButton();

                    Assert.NotNull(yesButton);

                    msg.CaptureToFile("okdialog.png");

                    yesButton.Click();

                }, TimeSpan.FromMilliseconds(M));


                if (!retry.Success)
                {
                    Assert.Fail("Нет диалогового окна регистрации");
                }

                System.Threading.Thread.Sleep(1000);

                app.Close();

            }
        }


        /// <summary>нет подключение к бд</summary>
        [Test]
        public void T_002_RegisterForm()
        {
            //Step #1
            FlaUI.Core.Application app = FlaUI.Core.Application.Launch(PathTestingApp, "1");

            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                //Окно с заголовком «Регистрация»
                string title = window.Title;
                Assert.AreEqual(registrationTitleString, title);


                //получаем ссылки на все элементы управления
                var registerButton = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRegisterButton)).AsButton());

                var loginTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginTextBox)).AsTextBox());
                var passwordTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordTextBox)).AsTextBox());
                var repPasswordTextB = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordTextB)).AsTextBox());

                var loginLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginLabel)).AsLabel());
                var passwordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordLabel)).AsLabel());
                var repPasswordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordLabel)).AsLabel());
                var errorLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idErrorLabel)).AsLabel());

                //Проверяем строки
                Assert.AreEqual(registrationButtonString, registerButton.AsLabel().Text);

                Assert.AreEqual(loginString, loginLabel.Text);
                Assert.AreEqual(passwordString, passwordLabel.Text);
                Assert.AreEqual(repPasswordString, repPasswordLabel.Text);
                Assert.AreEqual(errorLabelString, errorLabel.Text);

                Assert.AreEqual("", loginTextBox.Text);
                Assert.AreEqual("", passwordTextBox.Text);
                Assert.AreEqual("", repPasswordTextB.Text);

                //Step #2
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("DoctorSuperBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("NoConnectionDB.png");

                var retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.NoConnectionDB, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.NoConnectionDB, errorLabel.Text);
                }

                //Step #3
                app.Close();

            }
        }

        /// <summary>неуникальный логин</summary>
        [Test]
        public void T_003_RegisterForm()
        {
            //Step #1
            FlaUI.Core.Application app = FlaUI.Core.Application.Launch(PathTestingApp, "2");

            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                //Окно с заголовком «Регистрация»
                string title = window.Title;
                Assert.AreEqual(registrationTitleString, title);


                //получаем ссылки на все элементы управления
                var registerButton = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRegisterButton)).AsButton());

                var loginTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginTextBox)).AsTextBox());
                var passwordTextBox = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordTextBox)).AsTextBox());
                var repPasswordTextB = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordTextB)).AsTextBox());

                var loginLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idLoginLabel)).AsLabel());
                var passwordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idPasswordLabel)).AsLabel());
                var repPasswordLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idRepPasswordLabel)).AsLabel());
                var errorLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId(idErrorLabel)).AsLabel());

                //Проверяем строки
                Assert.AreEqual(registrationButtonString, registerButton.AsLabel().Text);

                Assert.AreEqual(loginString, loginLabel.Text);
                Assert.AreEqual(passwordString, passwordLabel.Text);
                Assert.AreEqual(repPasswordString, repPasswordLabel.Text);
                Assert.AreEqual(errorLabelString, errorLabel.Text);

                Assert.AreEqual("", loginTextBox.Text);
                Assert.AreEqual("", passwordTextBox.Text);
                Assert.AreEqual("", repPasswordTextB.Text);

                //Step #2
                loginTextBox.Enter("myname_doctor");
                passwordTextBox.Enter("DoctorSuperBest123!");
                repPasswordTextB.Enter("DoctorSuperBest123!");

                registerButton.Click();
                System.Threading.Thread.Sleep(1000);
                window.CaptureToFile("LoginAlreadyExists.png");

                var retry = Retry.WhileException(() =>
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.LoginAlreadyExists, errorLabel.Text);

                }, TimeSpan.FromMilliseconds(M));

                if (!retry.Success)
                {
                    Assert.AreEqual(RegisterForm.ExceptionStrings.LoginAlreadyExists, errorLabel.Text);
                }
                //Step #3
                app.Close();

            }
        }

    }
}
