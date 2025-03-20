using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistanOllama
{
    public class OllamaResponse
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("done")]
        public bool Done { get; set; }

        [JsonProperty("done_reason")]
        public string DoneReason { get; set; }

        [JsonProperty("total_duration")]
        public long? TotalDuration { get; set; }

        [JsonProperty("load_duration")]
        public long? LoadDuration { get; set; }

        [JsonProperty("prompt_eval_count")]
        public int? PromptEvalCount { get; set; }

        [JsonProperty("prompt_eval_duration")]
        public long? PromptEvalDuration { get; set; }

        [JsonProperty("eval_count")]
        public int? EvalCount { get; set; }

        [JsonProperty("eval_duration")]
        public long? EvalDuration { get; set; }
    }

    public class Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
