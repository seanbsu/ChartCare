using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Tests
{
    public class ExpectedPageContent
    {
        public List<string> ExpectedNavBarElements()
        {
            List<string> expectedNavBarElements = new List<string>
            {
                "Pricing",
                "Features",
                "FAQs",
                "Home",
                "About",
                "Login",
                "Sign-up"
            };
            return expectedNavBarElements;
        }

        public List<string> ExpectedRegistrationElements()
        {
            List<string> expectedFeaturesElements = new List<string>
            {
               "Company Name",
               "Email", 
               "Password",
               "Confirm Password", 
               "Pricing Plan",    
               "Address",    
               "Free",    
               "Standard",    
               "Premium"    
            };
            return expectedFeaturesElements;
        }

        public List<string> ExpectedLoginElements()
        {
            List<string> expectedLoginElements = new List<string>
            {
                 "Email", 
                  "Password", 
                  "Remember me?", 
                  "Login", 
                  "Register as a new user", 
                  "Forgot your password?" 
            };
            return expectedLoginElements;
        }
    }
}
