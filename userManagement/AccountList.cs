public class AccountList {
    
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