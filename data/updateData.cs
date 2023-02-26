class UpdateData : StandardMessage {

    String accountName;

    AccountData updatedData;

    public UpdateData(String accountName, AccountData update) : base(MessageType.update, 1.0) {
        this.accountName = accountName;
        this.updatedData = update;
    }

    public UpdateData() : base(MessageType.update, 1.0) {
        Random random = new Random();

        this.accountName = "Mercury";
        this.updatedData = new AccountData("Mercury", (int)(random.NextDouble() * 1000), random.NextDouble() * 3, random.NextDouble() * 15000);
    }


}