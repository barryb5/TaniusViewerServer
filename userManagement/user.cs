using System.Net.WebSockets;

public class User {
    public WebSocket webSocket { get; set; }
    public List<String> accessCodes { get; set; }
    public User(WebSocket webSocket, List<String> accessCodes) {
        this.webSocket = webSocket;
        this.accessCodes = accessCodes;
    }

    public List<String> getAccounts() {
        List<String> accounts = new List<string>();
        
        accessCodes.ForEach((code) => {
            // Decrypt the accessCodes
            accounts.Add(code);
        });

        return accounts
    }
}