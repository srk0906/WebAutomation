using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework;
using Test.Framework.Helpers;
using Faker;
using System.Threading;
using static DowningLLP.ExcelDataProvider.ExcelDataAccess;
using DowningLLP.ExcelDataProvider;

namespace DowningLLP.PageObjects
{
    class MyAccountPage
    {

        #region variables

        #region Downing

        // MyAccount page objects/elements

        public const string AccountBtn = "//*[@id='toggle-items']/ul[2]/li[1]";
        public const string ProfileSettBtn = "//*[@id='toggle-items']/ul[2]/li[1]/ul/li[5]";
        public const string PDMyselfInv = "//*[@id='container-top']/div[1]/personal-details/div/div/div/div/ul/li[1]/label/p";
        public const string Title = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[1]/select";
        public const string FirstName = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[2]/input";
        public const string LastName = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[3]/input";
        public const string DOBDay = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[4]/div[1]/div[1]/select";
        public const string DOBMonth = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[4]/div[1]/div[2]/select";
        public const string DOBYear = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[4]/div[1]/div[3]/select";
        public const string PhoneNumber = "//*[@id='about-me-tel']";
        public const string Intermediary = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[6]/input";
        public const string NatInsNumber = "#container-top > div:nth-child(1) > personal-details > div > div > div.row.main-content > div > about-me > div > div:nth-child(7) > input";
        public const string PostCode = "//*[@id='about-me-pcode']";
        public const string PostCodeBtn = "//*[@id='address-lookup']";
        public const string MoveInMonth = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[8]/section/client-address-details/div/div[1]/div[2]/dc-select-input/div/div/div/div/select";
        public const string MoveInYear = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[8]/section/client-address-details/div/div[1]/div[3]/dc-select-input/div/div/div/div/select";
        public const string SaveBtn = "//*[@id='container-top']/div[1]/personal-details/div/div/div[2]/div/about-me/div/div[9]/div/button";
        public const string PDBehalfInv = "//*[@id='container-top']/div[1]/personal-details/div/div/div/div/ul/li[2]/label/p";
        public const string MyAccCommuPreYes = "//*[@id='container-top']/div[2]/div/div/div/div/ul/li[1]/label/p";
        public const string MyAccCommuPreNo = "//*[@id='container-top']/div[2]/div/div/div/div/ul/li[2]/label/p";
        public const string Password = "//*[@id='change-password']/div[1]/dc-password-input/div[1]/input";
        public const string PasswordBtn = "//*[@id='change-password']/div[2]/div/button";
        public const string Logout = "//*[@id='logout']";

        #endregion

        #region Text Validations

        public const string RegEmailID = "//*[@id='content']/div[1]/div[2]/div/div/div[1]/h2[2]";
        public const string PasswordTxt = "//*[@id='change-password']/div[1]/div/div";

        #endregion

        #endregion

        #region Methods

        public static void DowningAccount()
        {
            Browser.Click(BySelection.XPath, AccountBtn);
        }

        public static void MyProfile()
        {
            Browser.Click(BySelection.XPath, ProfileSettBtn);
        }

        public static void MySelf()
        {
            Browser.Click(BySelection.XPath, PDMyselfInv);
        }

        public static void Behalf()
        {
            Browser.Click(BySelection.XPath, PDBehalfInv);
        }

        public static void MyTitle (int title = 5)
        {
            title = Randomizer.RandomNumber(1, title);
            Browser.SelectDropDownByIndex(By.XPath(Title), title);

        }

        public static void DowningFirstName(string firstNameTxt = "")
        {
            if (firstNameTxt == "")
            {
                firstNameTxt = NameFaker.FirstName();
            }
            Browser.EnterText(By.XPath(FirstName), firstNameTxt);
        }

        public static void DowningLastName(string lastNameTxt = "")
        {
            if (lastNameTxt == "")
            {
                lastNameTxt = NameFaker.LastName();
            }
            Browser.EnterText(By.XPath(LastName), lastNameTxt);
        }

        public static void DobDay(int day = 31)
        {
            day = Randomizer.RandomNumber(1, day);
            Browser.SelectDropDownByIndex(By.XPath(DOBDay), day);

        }

        public static void DobMonth(int month = 12)
        {
            month = Randomizer.RandomNumber(1, month);
            Browser.SelectDropDownByIndex(By.XPath(DOBMonth), month);

        }

        public static void DobYear(int year = 100)
        {
            year = Randomizer.RandomNumber(1, year);
            Browser.SelectDropDownByIndex(By.XPath(DOBYear), year);

        }

        public static void DowningPhone(string phoneNum = "")
        {
            if (phoneNum == "")
            {
                phoneNum = Faker.StringFaker.Numeric(10);
            }
            Browser.EnterText(By.XPath(PhoneNumber), phoneNum);
        }

        public static void DowningIntermediary(string interTxt = "")
        {
            if (interTxt == "")
            {
                interTxt = NameFaker.FirstName();
            }
            Browser.EnterText(By.XPath(Intermediary), interTxt);
        }

        public static void DowningNatInsurance(string insNum = "")
        {
            if (insNum == "")
            {
                insNum = Faker.StringFaker.AlphaNumeric(10);
            }
            Browser.EnterText(By.CssSelector(NatInsNumber), insNum);
            Thread.Sleep(30);
        }
        public static void DowningPostCode(string post = "")
        {
            if (post == "")
            {
                post = Faker.LocationFaker.PostCode();
            }
            Browser.EnterText(By.XPath(PostCode), post);
        }

        public static void PostCodeLook()
        {
            Browser.Click(BySelection.XPath, PostCodeBtn);
        }

        public static void DowningMoveInMonth(int Mmonth = 12)
        {
            Mmonth = Randomizer.RandomNumber(1, Mmonth);
            Browser.SelectDropDownByIndex(By.XPath(MoveInMonth), Mmonth);
        }

        public static void DowningMoveInYear(int Myear = 100)
        {
            Myear = Randomizer.RandomNumber(1, Myear);
            Browser.SelectDropDownByIndex(By.XPath(MoveInYear), Myear);
        }

        public static void DowningSaveBtn()
        {
            Browser.Click(BySelection.XPath, SaveBtn);
        }

        public static void CommPreYes()
        {
            Browser.Click(BySelection.XPath, MyAccCommuPreYes);
        }

        public static void ComPreNo()
        {
            Browser.Click(BySelection.XPath, MyAccCommuPreNo);
        }

        public static void MyPassword(string password = "")
        {
            //if (password == "")
            //{
            //    //password = Faker.StringFaker.AlphaNumeric(10);
            //}
            Browser.EnterText(By.XPath(Password), password);
        }

        public static void PassSave()
        {
            Browser.Click(BySelection.XPath, PasswordBtn);
        }

        public static void LogOut()
        {
            Browser.Click(BySelection.XPath, Logout);
        }
        #endregion

        #region Validating Email

        public static bool DowningEmailValidation
        {
            get
            {
                ExcelDataAccess exceldatacess = new ExcelDataAccess();
                UserData EmailValidation = new UserData();

                //string EmailTxt = Browser.GetAttribute(By.XPath(RegEmailID), "innerHTML");
                string EmailTxt = Browser.GetText(BySelection.XPath, RegEmailID);
                EmailValidation = exceldatacess.GetTestData1("RegisteredUser", "RegisteredEmail");
                string EmailExcel = EmailValidation.Email;
                Console.WriteLine(EmailTxt);
                if (EmailTxt.Replace(" ","") == EmailExcel.Replace(" ", "")) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
