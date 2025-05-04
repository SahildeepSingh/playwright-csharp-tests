using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework; // Add NUnit for proper test structure

namespace PlaywrightTests
{
    public class Tests
    {
        IPlaywright _playwright;
        IBrowser _browser;
        IPage _page;

        [SetUp]
        public async Task Setup()
        {
            // Initialize Playwright and browser
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // Browser UI visible
            });
            _page = await _browser.NewPageAsync();
        }

        [Test]
        public async Task Test()
        {
            // Go to the webpage
            await _page.GotoAsync("https://www.tutorialspoint.com/android/android_sqlite_database.htm");

            // Wait for a specific element to ensure page is fully loaded
            await _page.WaitForSelectorAsync("div, library-page-top-nav"); // Adjust selector based on page content

            // Take a screenshot
            await _page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "screenshot.png"
            });

            // Get the title and print it
            var title = await _page.TitleAsync();
            Console.WriteLine($"Title of the webpage: {title}");

            // Optional: Add assertions to verify page content
            Assert.That(title, Is.EqualTo("SQLite Database in Android"));
        }

        [TearDown]
        public async Task TearDown()
        {
            // Clean up
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}