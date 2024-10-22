using PhoneApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Domain;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DataTransferObjectPlugin
{
    [Author(Name = "Ivan Petrov")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Add new users");

            const string filepath = @"D:\source\repos\DataTransferObjectPlugin\DataTransferObjectPlugin\users.json";
            var json = File.ReadAllText(filepath);
            var data = JsonConvert.DeserializeObject<EDTOs>(json);
            var usersList = data.Users;
            List<EmployeesDTO> list = new List<EmployeesDTO>();

            foreach ( var user in usersList )
            {
                EmployeesDTO newUser = new EmployeesDTO();
                newUser.Name = user.Name;
                newUser.AddPhone(user.phone);
                list.Add(newUser);
            }
            
            logger.Info($"Add {usersList.Count()} users");

            return list.Cast<DataTransferObject>();
        }

        public class EDTOs
        {
            [JsonProperty("users")]
            public List<EDTO> Users { get; set; }
        }

        public class EDTO
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("phone")]
            public string phone { get; set; }

        }
    }
}
