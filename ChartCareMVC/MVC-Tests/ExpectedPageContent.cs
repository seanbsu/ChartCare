using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Tests
{
    public class ExpectedPageContent
    {
        public List<string> ExpectedNavBarElements(){
            List<String> ExpectedNavBarElements = new List<String>();
            ExpectedNavBarElements.Add("Pricing");
            ExpectedNavBarElements.Add("Features");
            ExpectedNavBarElements.Add("FAQs");
            ExpectedNavBarElements.Add("Home");
            ExpectedNavBarElements.Add("About");
            ExpectedNavBarElements.Add("Login");
            ExpectedNavBarElements.Add("Sign-up");
            return ExpectedNavBarElements;
        }
    }
}
