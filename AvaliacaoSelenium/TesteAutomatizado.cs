using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AvaliacaoSelenium
{
    public class Tests
    {
        IWebDriver driver;
        string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string url = "https://www.amazon.com.br/";
        string produto = "Toalha";
               
        [Test]
        public void TesteAvaliacao()
        {
            //Cenário 1 - Acessar o site da Amazon
            /*
                Dado que desejo acessar o site
                Quando preencho a URL com https://www.amazon.com.br/
                Então sou direcionado para a home page
             * */
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Assert.IsTrue(driver.FindElement(By.Id("nav-logo-sprites")).Displayed);

            //Cenário 2 - Buscar um produto
            /*
                Dado que desejo buscar um produto
                Quando preencho o campo de busca com Ar Condicionado
                E clico no botão Ir
                Então vejo a listagem com os produtos
             */
            driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(produto);
            driver.FindElement(By.Id("nav-search-submit-button")).Click();

            //Cenário 3 - Validar se o produto foi retornado na lista
            /*
                Dado que desejo validar se o produto retornado na busca contém o que eu procurei
                Quando verifico a listagem
                Então vejo os produtos relacionados à minha busca
             */
            driver.FindElement(By.XPath("//span[@class='a-size-base-plus a-color-base a-text-normal']")).Text.Contains(produto);

            //Cenário 4 - Adicionar o produto ao carrinho
            /*
                Dado que desejo adicionar o produto ao carrinho
                Quando seleciono o produto na lista
                E clico no botão Adicionar ao carrinho
                Então sou redirecionado ao carrinho com o produto adicionado
             */
            driver.FindElement(By.XPath("//span[@class='a-size-base-plus a-color-base a-text-normal']")).Click();
            driver.FindElement(By.Id("add-to-cart-button")).Click();

            //Cenário 5 - Validando o produto no carrinho
            /*
                Dado que desejo validar o produto adicionado no carrinho
                Quando clico no botão Carrinho
                Então sou direcionado à página contendo o produto adicionado
            */            
            driver.FindElement(By.Id("nav-cart-count")).Click();
            driver.FindElement(By.XPath("//span")).Text.Contains(produto);

            driver.Quit();
        }        
    }
}