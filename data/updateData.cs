class UpdateData : StandardMessage {

    public String accountName { get; set; }

    public AccountData updatedData { get; set; }

    public UpdateData(String accountName, AccountData update) : base(MessageType.update, 1.0) {
        this.accountName = accountName;
        this.updatedData = update;
    }

    public UpdateData() : base(MessageType.update, 1.0) {
        Random random = new Random();

        this.accountName = "Mercury";
        this.updatedData = new AccountData("Mercury", (int)(random.NextDouble() * 1000), Math.Round(random.NextDouble() * 3, 2), Math.Round(random.NextDouble() * 15000, 2));
    }
}
