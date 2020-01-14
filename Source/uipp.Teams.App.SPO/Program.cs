using System;
using System.Security;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

namespace uipp.Teams.App.SPO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void GetLstGrupos()
        {
            using (ClientContext clientContext = GetContextObject())
            {
                List docList = clientContext.Web.Lists.GetByTitle("GruposIntranet");
                clientContext.Load(docList); CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = "<View Scope='RecursiveAll'></View>";
                ListItemCollection listItems = docList.GetItems(camlQuery);
                clientContext.Load(listItems);
                clientContext.ExecuteQuery();

                foreach (ListItem listItem in listItems)
                {
                    var groupName = listItem.FieldValues["Title"].ToString();
                    var estruturaId = listItem.FieldValues["EstruturaId"].ToString();

                    Console.WriteLine("Id: {0} ", listItem.Id);

                    //Dictionary<string, object> ECFValues = listItem.FieldValues;
                }
            }
        }

        private static ClientContext GetContextObject()
        {

            //Creating Password
            const string PWD = "xxx";
            const string USER = "";

            //Creating Credentials
            var passWord = new SecureString();
            foreach (var c in PWD) passWord.AppendChar(c);

            ClientContext context = new ClientContext("https://<alterar>.sharepoint.com/sites/<alterar>");
            context.Credentials = new SharePointOnlineCredentials(USER, passWord);
            return context;
        }
    }
}
