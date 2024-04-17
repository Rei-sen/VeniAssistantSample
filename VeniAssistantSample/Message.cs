namespace VeniAssistantSample;

public class Message
{
    public class Text
    {
        public string Value { get; set; }
        public List<object> Annotations { get; set; }
    }

    public class MsgContent
    {
        public string Type { get; set; }
        public Text Text { get; set; }
    }
    public string Id { get; set; }
    public string Object { get; set; }
    public int CreatedAt { get; set; }
    public string AssistantId { get; set; }
    public string ThreadId { get; set; }
    public string RunId { get; set; }
    public string Role { get; set; }
    public List<MsgContent> Content { get; set; }
    public List<object> FileIds { get; set; }
    public object Metadata { get; set; }

   
}
