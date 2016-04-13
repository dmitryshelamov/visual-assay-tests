﻿using android_test.ActivityRepo;
using android_test.ActivityRepo.Library;
using android_test.ActivityRepo.Login;
using android_test.Entity;
using NUnit.Framework;

namespace android_test.Test.UserScenario
{
    class ShareLibraryWithUser
    {
        private User _user1;
        private User _user2;
        private string _team;
        private int _timeout;
        private int _shareDelay;
        private int _loginDelay;
        private string _version;
        private string _baseLibrary = "Standard";

        [OneTimeSetUp]
        public void BeforeClass()
        {
            _user1 = Settings.Instance.User1;
            _user2 = Settings.Instance.User2;
            _team = Settings.Instance.Team;
            _timeout = Settings.Instance.LoginTimeout;
            _shareDelay = Settings.Instance.ShareDelay;
            _loginDelay = Settings.Instance.LoginDelay;
            _version = Settings.Instance.Version;
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ViewOnly()
        {
            string shareName = string.Format("{0}=View", _version);
            Permission permission = new Permission(true, false, false, false);

            //share on owner
            Appium.Instance.Driver.LaunchApp();
            LoginActivity.LoginStep(_user1, _timeout);
            TabMenu.Library.Tap();
            LibraryActivity.SelectAndShareLibrary(_baseLibrary, shareName);
            LibraryShareDialog.ShareWithUserStep(_team, _user2.Name, permission);
            CommonOperation.Delay(_shareDelay);
            TabMenu.Logout.Tap();

            //verify on recipient
            LoginActivity.LoginStep(_user2, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            //verify can't add element
            LibraryActivity.AddElement.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Add Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            //verify can't delete library
            LibraryActivity.DeleteLibrary.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            //verify can't delete library element
            LibraryActivity.ElementList.FindAndTap("Add");
            LibraryElementActivity.Delete.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            LibraryElementActivity.Cancel.Tap();
            //verify can't share library
            LibraryActivity.ShareLibrary.Tap();
            LibraryActivity.VerifyCantShareLibrary(shareName);
            LibraryActivity.CancelShare.Tap();
            TabMenu.Logout.Tap();
        }

        [Test]
        public void AddOnly()
        {
            string shareName = string.Format("{0}=Add", _version);
            Permission permission = new Permission(true, false, false, true);

            //share on owner
            Appium.Instance.Driver.LaunchApp();
            LoginActivity.LoginStep(_user1, _timeout);
            TabMenu.Library.Tap();
            LibraryActivity.SelectAndShareLibrary(_baseLibrary, shareName);
            LibraryShareDialog.ShareWithUserStep(_team, _user2.Name, permission);
            CommonOperation.Delay(_shareDelay);
            TabMenu.Logout.Tap();

            //verify on recipient
            LoginActivity.LoginStep(_user2, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            //verify can add element
            LibraryActivity.AddElement.Tap();
            LibraryElementActivity.ElementName.ClearText();
            LibraryElementActivity.ElementName.EnterText("Element1");
            LibraryElementActivity.Ok.Tap();
            LibraryActivity.ElementList.VerifyElementCountById(21, "library_document_icon");
            //verify can't delete library
            LibraryActivity.DeleteLibrary.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            //verify can't delete library element
            LibraryActivity.ElementList.FindAndTap("Add");
            LibraryElementActivity.Delete.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            LibraryElementActivity.Cancel.Tap();
            //verify can't share library
            LibraryActivity.ShareLibrary.Tap();
            LibraryActivity.VerifyCantShareLibrary(shareName);
            LibraryActivity.CancelShare.Tap();
            TabMenu.Logout.Tap();

            //verify on owner
            LoginActivity.LoginStep(_user1, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(21, "library_document_icon");
            TabMenu.Logout.Tap();
        }

        [Test]
        public void ModifyOnly()
        {
            string shareName = string.Format("{0}=Modify", _version);
            Permission permission = new Permission(true, false, true, false);

            //share on owner
            Appium.Instance.Driver.LaunchApp();
            LoginActivity.LoginStep(_user1, _timeout);
            TabMenu.Library.Tap();
            LibraryActivity.SelectAndShareLibrary(_baseLibrary, shareName);
            LibraryShareDialog.ShareWithUserStep(_team, _user2.Name, permission);
            CommonOperation.Delay(_shareDelay);
            TabMenu.Logout.Tap();

            //verify on recipient
            LoginActivity.LoginStep(_user2, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            //verify can add element
            LibraryActivity.AddElement.Tap();
            LibraryElementActivity.ElementName.ClearText();
            LibraryElementActivity.ElementName.EnterText("Element1");
            LibraryElementActivity.Ok.Tap();
            LibraryActivity.ElementList.VerifyElementCountById(21, "library_document_icon");
            //verify can delete library element
            LibraryActivity.ElementList.FindAndTap("Add");
            LibraryElementActivity.Delete.Tap();
            LibraryDeleteElementDialog.Delete.Tap();
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            //verify can delete library
            LibraryActivity.DeleteLibrary.Tap();
            LibraryDeleteDialog.Cancel.Tap();
            //verify can't share library
            LibraryActivity.ShareLibrary.Tap();
            LibraryActivity.VerifyCantShareLibrary(shareName);
            LibraryActivity.CancelShare.Tap();
            TabMenu.Logout.Tap();

            //verify on owner
            LoginActivity.LoginStep(_user1, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            TabMenu.Logout.Tap();
        }

        [Test]
        public void ShareOnly()
        {
            string shareName = string.Format("{0}=Share", _version);
            string reshareName = string.Format("{0}=Rshr", _version);
            Permission permission = new Permission(true, true, false, false);
            Permission allPermission = new Permission(true, true, true, true);

            //share on owner
            Appium.Instance.Driver.LaunchApp();
            LoginActivity.LoginStep(_user1, _timeout);
            TabMenu.Library.Tap();
            LibraryActivity.SelectAndShareLibrary(_baseLibrary, shareName);
            LibraryShareDialog.ShareWithUserStep(_team, _user2.Name, permission);
            CommonOperation.Delay(_shareDelay);
            TabMenu.Logout.Tap();

            //verify on recipient
            LoginActivity.LoginStep(_user2, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(shareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            //verify can't add element
            LibraryActivity.AddElement.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Add Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            //verify can't delete library
            LibraryActivity.DeleteLibrary.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            //verify can't delete library element
            LibraryActivity.ElementList.FindAndTap("Add");
            LibraryElementActivity.Delete.Tap();
            LibraryPermissionErrorDialog.DialogName.VerifyText("Delete Permission");
            LibraryPermissionErrorDialog.Ok.Tap();
            LibraryElementActivity.Cancel.Tap();
            //verify can't share library
            LibraryActivity.SelectAndShareLibrary(shareName, reshareName);
            LibraryShareDialog.ShareWithUserStep(_team, _user1.Name, allPermission);
            CommonOperation.Delay(_shareDelay);
            TabMenu.Logout.Tap();

            //verify on owner
            LoginActivity.LoginStep(_user1, _timeout);
            CommonOperation.Delay(_loginDelay);
            TabMenu.Library.Tap();
            LibraryActivity.LibraryList.FindAndTap(reshareName);
            LibraryActivity.ElementList.VerifyElementCountById(20, "library_document_icon");
            TabMenu.Logout.Tap();
        }


        [TearDown]
        public void TearDown()
        {
            Appium.Instance.Driver.CloseApp();
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            //clean up after test
            Appium.Instance.Driver.LaunchApp();
            LoginActivity.LoginStep(_user1, _timeout);
            TabMenu.Library.Tap();
            LibraryActivity.DeleteAllLibs();
            CommonOperation.Delay(3);
            TabMenu.Logout.Tap();
            Appium.Instance.Driver.CloseApp();
        }
    }
}