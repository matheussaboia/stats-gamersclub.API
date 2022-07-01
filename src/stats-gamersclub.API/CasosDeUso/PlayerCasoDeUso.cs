using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using stats_gamersclub.API.Dominio;

namespace stats_gamersclub.API.CasosDeUso
{
    public class PlayerCasoDeUso
    {
        private SeleniumConfigurations _configurations;
        private IWebDriver _driver;

        public PlayerCasoDeUso(SeleniumConfigurations seleniumConfigurations)
        {
            _configurations = seleniumConfigurations;

            FirefoxOptions optionsFF = new FirefoxOptions();

            optionsFF.AddArgument("--headless");
      
            _driver = new FirefoxDriver(optionsFF);
        }

        public void HomePageLoad()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{_configurations.UrlGamersClub}");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[5]/div/main/div/div[2]/button")).Click();
        }

        public void LoadPage(string id, string monthStats)
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{_configurations.UrlGamersClub}{_configurations.UrlPlayerGamersClub}{id}");

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;

            var boxElement = _driver.FindElement(By.XPath("//*[@id=\"GamersClubStatsBox\"]"));
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", boxElement);

            //Thread.Sleep(1000);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            IWebElement webElement = wait.Until(drv => drv.FindElement(By.CssSelector("button.StatsBoxTab__Button:nth-child(2)")));
            executor.ExecuteScript("arguments[0].click();", webElement);

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            IWebElement webElementMonth = wait2.Until(drv => drv.FindElement(By.CssSelector("div.StatsBoxDropDownMenu:nth-child(1) > button:nth-child(1)")));
            executor.ExecuteScript("arguments[0].click();", webElementMonth);

            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var months = wait3.Until(drv => _driver.FindElement(By.CssSelector(".StatsBoxDropDownMenu__List")).FindElement(By.TagName("ul")).FindElements(By.TagName("li")));

            try {
                foreach (var month in months) {
                    if (month.Text.Equals(monthStats)) {
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

            player.nickname = _driver.FindElement(By.CssSelector(".gc-profile-user-name")).Text;
            player.level = _driver.FindElement(By.ClassName("gc-featured-sidebar-media")).FindElement(By.TagName("span")).Text;

            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            //foreach (var stat in stats) {
            //    player.Stats.Add(wait2.Until(drv => stat.FindElement(By.ClassName("StatsBoxPlayerInfoItem"))
            //        .FindElement(By.ClassName("StatsBoxPlayerInfoItem__Content"))
            //        .FindElement(By.ClassName("StatsBoxPlayerInfoItem__value"))
            //        .Text));
            //}

            player.Stats = new PlayerStats {
                kdr = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                adr = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                kast = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(5) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                multKill = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(6) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                firstKill = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(7) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                clutch = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(8) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                hs = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(9) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                precisao = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(10) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
                bombasPlantadas = _driver.FindElement(By.CssSelector("div.StatsBoxPlayerInfo:nth-child(11) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2)")).Text,
            };


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
