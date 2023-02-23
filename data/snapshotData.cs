class SnapshotData : StandardMessage {
    public List<Account> accounts { get; set; }

    public SnapshotData() : base(MessageType.snapshot, 1.0) {

        // base.messageType = MessageType.snapshot;
        // base.version = 1.0;

        this.accounts = new List<Account>() 
        {
            new Account(
                new List<AccountData>(){
                    new AccountData("Mercury", 750, 1.6, 1161.3),
                    new AccountData("BOB", 9, .4, 27.5),
                    new AccountData("Energy", 194, .9, 1008.4)
                }, 
                new List<ContractData>(){
                    new ContractData("BC", 0, 0.0, 835.0),
                    new ContractData("BC.J23", 3, 82.0, -510.0)
                }
            ),
            new Account(
                new List<AccountData>() {
                    new AccountData("Mike", 120, -.1, 16.6)
                },
                new List<ContractData>() {
                    new ContractData("BC", 0, 0.0, 835.0),
                    new ContractData("BC.J23", 3, 82.0, -510.0)
                }
            )
        };
    }
}