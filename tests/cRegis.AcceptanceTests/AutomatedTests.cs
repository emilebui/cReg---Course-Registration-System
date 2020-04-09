using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace cRegis.AcceptanceTests
{
    public class AutomatedTests : IDisposable
    {
        private Random rand = new Random();
        private readonly IWebDriver _driver;
        private string testingUrl = "http://creg.ca-central-1.elasticbeanstalk.com/";

        public AutomatedTests()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            signin();
            clearRegistrations();
            clearWishlist();
        }

        public void Dispose()
        {
            signout();

            _driver.Quit();
            _driver.Dispose();
        }

        //Helpers

        private void signin()
        {
            _driver.Navigate().GoToUrl(testingUrl);
            _driver.FindElement(By.Id("UserName")).SendKeys("jb");
            _driver.FindElement(By.Id("Password")).SendKeys("Password1!");
            _driver.FindElement(By.Id("Password")).Submit();
        }

        private void signout()
        {
            _driver.FindElement(By.XPath("/html/body/header/nav/div/div[2]/input")).Click();
        }

        private string registerCourse()
        {
            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();
            //Get the name of the selected (Top-most) Course
            string courseName = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[1]/div[2]/div/div[1]/h5")).Text;

            //Click the "Register" button for the Course
            _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[1]/div[2]/div/div[2]/div/a[2]")).Click();
            //Click "yes" for the popup dialog box
            _driver.SwitchTo().Alert().Accept();

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();

            return courseName;
        }

        private void dropCourse()
        {
            //Click the "Drop" button for the Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div/div[2]/div/div[2]/div/button")).Click();

            //Click "yes" for the popup dialog boxes
            _driver.SwitchTo().Alert().Accept();
            _driver.SwitchTo().Alert().Accept();

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();
        }

        private void clearRegistrations()
        {
            while (_driver.FindElements(By.LinkText("Detail")).Count != 0)
            {
                dropCourse();
            }

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();
        }

        private string addCourseToWishlist(int level = 1)
        {
            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();

            //Save the name of the top Course
            string courseName = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[" + level + "]/div[1]")).Text;

            //Click the "Add To Wishlist" button for the Course
            _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[" + level + "]/div[2]/div/div[2]/div/a[3]")).Click();
            //Click "yes" for the popup dialog box
            _driver.SwitchTo().Alert().Accept();

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();

            return courseName;
        }

        private void clearWishlist()
        {
            _driver.FindElement(By.LinkText("Wishlist")).Click();

            while (_driver.FindElements(By.LinkText("🗙")).Count != 0)
            {
                //Remove the topmost Course
                _driver.FindElement(By.LinkText("🗙")).Click();
                //Click "yes" for the popup dialog box
                _driver.SwitchTo().Alert().Accept();
            }

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();
        }

        //Tests

        [Fact]
        public void StudentInfo_Test()
        {
            //Verify that the student info is displayed
            Assert.Contains("Welcome, John Braico", _driver.PageSource);
            Assert.Contains("Student Name : John Braico", _driver.PageSource);
            Assert.Contains("Student Id : 1", _driver.PageSource);
            Assert.Contains("Major : Computer Science", _driver.PageSource);
            Assert.Contains("Credit hours remaining : 24", _driver.PageSource);
        }

        [Fact]
        public void RegisteredCourse_ViewDetails_Test()
        {
            string courseName = registerCourse();

            //Click the "Detail" button for the Course
            _driver.FindElement(By.LinkText("Detail")).Click();

            //Verify that it shows the details for that Course
            Assert.Contains(courseName, _driver.PageSource);
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);
        }

        [Fact]
        public void FindCourse_RegisterCourse_DropCourse_Test()
        {
            string courseName = registerCourse();

            Assert.Contains(courseName, _driver.PageSource);

            dropCourse();

            //Ensure that the Course is no displayed as Registered
            Assert.DoesNotContain(courseName, _driver.PageSource);

            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();

            //Ensure that the Course is displayed as Registerable
            Assert.Contains(courseName, _driver.PageSource);
        }

        [Fact]
        public void FindCourse_ViewDetails_Test()
        {
            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();

            //Click the "Details" button for the top Course
            _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[1]/div[2]/div/div[2]/div/a[1]")).Click();

            //Verify that it shows the Details for that Course
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);
        }

        [Fact]
        public void FindCourse_AddToWishlist_RemoveFromWishlist_Test()
        {
            string courseName = addCourseToWishlist(1);

            //Click the "Wishlist" Button
            _driver.FindElement(By.LinkText("Wishlist")).Click();

            //Verify that the course is in the Waitlist
            Assert.Contains(courseName, _driver.PageSource);

            //Remove the Course from the wishlist
            //_driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[5]")).Click();
            _driver.FindElement(By.LinkText("🗙")).Click();
            //Click "yes" for the popup dialog box
            _driver.SwitchTo().Alert().Accept();

            //Verify that the course is in the Waitlist
            Assert.DoesNotContain(courseName, _driver.PageSource);

            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();

            //Verify that the course is in the Waitlist
            Assert.Contains(courseName, _driver.PageSource);
        }

        [Fact]
        public void History_ViewDetails_Test()
        {
            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains("Registration History", _driver.PageSource);

            //Click the "Detail" button for this Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[1]")).Click();

            //Verify that it shows the Details for that Course
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);

            //Verify that it shows the rating for that Course
            Assert.Contains("NEVER AGAIN", _driver.PageSource);
        }

        [Fact]
        public void History_Rate_Test()
        {
            string rating = rand.Next(100).ToString();
            string randomComment = rand.Next().ToString();

            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains("Registration History", _driver.PageSource);
            string courseName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[1]")).Text;

            //Click the "Rate" button for this Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[2]")).Click();
            Assert.Contains("Your Rating(0-100)", _driver.PageSource);
            Assert.Contains("Your Comment", _driver.PageSource);

            //Enter known random input for rating
            _driver.FindElement(By.Id("Rating")).Clear();
            _driver.FindElement(By.Id("Rating")).SendKeys(rating);
            _driver.FindElement(By.Id("Comment")).Clear();
            _driver.FindElement(By.Id("Comment")).SendKeys(randomComment);
            _driver.FindElement(By.XPath("/html/body/div/main/div/form/div[3]/input")).Click();

            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains(courseName, _driver.PageSource);

            //Click the "Detail" button for this Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[1]")).Click();

            //Verify that the rating was added
            Assert.Contains(rating, _driver.PageSource);
            Assert.Contains(randomComment, _driver.PageSource);
        }

        [Fact]
        public void Wishlist_Detail_Test()
        {
            string courseName = addCourseToWishlist(1);

            //Click the "Wishlist" button
            _driver.FindElement(By.LinkText("Wishlist")).Click();

            //Click the "Detail" button for the Course
            _driver.FindElement(By.LinkText("Detail")).Click();

            //Verify that it shows the Details for that Course
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);
        }

        [Fact]
        public void Wishlist_Register_Test()
        {
            string courseName = addCourseToWishlist(1);

            //Click the "Wishlist" button
            _driver.FindElement(By.LinkText("Wishlist")).Click();

            //Click the "Register" button for the Course
            _driver.FindElement(By.LinkText("Register")).Click();
            //Click "yes" for the popup dialog box
            _driver.SwitchTo().Alert().Accept();

            //Click the home button
            _driver.FindElement(By.LinkText("jb")).Click();

            //Verify that the course is registered for
            Assert.Contains(courseName, _driver.PageSource);
        }

        [Fact]
        public void Wishlist_MoveUp_MoveDown_Test()
        {
            addCourseToWishlist(1);
            addCourseToWishlist(2);

            //Click the "Wishlist" button
            _driver.FindElement(By.LinkText("Wishlist")).Click();

            string topName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[1]")).Text;
            string bottomName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[1]")).Text;

            //Click the "move down" button for the top Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[4]")).Click();

            string currTopName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[1]")).Text;
            string currBottomName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[1]")).Text;

            Assert.Equal(bottomName, currTopName);
            Assert.Equal(topName, currBottomName);

            //Click the "move up" button for the botton Course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[2]/div/div[2]/div/a[3]")).Click();

            currTopName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[1]")).Text;
            currBottomName = _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[1]")).Text;

            Assert.Equal(topName, currTopName);
            Assert.Equal(bottomName, currBottomName);
        }
    }
}