using DowningLLP.ExcelDataProvider;
using Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Framework;
using Test.Framework.Helpers;

namespace DowningLLP.PageObjects
{
    class RegistrationPage
    {
        #region variables

        #region Downing

        // Registration page objects/elements

        private const string EmailID = "email";
        private const string HearAboutUs = "/html/body/portal/register/div/div/div/div/div[1]/div[2]/div[3]/div/div[1]/select";
        private const string Source = "/html/body/portal/register/div/div/div/div/div[1]/div[2]/div[3]/div/div[2]/input";
        private const string Terms = "/html/body/portal/register/div/div/div/div/div[1]/div[2]/div[4]/div/label/span[2]";
        private const string CommunicationYes = "/html/body/portal/register/div/div/div/div/div[1]/div[2]/div[5]/div/div/div/ul/li[1]/label/p";
        private const string CommunicationNo = "/html/body/portal/register/div/div/div/div/div[1]/div[2]/div[5]/div/div/div/ul/li[2]/label/p";
        private const string SubmitBtnID = "submit-btn-member-reg";
        private const string SuccessPopUp = "//*[@id='welcome-msg']/div/div/div[2]/button";

        // Registration page error messages

        private const string EmailErrorTxt = "//*[@id='error']/div/div/div/ul/li[1]/p";
        private const string TermsErrorTxt = "//*[@id='error']/div/div/div/ul/li[2]/p";
        private const string CommunicationErrorTxt = "//*[@id='error']/div/div/div/ul/li[3]/p";

        #endregion

        #endregion



        #region Methods

        #region Registration

        public static void DowningRegEnterEmail( string emailAddress = "")
        {
            if (emailAddress =="")
            {
                emailAddress = Faker.NameFaker.FirstName() + NumberFaker.Number(999) + "@yopmail.com";
            }
            Browser.EnterText(BySelection.Id, EmailID, emailAddress);         
            ExcelDataAccess excelDataAccess = new ExcelDataAccess();
            excelDataAccess.WriteTestData("Email address:" + "" + emailAddress);

        }

        public static void DowningRegAboutUs(int AboutUs = 10)
        {
            AboutUs = Randomizer.RandomNumber(1, AboutUs);
            Browser.SelectDropDownByIndex(By.XPath(HearAboutUs), AboutUs);

        }

        public static void DowningRegPleaseSpecify(string sourceTxt = "")
        {
            if (sourceTxt == "")
            {
                sourceTxt = Faker.CompanyFaker.Name();
            }
            Browser.EnterText(By.XPath(Source), sourceTxt);
        }

        public static void DowningRegTerms()
        {
            Browser.Click(BySelection.XPath, Terms);
        
        }

         public static void DowningRegComYes()
        {
            Browser.Click(BySelection.XPath, CommunicationYes);
        
        }

        public static void DowningRegComNo()
        {
            Browser.Click(BySelection.XPath, CommunicationNo);

        }

        public static void DowningRegSubmit()
        {
            Browser.Click(BySelection.Id, SubmitBtnID);

        }

        public static void DowningRegSuccess()
        {
            Browser.Click(BySelection.XPath, SuccessPopUp);

        }

        #endregion

        #region Registration Validations

        
        #endregion

        #endregion
    }
}
