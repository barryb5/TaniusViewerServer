using System.Net.WebSockets;

public class User {
    public WebSocket webSocket { get; set; }
    public List<String> accessCode { get; set; }
    public User(WebSocket webSocket, List<String> accessCode) {
        this.webSocket = webSocket;
        this.accessCode = accessCode;
    }
}