using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using stats_gamersclub.API.Models;

namespace stats_gamersclub.API.Controllers
{
    public class PlayerController
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        public PlayerController(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;

            FirefoxOptions optionsFF = new FirefoxOptions();

            //optionsFF.AddArgument("--headless");
      
            _driver = new FirefoxDriver(optionsFF);
        }

        public void HomePageLoad()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{_configurations.UrlGamersClub}");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[5]/div/main/div/div[2]/button")).Click();
        }

        public void LoadPage(string id)
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{_configurations.UrlGamersClub}{_configurations.UrlPlayerGamersClub}{id}");

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;

            var boxElement = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]"));
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", boxElement);

            //Thread.Sleep(1000);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            IWebElement webElement = wait.Until(drv => drv.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[1]/div[3]/div/button[2]")));
            executor.ExecuteScript("arguments[0].click();", webElement);

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            IWebElement webElementMonth = wait2.Until(drv => drv.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[1]/button")));
            executor.ExecuteScript("arguments[0].click();", webElementMonth);

            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var monthsElement = wait3.Until(drv => _driver.FindElement(By.CssSelector(".StatsBoxDropDownMenu__List > ul:nth-child(1)")));
            var months = _driver.FindElements(By.TagName("li"));

            try {
                foreach (var month in months) {
                    if (month.Text.Equals("MAR 2022")) {
                        month.Click();
                        return;
                    }
                }
            } catch {
                Exit();
                throw;
            }
        }

        public Player GetStatsFromPlayer() {

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            var stats = wait.Until(drv => drv.FindElements(By.ClassName("StatsBoxPlayerInfo")));

            Player player = new Player();
            player.Stats = new List<string>();

            player.nickname = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[1]/section[1]/div[1]/div[3]/div/div/span")).Text;

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            foreach (var stat in stats) {
                player.Stats.Add(wait2.Until(drv => stat.FindElement(By.ClassName("StatsBoxPlayerInfoItem"))
                    .FindElement(By.ClassName("StatsBoxPlayerInfoItem__Content"))
                    .FindElement(By.ClassName("StatsBoxPlayerInfoItem__value"))
                    .Text));
            }

            return player;

            //return new Player {
            //    PlayerStats = new PlayerStats {
            //        nickname = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[1]/section[1]/div[1]/div[3]/div/div/span")).Text,
            //        kdr = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[1]/div/div/div[2]")).Text,
            //        adr = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[2]/div/div/div[2]")).Text,
            //        kast = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[3]/div/div/div[2]")).Text,
            //        multKill = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[4]/div/div/div[2]")).Text,
            //        firstKill = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[5]/div/div/div[2]")).Text,
            //        clutch = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[6]/div/div/div[2]")).Text,
            //        hs = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[7]/div/div/div[2]")).Text,
            //        precisao = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[8]/div/div/div[2]")).Text,
            //        bombasPlantadas = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[9]/div/div/div[2]")).Text,
            //    }
            //};
        }

        public void Exit()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
