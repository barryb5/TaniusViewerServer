class Account {
    public List<AccountData> accountData {get; set; }
    public List<ContractData> contractData {get; set; }

    public Account(List<AccountData> accountData, List<ContractData> contractData) {
        this.accountData = accountData;
        this.contractData = contractData;
    }
}