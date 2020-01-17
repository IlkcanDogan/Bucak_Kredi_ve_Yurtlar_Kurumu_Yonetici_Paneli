using System.Web.Script.Serialization;
using RestSharp;
using System;
using System.Windows.Forms;
using System.Data;
using Newtonsoft.Json;

namespace KYKAPP
{
    class API
    {
        public string apiLink = fields.API_ADRES;

        public string HATA;
        public string KULLANICI_ADI;
        public string AD;
        public string SOYAD;
        public string TOKEN;
        public string G_PAROLA;
        public string KAYIT_ID;

        public void Istek(object jsonVeri ,string slink,string headerKey = "", string headerValue = "", DataGridView dataGridObje = null, string resimDizin = null, bool yemekListesi = false,CheckedListBox chkListePZT = null, CheckedListBox chkListeSAL = null, CheckedListBox chkListeCAR = null, CheckedListBox chkListePER = null, CheckedListBox chkListeCUM = null, CheckedListBox chkListeCMT = null, CheckedListBox chkListePAZ = null)
        {
            try
            {
                var link = new RestClient(apiLink + slink); 
                var request = new RestRequest(Method.POST);



                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                if (headerKey != "" && headerValue != "")
                {
                    request.AddHeader(headerKey, headerValue);
                }

                var jsonObject = SimpleJson.SerializeObject(jsonVeri);

                if(resimDizin == null)
                {
                    request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
                }
                else
                {
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Content-Type", "multipart/form-data");
                    request.AddFile("image", resimDizin);
                }
               
                IRestResponse response = link.Execute(request);
                
                if (dataGridObje == null)
                {
                    var serilestirici = new JavaScriptSerializer();
                    var cevapElemanlari = serilestirici.Deserialize<API>(response.Content);

                    HATA = cevapElemanlari.HATA;
                    KULLANICI_ADI = cevapElemanlari.KULLANICI_ADI;
                    AD = cevapElemanlari.AD;
                    SOYAD = cevapElemanlari.SOYAD;
                    TOKEN = cevapElemanlari.TOKEN;
                    G_PAROLA = cevapElemanlari.G_PAROLA;
                    KAYIT_ID = cevapElemanlari.KAYIT_ID;
                    
                }
                else
                {
                    var dataSet = JsonConvert.DeserializeObject<DataSet>(response.Content);

                    
                    if (yemekListesi)
                    {
                        DataTable dataTable;

                        dataTable = dataSet.Tables["PZT"];
                        YemekListesiElemanEkle(dataTable,chkListePZT);

                        dataTable = dataSet.Tables["SAL"];
                        YemekListesiElemanEkle(dataTable, chkListeSAL);

                        dataTable = dataSet.Tables["CAR"];
                        YemekListesiElemanEkle(dataTable, chkListeCAR);

                        dataTable = dataSet.Tables["PER"];
                        YemekListesiElemanEkle(dataTable, chkListePER);

                        dataTable = dataSet.Tables["CUM"];
                        YemekListesiElemanEkle(dataTable, chkListeCUM);

                        dataTable = dataSet.Tables["CMT"];
                        YemekListesiElemanEkle(dataTable, chkListeCMT);

                        dataTable = dataSet.Tables["PAZ"];
                        YemekListesiElemanEkle(dataTable, chkListePAZ);

                    }
                    else
                    {
                        dataGridVeriEkle(dataGridObje, dataSet);
                    }
                    


                }
                

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message.ToString());
            }

        }

        public void dataGridVeriEkle(DataGridView dataGridObje, DataSet dataSet)
        {
            try
            {
                dataGridObje.Rows.Clear();

                DataTable dataTable = dataSet.Tables["DATA"];
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridObje.Rows.Add(row.ItemArray);
                }
            }
            catch (Exception)
            {
                //
            }
            
        }

        private void YemekListesiElemanEkle(DataTable dataTable ,CheckedListBox checkedList)
        {
            foreach (DataRow item in dataTable.Rows)
            {
                checkedList.Items.AddRange(item.ItemArray);
            }
        }
    }
}
