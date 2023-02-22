class FullData {
    public List<Account> accounts { get; set; }

    public FullData() {
        this.accounts = new List<Account>() 
        {
            new Account(
                new List<AccountData>(){
                    new AccountData("Mercury", 750, 1.6, 1161.3)
                }, 
                new List<ContractData>(){
                    new ContractData("BC", 0, 0.0, 835.0),
                    new ContractData("BC.J23", 3, 82.0, -510.0)
                }
            )
        };
    }
}