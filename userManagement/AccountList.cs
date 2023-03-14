public class AccountList {
    // This class only exists so I can have an object to turn into a JSON string
    public List<String> accountList { get; set; }
    public AccountList(String[] accounts) {
        for (int i = 0; i < accounts.Count(); i++) {
            this.accountList.Add(accounts[i]);
        }
    }

    public AccountList(List<String> accountList) {
        this.accountList = accountList;
    }
}