using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using stats_gamersclub.Domain.Entities.Player;
using stats_gamersclub.Domain.WebScraper.Interfaces.Players;
using stats_gamersclub.Infra.Comum.Configs;

namespace stats_gamersclub.Infra.WebScraper {

    public class StatsWebScraper : IStatsWebScraper {
        
        private IWebDriver _driver;

        public StatsWebScraper() {
            FirefoxOptions optionsFF = new FirefoxOptions();
            optionsFF.AddArgument("--headless");

            _driver = new FirefoxDriver(optionsFF);
        }

        //public void LoadHomePage() {
        //    //Go to the GamersClub homepage
        //    _driver.Navigate().GoToUrl($"{AppSettings.GamersClub?.Url}");
        //    _driver.FindElement(By.CssSelector(".WasdButton--block")).Click();
        //}

        public List<string> ScrapMonthsById(string id) {

            //Go to the player path
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{AppSettings.GamersClub?.Url}{AppSettings.GamersClub?.PathPlayer}{id}");

            //Go to the stats box
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            var boxElement = _driver.FindElement(By.XPath("//*[@id=\"GamersClubStatsBox\"]"));
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", boxElement);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            IWebElement webElement = wait.Until(drv => drv.FindElement(By.CssSelector("button.StatsBoxTab__Button:nth-child(2)")));
            executor.ExecuteScript("arguments[0].click();", webElement);

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            IWebElement webElementMonth = wait2.Until(drv => drv.FindElement(By.CssSelector("div.StatsBoxDropDownMenu:nth-child(1) > button:nth-child(1)")));
            executor.ExecuteScript("arguments[0].click();", webElementMonth);

            Thread.Sleep(1000);

            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var months = wait3.Until(drv => _driver.FindElement(By.CssSelector(".StatsBoxDropDownMenu__List")).FindElement(By.TagName("ul")).FindElements(By.TagName("li")));

            var listMonths = new List<string>();
            foreach (var month in months) {
                listMonths.Add(month.Text);
            }

            return listMonths;
        }

        public Player ScrapStatsByIdAndMonth(string id, string monthStats) {

            //Go to the player path
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{AppSettings.GamersClub?.Url}{AppSettings.GamersClub?.PathPlayer}{id}");

            //Go to the stats box
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            var boxElement = _driver.FindElement(By.XPath("//*[@id=\"GamersClubStatsBox\"]"));
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", boxElement);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            IWebElement webElement = wait.Until(drv => drv.FindElement(By.CssSelector("button.StatsBoxTab__Button:nth-child(2)")));
            executor.ExecuteScript("arguments[0].click();", webElement);

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            IWebElement webElementMonth = wait2.Until(drv => drv.FindElement(By.CssSelector("div.StatsBoxDropDownMenu:nth-child(1) > button:nth-child(1)")));
            executor.ExecuteScript("arguments[0].click();", webElementMonth);

            Thread.Sleep(1000);

            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var months = wait3.Until(drv => _driver.FindElement(By.CssSelector(".StatsBoxDropDownMenu__List")).FindElement(By.TagName("ul")).FindElements(By.TagName("li")));

            var month = months.Where(c => c.Text == monthStats).FirstOrDefault()!;

            month.Click();

            Thread.Sleep(1000);

            return new Player() {
                Id = _driver.FindElement(By.CssSelector(".gc-profile-user-id")).Text.Split("ID: ")[1],
                Nickname = _driver.FindElement(By.CssSelector(".gc-profile-user-name")).Text,
                Level = _driver.FindElement(By.ClassName("gc-featured-sidebar-media")).FindElement(By.TagName("span")).Text,
                Stats = new PlayerStats {
                    month = _driver.FindElement(By.CssSelector("div.StatsBoxDropDownMenu:nth-child(1) > button:nth-child(1)")).Text,
                    kdr = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    adr = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    kast = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(5) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    multKill = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(6) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    firstKill = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(7) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    clutch = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(8) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    hs = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(9) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    precisao = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(10) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                    bombasPlantadas = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(11) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                }
            };
        }

        public void Exit() {
            _driver.Quit();
            _driver = null;
        }
    }
}
