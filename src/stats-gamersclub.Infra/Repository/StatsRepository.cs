using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using stats_gamersclub.Domain.Player;
using stats_gamersclub.Infra.Comum.Configs;

namespace stats_gamersclub.Infra.Repository {

    public class StatsRepository {
        private IWebDriver _driver;

        public StatsRepository() {
            FirefoxOptions optionsFF = new FirefoxOptions();
            optionsFF.AddArgument("--headless");

            _driver = new FirefoxDriver(optionsFF);
        }

        public void LoadHomePage() {
            //Go to the GamersClub homepage
            _driver.Navigate().GoToUrl($"{AppSettings.GamersClub?.Url}");
            _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[5]/div/main/div/div[2]/button")).Click();
        }

        public void LoadPlayerPage(string id, string monthStats) {

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

            Thread.Sleep(3000);
            var wait3 = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var months = wait3.Until(drv => _driver.FindElement(By.CssSelector(".StatsBoxDropDownMenu__List")).FindElement(By.TagName("ul")).FindElements(By.TagName("li")));
            
            var t = new List<string>();
            try {
                foreach (var month in months) {
                    t.Add(month.Text);
                    if (month.Text.Equals(monthStats)) {
                        month.Click();
                        return;
                    }
                }
            }
            catch {
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

            //foreach (var stat in stats) {
            //    player.Stats.Add(wait2.Until(drv => stat.FindElement(By.ClassName("StatsBoxPlayerInfoItem"))
            //        .FindElement(By.ClassName("StatsBoxPlayerInfoItem__Content"))
            //        .FindElement(By.ClassName("StatsBoxPlayerInfoItem__value"))
            //        .Text));
            //}

            player.Stats = new PlayerStats {
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
            };

            return player;
        }

        public void Exit() {
            _driver.Quit();
            _driver = null;
        }
    }
}
