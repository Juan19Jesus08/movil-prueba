using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Intertecs.Modelos;
using Newtonsoft.Json.Linq;

namespace Intertecs.WebServices
{
    class WSInstitution
    {
        HttpClient http;
        public async Task<List<Carrera>> listaInstituciones()
        {
            List<Carrera> listaInst = null;
            try
            {
                http = new HttpClient();
                http.BaseAddress = new Uri("http://localhost:59516");

               // var authData = string.Format("{0}:{1}", "intertecs", "1nt3rt3c5");                        //auth
                //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData)); //auth
                //http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                var result = await http.GetAsync("/Service1.svc/GetAllBachelors");//+Settings.settings.token);
                var cadena = result.Content.ReadAsStringAsync().Result;
                listaInst = new List<Carrera>();

                var objJson = JObject.Parse(cadena);

                var arrJson = objJson.SelectToken("carrera").ToList();
                
                Carrera institution;
                foreach (var institucion in arrJson)
                {
                    institution = new Carrera ();
                    institution.id_carrera = institucion["id_carrera"].ToString();
                    institution.carrera = institucion["carrera"].ToString();
                    listaInst.Add(institution);
                }
            }
            catch (Exception e) {

                e.ToString();
            }
            return listaInst;
        }
    }
}
