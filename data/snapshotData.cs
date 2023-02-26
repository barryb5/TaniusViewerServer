class SnapshotData : StandardMessage {
    public List<Account> accounts { get; set; }

    public SnapshotData() : base(MessageType.snapshot, 1.0) {

        // base.messageType = MessageType.snapshot;
        // base.version = 1.0;

        this.accounts = new List<Account>() 
        {
            new Account(
                new List<AccountData>(){
                    new AccountData("John", 3230, 0.0, 569.3)
                }, 
                new List<ContractData>(){
                    new ContractData("BC", 0, 0.0, 835.0),
                    new ContractData("BC.J23", 3, 82.0, -510.0)
                }
            ),
            new Account(
                new List<AccountData>(){
                    new AccountData("Mercury", 750, 1.6, 1161.3),
                    new AccountData("BOB", 9, .4, 27.5),
                    new AccountData("Energy", 194, .9, 1008.4),
                    new AccountData("FYT", 185, .9, 10.5),
                    new AccountData("ICS", 182, -2.3, 37.3),
                    new AccountData("NOB", 180, .5, 7.3),
                    new AccountData("TEX", 185, 1.8, 81.3),
                }, 
                new List<ContractData>(){
                    new ContractData("BC", 0, 0.0, 835.0),
                    new ContractData("BC.J23", 3, 82.0, -510.0),
                    new ContractData("BC.K23", -3, 81.86, 510.0),
                    new ContractData("BC.M23", 36, 81.53, -6300.0),
                    new ContractData("BC.N23", -90, 81.16, 16200.0),
                    new ContractData("BC.Q23", 85, 80.80, -140250.0),
                    new ContractData("BC.U23", 4, 80.40, -660.0),
                    new ContractData("BC.V23", -68, 79.98, 11560.0),
                    new ContractData("BC.X23", 33, 79.57, -5940.0),
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