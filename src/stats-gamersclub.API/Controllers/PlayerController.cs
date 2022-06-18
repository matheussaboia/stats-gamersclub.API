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

        public void LoadPage(string id)
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl($"{_configurations.UrlGamersClub}{id}");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            IWebElement webElement = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[1]/div[3]/div/button[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", webElement);

            IWebElement webElementMonth = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[1]/button"));
            executor.ExecuteScript("arguments[0].click();", webElementMonth);

            var months = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[1]/div[1]/div/ul")).FindElements(By.TagName("li")).Text;
        }

        public Player GetStatsFromPlayer(string playerId) {
            
            

            return new Player {
                PlayerStats = new PlayerStats {
                    nickname = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[1]/section[1]/div[1]/div[3]/div/div/span")).Text,
                    kdr = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[1]/div/div/div[2]")).Text,
                    adr = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[2]/div/div/div[2]")).Text,
                    kast = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[3]/div/div/div[2]")).Text,
                    multKill = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[4]/div/div/div[2]")).Text,
                    firstKill = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[5]/div/div/div[2]")).Text,
                    clutch = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[6]/div/div/div[2]")).Text,
                    hs = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[7]/div/div/div[2]")).Text,
                    precisao = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[8]/div/div/div[2]")).Text,
                    bombasPlantadas = _driver.FindElement(By.XPath("/html/body/div[2]/div[9]/div/div/div[15]/div[2]/div[2]/div/div[2]/div/div/div/div[2]/div[3]/div[9]/div/div/div[2]")).Text,
                }
            };
        }

        public void Exit()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
