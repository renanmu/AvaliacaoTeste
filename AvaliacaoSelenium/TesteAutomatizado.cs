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
            //Cen�rio 1 - Acessar o site da Amazon
            /*
                Dado que desejo acessar o site
                Quando preencho a URL com https://www.amazon.com.br/
                Ent�o sou direcionado para a home page
             * */
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Assert.IsTrue(driver.FindElement(By.Id("nav-logo-sprites")).Displayed);

            //Cen�rio 2 - Buscar um produto
            /*
                Dado que desejo buscar um produto
                Quando preencho o campo de busca com Ar Condicionado
                E clico no bot�o Ir
                Ent�o vejo a listagem com os produtos
             */
            driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(produto);
            driver.FindElement(By.Id("nav-search-submit-button")).Click();

            //Cen�rio 3 - Validar se o produto foi retornado na lista
            /*
                Dado que desejo validar se o produto retornado na busca cont�m o que eu procurei
                Quando verifico a listagem
                Ent�o vejo os produtos relacionados � minha busca
             */
            driver.FindElement(By.XPath("//span[@class='a-size-base-plus a-color-base a-text-normal']")).Text.Contains(produto);

            //Cen�rio 4 - Adicionar o produto ao carrinho
            /*
                Dado que desejo adicionar o produto ao carrinho
                Quando seleciono o produto na lista
                E clico no bot�o Adicionar ao carrinho
                Ent�o sou redirecionado ao carrinho com o produto adicionado
             */
            driver.FindElement(By.XPath("//span[@class='a-size-base-plus a-color-base a-text-normal']")).Click();
            driver.FindElement(By.Id("add-to-cart-button")).Click();

            //Cen�rio 5 - Validando o produto no carrinho
            /*
                Dado que desejo validar o produto adicionado no carrinho
                Quando clico no bot�o Carrinho
                Ent�o sou direcionado � p�gina contendo o produto adicionado
            */            
            driver.FindElement(By.Id("nav-cart-count")).Click();
            driver.FindElement(By.XPath("//span")).Text.Contains(produto);

            driver.Quit();
        }        
    }
}