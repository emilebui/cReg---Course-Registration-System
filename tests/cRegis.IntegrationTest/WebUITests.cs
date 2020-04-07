using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.IntegrationTest
{
    public class WebUITests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WebUITests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task LoginPageTest()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(response);


            var form = content.QuerySelector("form[id='form1']");
            var inputs = form.Children;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            Assert.True(inputs.Length == 7);
            Assert.Equal("LABEL", inputs[0].TagName);
            Assert.Equal("INPUT", inputs[1].TagName);
            Assert.Equal("LABEL", inputs[2].TagName);
            Assert.Equal("INPUT", inputs[3].TagName);
            Assert.Equal("DIV", inputs[4].TagName);
            Assert.Equal("INPUT", inputs[5].TagName);
        }

        [Fact]
        public async Task CourseDetailTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/course/Detail?cid=1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task CourseRateTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/course/Rate?eid=1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task FindCourseTests()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Home/Register");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task HistoryTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Home/History");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task WishlistTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Home/Wishlist");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task IndexPageTest()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home");
            var content = await HtmlHelpers.GetDocumentAsync(response);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            //Assert.Equal("Welcome jb", UserName.InnerHtml);
        }
    }
}
