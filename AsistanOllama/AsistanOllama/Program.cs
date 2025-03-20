using AsistanOllama;
using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string historyFile = "chat_history.json";
    private static List<Message> chatHistory = new List<Message>();

    static async Task Main()
    {

        // Önceki sohbet geçmişini yükle
        //LoadChatHistory();

        while (true)
        {
            Console.Write("Siz: ");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "exit") break;

            // Kullanıcı mesajını geçmişe ekle
            chatHistory.Add(new Message { Role = "user", Content = userInput });

            // API'ye gönderilecek veriyi hazırla
            var requestBody = new
            {
                model = "deepseek-r1", 
                messages = chatHistory
            };

            string json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                List<OllamaResponse> responses = new List<OllamaResponse>();

                HttpResponseMessage httpResponse = await client.PostAsync("http://localhost:11434/api/chat", content);

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                // JSON'u deserialize et

                foreach (string line in responseBody.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    OllamaResponse response = JsonConvert.DeserializeObject<OllamaResponse>(line);
                    responses.Add(response);
                }

               

                // Cevapları ekrana yazdır
                Console.WriteLine("Model: " + responses[0].Model);
                Console.WriteLine("Oluşturulma Zamanı: " + responses[0].CreatedAt);
                Console.WriteLine("Mesaj İçeriği: ");
                foreach (var res in responses)
                {
                    Console.Write(res.Message.Content);
                }

                Console.ReadKey();

                //response.EnsureSuccessStatusCode();

                //string responseBody = await response.Content.ReadAsStringAsync();
                //var responseJson = JsonSerializer.Deserialize<ResponseData>(responseBody);

                //// Yanıtı göster ve geçmişe ekle
                //string assistantResponse = responseJson.Message;
                //Console.WriteLine($"Asistan: {assistantResponse}");
                //chatHistory.Add(new Message { Role = "assistant", Content = assistantResponse });

                //// Sohbeti kaydet
                //SaveChatHistory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }
    }

    //static void LoadChatHistory()
    //{
    //    if (File.Exists(historyFile))
    //    {
    //        string json = File.ReadAllText(historyFile);
    //        chatHistory = JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();
    //    }
    //}

    //static void SaveChatHistory()
    //{
    //    string json = JsonSerializer.Serialize(chatHistory, new JsonSerializerOptions { WriteIndented = true });
    //    File.WriteAllText(historyFile, json);
    //}
}

class Message
{
    public string Role { get; set; }
    public string Content { get; set; }
}

class ResponseData
{
    public string Message { get; set; }
}
