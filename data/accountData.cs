using static System.Collections.IEnumerable;

class AccountData {
    public String account {get; set; }
    public int contracts {get; set; }
    public double pnl {get; set; }
    public double margin {get; set; }
    public AccountData(String account, int contracts, double pnl, double margin) {
        this.account = account;
        this.contracts = contracts;
        this.pnl = pnl;
        this.margin = margin;
    }
}