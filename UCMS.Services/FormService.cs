using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UCCM.Core;
using UCCM.Model;

namespace UCCM.Services
{
    public class FormService
    {
        public List<CForm> GetForms()
        {
            var cmd = "SELECT * FROM FormMetadata";
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null, ReadForms);
        }

        public string Insert(CForm form, string appFolderPath)
        {
            form.Id = IdHelper.Generate(25);
            var cmd = $"INSERT INTO FormMetadata(Id, Name, Title, Description) VALUES('{form.Id}','{form.Name}','{form.Title}','{form.Description}')";
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);

            var formDir = Path.Combine(appFolderPath, $"components/custom-form/{form.Id}");
            if (!Directory.Exists(formDir))
                Directory.CreateDirectory(formDir);

            var templateScriptPath = Path.Combine(appFolderPath, "templates/cform.js");
            var scriptPath = Path.Combine(formDir, "script.js");

            var content = File.ReadAllText(templateScriptPath);
            content = content.Replace("$$controllerName$$", form.Name + "Ctrl");
            File.WriteAllText(scriptPath, content);

            var templateViewPath = Path.Combine(appFolderPath, "templates/view.html");
            content = File.ReadAllText(templateViewPath);
            content = content.Replace("$$controllerName$$", form.Name + "Ctrl");

            var viewPath = Path.Combine(formDir, "view.html");
            File.WriteAllText(viewPath, content);

            return form.Id;
        }

        public void Update(CForm form, string appDir)
        {
            var scriptPath = Path.Combine(appDir, $"components/custom-form/{form.Id}/script.js");
            File.WriteAllText(scriptPath, form.Scripts);

            var cmd = $"UPDATE FormMetadata SET Name='{form.Name}', Title='{form.Title}', Description='{form.Description}', FormControls='{form.Controls.SerializeToJson()}' WHERE Id='{form.Id}'";
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.Text, cmd, null);


        }

        public CForm GetById(string id, string appDir)
        {
            var cmd = $"SELECT * FROM FormMetadata WHERE Id='{id}'";
            var form = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.Text, cmd, null,
                reader => ReadForms(reader).FirstOrDefault());
            var scriptPath = Path.Combine(appDir, $"components/custom-form/{form.Id}/script.js");
            form.Scripts = File.ReadAllText(scriptPath);
            return form;
        }

        private List<CForm> ReadForms(IDataReader reader)
        {
            var list = new List<CForm>();
            while (reader.Read())
            {
                var cform = new CForm
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    Scripts = reader["Scripts"].ToString(),
                    Controls = reader["FormControls"].ToString().FromJson<List<CFormControl>>()
                };

                list.Add(cform);
            }
            return list;
        } 
    }
}
